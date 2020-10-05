using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BindAbility : MonoBehaviour
{
    public TeamController.Member Ability;
    public GameObject HPBar;
    public Sprite HPBar1,HPBar2;
    private float MaxW,LastW;
    public int BindTeam = -1;
    public bool Recovery = false;
    private float RecoveryCD = 0;
    private void Awake() {
        if(BindTeam > -1) {
            Ability = TeamController.Team.Mem[BindTeam];
        }else{
            MaxW = HPBar.transform.localScale.x;
        }
    }
    public void ProcessAttack(float deepth,SkillManager.Skill s,TeamController.Member m1){
        if (Recovery) return;
        float Injury = s.Strength * m1.ATK;
        Injury /= 1.5f;
        if(s.Strength > 0) Injury -= Ability.DEF;
        float Source = Injury;
        int type = 0;
        Injury *= TeamController.JC[(int)s.Job-1,(int)Ability.Job[0]-1];
        Injury *= TeamController.JC[(int)s.Job-1,(int)Ability.Job[1]-1];
        type = (Injury > Source ? 2 : 1);
        Injury *= deepth;
        Ability.HP -= Injury;
        HPDisplayer.CreateHPAnimate(this.transform.localPosition,
            Mathf.Ceil(Injury), type);
        if(Ability.HP <= 0) HPDisplayer.CreateKillAnimate(this.transform.localPosition);
    }
    void Update()
    {
        if(Ability.HP < 0) Ability.HP = 0;
        if(Ability.HP > Ability.MaxHP) Ability.HP = Ability.MaxHP;

        if(BindTeam > -1) return;
        if(Ability.HP == 0 && Recovery == false) {
            HPBar.GetComponent<SpriteRenderer>().sprite = HPBar2;
            Recovery = true;
        }

        if(Recovery){
            RecoveryCD += Time.deltaTime;
            if(RecoveryCD >= 0.8f){
                RecoveryCD = 0;
                Ability.HP += 1;
            }
            if(Ability.HP / Ability.MaxHP >= 0.3f) {
                HPBar.GetComponent<SpriteRenderer>().sprite = HPBar1;
                Recovery = false;
            }
        }
        
        float W = Ability.HP / Ability.MaxHP * MaxW;
        if(W != LastW){
            HPBar.transform.localScale = new Vector3(W,HPBar.transform.localScale.y,HPBar.transform.localScale.z);
            LastW = W;
        }
    }
}
