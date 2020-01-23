using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemStateLoader : MonoBehaviour {
	private Text MoneyTips;
	// Use this for initialization
	void Start () {
		MoneyTips = GameObject.Find("MoneyTips").GetComponent<Text>();
		MoneyTips.text = "持有金额    " + PlayerPrefs.GetInt("Money",0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
