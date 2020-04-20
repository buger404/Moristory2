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
    private Vector3 Origin;
    private GameObject poscircle;
    private GameObject orcircle;
    private GameObject arcircle;
    private GameObject CircleCanvas;
    private Canvas CircleCanvasT;
    public float XTask = 0;public float YTask = 0;
    private void OnCollisionStay2D(Collision2D other) {
        
    }
    private void Awake() {
        s = this.gameObject.GetComponent<SpriteRenderer>();
        if(character == ""){return;}
        walker = Resources.LoadAll<Sprite>("Walkers/" + character);
        if(IsController){
            Direction = GameConfig.TpDir;
            GameConfig.Controller = this.gameObject;
            GameObject fab = (GameObject)Resources.Load("Prefabs\\poscircle");
            GameObject obj = Instantiate(fab,new Vector3(0,0,90),Quaternion.identity);
            CircleCanvas = obj;
            poscircle = GameObject.Find("orCircle");
            orcircle = GameObject.Find("fiCircle");
            arcircle = GameObject.Find("areaCircle");
            poscircle.SetActive(true);orcircle.SetActive(true);arcircle.SetActive(true);
            Origin = new Vector3(-1.0f,0f,0f);
            Origin.x = -1;
            Debug.Log("Circle Canvas has been created .");
            CircleCanvasT = CircleCanvas.GetComponent<Canvas>();
            CircleCanvas.SetActive(false);
            fab = (GameObject)Resources.Load("Prefabs\\DialogCanvas");
            obj = Instantiate(fab,new Vector3(0,0,90),Quaternion.identity);
            GameConfig.ActiveDialog = obj.GetComponent<DialogController>();
            obj.SetActive(false);
            fab = (GameObject)Resources.Load("Prefabs\\SpyCanvas");
            obj = Instantiate(fab,new Vector3(0,0,90),Quaternion.identity);
            GameConfig.ActiveSpy = obj.GetComponent<SpyController>();
            obj.SetActive(false);
            if(GameConfig.TpSpot != ""){
                transform.position = GameObject.Find(GameConfig.TpSpot).transform.position;
            }
        }
        s.sprite = walker[1 + 3 * Direction];
    }

    void FixedUpdate()
    {
        bool HandMove = false;
        if(XTask != 0 || YTask != 0){
            Vector3 t = transform.position;
            transform.position = 
            new Vector3(
                t.x + speed * (XTask > 0 ? 1 : -1) * (XTask != 0 ? 1 : 0),
                t.y - speed * (YTask > 0 ? 1 : -1) * (YTask != 0 ? 1 : 0),
                        t.z);
            if(XTask != 0){Direction = XTask > 0 ? 2 : 1;}
            if(YTask != 0){Direction = YTask < 0 ? 3 : 0;}
            XTask -= speed * (XTask > 0 ? 1 : -1) * (XTask != 0 ? 1 : 0);
            YTask -= speed * (YTask > 0 ? 1 : -1) * (YTask != 0 ? 1 : 0);
            if(Mathf.Abs(XTask) <= 0.1){XTask = 0;}
            if(Mathf.Abs(YTask) <= 0.1){YTask = 0;}
            HandMove = true;
            if(XTask == 0 && YTask == 0 && GameConfig.WalkingTask){
                Debug.Log("Walk let next");
                GameConfig.WalkingTask = false;
                GameConfig.IsBlocking = false;
                GameConfig.BlockEvent.Run();
            }
            if(XTask == 0 && YTask == 0){Origin.x = -244;HandMove = false;}
            goto Moves;
        }
        if(GameConfig.IsBlocking){return;}
        if(speed == 0){return;}
        //一步的坐标移动距离大约为1.5！
        Moves:
        if(IsController && !HandMove){
            if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)){
                Vector3 t = transform.position;
                transform.position = new Vector3(t.x - speed * 1.0f,t.y,t.z);
                Direction = 1;
                Origin.x = -404;HandMove = true;
            }
            if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)){
                Vector3 t = transform.position;
                transform.position = new Vector3(t.x + speed * 1.0f,t.y,t.z);
                Direction = 2;
                Origin.x = -404;HandMove = true;
            }
            if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)){
                Vector3 t = transform.position;
                transform.position = new Vector3(t.x,t.y + speed * 1.0f,t.z);
                Direction = 3;
                Origin.x = -404;HandMove = true;
            }
            if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)){
                Vector3 t = transform.position;
                transform.position = new Vector3(t.x,t.y - speed * 1.0f,t.z);
                Direction = 0;
                Origin.x = -404;HandMove = true;
            }
            if(Input.GetMouseButton(0) && Origin.x != -404){
                HandMove = true;
                Vector3 p = transform.position;
                Vector3 inp;
                if(Origin.x == -1){
                    Origin = Input.mousePosition;
                    inp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    CircleCanvas.SetActive(true);
                    poscircle.transform.position = new Vector3(inp.x,inp.y,p.z);
                    arcircle.transform.position = poscircle.transform.position;
                    //arcircle.GetComponent<Animator>().Play("CircleCatch",0);
                    poscircle.GetComponent<Animator>().Play("CircleCatch",0);
                }
                Vector3 Des = Input.mousePosition;
                inp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                orcircle.transform.position = new Vector3(inp.x,inp.y,p.z);

                float r = Mathf.Sqrt
                          (
                          Mathf.Pow(orcircle.transform.position.x - poscircle.transform.position.x,2) +
                          Mathf.Pow(orcircle.transform.position.y - poscircle.transform.position.y,2)
                          );
                r = r * 1.8f;
                if(r > 3){r = 3;}
                RectTransform rect = arcircle.GetComponent<RectTransform>();
                //rect.localScale = new Vector3(r,r,0);
                rect.sizeDelta = new Vector2(r,r);

                float k;
                if((Des.x - Origin.x == 0) || (Des.y - Origin.y == 0)){
                    k = 0;
                }else{
                    k = (Des.x - Origin.x) / (Des.y - Origin.y);
                }
                Vector3 t = transform.position;
                float b1 = 3.0f / 2.0f * 3.1415f - Mathf.Atan(k);
                float b2 = (Des.y < Origin.y ? 1 : -1) * speed * 1.0f;
                transform.position = new Vector3(t.x + Mathf.Cos(b1) * b2,
                                                 t.y + Mathf.Sin(b1) * b2,
                                                 t.z);
                float xD = orcircle.transform.position.x - poscircle.transform.position.x;
                float yD = orcircle.transform.position.y - poscircle.transform.position.y;
                if(xD >= yD){Direction = xD > 0 ? 2 : 0;}
                if(xD <= yD){Direction = yD > 0 ? 3 : 1;}

            }

        }

        if(!HandMove){
            if(Origin.x != -1){
                s.sprite = walker[1 + Direction * 3];
                Origin.x = -1;
                try{CircleCanvas.SetActive(false);}catch{}
            }
        }else{
            int index = (int)(Time.time * fps) % 3;
            s.sprite = walker[index + Direction * 3];
        }

    }
}
