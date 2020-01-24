using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Moristory II 背景处理类
//背景资源：Assets\Resources\Background\
//存在问题：无
public class BackgroundChanger : MonoBehaviour {
	/// <summary>
	/// 目标背景
	/// </summary>
	public static string TargetBackground = "";
	/// <summary>
	/// 切换模式
	/// </summary>
	private static int CMode = 0;
	/// <summary>
	/// 指向背景精灵
	/// </summary>
	private static Image Back;
	/// <summary>
	/// 背景的大小矩形
	/// </summary>
	private static RectTransform BackRect;
	/// <summary>
	/// 背景原始的长宽
	/// </summary>
	private static float OW,OH;
	/// <summary>
	/// 控制背景动画
	/// </summary>
	private static Animator BackAni;
	/// <summary>
	/// 显示对话UI
	/// </summary>
	/// <param name="show">是否显示</param>
	public static void ShowUI(bool show){
		if(show){
			BackAni.speed = 1;
		}else{
			//如果是传说模式就减慢UI速度
			BackAni.speed = 0.45f;
		}
		GameObject t;
		t = GameObject.Find("Speaker");
		if(t!=null){t.SetActive(show);}
		t = GameObject.Find("Dialogn");
		if(t!=null){t.SetActive(show);}
		t = GameObject.Find("BackBtn");
		if(t!=null){t.SetActive(show);}
		t = GameObject.Find("Role");
		if(t!=null){t.SetActive(show);}
	}
	/// <summary>
	/// 改变背景
	/// </summary>
	/// <param name="name">资源名</param>
	/// <param name="mode">模式：0为普通，1为长图</param>
	public static void ChangeBack(string name,int mode){
		TargetBackground = name;CMode = mode;
		BackAni.Play("Background_Fading",0,0f);
	}
	/// <summary>
	/// 由动画事件触发的更换背景
	/// </summary>
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
		//如果是长图就慢慢看吧
		if(CMode == 1){
			//直到图片尾部
			if(BackRect.localPosition.y <= BackRect.sizeDelta.y / 2 - 20 - OH / 2){
				BackRect.localPosition = new Vector3(BackRect.localPosition.x,BackRect.localPosition.y + 2f,BackRect.localPosition.z);
			}
		}
	}
}
