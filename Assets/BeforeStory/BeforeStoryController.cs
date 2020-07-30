using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeforeStoryController : MonoBehaviour
{
    public GameObject lyc;
    private AudioSource BGM;
    private Text lyt;
    public void StartBGM(){
        BGM.Play();
    }
    private void Awake() {
        lyt = lyc.GetComponent<Text>();
        BGM = GameObject.Find("BGM").GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        string ly = "";
        if(BGM.time >= 0.5) ly = "さくら ひらひら the leaves will flutter to the ground";
        if(BGM.time >= 6.3) ly = "It reminds me of the days";
        if(BGM.time >= 9.0) ly = "when you were here to hold my hand";
        if(BGM.time >= 13.5) ly = "And the promises we've made";
        if(BGM.time >= 16.5) ly = "along still keep me hanging on";
        if(BGM.time >= 20.0) ly = "Hold me now";
        if(BGM.time >= 21.5) ly = "just like";
        if(BGM.time >= 22.5) ly = "other lovers";
        if(BGM.time >= 26.0) ly = "さくら";
        if(BGM.time >= 28.0) ly = "舞い散る";
        if(BGM.time >= 31.0) ly = "";
        if(lyt.text != ly) lyt.text = ly;
        if(Input.GetMouseButtonUp(0)){
            if(PlayerPrefs.GetString("Watched") == ""){
                Debug.Log("You have not watched it!");
                if(BGM.time >= 31.0){
                    Debug.Log("Welcome~~~");
                    PlayerPrefs.SetString("Watched","√");
                    goto SkipOP;
                }
            }else{
                Debug.Log("Skip directly.");
                goto SkipOP;
            }
        }
        return;
        SkipOP:
        if(PlayerPrefs.GetString("scenecode") != ""){
            Switcher.SwitchTo("Saving");
        }else{
            Switcher.SwitchTo("NameTime");
        }

    }
}
