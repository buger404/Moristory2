using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageConfirm : MonoBehaviour
{
    private Animator animator;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void MessageOperation(){
        if(animator.GetFloat("Speed") == -3.0f){
            Destroy(this.transform.parent.gameObject);
            MessageCreator.MsgUsing = false;
            Debug.Log("Destoried Messagebox!");
        }
    }

    private void Awake() {
        animator = this.transform.parent.gameObject.GetComponent<Animator>();
    }
    private void OnMouseUp() {
        if(animator.speed != -1){
            animator.Play("Message_Show",0,1);
            animator.SetFloat("Speed", -3.0f);
        }
    }
}
