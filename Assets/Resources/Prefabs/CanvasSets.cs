using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasSets : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        Canvas canvas = this.gameObject.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.worldCamera = Camera.main;
        
        this.transform.position = new Vector3(0,0,90);
        Debug.Log("Canvas has successfully connected with the camera .");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
