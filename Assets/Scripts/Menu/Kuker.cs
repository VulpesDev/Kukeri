using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kuker : MonoBehaviour
{
    Animator anime;
    void Start()
    {
        anime = GetComponent<Animator>();
        StartCoroutine(Blink());
        StartCoroutine(Mounth());
    }

    void Update()
    {
        
    }
    IEnumerator Blink()
    {
        yield return new WaitForSeconds(Random.Range(2.0f, 5.0f));
        StartCoroutine(ABlink());
        StartCoroutine(Blink());
    }
    IEnumerator Mounth()
    {
        yield return new WaitForSeconds(Random.Range(2.0f, 5.0f));
        StartCoroutine(AMounth());
        StartCoroutine(Mounth());
    }
    IEnumerator ABlink()
    {
        anime.SetBool("Blink", true);
        yield return new WaitForSeconds(0.1f);
        anime.SetBool("Blink", false);
    }
    IEnumerator AMounth()
    {
        anime.SetBool("Mounth", true);
        yield return new WaitForSeconds(0.1f);
        anime.SetBool("Mounth", false);
    }
}
