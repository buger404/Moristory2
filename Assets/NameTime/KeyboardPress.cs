using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class KeyboardPress : MonoBehaviour {
	private static Text show;
	private Text key;
	private RectTransform myrect;
	private int TipTip = 120;
	private float ox,oy;
	// Use this for initialization
	void OnMouseUp() {
		if((GameVars.PlayerName.Length >= 5) || (key.text == "×")){
			return;
		}
		GameVars.PlayerName += key.text;
		show.text = GameVars.PlayerName;
		TipTip = 120;Update();
	}
	void Start() {
		GameVars.PlayerName = "";
		show = GameObject.Find("YourName").GetComponent<Text>();
		key = this.gameObject.GetComponent<Text>();
		myrect = this.gameObject.GetComponent<RectTransform>();
		ox = myrect.position.x;oy = myrect.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		float XP = Random.Range(-0.03f,0.03f);
		float YP = Random.Range(-0.03f,0.03f);
		myrect.position = new Vector3(ox + XP,oy + YP,0);
		string[] Tip = {"兮","黑","嘴","布","莱","矢","原","雪","狼","郎","斯","特","梦","枯","沽","卯","御","风","零","域","玉","亭","卡","楚","德","奥","伦","娜","羽","丘","木","拉","克","依","源","远","琰","志","蔚","冰","艾","罗","里","任","保","曲","冯","兰","嫒","棍"};
		if(TipTip >= 120){
			key.text = Tip[Random.Range(0,Tip.Length)];
			TipTip = 0;
		}
		TipTip++;
		if(GameVars.PlayerName.Length >= 5){
			key.text = "×";
		}
	}
}
