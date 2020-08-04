using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TouchButton : MonoBehaviour
{
    private Image s;           //控制对象图片
    public Sprite NormalSprite;
    public Sprite HoverSprite;
    private void Awake() {
        s = this.gameObject.GetComponent<Image>();
    }
    // Update is called once per frame
    void Update()
    {
        GraphicRaycaster gr = this.transform.parent.GetComponent<GraphicRaycaster>();
        PointerEventData data = new PointerEventData(EventSystem.current);
        data.pressPosition = Input.mousePosition;
        data.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        gr.Raycast(data, results);

        bool Hover = false;

        foreach(RaycastResult rr in results){
            if(rr.gameObject.Equals(this.gameObject)){
                Hover = true;
            }
        }

        s.sprite = Hover ? HoverSprite : NormalSprite;
    }
}
