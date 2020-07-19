using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGCamera : MonoBehaviour
{
    void Start() {

    }
    void Update()
    {
        Vector3 te = transform.position;
        if(GameConfig.Controller == null){return;}
        Vector3 r = GameConfig.Controller.transform.position;
        Vector3 ro = transform.localEulerAngles;
        if(te.x != r.x || te.z != r.z){
            transform.position = new Vector3(te.x + (r.x - te.x) / 15,te.y, te.z + ((r.z - 5) - te.z) / 15);
            transform.localEulerAngles = new Vector3(ro.x,ro.y + (0 - ro.y) / 25,ro.z);
        }
    }
}
