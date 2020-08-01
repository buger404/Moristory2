using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveComplete : MonoBehaviour
{
    void Sound(){
        SoundPlayer.Play("SaveDone");
        DataCenter.Put("map",GameConfig.CurrentMapName);
        DataCenter.Put("scene",SceneManager.GetActiveScene().name);
        DataCenter.Put("scenecode",GameConfig.RecordSceneToString());
        DataCenter.Put("mapdirection",GameConfig.Controller.GetComponent<RPGEvent>().Direction.ToString());
        Vector3 pp = GameConfig.Controller.transform.position;
        DataCenter.Put("mapx",pp.x.ToString());
        DataCenter.Put("mapz",pp.z.ToString());
        DataCenter.Save();
    }
    void Complete(){
        Destroy(this.gameObject);
        GameConfig.IsBlocking = false;
        GameConfig.BlockEvent.Run();
    }
}