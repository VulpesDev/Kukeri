using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveParticles : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Destruction());
    }
    IEnumerator Destruction()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
