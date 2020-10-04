using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        DataCenter.Put("name",GameObject.Find("NameBox").GetComponent<InputField>().text);
        ItemSystem.PItems.Data = new List<ItemSystem.GameItem>();
        TeamController.Team.Mem = new List<TeamController.Member>();
        TeamController.Team.Mem.Add(new TeamController.Member("世原·安诺"));
        TeamController.Team.Mem.Add(new TeamController.Member("兮·御冯"));
        //TeamController.Team.Mem.Add(new TeamController.Member("雪郎·梦亭"));
        //TeamController.Team.Mem.Add(new TeamController.Member("素鱼·艾桑"));
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
