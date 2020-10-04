using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidFirework : Firework
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
        SkillManager.PlaySkillAni(go.transform.position,f.BindS.Animate);
        Destroy(go);
    }
}
