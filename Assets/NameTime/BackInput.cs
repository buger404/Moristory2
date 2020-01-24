using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackInput : MonoBehaviour {
	void OnMouseUp(){
		if(GameVars.PlayerName.Length > 0){
			string n = "";
			for(int i = 0;i < GameVars.PlayerName.Length - 1;i++){
				n += GameVars.PlayerName[i];
			}
			GameVars.PlayerName = n;
			GameObject.Find("YourName").GetComponent<Text>().text = GameVars.PlayerName;
		}
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
