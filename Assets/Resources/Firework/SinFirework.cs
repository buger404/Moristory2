using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinFirework : Firework
{
    public float Speed,Angle = 0f;
    public float XD = 1,YD = 1;
    public float Life = 0.05f;
    SpriteRenderer sr;
    private void Awake() {
        sr = base.GetComponent<SpriteRenderer>();
    }
    public override float AttackDeepth(){
        return sr.color.a * 0.25f;
    }
    public void Update()
    {
        Vector3 p = base.transform.localPosition;
        p.x += Speed * Mathf.Cos(Angle / 360) * XD; p.z += Speed * Mathf.Sin(Angle / 360) * YD;
        base.transform.position = p;
        Color c = sr.color;
        c.a -= Life;
        sr.color = c;
        if(c.a <= 0f) Destroy(base.gameObject);
    }
}
