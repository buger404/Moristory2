using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollController : MonoBehaviour
{
    public Vector2 scrollPosition = Vector2.zero;
    public float scrollVelocity = 0f;
    public float timeTouchPhaseEnded = 0f;
    public float inertiaDuration = 0.5f;
    
    public Vector2 lastDeltaPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        return;
        float origin = scrollPosition.y;
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                scrollPosition.y += Input.GetTouch(0).deltaPosition.y;
                lastDeltaPos = Input.GetTouch(0).deltaPosition;
            }        
            else if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                print ("End:"+lastDeltaPos.y+"|"+Input.GetTouch(0).deltaTime);
                if (Mathf.Abs(lastDeltaPos.y)> 20.0f)
                {
                    scrollVelocity = (int)(lastDeltaPos.y * 0.5/ Input.GetTouch(0).deltaTime);
                    print(scrollVelocity);
                }
                timeTouchPhaseEnded = Time.time;
            }
        }
        else
        {
            if (scrollVelocity != 0.0f)
            {
                // slow down
                float t = (Time.time - timeTouchPhaseEnded)/inertiaDuration;
                float frameVelocity = Mathf.Lerp(scrollVelocity, 0, t);
                scrollPosition.y += frameVelocity * Time.deltaTime;
                
                if (t >= inertiaDuration)
                    scrollVelocity = 0;
            }
        }
        if(Input.GetAxis("Mouse ScrollWheel") != 0){
            scrollPosition.y += Input.GetAxis("Mouse ScrollWheel") * 200;
        }

        float del = scrollPosition.y - origin;
        if(del != 0){
            for (int i = 0; i < this.transform.childCount; i++)
            {
                Transform t = this.transform.GetChild(i).transform;
                t.localPosition = new Vector3(t.localPosition.x,t.localPosition.y - del,t.localPosition.z);
            }
        }
    }
}
