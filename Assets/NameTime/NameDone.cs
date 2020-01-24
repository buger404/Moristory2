using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameDone : MonoBehaviour {
	void OnMouseUp(){
		string oname = GameVars.PlayerName.Split(' ')[0] + GameVars.PlayerName.Split(' ')[1];
		string name = GameVars.PlayerName;
		GameVars.PlayerName = "";
		if((name.IndexOf("世原") >= 0) || (name.IndexOf("安渃") >= 0)){
			dialogShowing.StartConversation("NameTime","世原","NamingTime");
			return;
		}
		if((name.IndexOf("布莱") >= 0) || (name.IndexOf("卯斯") >= 0)){
			dialogShowing.StartConversation("NameTime","布莱","NamingTime");
			return;
		}
		if((name.IndexOf("布莱") >= 0) && (name.IndexOf("梦亭") >= 0)){
			dialogShowing.StartConversation("NameTime","布莱雪郎","NamingTime");
			return;
		}
		if((name.IndexOf("雪郎") >= 0) && (name.IndexOf("卯斯") >= 0)){
			dialogShowing.StartConversation("NameTime","布莱雪郎","NamingTime");
			return;
		}
		if((name.IndexOf("雪郎") >= 0) || (name.IndexOf("梦亭") >= 0)){
			dialogShowing.StartConversation("NameTime","雪郎","NamingTime");
			return;
		}
		if((oname.IndexOf("娜亭") >= 0)){
			dialogShowing.StartConversation("NameTime","娜亭","NamingTime");
			return;
		}
		if((oname.IndexOf("奥伦娜") >= 0)){
			dialogShowing.StartConversation("NameTime","奥伦娜","NamingTime");
			return;
		}
		if((oname.IndexOf("楚斯卡德") >= 0)){
			dialogShowing.StartConversation("NameTime","楚斯卡德","NamingTime");
			return;
		}
		if((name.IndexOf("域零") >= 0) || (name.IndexOf("占也") >= 0)){
			dialogShowing.StartConversation("NameTime","域零","NamingTime");
			return;
		}
		if((name.IndexOf("艾斯") >= 0) || (name.IndexOf("罗里") >= 0)){
			dialogShowing.StartConversation("NameTime","艾斯","NamingTime");
			return;
		}
		if((name.IndexOf("沽梦") >= 0) || (name.IndexOf("卡德") >= 0)){
			dialogShowing.StartConversation("NameTime","沽梦","NamingTime");
			return;
		}
		if((name.IndexOf("志琰") >= 0) || (name.IndexOf("琰琰") >= 0)){
			dialogShowing.StartConversation("NameTime","琰","NamingTime");
			return;
		}
		if((name.IndexOf("御冯") >= 0)){
			dialogShowing.StartConversation("NameTime","兮","NamingTime");
			return;
		}
		if((name[0] == name[1]) || (name[2] == name[3])){
			dialogShowing.StartConversation("NameTime","谢谢2","NamingTime");
			return;
		}
		GameVars.PlayerName = name;
		dialogShowing.StartConversation("NameTime","谢谢","NamingTime");
		return;
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
