using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGEvent : MonoBehaviour
{
    public bool IsController = false;
    public float speed = 3;
    public float fps = 6;
    public int Direction = 0;
    public string character;
    private SpriteRenderer s;
    private Sprite[] walker;
    private Vector3 Destination;
    private int xDir = 0;private int yDir = 0;
    private bool IsWalking = false;
    private float HitWall = 0;
    private GameObject poscircle;

    private void OnCollisionStay2D(Collision2D other) {
        HitWall += Time.deltaTime;
        if(HitWall >= 1){
            Destination.x = transform.position.x;
            Destination.y = transform.position.y;
        }
    }
    private void Awake() {
        s = this.gameObject.GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        if(character == ""){return;}
        walker = Resources.LoadAll<Sprite>("Walkers/" + character);
        s.sprite = walker[1 + 3 * Direction];
        if(IsController){
            GameConfig.Controller = this.gameObject;
            GameObject fab = (GameObject)Resources.Load("Prefabs\\poscircle");
            GameObject obj = Instantiate(fab,new Vector3(0,0,0),Quaternion.identity);
		    obj.SetActive(false);obj.transform.position = transform.position;
            poscircle = obj;
        }
    }

    void Update()
    {
        if(speed == 0){return;}
        if(IsController){
            if(Input.GetMouseButtonUp(0)){
                Vector3 p = transform.position;
                Destination = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                poscircle.transform.position = new Vector3(Destination.x,Destination.y,poscircle.transform.position.z);
                xDir = p.x >= Destination.x ? -1 : 1;
                yDir = p.y >= Destination.y ? -1 : 1;
                if(p.x == Destination.x){xDir = 0;}
                if(p.y == Destination.y){yDir = 0;}
                float xD = Mathf.Abs(Destination.x - p.x);
                float yD = Mathf.Abs(Destination.y - p.y);
                if(xD >= yD){Direction = xDir == 1 ? 2 : 1;}
                if(xD <= yD){Direction = yDir == 1 ? 3 : 0;}
                HitWall = 0;
                IsWalking = true;poscircle.SetActive(true);
            }
        }
        if(!IsWalking){return;}
        if(xDir == 0 && yDir == 0){poscircle.SetActive(false);s.sprite = walker[Direction * 3 + 1];IsWalking = false;return;}

        Vector3 te = transform.position;
        float dx = Time.deltaTime * speed;
        te.x += dx * xDir;te.y += dx * yDir;
        if(xDir == 1 && te.x >= Destination.x){te.x = Destination.x;xDir = 0;}
        if(xDir == -1 && te.x <= Destination.x){te.x = Destination.x;xDir = 0;}
        if(yDir == 1 && te.y >= Destination.y){te.y = Destination.y;yDir = 0;}
        if(yDir == -1 && te.y <= Destination.y){te.y = Destination.y;yDir = 0;}
        transform.position = te;

        int index = (int)(Time.time * fps) % 3;
        s.sprite = walker[index + Direction * 3];
    }
}
