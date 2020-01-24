using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Moristory II 对话脚本处理类
//脚本资源：Assets\Resources\Conversations\
//存在问题：莫名的报错（不影响）
public class dialogShowing : MonoBehaviour {
	/// <summary>
	/// 决定下次Update时脚本是否自动运行一行。
	/// </summary>
	public static bool NextWait = false;
	/// <summary>
	/// 对话脚本代码组
	/// </summary>
	private static string[] ConverCode;
	/// <summary>
	/// 目前执行到的脚本行数
	/// </summary>
	public static int button = 0,ConverLine = 0;
	/// <summary>
	/// 对话时的文本逐渐出现在屏幕上的效果需要的缓冲变量
	/// </summary>
	public static string dtext = "",buff = "",backS = "";
	/// <summary>
	/// 目前文字缓冲区的缓冲位置
	/// </summary>
	private static int playpos = 0;
	/// <summary>
	/// 对话时文本的字体大小，用于计算指示玩家对话已经显示完全的指示灯的坐标计算
	/// </summary>
	private static int fontsize;
	/// <summary>
	/// 当前对话目标角色名称，表情（默认值：常态）
	/// </summary>
	private static string rname,rface;
	/// <summary>
	/// 现在是否处于开头的传说讲述状态
	/// </summary>
	public static bool TaleMode = false;
	/// <summary>
	/// 用于在传说状态下减慢UI速度的临时变量
	/// </summary>
	private static int TaleTick = 0,TaleTick2 = 0;
	/// <summary>
	/// 指示灯的基础坐标
	/// </summary>
	private static float basex = 0,basey = 0;
	/// <summary>
	/// 指向对话框UI和角色名称UI
	/// </summary>
	private static Text mytext,mycaption;
	/// <summary>
	/// 指向人物精灵的图片
	/// </summary>
	private static Image Role;
	/// <summary>
	/// 角色大小矩形
	/// </summary>
	private static RectTransform RoleRect;
	/// <summary>
	/// 控制角色的动画（弹起）
	/// </summary>
	private static Animator RoleAni;
	/// <summary>
	/// 控制指示灯
	/// </summary>
	private static Transform flower;
	/// <summary>
	/// 父Canvas的大小矩形
	/// </summary>
	private static RectTransform PadR;
	/// <summary>
	/// 更换角色表情
	/// </summary>
	/// <param name="name">目标角色名</param>
	/// <param name="face">目标表情</param>
	public static void ChangeFace(string name,string face){
		Sprite sp = (Sprite)Resources.Load("Role/" + name + "_" + face, typeof(Sprite));
		Role.sprite = sp;
		int w,h;
		h = (int)(PadR.sizeDelta.y * 0.85);
		w = (int)(sp.rect.width * (h / sp.rect.height));
		RoleRect.sizeDelta = new Vector2(w,h);
	}
	/// <summary>
	/// 播放文本到对话框，但清空对话框的文本并重置角色
	/// </summary>
	/// <param name="text">目标文本</param>
	/// <param name="name">角色名称</param>
	/// <param name="face">角色表情</param>
	public static void PlayText(string text,string name,string face){
		ChangeFace(name,face);
		//使指示灯消失
		button = 0;
		flower.gameObject.SetActive(false);
		mytext.text = "";mycaption.text = name;
		flower.localPosition = new Vector2(basex,basey);
		dtext = text;buff = "";
		playpos = 0;
		if((rname != name) || (rface != face)){
			RoleAni.Play("RoleJump",0,0f);
		}
		rname = name;rface = face;
	}
	/// <summary>
	/// 仅播放文本到对话框
	/// </summary>
	/// <param name="text">目标文本</param>
	public static void PlayText(string text){
		//使指示灯消失
		button = 0;
		flower.gameObject.SetActive(false);
		if(mytext.text != ""){
			mytext.text += "\n";
			int w = GetFontlen("t");
			flower.localPosition = new Vector2(basex,flower.localPosition.y - w * 2);
		}
		dtext = text;buff = "";
		playpos = 0;
	}
	/// <summary>
	/// 取得字符串的宽度
	/// </summary>
	/// <param name="str">目标字符串</param>
	/// <returns></returns>
	private static int GetFontlen(string str)
	{
		int len = 0;
		Font font;
		font = Font.CreateDynamicFontFromOSFont("GAME", fontsize);
		font.RequestCharactersInTexture(str);
		for (int i = 0; i < str.Length; i++)
		{
			CharacterInfo ch;
			font.GetCharacterInfo(str[i], out ch);
			len += ch.advance;
		}
		return len;
	}
	void Awake() {
		//取得一些列对象
		mytext = this.gameObject.GetComponent<Text>();
		fontsize = mytext.fontSize;
		mycaption = GameObject.Find("Speaker").GetComponent<Text>();
		flower = GameObject.Find("Texting").transform;
		Role = GameObject.Find("Role").GetComponent<Image>();
		RoleRect = GameObject.Find("Role").GetComponent<RectTransform>();
		RoleAni = GameObject.Find("Role").GetComponent<Animator>();
		PadR = GameObject.Find("Canvas").GetComponent<RectTransform>();
		basex = flower.localPosition.x;
		basey = flower.localPosition.y;
	}
	/// <summary>
	/// 立即启动对话，不需要渐变动画。
	/// </summary>
	/// <param name="name">对话资源名称</param>
	/// <param name="part">对话部分标题</param>
	/// <param name="OnBack">对话完毕时转回的场景名称</param>
	public static void StartConversationRIGHTNOW(string name,string part,string OnBack){
		backS = OnBack;
		LoadConversation(name,part);
		FadeControlPad.ToScene("Dialog");
	}
	/// <summary>
	/// 启动对话。
	/// </summary>
	/// <param name="name">对话资源名称</param>
	/// <param name="part">对话部分标题</param>
	/// <param name="OnBack">对话完毕时转回的场景名称</param>
	public static void StartConversation(string name,string part,string OnBack){
		backS = OnBack;
		LoadConversation(name,part);
		FadeControlPad.FadeToScene("Dialog");
	}
	/// <summary>
	/// 加载对话资源文件
	/// </summary>
	/// <param name="name">对话资源名称</param>
	/// <param name="part">对话部分标题</param>
	public static void LoadConversation(string name,string part){
		TextAsset t = (TextAsset)Resources.Load(@"Conversations\" + name);  
		string s = t.text;
		string[] parts = s.Split(new[] {@">>>####"},System.StringSplitOptions.None);
		string[] temp;
		ConverLine = 0;
		ConverCode = ("雪郎：\n错误，指定的对话脚本丢失或异常，请联系Error 404。").Split('\n');
		//找对话
		for(int i = 0;i < parts.Length;i++){
			temp = parts[i].Split(new[] {@"####<<<"},System.StringSplitOptions.None);
			if(temp[0]==part){
				ConverCode = temp[1].Split('\n');
				break;
			}
		}
	}
	/// <summary>
	/// 执行下一行对话脚本代码
	/// </summary>
	public static void CarryConversation(){
		//如果玩家不耐烦提前点击了，则快进
		if(playpos < dtext.Length){
			//把缓冲区剩余的文字丢进去
			for(int i = playpos;i < dtext.Length;i++){
				mytext.text += dtext[i];
				buff += dtext[i];
			}
			playpos = dtext.Length;
			return;
		}

		bool neverConver = false;
		do{
			loophead://循环头
			string cmd;
			//如果当前行数已经高于总行数，是时候结束了
			if(ConverLine >= ConverCode.Length){
				if(backS==""){
					return;
				}else{
					FadeControlPad.FadeToScene(backS);
				}
				return;
			}
			//取脚本
			cmd = ConverCode[ConverLine];
			Debug.Log("Processing ... " + cmd);
			ConverLine++;
			//如果脚本有点空，就跳过这行吧
			if(cmd.Length <= 1){goto loophead;}
			//换音乐：格式♪xx♪
			if(cmd[0]=='♪'){
				GameVars.BGMController.PlayBGM(cmd.Split('♪')[1]);
				goto loophead;
			}
			//更换人物：格式xx：
			if(cmd[cmd.Length-2]=='：'){
				if(neverConver==false){
					PlayText("",cmd.Split('：')[0],"常态");
					neverConver = true;
					goto loophead;
				}else{
					ConverLine--;NextWait = false;
					return;
				}
			}
			//更换表情：格式（xx）
			if(cmd[cmd.Length-2]=='）'){
				string face = cmd.Split('（')[1].Split('）')[0];
				PlayText("",rname,face);
				goto loophead;
			}
			//各种指令：格式xx/xx*
			if(cmd[cmd.Length-2]=='*'){
				string[] param = cmd.Split('*')[0].Split('/');
				switch(param[0]){
					//跳转场景
					case("go"):
						FadeControlPad.FadeToScene(param[1]);
						break;
					//切换背景
					case("bg"):
						BackgroundChanger.ChangeBack(param[1],0);
						break;
					//切换长背景
					case("longbg"):
						BackgroundChanger.ChangeBack(param[1],1);
						break;
					//控制UI显示（传说模式）
					case("ui"):
						if(param[1] == "ok"){
							TaleMode = false;
							BackgroundChanger.ShowUI(true);
						}else{
							TaleMode = true;TaleTick2 = 0;
							BackgroundChanger.ShowUI(false);
						}
						break;
					//未知指令
					default:
						break;
				}
				//继续处理
				goto loophead;
			}
			//当做文字处理喽！
			if(cmd != ""){
				PlayText(cmd);
				//如果可以继续自动下一行的话，就继续
				if(ConverLine<ConverCode.Length){
					cmd = ConverCode[ConverLine];
					if((cmd[cmd.Length-2]!='：') && (cmd[cmd.Length-2]!='）')){
						NextWait = true;
					}
				}
				return;
			}
		}while(true);
	}
	/// <summary>
	/// 放置缓冲区中的文本
	/// </summary>
	void PutText(){
		//如果处于传说模式，则减慢UI速度
		if(TaleMode){
			if(TaleTick < 5){
				TaleTick++;return;
			}
			TaleTick=0;
		}
		//如果还没有开始执行脚本，则开始执行
		if(ConverLine == 0){
			CarryConversation();
		}
		//缓冲区里没有东西，跳出。
		if(dtext == ""){return;}
		//缓冲区里的文字丢完了
		if(playpos >= dtext.Length)
		{
			if(TaleMode){
				//如果是在传说模式，则留一些时间给玩家阅读文本
				if(TaleTick2 >= 10){
					TaleTick2 = 0;CarryConversation();
					return;
				}else{
					TaleTick2++;
				}
			}
			if(NextWait){
				//如果需要进一步执行，那就执行吧
				NextWait = false;
				CarryConversation();
			}
			if(playpos == dtext.Length){
				//还原指示灯
				int w = GetFontlen(dtext);
				flower.localPosition = new Vector2(basex + w / 2 ,flower.localPosition.y);
				flower.gameObject.SetActive(true);
				playpos++;
			}
			return;
		}
		//将缓冲区里的东西丢入对话框
		mytext.text += dtext[playpos];
		buff += dtext[playpos];
		playpos++;
	}
	void Update () {
		
	}
}
