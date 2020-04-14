using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitcherHandle : MonoBehaviour
{
    public string tarScene = "";
    void Carry(){
        Switcher.SwitchTo(tarScene);
    }
}
