using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{
    private bool Disabled = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Disabled){return;}
        if(Input.GetMouseButtonUp(0)){
            EndMsg();
        }
    }
    public void EndMsg(){
        Animator ani = this.GetComponent<Animator>();
        ani.SetFloat("Speed",-1);
        GameConfig.IsMsgProcess = true;
        ani.Play("DialogShow",0, 1);
        Disabled = true;
    }
    public void CreateMsg(string Name,string Content){
        Animator ani = this.GetComponent<Animator>();
        this.gameObject.SetActive(true);
        GameConfig.IsMsgProcess = false;
        ani.SetFloat("Speed",1);
        //ani.GetCurrentAnimatorStateInfo(0).normalizedTime
        ani.Play("DialogShow",0,0);
        Text N = GameObject.Find("NameZone").GetComponent<Text>();
        N.text = Name;
        Text C = GameObject.Find("Dialog").GetComponent<Text>();
        C.text = Content;
        Disabled = true;
    }
    void NotifyShowed()
    {
        GameConfig.IsMsgProcess = true;
        Disabled = false;
    }
    void HideMySelf()
    {
        Disabled = false;
        if(GameConfig.IsMsgProcess){
            this.gameObject.SetActive(false);
            GameConfig.IsMsgProcess = false;
        }
        if(GameConfig.IsBlocking){
            GameConfig.IsBlocking = false;
            GameConfig.BlockEvent.RunCode();
        }
    }
}
