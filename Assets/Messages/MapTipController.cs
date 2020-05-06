using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapTipController : MonoBehaviour
{
    public string MapName = "";
    // Start is called before the first frame update
    void Start()
    {
        GameConfig.CurrentMapName = MapName;
        GameObject.Find("MapContent").GetComponent<Text>().text = MapName;
        Destroy(this.gameObject,3.0f);
        //GameObject.Find("MapContent").GetComponent<Animator>().Play("MapMsg");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
