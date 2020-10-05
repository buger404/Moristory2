using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowardFirework : Firework
{
    public string SpriteGroupName = "";
    private Sprite[] s;
    private int index = 0;
    private float deltime = 0;
    public int XD,YD;
    private void Awake() {
        s = Resources.LoadAll<Sprite>("Firework/" + SpriteGroupName);
    }
    public override float AttackDeepth(){
        return 0.3f;
    }

    private void FixedUpdate() {
        deltime += Time.deltaTime;
        if(deltime >= 0.03f && index < s.Length - 1){
            deltime = 0;
            index++;
            this.GetComponent<SpriteRenderer>().sprite = s[index];
        }
        if(index == s.Length - 1 && deltime >= 1f){
            SkillManager.PlaySkillAni(this.transform.localPosition,this.BindS.Animate);
            Destroy(this.gameObject);
            deltime = 0;
        }
        Vector3 p = this.transform.localPosition;
        p.x += 0.3f * XD; p.z += 0.3f * YD;
        this.transform.localPosition = p;

    }
}

