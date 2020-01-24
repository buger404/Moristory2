using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogComplete : MonoBehaviour {
	void OnMouseUp(){
		if(dialogShowing.TaleMode){
			dialogShowing.TaleMode = false;
			GameVars.BGMController.PlayBGM("Lifetheory - Sakura");
			FadeControlPad.FadeToScene("StartupLOGO");
			return;
		}
		dialogShowing.CarryConversation();
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyUp(KeyCode.Z) || Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp(KeyCode.Space)){
			OnMouseUp();
		}
	}
}
