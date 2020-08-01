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
        if(BGM.time >= 0.5) ly = "樱树之花终将坠地";
        if(BGM.time >= 6.3) ly = "拾起回忆碎片";
        if(BGM.time >= 9.0) ly = "同行的时光模糊撕裂";
        if(BGM.time >= 13.5) ly = "昔日信誓旦旦的诺言";
        if(BGM.time >= 16.5) ly = "鼓舞见证明日的黎明";
        if(BGM.time >= 20.0) ly = "为何无法紧握我的手";
        if(BGM.time >= 21.5) ly = "仅仅像";
        if(BGM.time >= 22.5) ly = "普通的情侣一般";
        if(BGM.time >= 26.0) ly = "樱树之花";
        if(BGM.time >= 28.0) ly = "肆虐地舞落";
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
