using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Switcher :MonoBehaviour
{
    private Animator animator;
    public static bool SwitcherUsing = false;
    public static string TargetScene = "";
    void Start()
    {
        
    }

    void Update()
    {
        //GameObject.Find("Main Camera").transform.position = new Vector3(0,0,-10);
    }

    public static void SwitchTo(string Scene){
        if(SwitcherUsing) {return;}
        SwitcherUsing = true;
        GameObject swfab = (GameObject)Resources.Load("Prefabs\\Switcher");
        GameObject swbox = Instantiate(swfab,new Vector3(0,0,0),Quaternion.identity);
		swbox.SetActive(true);
        TargetScene = Scene;
    }

    void LoadScene(){
        if(animator.GetFloat("Speed") == 1.1f){
            SceneManager.LoadSceneAsync(TargetScene);
        }
    }

    void MessageOperation(){
        if(animator.GetFloat("Speed") == -1.1f){
            Destroy(this.gameObject);
            Debug.Log("Destoried Switcher!");
            SwitcherUsing = false;
        }
    }

    public void CallBack(Scene scene, LoadSceneMode sceneType)
    {
        Debug.Log("Call back!");
        SceneManager.sceneLoaded -= CallBack;
        if(animator.GetFloat("Speed") != -1.1f){
            Destroy(this.gameObject);
            Debug.Log("Destoried Switcher!");
            SwitcherUsing = false;
            return;
            animator.Play("Switcher_Showing",0,1);
            animator.SetFloat("Speed", -1.1f);
        }
    }

    private void Awake() {
        SceneManager.sceneLoaded += CallBack;
        animator = this.GetComponent<Animator>();
        DontDestroyOnLoad(this.gameObject);
    }
}
