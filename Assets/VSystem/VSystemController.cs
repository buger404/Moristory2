using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VSystemController : MonoBehaviour
{
    public GameObject EnterBtn;
    public GameObject ExitBtn;
    public List<GameObject> UI;
    private List<GameObject> Created = new List<GameObject>();
    private void Awake() {
        Carry(ExitBtn.name);
    }
    public void Carry(string name){
        if(name == EnterBtn.name){
            EnterBtn.SetActive(false);
            ExitBtn.SetActive(true);
            foreach(GameObject go in UI) go.SetActive(true);
            Created = new List<GameObject>();
            GameObject fab = (GameObject)Resources.Load("Prefabs\\Info");
            GameObject box = Instantiate(fab,new Vector3(0,0,0),Quaternion.identity,this.transform);
            box.SetActive(true);
            box.transform.localPosition = new Vector3(0,0,0);
            box.transform.Find("Caption").gameObject.GetComponent<Text>().text = "世原·安诺";
            box.transform.Find("Content").gameObject.GetComponent<Text>().text = "0G,  0Pt";
            box.transform.Find("Icon").gameObject.GetComponent<WalkingAnimate>().character = "世原·安诺";
            Created.Add(box);
            GameConfig.IsBlocking = true;
        }
        if(name == ExitBtn.name){
            EnterBtn.SetActive(true);
            ExitBtn.SetActive(false);
            foreach(GameObject go in UI) go.SetActive(false);
            foreach(GameObject go in Created) Destroy(go);
            GameConfig.IsBlocking = false;
        }
    }
    void Update()
    {
        if(Input.GetMouseButtonUp(0)){
            GraphicRaycaster gr = this.GetComponent<GraphicRaycaster>();
            PointerEventData data = new PointerEventData(EventSystem.current);
            data.pressPosition = Input.mousePosition;
            data.position = Input.mousePosition;
            List<RaycastResult> results = new List<RaycastResult>();
            gr.Raycast(data, results);

            foreach(RaycastResult rr in results){
                Carry(rr.gameObject.name);
            }
        }
        if(Input.GetKeyUp(KeyCode.C)){
            Carry(EnterBtn.activeSelf ? EnterBtn.name : ExitBtn.name);
        }
    }
}
