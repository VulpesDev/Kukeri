using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scenemanager : MonoBehaviour
{
    Animator anime;
    public static bool restart;
    public static bool custom;
    private void Start()
    {
        anime = GetComponent<Animator>();
        restart = false;
        custom = false;
    }
    public void LoadNextSceneAnime()
    {
        anime.SetBool("LoadNextScene", true);
    }
    public void SwitchToRestart()
    {
        restart = true;
    }
    public void SwitchToCustom()
    {
        custom = true;
    }
    public void LoadNextScene(int i)
    {
        if (custom)
            SceneManager.LoadScene(i);
        else if (restart)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        else
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
}