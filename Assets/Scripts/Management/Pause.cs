using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Pause : MonoBehaviour
{
    bool paused;
    [SerializeField] TextMeshProUGUI LoadCheckpoint;
    [SerializeField] TextMeshProUGUI ToMenu;
    [SerializeField] TextMeshProUGUI Level;
    void Start()
    {
        if (PlayerPrefs.GetString("Language") == "English")
        {
            LoadCheckpoint.text = "Load from last checkpoint";
            ToMenu.text = "To main menu";
            Level.text = "Level: " +
    (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex - 1).ToString();
        }
        else if (PlayerPrefs.GetString("Language") == "Български")
        {
            LoadCheckpoint.text = "Рестартирай нивото";
            ToMenu.text = "Към менюто";
            Level.text = "Ниво: " +
    (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex - 1).ToString();
        }

    }

    void Update()
    {
        if(Input.GetKeyDown(Controls.Pause) && 
            !GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().dead)
        {
            if (!paused)
                paused = true;
            else
                paused = false;

        }
        if (paused && !GetComponent<Canvas>().enabled)
        {
            Time.timeScale = 0f;
            GetComponent<Canvas>().enabled = true;
            GetComponent<GraphicRaycaster>().enabled = true;
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(true);
        }
        else if (!paused && GetComponent<Canvas>().enabled)
        {
            Time.timeScale = 1;
            GetComponent<Canvas>().enabled = false;
            GetComponent<GraphicRaycaster>().enabled = false;
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
        }
    }
    public void PauseOP()
    {
        if (paused) paused = false;
        else paused = true;
    }
}
