using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;

    GameObject manager;

    public bool dead;

    GameObject Cam;
    _Camera scriptCam;


    void Start()
    {
        Controls.LoadDefaults(); //remove later !!!

        manager = GameObject.Find("Manager");
        Cam = GameObject.FindGameObjectWithTag("Virtual");
        scriptCam = Cam.GetComponent<_Camera>();
        sr = GetComponent<SpriteRenderer>();
        anime = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (dead)
        {
            if(grounded())
            {
                rb.gravityScale = 0;
                anime.SetBool("Grounded", true);
            }
            return;
        }
        if (HP <= 0) Die();
        Movement();
        Attacking();
        AnimationAlignment();
        UIfix();
    }
    private void FixedUpdate()
    {
        FixedMovement();
    }

    void Sounds(string sound)
    {
        Transform SoundsTrans = transform.GetChild(1);
        if (sound == "Jump")
        {
            SoundsTrans.GetChild(0).GetComponent<AudioSource>().Play();
        }
    }
    //////////// HEALTH ////////////
    #region Health
    int HP = 100;
    [SerializeField]Slider UI_HP;
    void UIfix()
    {
        if(UI_HP.value != HP + 20)
        UI_HP.value = HP + 20;
    }
    void Die()
    {
        GetComponent<Collider2D>().enabled = false;
        StartCoroutine(WaitPause());
        dead = true;
        anime.SetBool("DIE", true);
    }
    IEnumerator WaitPause()
    {
        Debug.Log("Pause");
        yield return new WaitForSeconds(2f);
        GameObject.Find("Pause").GetComponent<Pause>().PauseOP();
    }
    public void DestGameobject()
    {
        Destroy(gameObject);
    }
    public void TakeDamage(int damage)
    {
        HP -= damage;
        rb.AddForce(new Vector2(0, 10), ForceMode2D.Impulse);
        StartCoroutine(ColorChange());
    }
    IEnumerator ColorChange()
    {
        sr.color = new Color(255, 0, 0, 0.5f);
        yield return new WaitForSeconds(0.1f);
        sr.color = Color.white;
    }
    #endregion  //////////// HEALTH ////////////

    //////////// MOVEMENT & VISUALS ////////////
    #region Movement&Visuals

                                                                 // Attack \\
    [SerializeField] GameObject soundWave;
    float stamina = 100;
    [SerializeField]Slider staminaSlider;
    
    // slide and gravity check
    void Attacking()
    {
        if(Input.GetKeyDown(Controls.PlayerAtc) && stamina > 0)
        {
            scriptCam.CameraShake(0.05f,2,2);
            if (transform.rotation.y == 0)
                Instantiate(soundWave, transform.position,
                    Quaternion.Euler(0, 0, -90));
            else
                Instantiate(soundWave, transform.position,
                    Quaternion.Euler(0, 0, 90));

            stamina -= 15;
            staminaSlider.value = stamina;
        }
        if(stamina < 100)
        {
            stamina += 0.1f;
            staminaSlider.value = stamina;
        }
    }
    // Movement \\

    void FixedMovement()
    {
        if (!slide)
        {
            if (anime.GetBool("Slide")) anime.SetBool("Slide", false);
            if (rb.velocity.x < -0.1)
            {
                transform.rotation = Quaternion.Euler(transform.rotation.x, 180,
                    transform.rotation.z);
            }
            else if (rb.velocity.x > 0.1)
            {
                transform.rotation = Quaternion.Euler(transform.rotation.x, 0,
                    transform.rotation.z);
            }
            if (Input.GetKey(Controls.PlayerLeft))
            {
                if (Mathf.Abs(rb.velocity.x) < maxSpeed)
                    rb.AddForce(Vector2.left * acceleration * Time.deltaTime);
            }
            if (Input.GetKey(Controls.PlayerRight))
            {
                if (Mathf.Abs(rb.velocity.x) < maxSpeed)
                    rb.AddForce(Vector2.right * acceleration * Time.deltaTime);
            }
        }
        else
        {
            if (transform.rotation.y < 0)
            {
                rb.AddForce(Vector2.left * slideSpeed);
            }
            else
            {
                rb.AddForce(Vector2.right * slideSpeed);
            }
            if (!anime.GetBool("Slide")) anime.SetBool("Slide", true);
            if (slideSpeed > 0) slideSpeed -= 0.15f;

        }


        //gravity check
        if (rb.velocity.y >= 0 || grounded())
        {
            if (rb.gravityScale != 3)
            {
                rb.gravityScale = 3;
            }
        }
        else
        {
            if (anime.GetBool("Jump"))
                anime.SetBool("Jump", false);
            rb.gravityScale += 0.25f;
        }
    }


    [SerializeField] float distance;

    bool grounded()
    {
        Transform raycastTransform = transform.GetChild(0).transform;
        RaycastHit2D hit = Physics2D.Raycast(raycastTransform.position, Vector2.down,
            distance);
        Transform raycastTransform2 = transform.GetChild(2).transform;
        RaycastHit2D hit2 = Physics2D.Raycast(raycastTransform2.position, Vector2.down,
            distance);
        if (hit || hit2)
        {
            return true;
        }
        return false;
    }


    [SerializeField] float acceleration;
    [SerializeField] float maxSpeed;
    [SerializeField] float JumpPower;
    float slideSpeed = 5;
    bool slide;
    void Movement()
    {
        if(rb.velocity.x < 0.1f && rb.velocity.x > -0.1f)
        {
            if(slide) slide = false;
        }

        if(Input.GetKeyDown(Controls.PlayerJump))
        {
            if (grounded())
            {
                if (!anime.GetBool("Jump"))
                    anime.SetBool("Jump", true);
                rb.AddForce(Vector2.up * JumpPower, ForceMode2D.Impulse);
                Sounds("Jump");
            }
        }


        //slide check
        if(Input.GetKey(Controls.PlayerSlide))
        {
            if (!slide)
            {
                slide = true;
                slideSpeed = Mathf.Abs(rb.velocity.x) * 7;
            }
        }
        else
        {
            if (slide)
                slide = false;
        }

    }

                                                    // Animation \\
    Animator anime;
    bool animground;
    bool once;
    void AnimationAlignment()
    {
        //walking
        anime.SetFloat("Speed", Mathf.Abs(rb.velocity.x / maxSpeed));
        //grounded
        if(grounded())
        {
            if(!animground)
            animground = true;
            if (once) once = false;
        }
        else
        {
            if(!once)
            StartCoroutine(WaitingForGround());
        }
        anime.SetBool("Grounded", animground);
    }
    IEnumerator WaitingForGround()
    {
        once = true;
        yield return new WaitForSeconds(0.3f);
        if (!grounded()) animground = false;
    }
    #endregion//////////// MOVEMENT & VISUALS ////////////

    //////////// TUTORIAL ////////////
    #region Tutorial
    Tutorial tut;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        tut = GameObject.Find("Manager").GetComponent<Tutorial>();
        if (collision.CompareTag("Log"))
        {
            tut.jumpy = true;
            tut.Freeze();
            if (PlayerPrefs.GetString("Language") == "English")
                tut.TypeInLabel($"Use {Controls.PlayerJump} to jump over obsticles");
            else if (PlayerPrefs.GetString("Language") == "Български")
                tut.TypeInLabel($"Използвай {Controls.PlayerJump} за да прескочиш дънера");
        }
        if (collision.CompareTag("Slide"))
        {
            collision.GetComponent<Collider2D>().enabled = false;
            tut.slidy = true;
            tut.Freeze();
            if (PlayerPrefs.GetString("Language") == "English")
                tut.TypeInLabel($"Use {Controls.PlayerSlide} to slide");
            else if (PlayerPrefs.GetString("Language") == "Български")
                tut.TypeInLabel($"Използвай {Controls.PlayerSlide} за да се плъзгаш");
        }
        if(collision.CompareTag("SpawnPhanthom"))
        {
            collision.GetComponent<Collider2D>().enabled = false;
            tut.SpawnPhanthom();
        }
        if(collision.CompareTag("NextScene"))
        {
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                manager.GetComponent<scenemanager>().LoadNextScene(0);
            }
        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Man"))
        {
            Die();
        }
    }
    #endregion//////////// TUTORIAL ////////////

}

