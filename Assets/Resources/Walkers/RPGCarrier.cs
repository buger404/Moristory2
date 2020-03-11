using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGCarrier : MonoBehaviour
{
    public struct codecmd{ 
        public string tag;
        public List<string> param;
    }
    public struct codearea{
        public string tag;
        public List<string> param;
        public List<codecmd> cmd;
    }
    public struct codescript{
        public string behavior;
        public List<codearea> area;
    }
    public struct codefile{
        public List<codescript> behaviors;
    }
    private codefile code;
    private int behave;
    private int areai;
    private int cmdi;
    private bool IsRunning;
    public TextAsset Script;
    void StartBehavior(string behavior){
        if(IsRunning){return;}
        int b = code.behaviors.FindIndex(m => m.behavior == behavior);
        if(b == -1){return;}
        IsRunning = true;
        areai=0;cmdi=0;behave = b;
        RunCode();
    }
    void RunCode(){
        if(areai > code.behaviors[behave].area.Count){IsRunning = false;return;}
        codearea ca = code.behaviors[behave].area[areai];
        if(cmdi == 0){
            //TODO: Area params
            switch(ca.tag){
                case("Action"):
                    switch(ca.param[1]){
                        case("++"):
                            PlayerPrefs.SetFloat(ca.param[0],PlayerPrefs.GetFloat(ca.param[0])+1);
                            break;
                    }
                    break;
                case("Chance"):
                    float data = PlayerPrefs.GetFloat(ca.param[0]);
                    if(ca.param.Count == 2){
                        if(data != System.Convert.ToDouble(ca.param[1])){goto NextArea;}
                    }else{
                        if(ca.param[2] == "∞"){
                            if(data < System.Convert.ToDouble(ca.param[1])){goto NextArea;}
                        }else{
                            if(ca.param[1] == "∞"){
                                if(data > System.Convert.ToDouble(ca.param[1])){goto NextArea;}
                            }else{
                                if(data < System.Convert.ToDouble(ca.param[1]) ||
                                   data > System.Convert.ToDouble(ca.param[2])){goto NextArea;}
                            }
                        }
                    }
                    break;
            }
            cmdi = 1;
        }
        //Next Area
        if(cmdi > ca.cmd.Count){goto NextArea;}
        codecmd cc = code.behaviors[behave].area[areai].cmd[cmdi];
        bool BlockCode = false;
        //TODO: Cmd params
        switch(cc.tag){
            case("say"):
                MessageCreator.CreateMsg("先代替",cc.param[0]);
                BlockCode = true;
                break;
            case("day"):
                GameConfig.DayNight = System.Convert.ToInt16(cc.param[0]);
                Debug.Log("Set:" + System.Convert.ToInt16(cc.param[0]));
                break;
            case("go"):
                Switcher.SwitchTo(cc.param[0]);
                break;
            default:
                MessageCreator.CreateMsg("先代替",cc.tag + "说");
                BlockCode = true;
                break;
        }
        cmdi++;
        if(!BlockCode){RunCode();}
        return;

        NextArea:
        cmdi=0;areai++;RunCode();
    }
    void LoadScript(){
        code = new codefile();
        string[] s = Script.text.Split(new string[]{"\r\n"},System.StringSplitOptions.None);
        code.behaviors = new List<codescript>();
        string co;int coi = 0;
        for(int j = 0;j < s.Length;j++){
            co = s[j].Trim();
            if(co == ""){continue;}
            if(co.StartsWith("#Behavior:")){
                codescript cs = new codescript{behavior = co.Split(':')[1]};
                cs.area = new List<codearea>();
                codearea ca = new codearea();
                ca.tag = "";ca.cmd = new List<codecmd>();ca.param = new List<string>();
                cs.area.Add(ca);
                code.behaviors.Add(cs);
                continue;
            }
            if(co.StartsWith("~")){
                if(coi == 1){
                    co = co.Replace("~","#");
                    coi = 0;
                }else{
                    codearea ca = new codearea();
                    string[] p = co.Split(':');
                    ca.tag = p[0].Remove(0,1);
                    ca.cmd = new List<codecmd>();
                    ca.param = new List<string>();
                    for(int i = 1;i < p.Length;i++)
                        ca.param.Add(p[i]);
                    code.behaviors[code.behaviors.Count - 1].area.Add(ca);
                    coi = 1;continue;
                }
            }
        recmd:
            if(co.StartsWith("#")){
                codecmd cc = new codecmd();
                string[] p = co.Split(':');
                cc.tag = p[0].Remove(0,1);
                cc.param = new List<string>();
                for(int i = 1;i < p.Length;i++)
                    cc.param.Add(p[i]);

                code.behaviors[code.behaviors.Count - 1]
                    .area[code.behaviors[code.behaviors.Count - 1].area.Count - 1]
                    .cmd.Add(cc);
                continue;
            }
            co = "#say:" + co;goto recmd;
        }
    }
    private void OnCollisionStay2D(Collision2D other) {
        if(Input.GetMouseButtonUp(0)){
            StartBehavior("Spy");
        }else{
            StartBehavior("Touch");
        }
    }
    private void Awake() {
        LoadScript();
    }
    void Start() {
        StartBehavior("Auto");
    }
    void Update()
    {

    }
}
