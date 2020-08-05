using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BattleController : MonoBehaviour
{
    public GameObject DialogB;
    public GameObject DialogT;
    public GameObject TarTeam;
    public List<GameObject> BattleBtn = new List<GameObject>();
    public GameObject SkillBoard;
    public GameObject BGM;
    public GameObject Lighten;
    private Light Lights;
    private float BZ,FX1,FX2,BY;
    private List<FighterController> F1,F2;
    public List<string> Msg = new List<string>();
    public List<GameObject> Teams = new List<GameObject>();
    private List<FighterController> Rounds = new List<FighterController>();
    private bool LastShow = true;
    private float cox,coy,coz;
    private int BattleStep = 0,LastBattle;
    private float ttick = 0;
    private FighterController lfc,tar;
    private SkillManager.Skill lSk;
    private GameObject SkObj;
    private float Injury,Source;
    private void Awake() {
        Lights = Lighten.GetComponent<Light>();

        Vector3 v = GameObject.Find("Fighter1").transform.position;
        BZ = v.z; FX1 = v.x; BY = v.y;
        v = GameObject.Find("Fighter2").transform.position;
        FX2 = v.x;

        Vector3 v2 = Camera.main.transform.position;
        cox = v2.x;coy = v2.y;coz = v2.z;

        #region 加载我方和敌方成员
            F1 = new List<FighterController>();
            F2 = new List<FighterController>();
            GameObject fab = (GameObject)Resources.Load("Prefabs\\Fighter");
            GameObject fab2 = (GameObject)Resources.Load("Prefabs\\TeamInfo");
            for(int i = 0;i < TeamController.Team.Mem.Count;i++){
                TeamController.Member m = TeamController.Team.Mem[i];
                GameObject box = 
                    Instantiate(fab,new Vector3(FX1-i*0.5f,BY,BZ - i*2),
                    Quaternion.identity,this.transform.parent);
                FighterController f = box.GetComponent<FighterController>();
                f.character = m.Name;
                f.IsEne = false;
                f.BindMember = m;
                f.Up();
                F1.Add(f);
                box.SetActive(true);
                /**box = 
                    Instantiate(fab2,new Vector3(160+i*150,220,0),
                    Quaternion.identity,this.transform);
                box.GetComponent<RectTransform>().localScale = new Vector3(0.4f,0.4f,0.4f);
                box.GetComponent<TeamInfoController>().BindMember = f.BindMember;
                box.GetComponent<TeamInfoController>().UpdateInfo();
                box.SetActive(true);**/
            }
            for(int i = 0;i < TeamController.Team.Mem.Count;i++){
                TeamController.Member m = TeamController.Team.Mem[i];
                GameObject box = 
                    Instantiate(fab,new Vector3(FX2+i*0.5f,BY,BZ - i*2),
                    Quaternion.identity,this.transform.parent);
                FighterController f = box.GetComponent<FighterController>();
                f.character = m.Name;
                f.IsEne = true;
                f.BindMember = m;
                f.Up();
                F2.Add(f);
                box.SetActive(true);
            }
            
        #endregion
        GameObject.Find("Fighter1").SetActive(false);
        GameObject.Find("Fighter2").SetActive(false);

        Msg.Add("双方的较量开始了！");
        Msg.Add("道具和动作还不能使用...");

        FetchRound();

    }
    
    void FetchRound(){
        Rounds.Clear();
        foreach(FighterController f in F1){
            if(f.BindMember.HP > 0 && f.BindMember.MP > SkillManager.MinMP(f.BindMember.Magics)){
                Rounds.Add(f);
            }
        }
        foreach(FighterController f in F2){
            if(f.BindMember.HP > 0 && f.BindMember.MP > SkillManager.MinMP(f.BindMember.Magics)){
                Rounds.Add(f);
            }
        }
        Rounds.Sort((m2,m1) => (m1.BindMember.SPD + Random.Range(-10,10)).CompareTo(m2.BindMember.SPD + Random.Range(-10,10)));
    }

    void EneUse(){
        List<SkillManager.Skill> sk = new List<SkillManager.Skill>();
        for(int i = 0;i < 3;i++){
            sk.Add(SkillManager.S.Find(m => m.Name == Rounds[0].BindMember.Magics[i]));
        }
        sk = sk.FindAll(m => m.MP <= Rounds[0].BindMember.MP);
        lSk = sk[Random.Range(0,sk.Count)];
        List<FighterController> lt;
        if(lSk.Strength >= 0){
            lt = F1.FindAll(m => m.BindMember.HP > 0);
        }else{
            lt = F2.FindAll(m => m.BindMember.HP > 0);
        }
        tar = lt[Random.Range(0,lt.Count)];
        Msg.Add(Rounds[0].BindMember.Name + "对" + tar.BindMember.Name + "使用了“" + lSk.Name + "”！");
        Rounds[0].State = FighterController.BattleState.Magic;
        BattleStep = 3;
        ttick = 0;
    }

    // Update is called once per frame
    void Update()
    {
        ttick += Time.deltaTime;

        if(BattleStep == 3 && Msg.Count == 0){
            Rounds[0].BindMember.MP -= lSk.MP;
            BattleStep = 4; ttick = 0;
            TeamInfoController tt = TarTeam.GetComponent<TeamInfoController>();
            tt.BindMember = new TeamController.Member();
            tt.BindMember.Name = tar.BindMember.Name;
            tt.BindMember.HP = tar.BindMember.HP;
            tt.BindMember.MaxHP = tar.BindMember.MaxHP;
            tt.UpdateInfo();

            Injury = lSk.Strength * Rounds[0].BindMember.ATK;
            if(lSk.Strength < 0) Injury /= 1.5f;
            if(lSk.Strength > 0) Injury -= tar.BindMember.DEF;
            Source = Injury;
            Injury *= TeamController.JC[(int)lSk.Job-1,(int)tar.BindMember.Job[0]-1];
            Injury *= TeamController.JC[(int)lSk.Job-1,(int)tar.BindMember.Job[1]-1];

            if(lSk.Strength >= 0) tar.State = FighterController.BattleState.Hurt;
            GameObject fab = (GameObject)Resources.Load("Epic Toon FX\\Prefabs\\" + lSk.Animate);
            SkObj = Instantiate(fab,
            new Vector3(tar.transform.position.x,tar.transform.position.y - 1,tar.transform.position.z), 
            Quaternion.identity,this.transform.parent);
            SkObj.transform.localScale = new Vector3(2.5f,2.5f,2.5f);
            SkObj.transform.localRotation = new Quaternion(-90f,0f,0f,0f);
            SkObj.SetActive(true);
        }

        if(BattleStep == 4 && ttick >= 3.0f){
            float fi = Injury / Source;
            if(lSk.Strength != 0){
                if(fi >= 1.1f && fi <= 1.3f) Msg.Add("效果不错");
                if(fi > 1.3f) Msg.Add("效果绝佳！！！");
                if(fi >= 0.75f && fi <= 0.9f) Msg.Add("效果不咋的...");
                if(fi < 0.75f && fi > 0f) Msg.Add("好像对对方没什么用...");
                if(fi == 0f) Msg.Add("似乎完全没有效果。");
            }
            tar.BindMember.HP -= Mathf.Floor(Injury);
            tar.State = FighterController.BattleState.Normal;

            if(tar.BindMember.HP > tar.BindMember.MaxHP) tar.BindMember.HP = tar.BindMember.MaxHP;
            if(tar.BindMember.HP / tar.BindMember.MaxHP <= 0.3f && tar.BindMember.HP > 0){
                Msg.Add(tar.BindMember.Name + "快要撑不住了！");
                tar.State = FighterController.BattleState.BadHurt;
            }
            if(tar.BindMember.HP <= 0){
                tar.BindMember.HP = 0;
                Msg.Add(tar.BindMember.Name + "倒下了！");
                Rounds.Remove(tar);
                tar.State = FighterController.BattleState.Die;
            } 

            Rounds[0].State = FighterController.BattleState.Normal;
            BattleStep = 0; ttick = 0;
            try{Destroy(SkObj);}catch{}
            Rounds.RemoveAt(0);

            bool GameClosed = false;
            if(F1.FindIndex(m => m.BindMember.HP > 0) == -1) {
                GameClosed = true;
                foreach(FighterController f in F2.FindAll(m => m.BindMember.HP > 0)){
                    f.State = FighterController.BattleState.Win;
                }
                BGM.GetComponent<AudioSource>().Stop();
                SoundPlayer.Play("Defeat2");
                Msg.Add("我方败给了敌方！");
            }
            if(F2.FindIndex(m => m.BindMember.HP > 0) == -1) {
                GameClosed = true;
                foreach(FighterController f in F1.FindAll(m => m.BindMember.HP > 0)){
                    f.State = FighterController.BattleState.Win;
                }
                BGM.GetComponent<AudioSource>().Stop();
                SoundPlayer.Play("Victory1");
                Msg.Add("我方战胜了敌方！");
            }
            if(GameClosed) {Rounds.Clear(); return;}
            if(Rounds.Count == 0) FetchRound();
        }

        bool ttA = (BattleStep >= 4);
        if(TarTeam.activeSelf != ttA) TarTeam.SetActive(ttA);
        if(ttA){
            TeamInfoController tt = TarTeam.GetComponent<TeamInfoController>();
            tt.BindMember.HP += (tar.BindMember.HP - Injury - tt.BindMember.HP) / 10;
            tt.BindMember.HP = Mathf.Floor(tt.BindMember.HP);
            if(tt.BindMember.HP < 0) tt.BindMember.HP = 0;
            if(tt.BindMember.HP > tt.BindMember.MaxHP) tt.BindMember.HP = tt.BindMember.MaxHP;
            tt.UpdateInfo();
        }

        bool ShowUI = (Msg.Count == 0);
        if(ShowUI != LastShow || BattleStep != LastBattle){
            LastShow = ShowUI; LastBattle = BattleStep;
            DialogB.SetActive(!ShowUI);
            DialogT.SetActive(!ShowUI);
            foreach(GameObject go in BattleBtn){
                go.SetActive(ShowUI && BattleStep == 0);
            }
        }
        if(ShowUI && Rounds.Count > 0){
            if(!Rounds[0].Equals(lfc)){
                if(!(lfc == null)) {lfc.IsYourTurn = false;lfc.State = FighterController.BattleState.Normal;}
                lfc = Rounds[0];
                Rounds[0].IsYourTurn = true;
                Msg.Add(lfc.BindMember.Name + "(" + (lfc.IsEne ? "敌方" : "我方") + ")的回合！");
                if(lfc.IsEne) EneUse();
            }
        }

        float cx = cox,cy = coy,cz = coz;
        float ins = 1;
        //FighterController f = Rounds[0];
        if(ShowUI){
            cx = Rounds[0].gameObject.transform.position.x;
            cz = Rounds[0].gameObject.transform.position.z - 14;
        }
        if(BattleStep == 4){
            cx = tar.gameObject.transform.position.x;
            cz = tar.gameObject.transform.position.z - 12;
            ins = 0.3f;
        }
        if(BattleStep == 1) cx += 12;
        if(BattleStep == 2) {
            if(lSk.Strength >= 0){
                cx += 5;
            }else{
                cx -= 10;
            }
        }
        Vector3 v2 = Camera.main.transform.position;
        Camera.main.transform.position = new Vector3(v2.x + (cx - v2.x) / 10,v2.y + (cy - v2.y) / 10,v2.z + (cz - v2.z) / 10);

        Lights.intensity += (ins - Lights.intensity) / 10;

        Transform Sk = SkillBoard.transform;
        float px = 1380;
        if(BattleStep == 1) px = 420;
        if(BattleStep == 2) px = -439;
        Sk.localPosition = new Vector3(px + (Sk.localPosition.x - px) / 10,Sk.localPosition.y,Sk.localPosition.z);

        if(!ShowUI) DialogT.GetComponent<Text>().text = Msg[0];
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
    }

    void Carry(string name,GameObject go){
        if(name == DialogB.name){
            DialogB.SetActive(false);
            DialogB.SetActive(true);
            SoundPlayer.Play("Cursor1");
            Msg.Remove(Msg[0]);
        }
        if(name == "ATK"){
            BattleStep = 1;
            SkillInfoController Si;
            for(int i = 0;i < 3;i++){
                Si = SkillBoard.transform.Find("Skill" + (i + 1)).GetComponent<SkillInfoController>();
                Si.BindS = SkillManager.S.Find(m => m.Name == Rounds[0].BindMember.Magics[i]);
                Si.UpdateInfo();
                SkillBoard.transform.Find("InfoText").GetComponent<Text>().text = 
                "MP  " + Rounds[0].BindMember.MP + " / " + Rounds[0].BindMember.MaxMP;
            }
            string TeamStr = "",HColor = "";
            for(int i = 0;i < F1.Count;i++){
                float f = F1[i].BindMember.HP / F1[i].BindMember.MaxHP;
                if(f >= 0.3f) HColor = "orange";
                if(f < 0.3f) HColor = "red";
                if(f >= 0.6f) HColor = "green";
                TeamStr += F1[i].BindMember.Name + "     <color=" + HColor + 
                            ">" + F1[i].BindMember.HP + " / " + F1[i].BindMember.MaxHP + "</color>\n";
            }
            SkillBoard.transform.Find("TeamText").GetComponent<Text>().text = TeamStr;
        }
        if(name == "NextBtn"){
            //BattleStep = 2;
        }
        if(name == "PrevBtn"){
            Debug.Log("Prev");
            BattleStep = 1;
        }
        if(name.StartsWith("Team") && name != "TeamText"){
            List<FighterController> lt = F2;
            if(lSk.Strength >= 0){
                lt = F2;
            }else{
                lt = F1;
            }
            tar = lt.Find(m => m.BindMember.Equals(go.GetComponent<TeamInfoController>().BindMember));
            Msg.Add(Rounds[0].BindMember.Name + "对" + tar.BindMember.Name + "使用了“" + lSk.Name + "”！");
            Rounds[0].State = FighterController.BattleState.Magic;
            BattleStep = 3;
            ttick = 0;
        }
        if(name.StartsWith("Skill") && name != "SkillBoard"){
            //Debug.Log("Skill？");
            List<FighterController> lt = F2;
            SkillInfoController Sk = SkillBoard.transform.Find(name).GetComponent<SkillInfoController>();
            SkillManager.Skill sk = Sk.BindS;
            if(sk.MP > Rounds[0].BindMember.MP){
                MessageCreator.CreateMsg("魔力不足","你无法使用这个魔法。");
                return;
            }
            lSk = sk;
            //Debug.Log(Sk.name);
            if(lSk.Strength >= 0){
                lt = F2.FindAll(m => m.BindMember.HP > 0);
            }else{
                lt = F1.FindAll(m => m.BindMember.HP > 0);
            }
            for(int i = 0;i < 4;i++){
                if(i >= lt.Count){
                    Teams[i].SetActive(false);
                }else{
                    Teams[i].SetActive(true);
                    Teams[i].GetComponent<TeamInfoController>().BindMember = lt[i].BindMember;
                    Teams[i].GetComponent<TeamInfoController>().UpdateInfo();
                }
            }
            BattleStep = 2;
        }
    }
}
