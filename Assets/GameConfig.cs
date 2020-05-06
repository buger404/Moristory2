using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Xml;

public class GameConfig
{
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
    public static string CurrentMapName = "";
    public static AsyncOperation ProcessingScene;
    public static string CurrentMenu = "";
    private static List<ObjectState> SceneRecord;
    private struct ObjectState{
        public GameObject Object;
        public bool State;
    }
    public static string RecordSceneToString(){
        string r = "";
        foreach(GameObject g in SceneManager.GetActiveScene().GetRootGameObjects()){
            r += g.name + ";" + g.activeSelf + ";" + g.transform.localPosition.x + ";" + g.transform.localPosition.y + "|";
        }
        return r;
    }
    public static void RecoverSceneFromString(string code){
        string[] g = code.Split('|');
        for(int i = 0;i < g.Length-1;i++){
            string[] c = code.Split(';');
            GameObject go = GameObject.Find(c[0]);
            go.SetActive(c[1] == "True");
            go.transform.localPosition = new Vector3(float.Parse(c[2]),float.Parse(c[3]),go.transform.localPosition.z);
        }
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