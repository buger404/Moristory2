using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPDisplayer : MonoBehaviour
{
    public int type;
    public int num;
    private float yj = 0;
    public float ry,rx,ss;
    private SpriteRenderer sr;
    private float st = 0;
    private void Start() {
        sr = this.GetComponent<SpriteRenderer>();
        sr.sprite = Resources.LoadAll<Sprite>("UI/HP" + type)[num];
        sr.color = new Color(1f,1f,1f,0f);
        Destroy(this.gameObject,2f);
    }
    private void Update() {
        if(yj < 1){
            Vector3 p = this.transform.localPosition;
            p.y += ry; p.x += rx;
            yj += 0.1f;
            this.transform.localPosition = p;
            Vector3 s = this.transform.localScale;
            s.x += ss; s.y += ss; s.z += ss;
            this.transform.localScale = s;
            float a = sr.color.a + 0.1f;
            if(a > 1) a = 1;
            sr.color = new Color(1f,1f,1f,a);
        }
        st += Time.deltaTime;
        if(st >= 1.5f && st <= 2f){
            sr.color = new Color(1f,1f,1f,1 - (st - 1.5f) / 0.5f);
        }

    }

    public static void CreateHPAnimate(Vector3 p,float n,int t){
        string s = Mathf.Abs(n).ToString();
        float sx = p.x - (s.Length) * 0.35f / 2;
        Vector3 po = p;
        po.x = sx;
        float x = Random.Range(-0.15f,0.15f),
              y = Random.Range(0.13f,0.25f),
              sp = Random.Range(0.005f,0.013f);
        if(n < 0) t = 3;
        GameObject fab = (GameObject)Resources.Load("Battle\\HPNum");
        GameObject Obj = Instantiate(fab, po, Quaternion.identity);
        Obj.GetComponent<HPDisplayer>().num = 10;
        Obj.GetComponent<HPDisplayer>().type = t;
        Obj.GetComponent<HPDisplayer>().rx = x;
        Obj.GetComponent<HPDisplayer>().ry = y;
        Obj.GetComponent<HPDisplayer>().ss = sp;
        Obj.SetActive(true);
        po.x += 0.35f;
        for(int i = 0;i < s.Length;i++){
            Obj = Instantiate(fab, po, Quaternion.identity);
            Obj.GetComponent<HPDisplayer>().num = int.Parse(s[i].ToString());
            Obj.GetComponent<HPDisplayer>().type = t;
            Obj.GetComponent<HPDisplayer>().rx = x;
            Obj.GetComponent<HPDisplayer>().ry = y;
            Obj.GetComponent<HPDisplayer>().ss = sp;
            Obj.SetActive(true);
            po.x += 0.35f;
        }
    }

    public static void CreateKillAnimate(Vector3 p){
        float sx = p.x - 2 * 0.9f / 2;
        Vector3 po = p;
        po.x = sx;
        float x = Random.Range(-0.15f,0.15f),
              y = Random.Range(0.13f,0.25f),
              sp = Random.Range(0.005f,0.013f);
        GameObject fab = (GameObject)Resources.Load("Battle\\HPNum");
        GameObject Obj = Instantiate(fab, po, Quaternion.identity);
        Obj.GetComponent<HPDisplayer>().num = 0;
        Obj.GetComponent<HPDisplayer>().type = 4;
        Obj.GetComponent<HPDisplayer>().rx = x;
        Obj.GetComponent<HPDisplayer>().ry = y;
        Obj.GetComponent<HPDisplayer>().ss = sp;
        Obj.SetActive(true);
        po.x += 0.9f;
        Obj = Instantiate(fab, po, Quaternion.identity);
        Obj.GetComponent<HPDisplayer>().num = 1;
        Obj.GetComponent<HPDisplayer>().type = 4;
        Obj.GetComponent<HPDisplayer>().rx = x;
        Obj.GetComponent<HPDisplayer>().ry = y;
        Obj.GetComponent<HPDisplayer>().ss = sp;
        Obj.SetActive(true);
    }
}
