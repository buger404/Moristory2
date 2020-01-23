using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDoor : MonoBehaviour {
	public string TargetScene = "";
	public bool IsMenuDoor = false;
	public bool DoNotFade = false;
	public KeyCode HotKey = KeyCode.Print;
	// Use this for initialization
	void OnMouseUp() {
		if(GameVars.FadeState == 1){return;}
		if(GameVars.ActiveScene != TargetScene){
			if(IsMenuDoor){
				GameVars.MenuBackScene = SceneManager.GetActiveScene().name;
			}
			if(DoNotFade){
				FadeControlPad.ToScene(TargetScene);
			}else{
				FadeControlPad.FadeToScene(TargetScene);
			}
		}
	}
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(HotKey != KeyCode.Print){
			if(Input.GetKeyUp(HotKey)){
				OnMouseUp();
			}
		}
	}
}
