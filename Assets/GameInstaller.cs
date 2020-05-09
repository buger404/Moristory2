using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameInstaller : MonoBehaviour
{
    private void Awake() {
        SoundPlayer.Play("");
        GameObject fab = (GameObject)Resources.Load("Prefabs\\PayCanvas");
        GameObject fab2 = (GameObject)Resources.Load("Prefabs\\SaveCanvas");
    }
}
