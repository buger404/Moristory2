using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public static int FACE = 0;
    public static AsyncOperation ProcessingScene;
    public static string CurrentMenu = "";
    private static List<ObjectState> SceneRecord;
    private struct ObjectState{
        public GameObject Object;
        public bool State;
    }
    public static void RecordScene(){
        SceneRecord = new List<ObjectState>();
        foreach(GameObject g in SceneManager.GetActiveScene().GetRootGameObjects()){
            SceneRecord.Add(new ObjectState{Object = g,State = g.activeSelf});
            g.SetActive(false);
        }
    }
    public static void RecoverScene(){
        foreach(ObjectState o in SceneRecord){
            o.Object.SetActive(o.State);
        }
    }
}