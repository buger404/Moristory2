using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoveryFirework : Firework
{
    private SpriteRenderer sr;
    private Sprite[] s;
    private int index = 0;float delt = 0;
    private void Awake() {
        sr = base.GetComponent<SpriteRenderer>();
        s = Resources.LoadAll<Sprite>("Firework/Recovery");
        Destroy(base.gameObject,4f);
    }
    public override float AttackDeepth(){
        SoundPlayer.Play("Heal3");
        return 0.15f * Random.Range(0.8f,1.4f);
    }
    private void Update() {
        delt += Time.deltaTime;
        if(delt >= 0.03f){
            delt = 0;
            index++;
            if(index >= s.Length) index = 1;
            sr.sprite = s[index];
        }
    }
}
