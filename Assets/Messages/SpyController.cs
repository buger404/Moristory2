using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpyController : MonoBehaviour
{
    private string TextBuff = "";
    private int BuffIndex = 0;
    private float BuffDelta = 0;
    private Text ContentText;
    private bool IsDisabled = false;
    private void Show(){
        if(this.GetComponent<Animator>().GetFloat("Speed") == 1){
            Debug.Log("start spying.");
            IsDisabled = false;
        }
    }
    private void Hide(){
        if(this.GetComponent<Animator>().GetFloat("Speed") == -1.5){
            Debug.Log("end spying.");
            GameConfig.IsBlocking = false;
            GameConfig.BlockEvent.Run();
            IsDisabled = false;
            this.gameObject.SetActive(false);
        }
    }
    public void CreateSpy(string content){
        this.gameObject.SetActive(true);
        this.GetComponent<Animator>().Play("SpyShow",0,0f);
        this.GetComponent<Animator>().SetFloat("Speed",1);
        ContentText.text = "";TextBuff = content;
        BuffDelta = 0;BuffIndex = 0;
        IsDisabled = true;
    }
    private void Awake() {
        ContentText = GameObject.Find("SpyText").GetComponent<Text>();
    }

    private void Update() {
        if(BuffIndex < TextBuff.Length){
            BuffDelta += Time.deltaTime;
            if(BuffDelta >= 0.07){
                BuffDelta = 0;
                ContentText.text += TextBuff[BuffIndex];
                BuffIndex++;
            }
        }
        if(IsDisabled){return;}
        if(Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.Z)){
            if(BuffIndex < TextBuff.Length){
                BuffDelta = 0;ContentText.text = TextBuff;
                BuffIndex = TextBuff.Length;
                return;
            }
            Debug.Log("time to end spying.");
            IsDisabled = true;
            this.GetComponent<Animator>().Play("SpyShow",0,1f);
            this.GetComponent<Animator>().SetFloat("Speed",-1.5f);
        }
    }
}
