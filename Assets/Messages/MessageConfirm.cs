using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MessageConfirm : MonoBehaviour
{
    private Animator animator;
    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetMouseButtonUp(0)){
            GraphicRaycaster gr = this.transform.parent.GetComponent<GraphicRaycaster>();
            PointerEventData data = new PointerEventData(EventSystem.current);
            data.pressPosition = Input.mousePosition;
            data.position = Input.mousePosition;
            List<RaycastResult> results = new List<RaycastResult>();
            gr.Raycast(data, results);

            foreach(RaycastResult rr in results){
                if(rr.gameObject.name == "Confirm"){
                    OnMouseUp();
                }
            }
        }
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
