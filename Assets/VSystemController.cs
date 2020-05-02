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
            Walker.Add(this.transform.Find("WakerAni" + i).GetComponent<WalkingAnimate>());
            Roles.Add(abi[0]);
            Walker[i].character = Roles[i];
        }
        HP.Add(this.transform.Find("HPBar").GetComponent<AbilityBarController>());
        MP.Add(this.transform.Find("MPBar").GetComponent<AbilityBarController>());
        HP[3].Value = HP[0].Value;HP[3].Max = HP[0].Max;
        MP[3].Value = MP[0].Value;MP[3].Max = MP[0].Max;
        RoleName = this.transform.Find("RoleName").GetComponent<Text>();
        Role = this.transform.Find("Role").gameObject;
        RoleName.text = Roles[0];
        SetFace();
    }
    void SetFace(){
        Vector3 v = Role.GetComponent<RectTransform>().sizeDelta;
        Sprite s = Resources.Load<Sprite>("Characters/" + RoleName.text);
        Role.GetComponent<Image>().sprite = s;
        Role.GetComponent<RectTransform>().sizeDelta = new Vector3(s.rect.width / s.rect.height * v.y,v.y,v.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
