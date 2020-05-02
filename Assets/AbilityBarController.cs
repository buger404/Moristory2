using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityBarController : MonoBehaviour
{
    private GameObject BaseBar;
    private GameObject OverBar;
    public float Max = 100;
    public float Value = 100;
    public Color BarColor = Color.cyan;
    void Awake()
    {
        BaseBar = this.transform.GetChild(0).gameObject;
        BaseBar.SetActive(true);
        OverBar = this.transform.GetChild(1).gameObject;
        OverBar.SetActive(true);
        BaseBar.GetComponent<Image>().color = BarColor;
        this.GetComponent<Image>().color = new Color(0,0,0,0);
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            Vector2 rt,s;
            s = this.GetComponent<RectTransform>().sizeDelta;
            rt = BaseBar.GetComponent<RectTransform>().sizeDelta;
            if(rt != s) BaseBar.GetComponent<RectTransform>().sizeDelta = s;
            s = new Vector2(s.x * (1.0f - Value / Max),s.y);
            rt = OverBar.GetComponent<RectTransform>().sizeDelta;
            if(rt != s) OverBar.GetComponent<RectTransform>().sizeDelta = s;
            float w = this.GetComponent<RectTransform>().sizeDelta.x;
            float x = w - s.x - (w - s.x) / 2;
            Vector3 p = OverBar.GetComponent<RectTransform>().localPosition;
            if(p.x != x) 
            {   
                OverBar.GetComponent<RectTransform>().localPosition = new Vector3(x,0,p.z);
            }
        }
        catch
        {

        }
    }
}
