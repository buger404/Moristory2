using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class RunState{
    public RunState next;
    public RunState prev;
    public int line;
    public XmlNodeList code;
    public XmlNode root;
    public RunState(){
        prev = null;next = null;line = 0;
    }
    public void Append(XmlNodeList xml,XmlNode dad){
        next = new RunState{code = xml, prev = this, root = dad};
    }
    public void NextLine(){
        line++;
    }
}
public class RPG : MonoBehaviour
{
    public TextAsset Code;
    private string Character = "";
    private XmlDocument xml = new XmlDocument();
    private bool IsRunning = false;
    private RunState rs = new RunState();
    public void HangUp(){
        GameConfig.IsBlocking = true;
        GameConfig.BlockEvent = this;
        Debug.Log("Behave hang up");
    }
    public void Run(){
        RunState r = rs, pr = rs;
        XmlElement c = (XmlElement)r.code[r.line];
        while(r != null){
            recode:
            if(r.line >= r.code.Count){
                if(r.prev == null) {
                    if(GameConfig.IsMsgProcess){GameConfig.ActiveDialog.EndMsg();}
                    IsRunning = false;
                    return;
                }
                r.prev.NextLine();
                r.prev.next = null;
                r = r.prev;
                goto recode;
            }
            c = (XmlElement)r.code[r.line];
            if(c.ParentNode != r.root){
                r.NextLine();goto recode;
            }
            pr = r;r = r.next;
        }
        
        bool MsgProccessed = false;
        bool ExitMark = false;
        bool BlockCode = false;

        if(c.Name == "var"){
            Storage.Set(c.GetAttribute("tar"),Storage.Condition(c.InnerText).ToString());
        }
        if(c.Name == "chara"){
            Character = c.InnerText;
        }
        if(c.Name == "if"){
            if((bool)Storage.Condition(c.GetAttribute("condition"))){
                Debug.Log("enter to a new if");
                pr.Append(c.GetElementsByTagName("*"),c);
                Run();return;
            }
        }
        if(c.Name == "s"){
            GameConfig.ActiveDialog.CreateMsg(Character,Storage.Deepin(c.InnerText));
            BlockCode = true;MsgProccessed = true;
        }

        if(!MsgProccessed){
            if(GameConfig.IsMsgProcess){GameConfig.ActiveDialog.EndMsg();}
        }
        if(ExitMark){
            IsRunning = false; return;
        }

        pr.NextLine();
        if(!BlockCode){Run();}
        if(BlockCode){HangUp();}
    }
    public void Begin(string behave){
        if(IsRunning) return;

        XmlNodeList behaviours = xml.GetElementsByTagName("behaviour");
        foreach(XmlElement behaviour in behaviours){
            if(behaviour.GetAttribute("action") == behave &&
               (bool)Storage.Condition(behaviour.GetAttribute("condition"))
               )
            {
                IsRunning = true;
                rs = new RunState{code = behaviour.GetElementsByTagName("*"), root = behaviour};
                Run();
                return;
            }
        }
    }

    private void Start() {
        xml.LoadXml(Code.text);
        Begin("auto");
    }

    private void Update() {
        if(GameConfig.LastEvent == this){
            if(Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.Z)){
                Begin("spy");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        GameConfig.LastEvent = this;
    }
    private void OnTriggerExit2D(Collider2D other) {
        GameConfig.LastEvent = null;
    }
    private void OnTriggerStay2D(Collider2D other) {
        Begin("touch");
    }
}