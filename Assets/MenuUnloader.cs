using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUnloader : MonoBehaviour
{
    private Animator FallBackAni;
    public GameObject FallBackAniOwner;
    public string stateName;
    void Unload(){
        if(FallBackAni.GetFloat("Speed") == -2.0f) return;

        if(FallBackAni != null){
            FallBackAni.SetFloat("Speed",-2.0f);
            FallBackAni.Play(stateName,0,1.0f);
        }
        Debug.Log("Set Unload!");
        Destroy(this.gameObject,0.5f);
    }
    private void OnDestroy() {
        Debug.Log("Unload!");
        if(GameConfig.CurrentMenu == "") return;
        SceneManager.UnloadSceneAsync(GameConfig.CurrentMenu);
        GameConfig.CurrentMenu = "";
        GameConfig.IsBlocking = false;
        GameConfig.RecoverScene();
    }
    private void OnMouseUp() {
        Unload();
    }
    // Start is called before the first frame update
    void Awake()
    {
        FallBackAni = FallBackAniOwner.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.X)){
            Unload();
        }
    }
}
