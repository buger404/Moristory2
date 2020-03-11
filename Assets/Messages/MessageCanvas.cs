using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageCanvas :MonoBehaviour
{
    private MessageConfirm Controller;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void MessageOperation(){
        Controller.MessageOperation();
    }

    private void Awake() {
        Controller = GameObject.Find("Confirm").GetComponent<MessageConfirm>();
    }
}
