using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 代码状态
/// </summary>
public class RunState{
    public RunState next; //if环的下一层
    public RunState prev; //if环的上一层
    public int line; //当前层的代码执行到的行号
    public XmlNodeList code; //代码集合
    public XmlNode root; //父代码
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
    /// <summary>
    /// 储存代码xml源文本
    /// </summary>
    public TextAsset Code;
    /// <summary>
    /// 当前对话的角色名
    /// </summary>
    private string Character = "";
    /// <summary>
    /// 储存代码xml
    /// </summary>
    /// <returns></returns>
    private XmlDocument xml = new XmlDocument();
    /// <summary>
    /// 是否有别的behaviour正在执行
    /// </summary>
    private bool IsRunning = false;
    /// <summary>
    /// 代码运行状态
    /// </summary>
    /// <returns></returns>
    private RunState rs = new RunState();

    //挂起游戏部分操作
    public void HangUp(){
        //挂起游戏的部分操作
        GameConfig.IsBlocking = true;
        GameConfig.BlockEvent = this;
        Debug.Log("Behave hang up");
    }

    //每行代码的执行
    public void Run(){
        //--代码执行准备-----------------------------------------------------
        RunState r = rs, pr = rs;
        XmlElement c = (XmlElement)r.code[r.line];
        //深入当前执行的最内层，取得当前执行的代码
        while(r != null){
            recode:
            //当前层代码执行完毕
            if(r.line >= r.code.Count){
                if(r.prev == null) {
                    //如果是最上层代码执行完毕，说明该behaviour执行结束
                    //关闭当前开启的对话框
                    if(GameConfig.IsMsgProcess){GameConfig.ActiveDialog.EndMsg();}
                    IsRunning = false;
                    return;
                }
                //删除该层
                r.prev.NextLine();
                r.prev.next = null;
                r = r.prev;
                goto recode;
            }
            c = (XmlElement)r.code[r.line];
            //如果该代码属于该层
            if(c.ParentNode != r.root){
                r.NextLine();goto recode;
            }
            pr = r;r = r.next;
        }
        bool MsgProccessed = false;     //是否有对话框执行的操作
        bool ExitMark = false;          //是否有立即退出behaviour的操作
        bool BlockCode = false;         //是否暂停运行下一行代码
        //临时变量
        RPGEvent rpg = this.gameObject.GetComponent<RPGEvent>();
        GameObject go = this.gameObject;



        //--代码本体的关键操作-----------------------------------------------
        //套if层到当前层的next
        if(c.Name == "if"){
            //判断时候满足if条件
            if((bool)Storage.Condition(c.GetAttribute("condition"))){
                Debug.Log("enter to a new if");
                pr.Append(c.GetElementsByTagName("*"),c);
                Run();return;
            }
        }
        //退出behaviour
        if(c.Name == "exit"){
            ExitMark = true;
        }


        //--变量操作--------------------------------------------------------
        //游戏变量操作
        if(c.Name == "var"){
            Storage.Set(c.GetAttribute("tar"),Storage.Condition(c.InnerText).ToString());
        }
        

        //--对话框操作------------------------------------------------------
        //设置当前对话角色
        if(c.Name == "chara"){
            Character = c.InnerText;
        }
        //发起对话框的对话
        if(c.Name == "s"){
            GameConfig.ActiveDialog.CreateMsg(Character,Storage.Deepin(c.InnerText));
            //阻止代码继续运行直到对话框被用户关闭
            BlockCode = true;MsgProccessed = true;
        }
        //调查文本
        if(c.Name == "a"){
            GameConfig.ActiveSpy.CreateSpy(Storage.Deepin(c.InnerText));
            BlockCode = true;
        }


        //--人物操作-------------------------------------------------------
        //设置行走任务
        if(c.Name == "walk"){
            if(c.GetAttribute("tar") != "")
            rpg = GameObject.Find(c.GetAttribute("tar")).GetComponent<RPGEvent>();
            if(c.GetAttribute("x") != "")
            rpg.XTask = float.Parse(Storage.Condition(c.GetAttribute("x")).ToString());
            if(c.GetAttribute("y") != "")
            rpg.YTask = float.Parse(Storage.Condition(c.GetAttribute("y")).ToString());
        }
        //开始根据设定的任务行走
        if(c.Name == "walktask"){
            //等待走完
            BlockCode = true;GameConfig.WalkingTask = true;
        }
        //和指定目标对齐坐标
        if(c.Name == "fix"){
            Vector3 otrans = this.gameObject.transform.position;
            Transform ttrans = GameObject.Find(c.GetAttribute("src")).transform;
            if(c.GetAttribute("tar").IndexOf("x") >= 0){
                ttrans.position = new Vector3(otrans.x,ttrans.position.y,ttrans.position.z);
            }
            if(c.GetAttribute("tar").IndexOf("y") >= 0){
                ttrans.position = new Vector3(ttrans.position.x,otrans.y,ttrans.position.z);
            }
        }
        //显示或隐藏目标
        if(c.Name == "visible"){
            if(c.GetAttribute("tar") != null)  go = GameObject.Find(c.GetAttribute("tar"));
            go.SetActive((bool)Storage.Condition(c.InnerText));
        }


        //--地图操作-------------------------------------------------------
        //昼夜开关
        if(c.Name == "time"){
            if(c.InnerText == "day") GameConfig.DayNight = 0;
            if(c.InnerText == "night") GameConfig.DayNight = 1;
        }
        //传送
        if(c.Name == "tp"){
            GameConfig.TpSpot = c.GetAttribute("spot");
            if(GameConfig.TpSpot == null) GameConfig.TpSpot = "";
            GameConfig.TpDir = int.Parse(Storage.Condition(c.GetAttribute("face")).ToString());
            Switcher.SwitchTo(c.InnerText);
        }


        //--交易操作-------------------------------------------------------
        //显示售货页面
        if(c.Name == "shop"){
            PaySystem.CreateShop(c.InnerText,c.GetAttribute("cut"),c.GetAttribute("owner"),c.GetAttribute("post"));
            BlockCode = true;
        }


        //--存档操作-------------------------------------------------------
        //存入存档
        if(c.Name == "save"){
            PlayerPrefs.SetString("map",GameConfig.CurrentMapName);
            PlayerPrefs.SetString("scene",SceneManager.GetActiveScene().name);
            PlayerPrefs.SetString("scenecode",GameConfig.RecordSceneToString());
        }


        //--代码执行收尾----------------------------------------------------
        //若对话框没有被使用，则当作废弃关闭
        if(!MsgProccessed){
            if(GameConfig.IsMsgProcess){GameConfig.ActiveDialog.EndMsg();}
        }
        //需要立即退出behaviour
        if(ExitMark){
            IsRunning = false; return;
        }
        //跳转到下一行
        pr.NextLine();
        //是否挂起
        if(!BlockCode){Run();}else{HangUp();}
    }

