using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefletorFirework : Firework
{
    private void Awake() {
        Destroy(base.gameObject,1f);
    }
    public override void OnCollisionEnter(Collision other) {
        Process(other.gameObject);
    }
    public override void OnTriggerEnter(Collider other) {
        Process(other.gameObject);
    }
    private void Process(GameObject go){
        Firework f;
        if(!go.TryGetComponent<Firework>(out f)) return;
        
        SinFirework sf;
        bool nodes = false;
        if(go.TryGetComponent<SinFirework>(out sf)){
            if(!sf.Mark.Contains("re;")){
                sf.XD = -sf.XD; sf.YD = -sf.YD;
                sf.Mark += "re;";
            }else{
                return;
            }
            f.Owner = base.Owner;
            nodes = true;
        }
        if(nodes) {
            SkillManager.PlaySkillAni(go.transform.position,f.BindS.Animate);
            return;
        }
        SkillManager.PlaySkillAni(go.transform.position,f.BindS.Animate);
        Destroy(go);
    }
}
