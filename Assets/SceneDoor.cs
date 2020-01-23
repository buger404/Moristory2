using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDoor : MonoBehaviour {
	public string TargetScene = "";
	public bool IsMenuDoor = false;
	// Use this for initialization
	void OnMouseUp() {
		if(SceneManager.GetActiveScene().name != TargetScene){
			if(IsMenuDoor){
				GameVars.MenuBackScene = SceneManager.GetActiveScene().name;
			}
			FadeControlPad.FadeToScene(TargetScene);
		}
	}
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
