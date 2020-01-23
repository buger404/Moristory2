using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdaptScreen : MonoBehaviour {


    private const int standardScreenHeight = 1920;  
    private const int standardScreenWidth = 1080;                     

    void Start()
    {
		Screen.SetResolution((int)(912 * 1.2), (int)(620 * 1.2), false);
    }


}
