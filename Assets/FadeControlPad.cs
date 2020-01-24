using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeControlPad : MonoBehaviour {
	/// <summary>
	/// 立刻转移到目标场景
	/// </summary>
	/// <param name="name">场景名称</param>
	public static void ToScene(string name) {
		if(GameVars.FadeState == 1){return;}
		GameVars.FadeState = 1;
		FadeController.CScene(name);
		GameVars.FadeState = 0;
    }
	/// <summary>
	/// 渐变过渡到目标场景
	/// </summary>
	/// <param name="name">场景名称</param>
	public static void FadeToScene(string name) {
		if(GameVars.FadeState == 1){return;}
        GameVars.FadeState = 1;
		GameObject FadeScene = (GameObject)Resources.Load("FadeScene");
        GameObject go = Instantiate(FadeScene,new Vector3(0,0,0),Quaternion.identity);
		go.SetActive(true);

        Transform[] father = go.GetComponentsInChildren<Transform>();

        foreach (var child in father)
		{
			if(child.name == "Image"){
				GameObject ge = child.gameObject;

				FadeController fd = ge.GetComponent<FadeController>();
				fd.TargetScene = name;

				RectTransform r = ge.GetComponent<RectTransform>();
				r.sizeDelta = new Vector2(Screen.width,Screen.height);
			}
        }
    }
}
