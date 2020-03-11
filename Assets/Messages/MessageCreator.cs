using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class MessageCreator : MonoBehaviour
{
    public static bool MsgUsing = false;
    public static void CreateMsg(string Title,string Content){
        if(MsgUsing) {return;}
        MsgUsing = true;
        GameObject msgfab = (GameObject)Resources.Load("Prefabs\\Messagebox");
        GameObject msgbox = Instantiate(msgfab,new Vector3(0,0,0),Quaternion.identity);
		msgbox.SetActive(true);
        msgbox.transform.position = new Vector3(
            Camera.main.transform.position.x,
            Camera.main.transform.position.y,
            msgbox.transform.position.z
        ) ;
        Transform[] father = msgbox.GetComponentsInChildren<Transform>();

        foreach (var child in father)
		{
			if(child.name == "Title"){
				child.GetComponent<Text>().text = Title;
			}
			if(child.name == "Content"){
				child.GetComponent<Text>().text = Content;
			}
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
