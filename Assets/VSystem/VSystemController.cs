﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VSystemController : MonoBehaviour
{
    public GameObject EnterBtn;
    public GameObject ExitBtn;
    public GameObject ScrollView;
    private GameObject Character;
    public string LastCover = "";
    public List<GameObject> UI;
    private List<GameObject> Created = new List<GameObject>();
    private void Awake() {
        Character = GameObject.Find("RoleCover");
        Carry(ExitBtn.name,null);
    }
    public void Carry(string name,GameObject goe){
        if(name == EnterBtn.name && GameConfig.IsBlocking == false){
            EnterBtn.SetActive(false);
            ExitBtn.SetActive(true);
            foreach(GameObject go in UI) go.SetActive(true);
            Created = new List<GameObject>();
            GameObject fab = (GameObject)Resources.Load("Prefabs\\TeamInfo");
            int index = 0;
            foreach(TeamController.Member m in TeamController.Team.Mem){
                GameObject box = Instantiate(fab,new Vector3(0,0,0),Quaternion.identity,
                    ScrollView.transform);
                box.SetActive(true);
                box.transform.localPosition = new Vector3(-230,100 -index * 90,0);
                index++;
                box.GetComponent<TeamInfoController>().BindMember = m;
                box.GetComponent<TeamInfoController>().UpdateInfo();
                Created.Add(box);
            }
            GameConfig.IsBlocking = true;
        }
        if(name == ExitBtn.name){
            EnterBtn.SetActive(true);
            ExitBtn.SetActive(false);
            foreach(GameObject go in UI) go.SetActive(false);
            foreach(GameObject go in Created) Destroy(go);
            GameConfig.IsBlocking = false;
        }
        if(name.StartsWith("TeamInfo")){
            goe.GetComponent<TeamInfoController>().Choose();
        }
        if(name.StartsWith("SkillInfo")){
            goe.GetComponent<SkillInfoController>().Detail();
        }
    }
    void Update()
    {
        /**GraphicRaycaster gr = this.GetComponent<GraphicRaycaster>();
        PointerEventData data = new PointerEventData(EventSystem.current);
        data.pressPosition = new Vector3(Camera.main.scaledPixelWidth/2,Camera.main.scaledPixelHeight/2,0);
        data.position = data.pressPosition;
        List<RaycastResult> results = new List<RaycastResult>();
        gr.Raycast(data, results);
        string Cover = "";

        foreach(RaycastResult rr in results){
            if(rr.gameObject.transform.Find("Caption") != null){
                Cover = rr.gameObject.transform.Find("Caption").GetComponent<Text>().text;
            }
        }

        if(LastCover != Cover){
            LastCover = Cover;
            Character.SetActive(Cover != "");
            if(Cover != ""){
                Sprite chara = Resources.Load<Sprite>("Characters\\" + LastCover);
                Image csp = Character.GetComponent<Image>();
                RectTransform csr = Character.GetComponent<RectTransform>();
                float ow = csr.sizeDelta.x;
                csp.sprite = chara;
                csr.sizeDelta = new Vector2 (ow,(chara.rect.height / chara.rect.width) * ow);
            }
        }**/

        if(Input.GetMouseButtonUp(0)){
            GraphicRaycaster gr = this.GetComponent<GraphicRaycaster>();
            PointerEventData data = new PointerEventData(EventSystem.current);
            data.pressPosition = Input.mousePosition;
            data.position = Input.mousePosition;
            List<RaycastResult> results = new List<RaycastResult>();
            gr.Raycast(data, results);

            foreach(RaycastResult rr in results){
                Carry(rr.gameObject.name,rr.gameObject);
            }
        }
        if(Input.GetKeyUp(KeyCode.C)){
            Carry(EnterBtn.activeSelf ? EnterBtn.name : ExitBtn.name,null);
        }
    }
}
