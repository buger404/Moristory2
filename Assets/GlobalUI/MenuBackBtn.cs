using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBackBtn : MonoBehaviour {
	void OnMouseUp() {
		if(GameVars.MenuBackScene!=""){
			FadeControlPad.FadeToScene(GameVars.MenuBackScene);
			GameVars.MenuBackScene = "";
		}else{
			FadeControlPad.FadeToScene("StartupLOGO");
		}
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
