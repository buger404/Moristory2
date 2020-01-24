using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameVars{
    public static string MenuBackScene = "";
    public static int FadeState = 0;
    public static bool DialogLoaded = false;
    public static AudioSource BGM;
    public static GlobalEvents BGMController;
    public static string ActiveScene;
    public static string PlayerName = "";
    public static int TaleShowed = 0;
    public static bool IsFading(){
        if(FadeState == 0){
            return true;
        }else{
            return false;
        }
    }
    public static bool IsDialogLoaded(){
        if(DialogLoaded){
            return true;
        }else{
            return false;
        }
    }
}