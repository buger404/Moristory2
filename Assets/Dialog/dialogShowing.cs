using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dialogShowing : MonoBehaviour {
	public static bool NextWait = false;
	private static string[] ConverCode;
	public static int button = 0,ConverLine = 0;
	public static string dtext = "",buff = "",backS = "";
	private static int playpos = 0,fontsize;
	private static string rname,rface;
	private static float basex = 0,basey = 0;
	private static Text mytext,mycaption;
	private static Image Role;
	private static Animator RoleAni;
	private static RectTransform RoleRect;
	private static Transform flower;
	public static void PlayText(string text,bool resets){
		//使指示灯消失
		button = 0;
		flower.gameObject.SetActive(false);
		if(!resets){return;}
		mytext.text = "";flower.localPosition = new Vector2(basex,basey);
		dtext = text;buff = "";
		playpos = 0;
	}
	public static void ChangeFace(string name,string face){
		Sprite sp = (Sprite)Resources.Load("Role/" + name + "_" + face, typeof(Sprite));
		Role.sprite = sp;
		int w,h;
		h = (int)(Screen.height * 0.55);
		w = (int)(sp.rect.width * (h / sp.rect.height));
		RoleRect.sizeDelta = new Vector2(w,h);
	}
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
		mytext = this.gameObject.GetComponent<Text>();
		fontsize = mytext.fontSize;
		mycaption = GameObject.Find("Speaker").GetComponent<Text>();
		flower = GameObject.Find("Texting").transform;
		Role = GameObject.Find("Role").GetComponent<Image>();
		RoleRect = GameObject.Find("Role").GetComponent<RectTransform>();
		RoleAni = GameObject.Find("Role").GetComponent<Animator>();
		basex = flower.localPosition.x;
		basey = flower.localPosition.y;
	}
	public static void StartConversation(string name,string part,string OnBack){
		backS = OnBack;
		LoadConversation(name,part);
		FadeControlPad.FadeToScene("Dialog");
	}
	public static void LoadConversation(string name,string part){
		TextAsset t = (TextAsset)Resources.Load(@"Conversations\" + name);  
		string s = t.text;
		string[] parts = s.Split(new[] {@">>>####"},System.StringSplitOptions.None);
		string[] temp;
		ConverLine = 0;
		ConverCode = ("雪郎：\n错误，无法找到指定的对话脚本，请联系Error 404。").Split('\n');
		for(int i = 0;i < parts.Length;i++){
			temp = parts[i].Split(new[] {@"####<<<"},System.StringSplitOptions.None);
			if(temp[0]==part){
				ConverCode = temp[1].Split('\n');
			}
		}
	}
	public static void CarryConversation(){
		//快进
		if(playpos < dtext.Length){
			for(int i = playpos;i < dtext.Length;i++){
				mytext.text += dtext[i];
				buff += dtext[i];
			}
			playpos = dtext.Length;
			return;
		}

		bool neverConver = false;
		do{
			loophead:
			string cmd;
			if(ConverLine >= ConverCode.Length){
				if(backS==""){
					return;
				}else{
					FadeControlPad.FadeToScene(backS);
				}
			}
			cmd = ConverCode[ConverLine];
			ConverLine++;
			if(cmd.Length <= 1){goto loophead;}
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
			if(cmd[cmd.Length-2]=='）'){
				string face = cmd.Split('（')[1].Split('）')[0];
				PlayText("",rname,face);
				goto loophead;
			}
			if(cmd[cmd.Length-2]=='*'){
				string[] param = cmd.Split('*')[0].Split('/');
				switch(param[0]){
					case("go"):
						FadeControlPad.FadeToScene(param[1]);
						return;
					default:
						return;
				}
			}
			if(cmd != ""){
				PlayText(cmd);
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
	void PutText(){
		if(ConverLine == 0){
			CarryConversation();
		}
		if(dtext == ""){return;}
		if(playpos >= dtext.Length)
		{
			if(NextWait){
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
		mytext.text += dtext[playpos];
		buff += dtext[playpos];
		playpos++;
	}
	void Update () {
		
	}
}
