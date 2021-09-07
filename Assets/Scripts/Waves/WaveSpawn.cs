using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawn : MonoBehaviour
{
    [SerializeField] GameObject Wave1, Wave2;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GetComponent<Collider2D>().enabled = false;
        switch(gameObject.name)
        {
            case "Wave1Act":
                Wave1.SetActive(true);
                break;
            case "Wave2Act":
                Wave2.SetActive(true);
                break;
        }
    }
}
