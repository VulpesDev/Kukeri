using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering;

public class Tutorial : MonoBehaviour
{
    [SerializeField] GameObject camMain, cam1, timeline1;

    GameObject player;
    [HideInInspector] public bool _freeze;
    [SerializeField] TextMeshProUGUI tx_leftright;
    [SerializeField] Volume basicVolume;
    [SerializeField] Volume tutorialVolume;

    [SerializeField] GameObject TutPhanthom;

    AudioSource SlowDownSound;

    bool leftright;
    [HideInInspector] public bool jumpy;
    [HideInInspector] public bool slidy;
    [HideInInspector] public bool attacky;
    bool spawnPh = true;
    [SerializeField] Transform PhanthomSpawner;
    [SerializeField] Transform PhanthomSpawner1;
    bool jump;
    float acceleration = 0.015f;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        SlowDownSound = transform.GetChild(0).GetComponent<AudioSource>();
        if (PlayerPrefs.GetString("Language") == "English")
        {
            TypeInLabel($"Use {Controls.PlayerLeft}" +
                $" to go left and {Controls.PlayerRight} to go right");
        }
        else if (PlayerPrefs.GetString("Language") == "Български")
        {
            TypeInLabel($"Използвай {Controls.PlayerLeft}" +
                $" , за да се движиш наляво и {Controls.PlayerRight} надясно");
        }
        Freeze();
        leftright = true;
    }

    public void TypeInLabel(string text)
    {
        tx_leftright.text = text;
    }

    void Update()
    {
        if(spawnPh && attacky)
        {
            StartCoroutine(SpawnPhanthoms());
        }
        if ((Input.GetKeyDown(Controls.PlayerLeft) ||
            Input.GetKeyDown(Controls.PlayerRight)) && leftright)
        {
            leftright = false;
            StartCoroutine(GameObject.Find("ForestMan").
                GetComponent<ForestMan>().WaitForStart());
            BackToNormal();
        }
        if (Input.GetKeyDown(Controls.PlayerJump) && jumpy)
        {
            jumpy = false;
            BackToNormal();
        }
        if (Input.GetKeyDown(Controls.PlayerSlide) && slidy)
        {
            slidy = false;
            BackToNormal();
        }
        if (Input.GetKeyDown(Controls.PlayerAtc) && attacky)
        {
            attacky = false;
            BackToNormal();
        }
    }

    public void Freeze()
    {
        SlowDownSound.Play();
        _freeze = true;
        StartCoroutine(TimeForward());
        StartCoroutine(VolumeForward());
        tx_leftright.gameObject.SetActive(true);
    }
    IEnumerator TimeForward()
    { 
        Time.timeScale -= acceleration;
        yield return new WaitForSeconds(acceleration - 0.05f);
        if (Time.timeScale > 0.5f && _freeze) StartCoroutine(TimeForward());
        else if(Time.timeScale <=0.5f && _freeze)
        {
            Time.timeScale = 0;
        }
    }
    IEnumerator VolumeForward()
    {
        basicVolume.weight -= acceleration;
        tutorialVolume.weight += acceleration;
        yield return new WaitForSeconds(acceleration - 0.05f);
        if (tutorialVolume.weight < 1 && _freeze) StartCoroutine(VolumeForward());
    }

    void BackToNormal()
    {
        SlowDownSound.Stop();
        _freeze = false;
        StartCoroutine(TimeBack());
        StartCoroutine(VolumeBack());
        tx_leftright.gameObject.SetActive(false);
    }


    IEnumerator TimeBack()
    {
        Time.timeScale += acceleration;
        yield return new WaitForSeconds(acceleration - 0.05f);
        if(Time.timeScale >= 1f)
        {

        }
        else if (Time.timeScale > 0.5f && !_freeze) StartCoroutine(TimeBack());
        else
        {
            Time.timeScale = 0.5f;
            StartCoroutine(TimeBack());
        }
    }
    IEnumerator VolumeBack()
    {
        basicVolume.weight += acceleration;
        tutorialVolume.weight -= acceleration;
        yield return new WaitForSeconds(acceleration - 0.05f);
        if (basicVolume.weight < 1 && !_freeze) StartCoroutine(VolumeBack());
    }

    public void SpawnPhanthom()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
        //change cameras
        camMain.SetActive(false);
        cam1.SetActive(true);
        //disable player
        player.GetComponent<Player>().enabled = false;
        player.GetComponent<Animator>().SetFloat("Speed", 0);
        //start timeline
        timeline1.SetActive(true);
        //get the signal
    }
    public void SignalReceived()
    {
        //enable player
        player.GetComponent<Player>().enabled = true;
        //change cameras
        camMain.SetActive(true);
        cam1.SetActive(false);
        //spawn phanthom
        TutPhanthom.SetActive(true);
        TutPhanthom.GetComponent<Phanthom>().enabled = false;
        StartCoroutine(WaitPhanthom());
        //start tutorial (SPACE)
        attacky = true;
        if (PlayerPrefs.GetString("Language") == "English")
        {
            TypeInLabel($"Use {Controls.PlayerAtc} to ring" +
                $" your cowbell and kill the phanthom");
        }
        else if (PlayerPrefs.GetString("Language") == "Български")
        {
            TypeInLabel($"Използвай {Controls.PlayerAtc} , за да използваш звука като оръжие срещу злите сили ");
        }
        Freeze();
        //spawn other monsters
    }
    IEnumerator WaitPhanthom()
    {
        yield return new WaitForSeconds(1f);
        TutPhanthom.GetComponent<Phanthom>().enabled = true;
    }
    IEnumerator SpawnPhanthoms()
    {
        spawnPh = false;
        for (int i = 0; i < 4; i++)
        {
            if (Random.Range(0, 2) == 0)
            {
                Instantiate(Resources.Load<GameObject>("Phanthom"),
                    PhanthomSpawner.position, Resources.Load<Transform>("Phanthom").rotation);
            }
            else
            {
                Instantiate(Resources.Load<GameObject>("Phanthom"),
                      PhanthomSpawner1.position, Resources.Load<Transform>("Phanthom").rotation);
            }
            yield return new WaitForSeconds(Random.Range(1.0f, 3.5f));
        }

    }
}
