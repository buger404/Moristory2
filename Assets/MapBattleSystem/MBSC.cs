﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//Map Battle System Controller
public class MBSC : MonoBehaviour
{
    public Image HPBar;
    public Text HPText;
    public int ControlRole = 0;
    public Sprite HPBar1,HPBar2;
    public GameObject Skill1,Skill2,Skill3;
    public GameObject ExBar,ExSkill;
    public GameObject Switcher;
    private RectTransform rect;
    private float MaxW;
    private TeamController.Member Ability1,Ability2;
    public GameObject DieScreen;
    private BindAbility ba;
    private string lSkill;
    void Start()
    {
        Ability1 = TeamController.Team.Mem[0];
        Ability2 = TeamController.Team.Mem[1];
        rect = HPBar.GetComponent<RectTransform>();
        MaxW = rect.sizeDelta.x;
        ba = GameConfig.Controller.GetComponent<BindAbility>();
        Reset();
    }

    void Reset(){
        HPBar.sprite = (ControlRole == 0) ? HPBar1 : HPBar2;
        Skill1.GetComponent<MSBC>().SetInfo(ControlRole);
        Skill2.GetComponent<MSBC>().SetInfo(ControlRole);
        Skill3.GetComponent<MSBC>().SetInfo(ControlRole);
        ba.Ability = (ControlRole == 0) ? Ability1 : Ability2;
    }

    void FixedUpdate()
    {
        HPText.text = "HP  " + Mathf.Ceil(ba.Ability.HP) + "/" + ba.Ability.MaxHP;
        rect.sizeDelta = new Vector2(ba.Ability.HP / ba.Ability.MaxHP * MaxW, rect.sizeDelta.y);

        //角色死亡处理
        DieScreen.SetActive(ba.Recovery);
        if(ba.Recovery) return;

        bool ExV = (GameConfig.ExS != "");
        ExBar.SetActive(ExV); ExSkill.SetActive(ExV);
        if(GameConfig.ExS != lSkill && ExV){
            lSkill = GameConfig.ExS;
            SkillManager.Skill s = SkillManager.S.Find(m => m.Name == GameConfig.ExS);
            s.CD /= 5;
            ExSkill.GetComponent<MSBC>().s = s;
            ExSkill.GetComponent<MSBC>().ReLoad();
        }

        //Role Switcher
        if(Input.GetKeyUp(KeyCode.Q) || GameConfig.IsTouched(Switcher)){
            ControlRole = (ControlRole == 0) ? 1 : 0;
            RPGEvent rpg = GameConfig.Controller.GetComponent<RPGEvent>();
            GameConfig.Followers[0].character = rpg.character;
            GameConfig.Followers[0].Confirm();
            rpg.character = TeamController.Team.Mem[ControlRole].Name;
            rpg.Reload();
            if(ControlRole == 0) Ability2 = ba.Ability;
            if(ControlRole == 1) Ability1 = ba.Ability;
            Reset();
            SkillManager.PlaySkillAni(GameConfig.Controller.transform.localPosition,
                                     "Interactive\\Stars\\StarExplosion");
        }

        //Touch Hander
        foreach(Touch t in Input.touches){
            if(t.phase == TouchPhase.Ended){
                GraphicRaycaster gr = this.GetComponent<GraphicRaycaster>();
                PointerEventData data = new PointerEventData(EventSystem.current);
                data.pressPosition = t.position;
                data.position = t.position;
                List<RaycastResult> results = new List<RaycastResult>();
                gr.Raycast(data, results);
                
                GameConfig.TouchAt.Clear();
                foreach(RaycastResult rr in results){
                    GameConfig.TouchAt.Add(rr.gameObject);
                }
            }
        }
    }
}