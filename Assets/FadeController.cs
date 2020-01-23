using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeController : MonoBehaviour {
    public string TargetScene;
    private static ArrayList menus = new ArrayList();
    private static string BackS;
    public static void CScene(string name){
        if(name.StartsWith("GameMenu_")){
            if(menus.Count > 0){
                for(int i = 0;i < menus.Count;i++){
                    SceneManager.UnloadSceneAsync((string)menus[i]);
                }
                menus.Clear();
            }
            menus.Add(name);
            SceneManager.LoadScene(name,LoadSceneMode.Additive);
        }else{
            if(menus.Count > 0){
                for(int i = 0;i < menus.Count;i++){
                    SceneManager.UnloadSceneAsync((string)menus[i]);
                }
                menus.Clear();
                if(name != BackS){
                    SceneManager.LoadScene(name);
                }
            }else{
                SceneManager.LoadScene(name);
                BackS = name;
            }
        }
    }
    void ChangeScene(){
        CScene(TargetScene);
    }
    void Awake(){
        DontDestroyOnLoad(this.gameObject);
        DontDestroyOnLoad(this.transform.parent.gameObject);
    }
    void DestroySelf(){
        GameVars.FadeState = 0;
        Destroy(this.gameObject);
    }
}
