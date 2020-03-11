using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public GameObject Camera;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        if(this.gameObject.transform.position != Camera.transform.position){
            this.gameObject.transform.position = Camera.transform.position;
        }
    }
}
