using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenu : MonoBehaviour
{
    private void Update()
    {
        CheckOption();
    }
    private void Start()
    {
        if (PlayerPrefs.GetString("Language") == "English" && _languages.value != 0)
        {
            _languages.value = 0;
        }
        else if (PlayerPrefs.GetString("Language") == "Български" && _languages.value != 1)
        {
            _languages.value = 1;
        }
        Controls.LoadDefaults();
        CheckOption();
    }
    #region Language
    //Settings
    [SerializeField]TMP_Dropdown _languages;
    [SerializeField]TextMeshProUGUI _LanguageLabel;
    [SerializeField]TextMeshProUGUI tx_ButtonBack;
    [SerializeField]TextMeshProUGUI tx_Jump;
    [SerializeField]TextMeshProUGUI tx_Left;
    [SerializeField]TextMeshProUGUI tx_Right;
    [SerializeField]TextMeshProUGUI tx_Slide;
    [SerializeField]TextMeshProUGUI tx_Attack;

    //Main
    [SerializeField]TextMeshProUGUI tx_Play;
    [SerializeField]TextMeshProUGUI tx_Settings;
    [SerializeField]TextMeshProUGUI tx_Quit;

    public void CheckOption()
    {
        Language.languageIndex = _languages.value;
        if (PlayerPrefs.GetString("Language") == "English")
        {
            ChangeToEnglish();
        }
        else if (PlayerPrefs.GetString("Language") == "Български")
        {
            ChangeToBulgarian();
        }
    }
    void ChangeToEnglish()
    {
        //Settings
        Debug.Log("English");
        _languages.options[0].text = "English";
        _languages.options[1].text = "Bulgarian";
        _languages.gameObject.transform.GetChild(0) //label
            .GetComponent<TextMeshProUGUI>().text = "English";

        tx_Jump.text = "Jump";
        tx_Left.text = "Left";
        tx_Right.text = "Right";
        tx_Slide.text = "Slide";
        tx_Attack.text = "Attack";
        

        _LanguageLabel.text = "Language";

        tx_ButtonBack.text = "Back";

        //Main
        tx_Play.text = "Play";
        tx_Settings.text = "Settings";
        tx_Quit.text = "Quit";
    }
    void ChangeToBulgarian()
    {
        //Settings
        Debug.Log("Bulgarian");
        _languages.options[0].text = "Английски";
        _languages.options[1].text = "Български";
        _languages.gameObject.transform.GetChild(0) //label
    .GetComponent<TextMeshProUGUI>().text = "Български";


        tx_Jump.text = "Скачане";
        tx_Left.text = "Ляво";
        tx_Right.text = "Дясно";
        tx_Slide.text = "Хлъзгане";
        tx_Attack.text = "Атака";

        _LanguageLabel.text = "Език";

        tx_ButtonBack.text = "Назад";

        //Main
        tx_Play.text = "Играй";
        tx_Settings.text = "Настройки";
        tx_Quit.text = "Изход";
    }
    #endregion
}
