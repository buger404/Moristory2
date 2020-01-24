using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundChanger : MonoBehaviour {
	public static string TargetBackground = "";
	private static int CMode = 0;
	private static Image Back;
	private static RectTransform BackRect;
	private static float OW,OH;
	private static Animator BackAni;
	public static void ShowUI(bool show){
		if(show){
			BackAni.speed = 1;
		}else{
			BackAni.speed = 0.45f;
		}
		GameObject.Find("Speaker").SetActive(show);
		GameObject.Find("Dialogn").SetActive(show);
		GameObject.Find("BackBtn").SetActive(show);
		GameObject.Find("Role").SetActive(show);
	}
	public static void ChangeBack(string name,int mode){
		TargetBackground = name;CMode = mode;
		BackAni.Play("Background_Fading",0,0f);
	}
	void Change(){
		if(TargetBackground == ""){return;}
		Sprite sp = (Sprite)Resources.Load("Background/" + TargetBackground, typeof(Sprite));
		Back.sprite = sp;
		int w,h;
		w = (int)(OW);
		h = (int)(sp.rect.height * (w / sp.rect.width));
		BackRect.sizeDelta = new Vector2(w,h);
		if(CMode == 0){
			BackRect.localPosition = new Vector3(0,0,0);
		}else{
			BackRect.localPosition = new Vector3(0,-h / 2 + 10,0);
		}
		
	}
	// Use this for initialization
	void Awake () {
		Back = this.gameObject.GetComponent<Image>();
		BackRect = this.gameObject.GetComponent<RectTransform>();
		BackAni = this.gameObject.GetComponent<Animator>();
		OW = BackRect.sizeDelta.x;
		OH = BackRect.sizeDelta.y;
	}
	
	// Update is called once per frame
	void Update () {
		if(CMode == 1){
			if(BackRect.localPosition.y <= BackRect.sizeDelta.y / 2 - 20 - OH / 2){
				BackRect.localPosition = new Vector3(BackRect.localPosition.x,BackRect.localPosition.y + 2f,BackRect.localPosition.z);
			}
		}
	}
}
