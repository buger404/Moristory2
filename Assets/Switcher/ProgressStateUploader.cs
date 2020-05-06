using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressStateUploader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float pro = 0;
        if (GameConfig.ProcessingScene!=null)
        {
            pro = GameConfig.ProcessingScene.progress;
        }
        if(pro > 0.9) pro = 1;
        this.GetComponent<Text>().text = (int)(pro * 1000) / 10 + "%";
    }
}
