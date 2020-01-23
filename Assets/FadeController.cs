using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeController : MonoBehaviour {
    public string TargetScene;
    private static ArrayList menus = new ArrayList();
    private static string BackS;
    void ChangeScene(){
        if(TargetScene.StartsWith("GameMenu_")){
            if(menus.Count > 0){
                for(int i = 0;i < menus.Count;i++){
                    SceneManager.UnloadSceneAsync((string)menus[i]);
                }
                menus.Clear();
            }
            menus.Add(TargetScene);
            SceneManager.LoadScene(TargetScene,LoadSceneMode.Additive);
        }else{
            if(menus.Count > 0){
                for(int i = 0;i < menus.Count;i++){
                    SceneManager.UnloadSceneAsync((string)menus[i]);
                }
                menus.Clear();
                if(TargetScene != BackS){
                    SceneManager.LoadScene(TargetScene);
                }
            }else{
                SceneManager.LoadScene(TargetScene);
                BackS = TargetScene;
            }
        }
    }
    void Awake(){
        DontDestroyOnLoad(this.gameObject);
        DontDestroyOnLoad(this.transform.parent.gameObject);
    }
    void DestroySelf(){
        Destroy(this.gameObject);
    }
}
