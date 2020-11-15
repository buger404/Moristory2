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
        if(BGM.time >= 0.5) ly = "此刻 樱花翩然飞舞 树叶缓缓飘落地面";
        if(BGM.time >= 6.3) ly = "这让我忆起当初";
        if(BGM.time >= 9.0) ly = "那时你牵着我的手";
        if(BGM.time >= 13.5) ly = "我们始终如一坚守的承诺";
        if(BGM.time >= 16.5) ly = "让我更加坚定你我";
        if(BGM.time >= 20.0) ly = "抱着我就像其他恋人一般";
        if(BGM.time >= 26.0) ly = "此刻 樱花凄零飘落";
        if(BGM.time >= 31.0) ly = "";
        if(lyt.text != ly) lyt.text = ly;
        if(Input.GetMouseButtonUp(0)){
            if(PlayerPrefs.GetString("Watched") == ""){
                Debug.Log("You have not watched it!");
                if(BGM.time >= 31.0 || BGM.isPlaying == false){
                    Debug.Log("Welcome~~~");
                    PlayerPrefs.SetString("Watched","√");
                    goto SkipOP;
                    
                }
            }else{
                Debug.Log("Skip directly:" + BGM.isPlaying);
                goto SkipOP;
            }
        }
        return;
        SkipOP:
        if(PlayerPrefs.GetString("global") != ""){
            DataCenter.Load();
            Switcher.SwitchTo("Saving");
        }else{
            Switcher.SwitchTo("NameTime");
        }

    }
}
