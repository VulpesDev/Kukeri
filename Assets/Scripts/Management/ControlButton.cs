using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlButton : MonoBehaviour
{
    bool clicked;
    Button bt;
    private void Start()
    {
        bt = GetComponent<Button>();
        Calibrations();
    }


    void Calibrations()
    {
        if (name == "Jump") GetComponentInChildren<Text>().text =
        Controls.PlayerJump.ToString();
        else if (name == "Left") GetComponentInChildren<Text>().text =
                Controls.PlayerLeft.ToString();
        else if (name == "Right") GetComponentInChildren<Text>().text =
                Controls.PlayerRight.ToString();
        else if (name == "Slide") GetComponentInChildren<Text>().text =
                Controls.PlayerSlide.ToString();
        else if (name == "Pause") GetComponentInChildren<Text>().text =
                Controls.Pause.ToString();
        else GetComponentInChildren<Text>().text =
                Controls.PlayerAtc.ToString();
    }

    private void Update()
    {
        bt.onClick.AddListener(ChangeValue);
        if(Input.anyKeyDown)
        ListenForInput();
    }
    public void ChangeValue()
    {
        if (PlayerPrefs.GetString("Language") == "English")
        GetComponentInChildren<Text>().text = "none";
        else if (PlayerPrefs.GetString("Language") == "Български")
            GetComponentInChildren<Text>().text = "нищо";
        clicked = true;
        
    }
    void ListenForInput()
    {
        if (!clicked) return;
        foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKey(vKey))
            {
                GetComponentInChildren<Text>().text = vKey.ToString();
                if (name == "Jump") PlayerPrefs.SetString("PlayerJump", vKey.ToString().ToLower());
                else if (name == "Left") PlayerPrefs.SetString("PlayerLeft", vKey.ToString().ToLower());
                else if (name == "Right") PlayerPrefs.SetString("PlayerRight", vKey.ToString().ToLower());
                else if (name == "Slide") PlayerPrefs.SetString("PlayerSlide", vKey.ToString().ToLower());
                else if (name == "Pause") PlayerPrefs.SetString("Pause", vKey.ToString().ToLower());
                else PlayerPrefs.SetString("PlayerAtc", vKey.ToString().ToLower());
                Controls.LoadDefaults();

            }
        }
        clicked = false;
    }
}
