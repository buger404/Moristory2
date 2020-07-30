using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy() {
        Debug.Log("Wait broke!");
        GameConfig.MsgLock = false;
    }
}
