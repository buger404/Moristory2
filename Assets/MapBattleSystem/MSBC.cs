using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Map Skill Bar Controller
public class MSBC : MonoBehaviour
{
    public int SkillIndex = 0;
    Image Mask;
    SkillManager.Skill s;
    void Start()
    {
        s = SkillManager.S.Find(m => m.Name == TeamController.Team.Mem[0].Magics[SkillIndex]);
        
        this.transform.Find("Title").GetComponent<Text>().text = s.Name;
        this.transform.Find("Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("Job\\s" + (int)s.Job);
    }

    void Update()
    {
        if(SkillIndex == 1 && Input.GetKeyUp(KeyCode.X)) 
        SkillManager.MakeFireworks(s,GameConfig.Controller.transform.localPosition);
    }
}
