using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhanthomHead : MonoBehaviour
{
    Rigidbody2D rb;
    GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
            Vector2 direction = (transform.position
                - Player.transform.position).normalized;
            rb.AddForce(new Vector2(direction.x * 5, 3),
                ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, Random.Range(-6, 7));
    }

}
