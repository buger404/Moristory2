using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillInfoController : MonoBehaviour
{
    public SkillManager.Skill BindS;
    private Text Name,MP;
    private Image Back,Icon;

    public void UpdateInfo(){
        Name = this.transform.Find("Caption").GetComponent<Text>();
        MP = this.transform.Find("Content").GetComponent<Text>();
        Back = this.transform.Find("Background").GetComponent<Image>();
        Icon = this.transform.Find("Icon").GetComponent<Image>();

        Icon.sprite = Resources.Load<Sprite>("Job\\s" + (int)BindS.Job);

        Name.text = BindS.Name;
        MP.text = BindS.MP + " MP";

        Color BackColor = new Color(0,0,0);
        if(BindS.Job == TeamController.JOB.Academy) BackColor = new Color(0f,0f,0f,1f);
        if(BindS.Job == TeamController.JOB.Art) BackColor = new Color(190f / 255f,44f / 255f,1f,1f);
        if(BindS.Job == TeamController.JOB.Battle) BackColor = new Color(1f,83f / 255f,57f / 255f,1f);
        if(BindS.Job == TeamController.JOB.Defence) BackColor = new Color(1f,152f / 255f,0f,1f);
        if(BindS.Job == TeamController.JOB.Master) BackColor = new Color(36f / 255f,167f / 255f,1f,1f);
        if(BindS.Job == TeamController.JOB.Monster) BackColor = new Color(140f / 255f,100f / 255f,80f / 255f,1f);
        if(BindS.Job == TeamController.JOB.Normal) BackColor = new Color(166f / 255f,166f / 255f,166f / 255f,1f);
        if(BindS.Job == TeamController.JOB.Recovery) BackColor = new Color(0f,232f / 255f,120f / 255f,1f);
    
        //Debug.Log(BackColor.r + "," + BackColor.g + "," + BackColor.b);
        Back.color = BackColor;

        Name.color = (BindS.Job == TeamController.JOB.Recovery || BindS.Job == TeamController.JOB.Normal)
                        ? new Color(0,0,0) : new Color(1,1,1);
    }

    void Update()
    {
        
    }
    public void Detail(){
        MessageCreator.CreateMsg(BindS.Name,
            $"威力  <color=red>{BindS.Strength * 100}</color>"
            + $"     需要魔力  <color=blue>{BindS.MP}</color>\n\n{BindS.Describe}"
            );
    }
}
