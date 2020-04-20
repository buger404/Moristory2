using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConfig{
    public static GameObject Controller;
    public static int DayNight = 0;
    public static bool IsBlocking = false;
    public static bool WalkingTask = false;
    public static RPG BlockEvent;
    public static DialogController ActiveDialog;
    public static SpyController ActiveSpy;
    public static RPG LastEvent;
    public static bool IsMsgProcess = false;
    public static string TpSpot = "";
    public static int TpDir = 0;
}