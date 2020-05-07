using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SavePoint : MonoBehaviour
{
    void Carry(string name){
        Debug.Log("try:" + name);
        if(name.StartsWith("Continue")){
            SoundPlayer.Play("Load");
            Switcher.SwitchTo(PlayerPrefs.GetString("scene"));
        }
        if(name.StartsWith("Reset")){
            Switcher.SwitchTo("NameTime");
        }
    }
    private void Update() {
        this.transform.Find("Tip").GetComponent<Text>().text =
            PlayerPrefs.GetString("name") + "·" + PlayerPrefs.GetString("map");
        if(Input.GetMouseButtonUp(0)){
            foreach(RaycastHit2D hit in Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition),Vector2.zero)){
                Carry(hit.transform.name);
            }
        }
    }
}