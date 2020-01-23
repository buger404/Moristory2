using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GlobalEvents : MonoBehaviour {
	private static bool IsLoad = false;
	public AudioClip[] BGMS; 
	public void PlayBGM(string name){
		for(int i = 0;i < BGMS.Length;i++){
			if(BGMS[i].name == name){
				GameVars.BGM.Stop();
				GameVars.BGM.clip = BGMS[i];
				GameVars.BGM.Play();
			}
		}
	}
	void Awake(){
		if(GameVars.BGM != null){Destroy(this.gameObject);return;}
		DontDestroyOnLoad(this.gameObject);
		GameVars.BGM = this.gameObject.GetComponent<AudioSource>();
		GameVars.BGMController = this.gameObject.GetComponent<GlobalEvents>();
		PlayBGM("Lifetheory - Sakura");
	}
	void Start () {

	}
	// Update is called once per frame
	void Update () {
		bool load = false;
		double d = Screen.width,su = 1.5,su2 = 1.9;
		d /= Screen.height;

		if(d < su || d > su2){
			load = true;
		}
		if(IsLoad != load){
			if(load){
				SceneManager.LoadScene("SizeDialog",LoadSceneMode.Additive);
			}else{
				SceneManager.UnloadSceneAsync("SizeDialog");
			}
			IsLoad = load;
		}
	}
}
