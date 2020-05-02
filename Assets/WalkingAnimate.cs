using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingAnimate : MonoBehaviour
{
    private Sprite[] walker;
    public float fps = 24;
    public int Direction = 0;
    private SpriteRenderer s;
    public string character = "";
    // Start is called before the first frame update
    void Start()
    {
        s = this.gameObject.GetComponent<SpriteRenderer>();
        if(character == ""){
            int i = Random.Range(0,6);
            if(i == 0) character="世原·安诺";
            if(i == 1) character="兮·御冯";
            if(i == 2) character="布莱·昴斯";
            if(i == 3) character="域零·占也";
            if(i == 4) character="谭娜·霓悦";
            if(i == 5) character="雪郎·梦亭";
        }
        walker = Resources.LoadAll<Sprite>("Walkers/" + character);
    }

    // Update is called once per frame
    void Update()
    {
        int index = (int)(Time.time * fps) % 3;
        s.sprite = walker[index + Direction * 3];
    }
}
