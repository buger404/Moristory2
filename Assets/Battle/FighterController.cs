using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterController : MonoBehaviour
{
    public enum BattleState{
        Normal = 0,
        Attack = 1,
        Miss = 2,
        Prepare = 3,
        Attack2 = 4,
        SmileWin = 5,
        Magic = 6,
        Attack3 = 7,
        BadHurt = 8,
        ExPrepare = 9,
        Attack4 = 10,
        ExBadHurt = 11,
        Hurt = 12,
        Attack5 = 13,
        Sleep = 14,
        Miss2 = 15,
        Win = 16,
        Die = 17
    }
    public float fps = 6;               //每秒行走图刷新次数
    private SpriteRenderer s;           //控制对象图片
    private Sprite[] walker;            //行走图图片集
    public string character;            //使用的人物的行走图名称
    public bool IsEne = false;
    public bool IsYourTurn = false;
    public BattleState State = BattleState.Normal;
    public TeamController.Member BindMember;
    private BattleState lState;
    private int tick = 0;
    private float lowx,highx;
    private float ttick = 0;
    private void Awake() {
        s = this.gameObject.GetComponent<SpriteRenderer>();
        walker = Resources.LoadAll<Sprite>("Fighters/" + character);
        Vector3 v = this.gameObject.transform.localPosition;
        highx = v.x;lowx = highx + (IsEne ? 1 : -1);
        s.flipX = !IsEne;
    }
    public void Up(){
        Awake();
    }
    void Update() {
        if(lState != State){
            tick = 0;lState = State;
        }
        ttick += Time.deltaTime;
        if(ttick > 1 / fps) {tick++; ttick = 0;}
        if(tick > 2 && (State == BattleState.Normal || State == BattleState.Magic)) tick = 0;
        if(tick > 2) tick = 2;
        //Debug.Log(character + "," + tick);
        s.sprite = walker[tick + (int)State * 3];
        float suitx = 
        (State == BattleState.Miss || State == BattleState.Miss2 || State == BattleState.Hurt)
        ? lowx : highx;
        if(IsYourTurn) suitx += (IsEne ? -2 : 2);

        Transform v = this.gameObject.transform;
        if(v.localPosition.x != suitx){
            v.localPosition = new Vector3(v.localPosition.x + (suitx - v.localPosition.x) / 3,v.localPosition.y,v.localPosition.z);
        }
    }
}
