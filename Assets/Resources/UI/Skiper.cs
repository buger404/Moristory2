using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Skiper : MonoBehaviour
{
    public string SkipTo = "";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonUp(0)){
            foreach(RaycastHit2D hit in Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition),Vector2.zero)){
                if(hit.transform.name.StartsWith("Skip")){
                    if(SkipTo == "NameTime"){
                        if(PlayerPrefs.GetString("scenecode") != ""){
                            Switcher.SwitchTo("Saving");
                        }else{
                            Switcher.SwitchTo(SkipTo);
                        }
                    }else{
                        Switcher.SwitchTo(SkipTo);
                    }
                }
            }
        }
    }
}
