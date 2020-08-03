using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerController : MonoBehaviour
{
    public string character;            //使用的人物的行走图名称
    public int DelayStep;
    private SpriteRenderer s;           //控制对象图片
    private Sprite[] walker;            //行走图图片集
    private void Awake() {
        s = this.gameObject.GetComponent<SpriteRenderer>();
    }
    public void Confirm(){
        walker = Resources.LoadAll<Sprite>("Walkers/" + character);
        this.transform.position = GameConfig.StateFlow[GameConfig.StatePos].pos;
        s.sprite = walker[GameConfig.StateFlow[GameConfig.StatePos].FPS];
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int Pointer = GameConfig.StatePos - DelayStep;
        if(Pointer < 0) Pointer += 1000;
        if(GameConfig.StateFlow[Pointer].pos != this.transform.position){
            this.transform.position = GameConfig.StateFlow[Pointer].pos;
            s.sprite = walker[GameConfig.StateFlow[Pointer].FPS];
        }
    }
}
