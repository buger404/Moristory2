using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VSystemActivter : MonoBehaviour
{
    public Image Btn;
    void ActiveVSystem(){
        if(GameConfig.IsBlocking){return;}
        GameConfig.RecordScene();
        SceneManager.LoadScene("VSystem",LoadSceneMode.Additive);
        GameConfig.CurrentMenu = "VSystem";
        GameConfig.IsBlocking = true;
        Camera.main.gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Awake()
    {
        Btn = this.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        float a = (GameConfig.IsBlocking ? 0.5f : 1.0f);
        if(Btn.color.a != a){
            if(a == 0.5f) Btn.color = new Color(0.5f,0.5f,0.5f,a);
            if(a == 1.0f) Btn.color = new Color(1.0f,1.0f,1.0f,a);
        }
        if(Input.GetKeyUp(KeyCode.X)){
            ActiveVSystem();
        }
        if(Input.GetMouseButtonUp(0)){
            foreach(RaycastHit2D hit in Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition),Vector2.zero)){
                if(hit.transform == this.transform){
                    ActiveVSystem();
                }
            }
        }
    }
}
