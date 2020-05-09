using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveComplete : MonoBehaviour
{
    void Sound(){
        SoundPlayer.Play("SaveDone");
        PlayerPrefs.SetString("map",GameConfig.CurrentMapName);
        PlayerPrefs.SetString("scene",SceneManager.GetActiveScene().name);
        PlayerPrefs.SetString("scenecode",GameConfig.RecordSceneToString());
        PlayerPrefs.SetInt("mapdirection",GameConfig.Controller.GetComponent<RPGEvent>().Direction);
    }
    void Complete(){
        Destroy(this.gameObject);
        GameConfig.IsBlocking = false;
        GameConfig.BlockEvent.Run();
    }
}