using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartBtn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseUp() {
        InputField t = GameObject.Find("NameBox").GetComponent<InputField>();
        t.text = t.text.Replace(" ","").Replace("　","");
        if(t.text.Length != 4){
            MessageCreator.CreateMsg("名字输入错误","请输入4个字的名字哦，不能任性。");
        }else{
            DataCenter.Put("name",t.text);
            this.transform.parent.gameObject.GetComponent<Animator>().speed = 1;
        }
    }
}
