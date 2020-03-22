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
    private bool WaitForNew = false;
    private string TextBuff = "";
    private int BuffIndex = 0;
    private float BuffDelta = 0;
    private Text ContentText;

    // Start is called before the first frame update
    void Awake()
    {
        this.gameObject.SetActive(true);
        NameZone = GameObject.Find("NameZone");
        NameB = GameObject.Find("NameB");
        Character = GameObject.Find("CharacterD");
        ContentText = GameObject.Find("Dialog").GetComponent<Text>();
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(BuffIndex < TextBuff.Length){
            BuffDelta += Time.deltaTime;
            if(BuffDelta >= 0.07){
                BuffDelta = 0;
                ContentText.text += TextBuff[BuffIndex];
                BuffIndex++;
            }
        }
        if(Disabled){return;}
        if(Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.Space)){
            if(BuffIndex < TextBuff.Length){
                BuffDelta = 0;ContentText.text = TextBuff;
                BuffIndex = TextBuff.Length;
                return;
            }
            WaitForNew = true;
            GameConfig.IsBlocking = false;
            GameConfig.BlockEvent.RunCode();
        }
    }
    public void EndMsg(){
        WaitForNew = false;
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

        if(!WaitForNew){
            Disabled = true;
            GameConfig.IsMsgProcess = false;
            ani.SetFloat("Speed",2);
            ani.Play("DialogShow",0, 0);
        }

        WaitForNew = false;

        try{
            ContentText.text = "";
            TextBuff = Content;BuffIndex = 0;
            Text N = NameZone.GetComponent<Text>();
            N.text = Name;
        }catch{

        }
        
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
