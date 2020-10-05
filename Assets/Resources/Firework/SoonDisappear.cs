using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoonDisappear : Firework
{
    public float second = 1f;
    private void Awake() {
        Destroy(base.gameObject,second);
    }
    public override void OnCollisionEnter(Collision other) {
        return;
    }
    public override void OnTriggerEnter(Collider other) {
        return;
    }

    private void FixedUpdate() {
        if(base.BindS.Name == "水立方"){
            Vector3 p = base.transform.localPosition;
            p.y += 0.15f;
            base.transform.localPosition = p;
            float ss2 = ((9f - p.y) / 9f) * 4f;
            if(ss2 < 0) ss2 = 0;
            base.transform.localScale = new Vector3(2f,ss2,2f);
        }
    }
}

