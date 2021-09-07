using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phanthom : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anime;
    float maxSpeed = 7; //7
    float acceleration = 1000; // 2000

    float HP = 100f;

    GameObject Cam;
    _Camera scriptCam;

    bool dontChange;
    GameObject Player;
    SpriteRenderer sr;
    void Start()
    {
        audioSourceMapping();
        Cam = GameObject.FindGameObjectWithTag("Virtual");
        scriptCam = Cam.GetComponent<_Camera>();
        Player = GameObject.FindGameObjectWithTag("Player");
        sr = GetComponent<SpriteRenderer>();
        anime = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    AudioSource[] _sounds;
    void audioSourceMapping()
    {
        int count = transform.GetChild(1).childCount;
        _sounds = new AudioSource[count];
        for (int i = 0; i < count; i++)
        {
            _sounds[i] = transform.GetChild(1).transform.GetChild(i).
                GetComponent<AudioSource>();
        }
    }

    void Update()
    {
        if(HP <= 0)
        {
            Die();
            Destroy(gameObject);
        }
        Movement();
    }

    void Movement()
    {
        //Running sound
        if(rb.velocity.x != 0 && grounded())
        {
            if(!_sounds[1].isPlaying)
            _sounds[1].Play();
            if(Mathf.Abs(rb.velocity.x) / maxSpeed >= 0.5f)
            _sounds[1].pitch = Mathf.Abs(rb.velocity.x) / maxSpeed;
        }
        else
        {
            _sounds[1].Pause();
        }
        //Running sound

        if (grounded() && !dontChange)
        {
            if (anime.GetBool("Attack"))
                anime.SetBool("Attack", false);
            if (anime.GetBool("Hit"))
                anime.SetBool("Hit", false);
            if (rb.velocity.x < -0.1)
            {
                sr.flipX = false;
            }
            else if (rb.velocity.x > 0.1)
            {
                sr.flipX = true;
            }
        }



        if (anime.GetBool("Hit")) return;

        if (Mathf.Abs(Vector2.Distance(transform.position,
            Player.transform.position)) >= 3)
        {
            if (Player.transform.position.x <
                     transform.position.x && Mathf.Abs(rb.velocity.x) < maxSpeed)
            {
                rb.AddForce(Vector2.left * acceleration * Time.deltaTime);
            }
            else if (Player.transform.position.x >
                transform.position.x && Mathf.Abs(rb.velocity.x) < maxSpeed)
            {
                rb.AddForce(Vector2.right * acceleration * Time.deltaTime);
            }
            anime.SetFloat("Speed", Mathf.Abs(rb.velocity.x / maxSpeed));
        }
        else
        {
            if(!dontChange && grounded())
            Attack();
        }
    }

    bool grounded()
    {
        float distance = 0.01f;
        Transform raycastTransform = transform.GetChild(0).transform;
        RaycastHit2D hit = Physics2D.Raycast(raycastTransform.position, Vector2.down,
            distance);
        if (hit)
        {
            return true;
        }
        return false;
    }


    float jumpForce;
    void Attack()
    {
        //sound
        _sounds[2].Play();
        //sound
        StartCoroutine(StopChange());
        jumpForce = Random.Range(9.5f, 12.5f);
        if (Player.transform.position.x <
                     transform.position.x)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            rb.AddForce(Vector2.left * jumpForce, ForceMode2D.Impulse);
        }
        else if (Player.transform.position.x >
                     transform.position.x)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            rb.AddForce(Vector2.right * jumpForce, ForceMode2D.Impulse);
        }
        anime.SetBool("Attack", true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Wave"))
        {
            scriptCam.CameraShake(0.05f, 10, 10);
            StartCoroutine(StopChange());
            Vector2 direction = (transform.position
                - Player.transform.position).normalized;
            rb.AddForce( new Vector2(direction.x * 10, 5),
                ForceMode2D.Impulse);
            anime.SetBool("Hit", true);

            //sound
            //Being hit sound
            _sounds[3].Play();

            //take damage
            //goes two times
            HP -= 50*0.5f;

        }
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().TakeDamage(20);
        }
    }

    void Die()
    {
        //sound
        //Die sound
        _sounds[0].Play();

        Instantiate(Resources.Load<GameObject>("Phanthom_Head"),transform.position,
            transform.rotation);
    }

    IEnumerator StopChange()
    {
        dontChange = true;
        yield return new WaitForSeconds(0.2f);
        dontChange = false;
    }
}
