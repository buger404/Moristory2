using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{
    private bool Disabled = false;
    private GameObject NameB;
    private GameObject NameZone;
    private GameObject Character;
    // Start is called before the first frame update
    void Awake()
    {
        this.gameObject.SetActive(true);
        NameZone = GameObject.Find("NameZone");
        NameB = GameObject.Find("NameB");
        Character = GameObject.Find("CharacterD");
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Disabled){return;}
        if(Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.Space)){
            GameConfig.IsBlocking = false;
            GameConfig.BlockEvent.RunCode();
        }
    }
    public void EndMsg(){
        Animator ani = this.GetComponent<Animator>();
        ani.SetFloat("Speed",-4);
        GameConfig.IsMsgProcess = true;
        ani.Play("DialogShow",0, 1);
        Disabled = true;
    }
    public void CreateMsg(string Name,string Content){
        bool ShowChara = (Name != "旁白");
        this.gameObject.SetActive(true);

        NameZone.SetActive(ShowChara);
        NameB.SetActive(ShowChara);
        Character.SetActive(ShowChara);

        Animator ani = this.GetComponent<Animator>();
        GameConfig.IsMsgProcess = false;
        ani.SetFloat("Speed",2);
        //ani.GetCurrentAnimatorStateInfo(0).normalizedTime
        ani.Play("DialogShow",0, 0);
        try{
            Text C = GameObject.Find("Dialog").GetComponent<Text>();
            C.text = Content;
            Text N = NameZone.GetComponent<Text>();
            N.text = Name;
        }catch{

        }
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
            if(GameConfig.IsBlocking){
                Debug.Log("Hold on next ...");
            }
        }

    }
}
