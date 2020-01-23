using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMLighting : MonoBehaviour {
	AudioSource me;
	GameObject Back;
	// Use this for initialization
	void Start () {
		me = this.GetComponent<AudioSource>();
		Back = GameObject.Find("Back0");
	}
	
	// Update is called once per frame
	void Update () {
		float[] data = new float[128];
		float datatotal = 0;
		me.GetSpectrumData(data, 0, FFTWindow.BlackmanHarris) ;
		for (int i = 0; i < data.Length; i++){
			datatotal += Mathf.Clamp(data[i]*(50+i*i*0.5f),0,100);
		}
		datatotal = Mathf.Clamp(datatotal,200,1000);
		SpriteRenderer sr = Back.GetComponent<SpriteRenderer>();
		sr.color = new Color(sr.color.r,sr.color.g,sr.color.b,(float)(datatotal / 1000 * 0.05 + 0.95));
	}
}
