using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGCamera : MonoBehaviour
{
    GameObject Player;
    private void Awake() {
        Player = GameObject.Find("Player");
    }
    void Start() {

    }
    void Update()
    {
        Vector3 te = transform.position;
        if(GameConfig.Controller == null){return;}
        Vector3 r = GameConfig.Controller.transform.position;
        Vector3 ro = transform.localEulerAngles;
        if(te.x != r.x || te.z != r.z){
            transform.position = new Vector3(te.x + (r.x - te.x) / 10,
            te.y + (((r.y + 5f) - te.y) / 15), 
            te.z + ((r.z - (GameConfig.IsMsgProcess ? 5f : 7f)) - te.z) / 15);
            transform.localEulerAngles = new Vector3(ro.x + (50 - ro.x) / 25,ro.y + (0 - ro.y) / 25,ro.z);
        }
        //塞入玩家状态流
        if(GameConfig.StateFlow[GameConfig.StatePos].pos != Player.transform.position){
            GameConfig.StatePos++;
            if(GameConfig.StatePos >= 1000) GameConfig.StatePos = 0;
            GameConfig.PlayerState ps = new GameConfig.PlayerState();
            ps.FPS = Player.GetComponent<RPGEvent>().realfps;
            ps.pos = Player.transform.position;
            GameConfig.StateFlow[GameConfig.StatePos] = ps;
        }
    }
}
