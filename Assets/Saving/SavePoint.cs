using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SavePoint : MonoBehaviour
{
    void Carry(string name){
        Debug.Log("try:" + name);
        if(name.StartsWith("Continue")){
            DataCenter.Load();
            SoundPlayer.Play("Load");
            Switcher.SwitchTo(DataCenter.Get("scene"));
        }
        if(name.StartsWith("Reset")){
            Switcher.SwitchTo("NameTime");
        }
    }
    private void Update() {
        this.transform.Find("Tip").GetComponent<Text>().text =
            DataCenter.Get("name") + "·" + DataCenter.Get("map");
        if(Input.GetMouseButtonUp(0)){
            foreach(RaycastHit2D hit in Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition),Vector2.zero)){
                Carry(hit.transform.name);
            }
        }
    }
}