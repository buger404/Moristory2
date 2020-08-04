using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    public GameObject DialogB;
    public GameObject DialogT;
    private float BZ,FX1,FX2,BY;
    private List<FighterController> F1,F2;

    private void Awake() {
        Vector3 v = GameObject.Find("Fighter1").transform.position;
        BZ = v.z; FX1 = v.x; BY = v.y;
        v = GameObject.Find("Fighter2").transform.position;
        FX2 = v.x;
        #region 加载我方和敌方成员
            F1 = new List<FighterController>();
            F2 = new List<FighterController>();
            GameObject fab = (GameObject)Resources.Load("Prefabs\\Fighter");
            for(int i = 0;i < TeamController.Team.Mem.Count;i++){
                TeamController.Member m = TeamController.Team.Mem[i];
                GameObject box = 
                    Instantiate(fab,new Vector3(FX1-i*0.5f,BY,BZ - i*2),
                    Quaternion.identity,this.transform.parent);
                FighterController f = box.GetComponent<FighterController>();
                f.character = m.Name;
                f.IsEne = false;
                f.BindMember = m;
                f.Up();
                F1.Add(f);
                box.SetActive(true);
            }
            for(int i = 0;i < TeamController.Team.Mem.Count;i++){
                TeamController.Member m = TeamController.Team.Mem[i];
                GameObject box = 
                    Instantiate(fab,new Vector3(FX2+i*0.5f,BY,BZ - i*2),
                    Quaternion.identity,this.transform.parent);
                FighterController f = box.GetComponent<FighterController>();
                f.character = m.Name;
                f.IsEne = true;
                f.BindMember = m;
                f.Up();
                F2.Add(f);
                box.SetActive(true);
            }
        #endregion
        GameObject.Find("Fighter1").SetActive(false);
        GameObject.Find("Fighter2").SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
