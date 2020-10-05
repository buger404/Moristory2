using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaFirework : Firework
{
    public string SpriteGroupName = "";
    public bool WithFallDown = false;
    private Sprite[] s;
    private int index = 0;
    private float deltime = 0;
    private void Awake() {
        s = Resources.LoadAll<Sprite>("Firework/" + SpriteGroupName);
    }
    public override float AttackDeepth(){
        return 0.08f;
    }
    private void FixedUpdate() {
        deltime += Time.deltaTime;
        if(deltime >= 0.03f && index < s.Length - 1){
            deltime = 0;
            index++;
            this.GetComponent<SpriteRenderer>().sprite = s[index];
        }
        if(index == s.Length - 1 && (deltime >= 1.5f || !WithFallDown)){
            SkillManager.PlaySkillAni(this.transform.localPosition,this.BindS.Animate);
            Destroy(this.gameObject);
            deltime = 0;
        }
        if(!WithFallDown) return;
        Vector3 p = this.transform.localPosition;
        if(p.y > 3f){
            p.y -= 1f;
            this.transform.localPosition = p;
            if(p.y <= 3f && BindS.Name == "圣羽之刃") SoundPlayer.Play("Flash2");
        }
    }
}

