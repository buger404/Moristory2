using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeController : MonoBehaviour {
    public string TargetScene;
    private static ArrayList menus = new ArrayList();
    private static string BackS;
    /// <summary>
    /// 切换场景
    /// </summary>
    /// <param name="name">目标场景名</param>
    public static void CScene(string name){
        if(name.StartsWith("GameMenu_")){
            //如果是菜单则需要暂时加入需要卸载的场景名单中
            if(menus.Count > 0){
                for(int i = 0;i < menus.Count;i++){
                    SceneManager.UnloadSceneAsync((string)menus[i]);
                }
                menus.Clear();
            }
            menus.Add(name);
            SceneManager.LoadScene(name,LoadSceneMode.Additive);
            GameVars.ActiveScene = name;
        }else{
            //如果不是菜单
            if(menus.Count > 0){
                //菜单里有未卸载的菜单
                for(int i = 0;i < menus.Count;i++){
                    SceneManager.UnloadSceneAsync((string)menus[i]);
                }
                menus.Clear();
                if(name != BackS){
                    GameVars.ActiveScene = name;
                    SceneManager.LoadScene(name);
                }else{
                    GameVars.ActiveScene = BackS;
                }
            }else{
                GameVars.ActiveScene = name;
                SceneManager.LoadScene(name);
                BackS = name;
            }
        }
    }
    /// <summary>
    /// 动画控制的切换场景事件
    /// </summary>
    void ChangeScene(){
        CScene(TargetScene);
    }
    void Awake(){
        //防止切换场景的时候把自己弄没了
        DontDestroyOnLoad(this.transform.parent.gameObject);
    }
    void DestroySelf(){
        GameVars.FadeState = 0;
        Destroy(this.gameObject);
    }
}
