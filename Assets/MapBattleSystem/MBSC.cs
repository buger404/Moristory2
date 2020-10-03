using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Map Battle System Controller
public class MBSC : MonoBehaviour
{
    public Image HPBar;
    public Text HPText;
    private RectTransform rect;
    private float MaxW;
    void Awake()
    {
        rect = HPBar.GetComponent<RectTransform>();
        MaxW = rect.sizeDelta.x;
    }

    void Update()
    {
        TeamController.Member m = TeamController.Team.Mem[0];
        HPText.text = "HP  " + m.HP + "/" + m.MaxHP;
        rect.sizeDelta = new Vector2(m.HP / m.MaxHP * MaxW, rect.sizeDelta.y);
    }
}
