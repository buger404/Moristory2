using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGCamera : MonoBehaviour
{
    public GameObject DayLight;
    public GameObject NightLight;
    void Start() {
        if(GameConfig.DayNight == 0){
            DayLight.SetActive(true);NightLight.SetActive(false);
        }else{
            DayLight.SetActive(false);NightLight.SetActive(true);
        }
    }
    void Update()
    {
        Vector3 te = transform.position;
        if(GameConfig.Controller == null){return;}
        Vector3 r = GameConfig.Controller.transform.position;
        if(te.x != r.x || te.y != r.y){
            transform.position = new Vector3(te.x + (r.x - te.x) / 30,te.y + (r.y - te.y) / 30,te.z );
        }
    }
}
