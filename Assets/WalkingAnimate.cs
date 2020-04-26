using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingAnimate : MonoBehaviour
{
    private Sprite[] walker;
    public float fps = 24;
    public int Direction = 0;
    private SpriteRenderer s;
    // Start is called before the first frame update
    void Awake()
    {
        s = this.gameObject.GetComponent<SpriteRenderer>();
        string character = "";
        int i = Random.Range(0,6);
        if(i == 0) character="世";
        if(i == 1) character="兮";
        if(i == 2) character="布莱";
        if(i == 3) character="域零";
        if(i == 4) character="谭娜";
        if(i == 5) character="雪郎";

        walker = Resources.LoadAll<Sprite>("Walkers/" + character);
    }

    // Update is called once per frame
    void Update()
    {
        int index = (int)(Time.time * fps) % 3;
        s.sprite = walker[index + Direction * 3];
    }
}
