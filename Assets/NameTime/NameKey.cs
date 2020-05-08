using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameKey : MonoBehaviour
{
    private static Text Name;
    private static Text Key;
    private Animator ani;
    public int Type = 0;
    private void Awake() {
        ani = GameObject.Find("SnowCanvas").GetComponent<Animator>();
        ani.SetFloat("Speed",0.0f);
    }
    void Start()
    {
        Name = GameObject.Find("Name").GetComponent<Text>();
        Key = GameObject.Find("NameKey").GetComponent<Text>();
        GetKey();//FixPos();
    }
    void NameComplete(){
        if(Type != 5){return;}
        PlayerPrefs.SetString("name",Name.text);
        Debug.Log($"player created: {Name.text}");
        //销毁旧的存档
        PlayerPrefs.DeleteAll();
        //PlayerPrefs.SetString("scene","");
        Switcher.SwitchTo("DocumentSpy");
    }
    private void OnMouseUp() {
        if(Type == 0){
            GetKey();
        }
        if(Type == 1 && Name.text.Length > 0){
            Name.text = Name.text.Remove(Name.text.Length - 1);
            //KeyR.position.Set(KeyR.position.x - KeyR.sizeDelta.x, KeyR.position.y,KeyR.position.z);
            //FixPos();
        }
        if(Type == 2){
            Debug.Log("push!");
            SoundPlayer.Play("Cursor1");
            Name.text = Name.text + Key.text;
            //KeyR.position.Set(KeyR.position.x + KeyR.sizeDelta.x, KeyR.position.y,KeyR.position.z);
            GetKey();//FixPos();
            if(Name.text.Length == 4){
                if(Name.text.IndexOf("黑嘴") >= 0){Application.Quit();}
                if(Name.text.IndexOf("兮兮") >= 0){Application.Quit();}
                if(Name.text.IndexOf("雪狼") >= 0){Application.Quit();}
                if(Name.text.IndexOf("久悠") >= 0){Application.Quit();}
                if(Name.text.IndexOf("志琰") >= 0){
                    GameObject.Find("Praise").GetComponent<Text>().text = "...行吧，就叫这个名字:("; 
                }
                SoundPlayer.Play("Cursor_2");
                ani.Play("NameDone",0,0);
                ani.SetFloat("Speed",1.0f);
            }
        }
    }
    void GetKey(){
        string[] t = new string[]
        {"冰","棍","兮","黑","嘴","布","莱","昴","斯","塞","克","赛",
        "沽","御","冯","风","思","源","世","原","安","渃","浮","橘",
        "弗","莱","夫","久","悠","九","游","志","雪","郎","狼","琰",
        "梦","素","艾","桑","鱼","亭"};
        Key.text = t[Random.Range(0,t.Length)];
    }
    void FixPos(){
        //  ResetR.position = new Vector3(
        //    KeyR.position.x + KeyR.sizeDelta.x / 2 - ResetR.localScale.x / 2,
        //    ResetR.position.y,
        //    ResetR.position.z);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
