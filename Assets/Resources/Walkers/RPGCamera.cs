using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGCamera : MonoBehaviour
{
    private List<Renderer> HiddenObj = new List<Renderer>(); 
    public List<GameObject> HiddenWhiteList = new List<GameObject>();
    GameObject Player;
    public float minx = float.MinValue,minz = float.MinValue,maxx = float.MaxValue,maxz = float.MaxValue;
    private void Awake() {
        Player = GameObject.Find("Player");
    }
    void Start() {

    }
    
    void FixedUpdate()
    {
        Vector3 te = transform.position;
        if(GameConfig.Controller == null){return;}
        Vector3 r = GameConfig.Controller.transform.position;
        Vector3 ro = transform.localEulerAngles;
        if(te.x != r.x || te.z != r.z){
            transform.position = new Vector3(te.x + (r.x - te.x) / 10,
            te.y + (((r.y + 1.5f + (GameConfig.IsMsgProcess ? -1.5f : 0f)) - te.y) / 15), 
            te.z + ((r.z - (GameConfig.IsMsgProcess ? 5f : 7f)) - te.z) / 15);
            if(transform.position.x < minx) transform.position = new Vector3(minx,transform.position.y,transform.position.z);
            if(transform.position.z < minz) transform.position = new Vector3(transform.position.x,transform.position.y,minz);
            if(transform.position.x > maxx) transform.position = new Vector3(maxx,transform.position.y,transform.position.z);
            if(transform.position.z > maxz) transform.position = new Vector3(transform.position.x,transform.position.y,maxz);

            /**transform.position = new Vector3(r.x,(r.y + 5f), 
            te.z + ((r.z - (GameConfig.IsMsgProcess ? 5f : 7f)) - te.z) / 15);**/
            transform.localEulerAngles = new Vector3(ro.x + (10 - ro.x) / 25,ro.y + (0 - ro.y) / 25,ro.z);
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

        return;
        //障碍物隐藏
        RaycastHit[] hit;Renderer ren;  
        hit = Physics.RaycastAll(Player.transform.position, transform.position);  
        if (hit.Length > 0)  
        {   
            foreach (RaycastHit ra in hit)  
            {  
                if(!IsInWhiteList(ra.collider.gameObject)){
                    ren = ra.collider.gameObject.GetComponent<Renderer>(); 
                    if(ren != null){
                        HiddenObj.Add(ren);  
                        SetMaterialsAlpha(ren, 0.5f);  
                    } 
                }
            }  
        }
        else  
        {  
            foreach (Renderer re in HiddenObj) SetMaterialsAlpha(re, 1f);  
            HiddenObj.Clear();
        }  
    }
    public bool IsInWhiteList(GameObject go){
        int parent = HiddenObj.FindIndex(m => m.transform == go.transform.parent);
        int self = HiddenObj.FindIndex(m => m.transform == go.transform);
        return parent != -1 || self != -1;
    }
    private void SetMaterialsAlpha(Renderer re, float a)  
    {  
        int materialsCount = re.materials.Length;  
        for (int i = 0; i < materialsCount; i++)  
        {  
            Color color = re.materials[i].color;   
            color.a = a;    
            re.materials[i].SetColor("_Color", color);  
        } 
    } 
}
