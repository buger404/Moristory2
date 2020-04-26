using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpyController : MonoBehaviour
{
    private Text ContentText;
    private bool IsDisabled = false;
    private void Show(){
        if(this.GetComponent<Animator>().GetFloat("Speed") == 1){
            IsDisabled = false;
        }
    }
    private void Hide(){
        if(this.GetComponent<Animator>().GetFloat("Speed") == -1.5){
            GameConfig.IsBlocking = false;
            IsDisabled = false;
            this.gameObject.SetActive(false);
            Debug.Log("Spyer disabled!");
            GameConfig.BlockEvent.Run();
        }
    }
    public void CreateSpy(string content){
        this.gameObject.SetActive(true);
        this.GetComponent<Animator>().Play("SpyShow",0,0f);
        this.GetComponent<Animator>().SetFloat("Speed",1);
        ContentText.text = content;
        IsDisabled = true;
    }
    private void Awake() {
        ContentText = GameObject.Find("SpyText").GetComponent<Text>();
    }

    private void Update() {
        if(IsDisabled){return;}
        if(Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.Z)){
            IsDisabled = true;
            this.GetComponent<Animator>().Play("SpyShow",0,1f);
            this.GetComponent<Animator>().SetFloat("Speed",-1.5f);
        }
    }
}
