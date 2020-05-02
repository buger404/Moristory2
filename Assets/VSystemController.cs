using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VSystemController : MonoBehaviour
{
    private Text RoleName;
    private GameObject Role;
    private List<AbilityBarController> HP = new List<AbilityBarController>(),MP = new List<AbilityBarController>();
    private List<WalkingAnimate> Walker = new List<WalkingAnimate>();
    private List<Image> focus = new List<Image>();
    private List<string> Roles = new List<string>();
    private struct Ability{
        public float HP;
        public float MP;
    }
    // Start is called before the first frame update
    Ability GetAbility(string tar){
        if(tar == "世原·安诺") return new Ability{HP = 100,MP = 100};
        if(tar == "兮·御冯") return new Ability{HP = 90,MP = 100};
        if(tar == "埃斯·洛里") return new Ability{HP = 80,MP = 90};
        return new Ability();
    }
    void Awake()
    {
        string code = "世原·安诺,100,90;兮·御冯,90,50;埃斯·洛里,40,20;";
        string[] role = code.Split(';');
        for(int i = 0;i < role.Length - 1;i++){
            string[] abi = role[i].Split(',');
            HP.Add(this.transform.Find("HPBar" + i).GetComponent<AbilityBarController>());
            MP.Add(this.transform.Find("MPBar" + i).GetComponent<AbilityBarController>());
            Ability ab = GetAbility(abi[0]);
            HP[i].Value = float.Parse(abi[1]); MP[i].Value = float.Parse(abi[2]);
            HP[i].Max = ab.HP; MP[i].Max = ab.MP;
            focus.Add(this.transform.Find("rolefocus" + i).GetComponent<Image>());
            focus[i].GetComponent<RoleClicker>().VController = this;
            Walker.Add(this.transform.Find("WakerAni" + i).GetComponent<WalkingAnimate>());
            Roles.Add(abi[0]);
            Walker[i].character = Roles[i];
        }
        HP.Add(this.transform.Find("HPBar").GetComponent<AbilityBarController>());
        MP.Add(this.transform.Find("MPBar").GetComponent<AbilityBarController>());
        RoleName = this.transform.Find("RoleName").GetComponent<Text>();
        Role = this.transform.Find("Role").gameObject;
        SwitchRole(0);
    }
    void SetFace(){
        Vector3 v = Role.GetComponent<RectTransform>().sizeDelta;
        Sprite s = Resources.Load<Sprite>("Characters/" + RoleName.text);
        Role.GetComponent<Image>().sprite = s;
        Role.GetComponent<RectTransform>().sizeDelta = new Vector3(s.rect.width / s.rect.height * v.y,v.y,v.z);
    }
    public void SwitchRole(int Index){
        foreach(Image f in focus)
            f.color = new Color(1.0f,1.0f,1.0f,1.0f / 255.0f);

        focus[Index].color = new Color(1.0f,1.0f,1.0f,1.0f);
        RoleName.text = Roles[Index];
        HP[3].Value = HP[Index].Value;HP[3].Max = HP[Index].Max;
        MP[3].Value = MP[Index].Value;MP[3].Max = MP[Index].Max;
        SetFace();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
