using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameTimeController : MonoBehaviour
{
    private Animator BindAni;
    void PauseforInputing(){
        BindAni = this.GetComponent<Animator>();
        BindAni.speed = 0;
    }
    void NameComplete(){
        //销毁旧的存档
        PlayerPrefs.DeleteAll();
        DataCenter.Saves.Data = new List<DataCenter.Key>();
        ItemSystem.PItems.Data = new List<ItemSystem.GameItem>();
        TeamController.Team.Mem = new List<TeamController.Member>();
        TeamController.Team.Mem.Add(new TeamController.Member("世原·安诺"));
        Switcher.SwitchTo("NatingDaily");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
