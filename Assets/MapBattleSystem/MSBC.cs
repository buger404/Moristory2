using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Map Skill Bar Controller
public class MSBC : MonoBehaviour
{
    public int SkillIndex = 0;
    Image Mask;
    public SkillManager.Skill s;
    void Start()
    {
        Mask = this.transform.Find("Mask").GetComponent<Image>();
    }

    public void ReLoad(){
        this.transform.Find("Title").GetComponent<Text>().text = s.Name;
        this.transform.Find("Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("Job\\s" + (int)s.Job);
    }
    public void SetInfo(int index){
        s = SkillManager.S.Find(m => m.Name == TeamController.Team.Mem[index].Magics[SkillIndex]);
        s.CD /= 5;
        ReLoad();
    }

    void Update()
    {
        s.CDa += Time.deltaTime;
        if(s.CDa > s.CD) s.CDa = s.CD;
        Mask.color = new Color(1f,1f,1f,1f - s.CDa / s.CD);
        bool Pressed = false;
        if(SkillIndex == 0 && Input.GetKeyUp(KeyCode.X)) Pressed = true;
        if(SkillIndex == 1 && Input.GetKeyUp(KeyCode.C)) Pressed = true;
        if(SkillIndex == 2 && Input.GetKeyUp(KeyCode.V)) Pressed = true;
        if(GameConfig.IsTouched(this.gameObject)) Pressed = true;
        if(Pressed){
            if(GameConfig.ExS != ""){
                GameConfig.ExS = s.Name;
                MagicExchange me = GameConfig.ExMObj.GetComponent<MagicExchange>();
                s = SkillManager.S.Find(m => m.Name == me.ExMagic);
                me.ExMagic = GameConfig.ExS;
                me.ReLoad();
                ReLoad();
                /**MSBC msbc = GameConfig.ExMObj.GetComponent<MSBC>();
                msbc.s = SkillManager.S.Find(m => m.Name == GameConfig.ExS);
                msbc.ReLoad();**/
            }else{
                if(s.CDa < s.CD) return;
                s.CDa = 0;
                SkillManager.MakeFireworks(s,GameConfig.Controller.transform.localPosition,GameConfig.Controller);
            }
        }
    }
}
