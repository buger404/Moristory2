using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameJuding : MonoBehaviour {
	GameObject OKBtn,OKBtnText;
	// Use this for initialization
	void Awake () {
		OKBtn = GameObject.Find("selectbtn");
		OKBtnText = GameObject.Find("selectBtnText");
	}
	
	// Update is called once per frame
	void Update () {
		bool show = (GameVars.PlayerName.Length == 5);
		OKBtn.SetActive(show);
		OKBtnText.SetActive(show);
		if(GameVars.PlayerName.Length == 3 && GameVars.PlayerName[2] != ' '){
			GameVars.PlayerName = GameVars.PlayerName[0].ToString() + GameVars.PlayerName[1].ToString() + ' ' + GameVars.PlayerName[2].ToString();
			GameObject.Find("YourName").GetComponent<Text>().text = GameVars.PlayerName;
		}
		if((GameVars.PlayerName == "兮兮") || (GameVars.PlayerName == "黑嘴") || (GameVars.PlayerName == "世") || (GameVars.PlayerName == "枯梦") || (GameVars.PlayerName == "冰棍") || (GameVars.PlayerName == "雪狼")){
			GameVars.PlayerName = "";
			if(GameVars.PlayerName == "兮兮"){Application.Quit();return;}
			dialogShowing.StartConversationRIGHTNOW("Tale","传说","StartupLOGO");
			return;
		}
	}
}
