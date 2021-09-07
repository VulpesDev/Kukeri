using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.CodeDom;
using System;

public static class Controls
{
    public static string PlayerJump;
    public static string PlayerLeft;
    public static string PlayerSlide;
    public static string PlayerRight;
    public static string PlayerAtc;
    public static string Pause;

    public static void LoadDefaults()
    {
       PlayerJump = PlayerPrefs.GetString("PlayerJump", "w");
       PlayerLeft = PlayerPrefs.GetString("PlayerLeft", "a");
       PlayerSlide = PlayerPrefs.GetString("PlayerSlide", "s");
       PlayerRight = PlayerPrefs.GetString("PlayerRight", "d");
       PlayerAtc = PlayerPrefs.GetString("PlayerAtc", "space");
       Pause = PlayerPrefs.GetString("Pause", "escape");
    }

}
