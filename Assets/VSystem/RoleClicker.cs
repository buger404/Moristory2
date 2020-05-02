using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleClicker : MonoBehaviour
{
    // Start is called before the first frame update
    public VSystemController VController;
    public int Index;
    void Start()
    {
        
    }
    private void OnMouseUp() {
        VController.SwitchRole(Index);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
