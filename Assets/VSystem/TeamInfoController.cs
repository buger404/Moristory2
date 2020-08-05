using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamInfoController : MonoBehaviour
{
    public static List<GameObject> Gos = new List<GameObject>();
    public static List<TeamInfoController> peer = new List<TeamInfoController>();
    public static TeamInfoController last;
    public Animator ani;
    public TeamController.Member BindMember;
    private Text Name,HPT,Abi;
    private WalkingAnimate Walking;
    private RectTransform HPBar;
    private Image HPC,J1,J2;
    private float HPMaxW;
    private void Awake() {
        ani = this.GetComponent<Animator>();
        peer.Add(this);
        HPBar = this.transform.Find("HP").GetComponent<RectTransform>();
        HPMaxW = HPBar.sizeDelta.x;
    }

    public void UpdateInfo(){
        Name = this.transform.Find("Caption").GetComponent<Text>();
        HPT = this.transform.Find("Content").GetComponent<Text>();
        try{Abi = GameObject.Find("Abilities").GetComponent<Text>();}catch{}
        Walking = this.transform.Find("Icon").GetComponent<WalkingAnimate>();
        HPBar = this.transform.Find("HP").GetComponent<RectTransform>();
        HPC = this.transform.Find("HP").GetComponent<Image>();
        try{
            J1 = GameObject.Find("JobIcon1").GetComponent<Image>();
            J2 = GameObject.Find("JobIcon2").GetComponent<Image>();
        }catch{

        }
        
        Name.text = BindMember.Name;
        HPT.text = BindMember.HP + " / " + BindMember.MaxHP;
        Walking.character = BindMember.Name;
        Walking.UpdateWalker();
        HPBar.sizeDelta = new Vector2(BindMember.HP / BindMember.MaxHP * HPMaxW,HPBar.sizeDelta.y);

        float p = BindMember.HP / BindMember.MaxHP;
        if(p >= 0) HPC.color = new Color(1f,83f / 255f,57f / 255f);
        if(p >= 0.3) HPC.color = new Color(1f,15f / 255f,0f);
        if(p >= 0.6) HPC.color = new Color(0f,233f / 255f,118f / 255f);

        if(last == null && Abi != null) Choose();
    }
    private void OnDestroy() {
        peer.Remove(this);
        foreach(GameObject go in Gos){
            Destroy(go);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void Choose(){
        if(this.Equals(last)) return;

        foreach(GameObject go in Gos){
            Destroy(go);
        }

        ani.Play("HighLightTeam",0,0.0f);
        ani.SetFloat("HSpeed",1.0f);
        J1.sprite = Resources.Load<Sprite>("Job/t" + (int)BindMember.Job[0]);
        J2.sprite = Resources.Load<Sprite>("Job/t" + (int)BindMember.Job[1]);
        J2.color = BindMember.Job[1] == TeamController.JOB.Normal ? new Color(0,0,0,0) : new Color(1,1,1);

        Abi.text = $"攻击  <color=red>{BindMember.ATK}</color>     " +
                   $"防御  <color=blue>{BindMember.DEF}</color>     " +
                   $"敏捷  <color=green>{BindMember.SPD}</color>\n" +
                   $"体力  <color=green>{BindMember.HP}/{BindMember.MaxHP}</color>     " +
                   $"魔力  <color=blue>{BindMember.MP}/{BindMember.MaxMP}</color>";

        GameObject fab = (GameObject)Resources.Load("Prefabs\\SkillInfo");
        int index = 0;
        foreach(string s in BindMember.Magics){
            SkillManager.Skill sk = SkillManager.S.Find(m => m.Name == s);
            GameObject box = Instantiate(fab,new Vector3(0,0,0),Quaternion.identity,
                this.transform.parent);
            box.SetActive(true);
            box.transform.localPosition = new Vector3(160,-80 - index * 45,0);
            index++;
            box.GetComponent<SkillInfoController>().BindS = sk;
            box.GetComponent<SkillInfoController>().UpdateInfo();
            Gos.Add(box);
        }

        foreach(TeamInfoController ti in peer){
            if(ti.Equals(last)){
                ti.ani.Play("HighLightTeam",0,0.25f);
                ti.ani.SetFloat("HSpeed",-1.0f);
            }
        }
        last = this;
    }
}
