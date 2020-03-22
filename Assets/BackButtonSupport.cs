using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButtonSupport : MonoBehaviour
{
    // Start is called before the first frame update
    private float TimeShift = -999;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape)){
            if(Time.time - TimeShift <= 3.0f){
                Debug.Log("App exited !");
                Application.Quit();
            }else{
                Debug.Log("Delay Destoried");
                GameObject fab = (GameObject)Resources.Load("Prefabs\\ExitCanvas");
                GameObject obj = Instantiate(fab,new Vector3(0,0,0),Quaternion.identity);
                obj.SetActive(true);
                Destroy(obj,3.0f);
            }
            TimeShift = Time.time;
        }
    }
}
