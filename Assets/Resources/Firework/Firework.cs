using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firework : MonoBehaviour
{
    public string Mark;
    public GameObject Owner;
    public SkillManager.Skill BindS;
    public virtual void OnCollisionEnter(Collision other) {
        Process(other.gameObject);
    }
    public virtual void OnTriggerEnter(Collider other) {
        Process(other.gameObject);
    }
    private void Process(GameObject go){
        BindAbility b;
        if(!go.TryGetComponent<BindAbility>(out b)) return;
        
        if((go != Owner && BindS.Strength > 0) || (go == Owner && BindS.Strength < 0)){
            if(BindS.Strength > 0) SkillManager.PlaySkillAni(go.transform.position,BindS.Animate);
            go.GetComponent<BindAbility>().ProcessAttack
            (AttackDeepth(),BindS,Owner.GetComponent<BindAbility>().Ability);
            if(BindS.Strength > 0) Destroy(this.gameObject);
        }
    }
    public virtual float AttackDeepth(){
        return 0.15f;
    }

}
