using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    Rigidbody2D rb;
    float speed = 50f;
    GameObject shootSound;
    GameObject particles;
    void Start()
    {
         shootSound = Resources.Load<GameObject>("ShootSound");
         particles = Resources.Load<GameObject>("WaveParticles");
        rb = GetComponent<Rigidbody2D>();
        Instantiate(shootSound, transform.position, transform.rotation);
    }

    void Update()
    {
        rb.AddForce(transform.up * speed);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Instantiate(particles, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
