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
    private GameObject Sakura;
    private string lastchara = "";
    private bool WaitForNew = false;
    private string TextBuff = "";
    private int BuffIndex = 0;
    private float BuffDelta = 0;
    private Text ContentText;
    private bool Disabled2 = false;

    // Start is called before the first frame update
    void Awake()
    {
        this.gameObject.SetActive(true);
        NameZone = GameObject.Find("NameZone");
        NameB = GameObject.Find("NameB");
        Character = GameObject.Find("CharacterD");
        Sakura = GameObject.Find("Sakura");
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
        if(Disabled2){return;}
        if(Disabled || WaitForNew){return;}

        if(Sakura.activeSelf != !GameConfig.MsgLock) Sakura.SetActive(!GameConfig.MsgLock);

        if(Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.Z)){
            if(GameConfig.MsgLock) return;
            if(BuffIndex < TextBuff.Length){
                BuffDelta = 0;ContentText.text = TextBuff;
                BuffIndex = TextBuff.Length;
                SoundPlayer.Play("Cursor1");
                return;
            }
            SoundPlayer.Play("Cursor1");
            WaitForNew = true;
            GameConfig.IsBlocking = false;
            Debug.Log("Auto next");
            GameConfig.BlockEvent.Run();
        }
    }
    public void EndMsg(){
        WaitForNew = false;Disabled2 = true;
        Animator ani = this.GetComponent<Animator>();
        GameConfig.IsMsgProcess = true;
        HideMySelf();
        Disabled = true;lastchara = "";
    }
    public void CreateMsg(string Name,string Content){
        bool ShowChara = (Name != "旁白");
        this.gameObject.SetActive(true);
        Disabled2 = false;

        if(Name != lastchara){
            Sprite chara = Resources.Load<Sprite>("Characters\\" + Name);
            if(chara != null){
                Image csp = Character.GetComponent<Image>();
                RectTransform csr = Character.GetComponent<RectTransform>();
                float ow = csr.sizeDelta.x;
                csp.sprite = chara;
                csr.sizeDelta = new Vector2 (ow,(chara.rect.height / chara.rect.width) * ow);
            }else{
                ShowChara = false;
            }
            lastchara = Name;
            NameZone.SetActive((Name != "旁白"));
            NameB.SetActive((Name != "旁白"));
            Character.SetActive(ShowChara);
        }

        Animator ani = this.GetComponent<Animator>();

        if(!WaitForNew){
            Disabled = true;
            GameConfig.IsMsgProcess = false;
            NotifyShowed();
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