    //Behaviour装载准备
    public void Begin(string behave){
        //如果当前有别的behaviour在执行，则退出
        if(IsRunning) return;
        
        RPGEvent Player = GameConfig.Controller.GetComponent<RPGEvent>();
        Vector3 Pp = Player.transform.position;
        Vector3 Px = Player.transform.localScale;
        float XD = 0,YD = 0;
        if(Player.Direction == 0) {YD = Pp.y;Pp.y -= (Px.y/2);}
        if(Player.Direction == 1) {XD = -Pp.x;Pp.x -= (Px.x/2);}
        if(Player.Direction == 2) {XD = Pp.x;Pp.x += (Px.x/2);}
        if(Player.Direction == 3) {YD = -Pp.y;Pp.y += (Px.y/2);}

        RaycastHit2D hit = Physics2D.Raycast(new Vector2(Pp.x,Pp.y),new Vector2(XD,YD));
        GameConfig.FACE = hit.collider == this.GetComponent<BoxCollider2D>() ? 1 : 0;

        XmlNodeList behaviours = xml.GetElementsByTagName("behaviour");
        foreach(XmlElement behaviour in behaviours){
            //判断是否是请求的behaviour，并判断是否满足其要求的条件
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
        //装载代码xml
        xml.LoadXml(Code.text);
        //调用auto behaviour
        Begin("auto");
    }

    private void Update() {
        //如果最后一个进入碰撞的RPG是自己
        if(GameConfig.LastEvent == this){
            //支持的触发输入
            if(Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.Z)){
                Begin("spy"); //调用spy behaviour
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        //当玩家进入碰撞区域时，开始允许玩家触发spy behaviour
        GameConfig.LastEvent = this;
    }
    private void OnTriggerExit2D(Collider2D other) {
        GameConfig.LastEvent = null;
    }
    private void OnTriggerStay2D(Collider2D other) {
        //接触即调用touch behaviour
        Begin("touch");
    }
}