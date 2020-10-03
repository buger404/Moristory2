﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public float speed = 0.1f;          //此为行走速度
    public float fps = 6;               //每秒行走图刷新次数
    private bool Updated = false;
    public int Direction = 0;           //人物朝向
    public string character;            //使用的人物的行走图名称
    private SpriteRenderer s;           //控制对象图片
    private Sprite[] walker;            //行走图图片集
    private Vector3 lp;
    private BindAbility ba;
    private bool RState = false;
    private void Awake() {
        s = this.gameObject.GetComponent<SpriteRenderer>();
        if(character == ""){return;}
        walker = Resources.LoadAll<Sprite>("Walkers/" + character);
        lp = this.transform.localPosition;
        ba = this.GetComponent<BindAbility>();
    }

    public void UpdateFace(){
        s.sprite = walker[1 + 3 * Direction];
    }

    void FixedUpdate()
    {
        Vector3 p = this.transform.localPosition;
        if(RState != ba.Recovery){
            if(ba.Recovery){
                s.color = new Color(s.color.r,s.color.g,s.color.b,0.3f);
            }else{
                s.color = new Color(s.color.r,s.color.g,s.color.b,1f);
            }
            RState = ba.Recovery;
        }

        if(lp.x == p.x && lp.z == p.z){
            if(!Updated){
                UpdateFace();
                Updated = true;
            }
        }else{
            Updated = false;
            int index = (int)(Time.time * fps) % 3;
            if(p.z < lp.z) Direction = 0;
            if(p.z > lp.z) Direction = 3;
            if(p.x < lp.x) Direction = 1;
            if(p.x > lp.x) Direction = 2;
            s.sprite = walker[index + Direction * 3];
        }
        lp = p;
    }
}
