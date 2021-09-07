using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Language : MonoBehaviour
{
    static public int languageIndex = 0;
    int oldIndex = 1;
    private void Start()
    {
    }
    private void Update()
    {
        // if (oldLanguage != languageChosen)
        //ChangeLanguage();
        if (oldIndex != languageIndex)
            ChangeLanguage();
    }
    public void ChangeLanguage()
    {
        switch (languageIndex)
        {
            case 0:
                PlayerPrefs.SetString("Language", "English");
                break;
            case 1:
                PlayerPrefs.SetString("Language", "Български");
                break;
        }
        
        oldIndex = languageIndex;
    }
}
