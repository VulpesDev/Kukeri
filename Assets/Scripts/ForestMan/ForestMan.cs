using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestMan : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anime;
    float speed;
    float normalSpeed = 5.5f;
    bool monsterGO;
    AudioSource scream;




    void Start()
    {
        speed = normalSpeed;
        scream = transform.GetChild(1).GetComponent<AudioSource>();
        anime = GetComponent<Animator>();
        anime.enabled = false;
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(Scream());
    }

    public IEnumerator WaitForStart()
    {
        yield return new WaitForSeconds(0.5f);
        anime.enabled = true;
        monsterGO = true;
    }
    IEnumerator Scream()
    {
        
        yield return new WaitForSeconds(Random.Range(2.5f, 4.0f));
        if (!GameObject.Find("Manager").GetComponent<Tutorial>()._freeze)
        {
            if (!scream.isPlaying)
            {
                scream.Play();
                scream.pitch = Random.Range(0.3f, 1.5f);
            }
        }
        StartCoroutine(Scream());
    }

    IEnumerator HitLog()
    {
        speed = normalSpeed *0.5f;
        yield return new WaitForSeconds(1f);
        speed = 6.5f;
    }

    void Update()
    {
        
        if (!monsterGO) return;
        rb.velocity = new Vector2(speed, 0);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Log"))
        {
            StartCoroutine(HitLog());
            collision.GetComponent<Animator>().SetBool("Explode", true);
            collision.GetComponentInParent<SpriteRenderer>().enabled = false;
        }
    }
}
