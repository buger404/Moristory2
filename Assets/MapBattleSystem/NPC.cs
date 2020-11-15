using System.Collections;
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
    private SkillManager.Skill[] S = new SkillManager.Skill[3];
    public enum NPCType{
        None = 0,
        Tester = 1,
        Approach = 2,
        Backward = 3,
        Hidden = 4
    }
    public NPCType type = NPCType.None;
    private float dtime = 0;
    private float lasttime = 0;
    private int XD,YD;
    private bool Moved = false;
    private void Awake() {
        s = this.gameObject.GetComponent<SpriteRenderer>();
        if(character == ""){return;}
        walker = Resources.LoadAll<Sprite>("Walkers/" + character);
        lp = this.transform.localPosition;
        ba = this.GetComponent<BindAbility>();
        for(int i = 0;i < 3;i++)
        S[i] = SkillManager.S.Find(m => m.Name == ba.Ability.Magics[i]);
    }

    public void UpdateFace(){
        s.sprite = walker[1 + 3 * Direction];
    }

    void Skilling(){
        for(int i = 0;i < 3;i++){
            S[i].CDa += Time.deltaTime;
            if(S[i].CDa >= S[i].CD){
                S[i].CDa = 0;
                SkillManager.MakeFireworks(S[i],this.transform.localPosition,this.gameObject);
            }
        } 
    }

    void ApproachNPC(){
        if(GameConfig.IsBlocking) {BackwardNPC(); return;}
        Vector3 pp = GameConfig.Controller.transform.localPosition;
        Vector3 p = this.transform.localPosition;
        if(Mathf.Abs(pp.x - p.x) > 10 || Mathf.Abs(pp.z - p.z) > 10) return;
        if(pp.x > p.x) {p.x += 0.06f; Moved = true;}
        if(pp.x < p.x) {p.x -= 0.06f; Moved = true;}
        if(pp.z > p.z) {p.z += 0.06f; Moved = true;}
        if(pp.z < p.z) {p.z -= 0.06f; Moved = true;}
        this.transform.localPosition = p;
        Skilling();
    }

    void BackwardNPC(){
        Vector3 pp = GameConfig.Controller.transform.localPosition;
        Vector3 p = this.transform.localPosition;
        if(Mathf.Abs(pp.x - p.x) > 10 || Mathf.Abs(pp.z - p.z) > 10) return;
        if(pp.x > p.x) {p.x -= 0.06f; Moved = true;}
        if(pp.x < p.x) {p.x += 0.06f; Moved = true;}
        if(pp.z > p.z) {p.z -= 0.06f; Moved = true;}
        if(pp.z < p.z) {p.z += 0.06f; Moved = true;}
        this.transform.localPosition = p;
        Skilling();
    }

    void HiddenNPC(){
        Vector3 pp = GameConfig.Controller.transform.localPosition;
        Vector3 p = this.transform.localPosition;
        if(Mathf.Abs(pp.x - p.x) > 5 || Mathf.Abs(pp.z - p.z) > 5) return;
        Skilling();
    }

    void FixedUpdate()
    {
        Vector3 pp = GameConfig.Controller.transform.localPosition;
        Vector3 p = this.transform.localPosition;
        if(Mathf.Abs(pp.x - p.x) > 20 || Mathf.Abs(pp.z - p.z) > 20) return;

        if(RState != ba.Recovery){
            if(ba.Recovery){
                s.color = new Color(s.color.r,s.color.g,s.color.b,0.3f);
            }else{
                s.color = new Color(s.color.r,s.color.g,s.color.b,1f);
            }
            RState = ba.Recovery;
        }

        if(ba.Recovery) return;
        
        Moved = false;

        if(type == NPCType.Approach) ApproachNPC();
        if(type == NPCType.Backward) BackwardNPC();
        if(type == NPCType.Hidden) HiddenNPC();
        if(type == NPCType.None) Skilling();

        dtime += Time.deltaTime;
        if(dtime > lasttime){
            dtime = 0;lasttime = Random.Range(5f,15f);
            XD = Random.Range(-1,2); YD = Random.Range(-1,2);
        }
        if(!Moved && type != NPCType.Hidden){
            p.x += 0.06f * XD; p.z += 0.06f * YD;
            this.transform.localPosition = p;
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
