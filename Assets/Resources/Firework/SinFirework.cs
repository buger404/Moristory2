using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinFirework : MonoBehaviour
{
    public float Speed,Angle = 0f;
    public float XD = 1,YD = 1;
    public float Life = 0.05f;
    public GameObject Owner;
    public SkillManager.Skill BindS;
    SpriteRenderer sr;
    private void Awake() {
        sr = this.GetComponent<SpriteRenderer>();
    }
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject != Owner){
            SkillManager.PlaySkillAni(this.transform.position,BindS.Animate);
            other.gameObject.GetComponent<BindAbility>().ProcessAttack
            (sr.color.a * 0.15f,BindS,Owner.GetComponent<BindAbility>().Ability);
            Destroy(this.gameObject);
        }
    }

    void Update()
    {
        Vector3 p = this.transform.localPosition;
        p.x += Speed * Mathf.Cos(Angle / 360) * XD; p.z += Speed * Mathf.Sin(Angle / 360) * YD;
        this.transform.position = p;
        Color c = sr.color;
        c.a -= Life;
        sr.color = c;
        if(c.a <= 0f) Destroy(this.gameObject);
    }
}
