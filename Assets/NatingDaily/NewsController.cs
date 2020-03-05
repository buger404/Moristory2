using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewsController : MonoBehaviour
{
    public int Type;
    private static AudioSource BGM;
    private static AudioSource Voice;
    private static Text Content;
    private void Awake() {
        BGM = GameObject.Find("BGM").GetComponent<AudioSource>();
        Voice = GameObject.Find("Voice").GetComponent<AudioSource>();
        Content = GameObject.Find("Content").GetComponent<Text>();
    }
    void EnableMusic(){
        if(Type != 0){return;}
        BGM.Play();
        Voice.Play();
    }
    void Start()
    {
        
    }
    void Update()
    {
        if(Type != 0){return;}
        string word = "";float pos = Voice.time;
        if(pos >= 0){word = "欢迎收看『娜亭日报』";}
        if(pos >= 1.9){word = "我是主持人艾伦·思瑞";}
        if(pos >= 4.5){word = "娜亭时间上午九时三十分";}
        if(pos >= 6.9){word = "楚斯卡德就墓之森领土问题再访奥伦娜";}
        if(pos >= 10.6){word = "楚斯卡德首领与奥伦娜女王在娜亭 蕾尔玫宫会见";}
        if(pos >= 14.8){word = "楚斯卡德首领沽梦·卡德在会议中";}
        if(pos >= 18){word = "不断强调『卡德一世埋葬在墓之森』";}
        if(pos >= 21){word = "并以此为借口";}
        if(pos >= 22.8){word = "捏造『墓之森本来就是楚斯卡德的领土』";}
        if(pos >= 27.5){word = "这一虚假事实";}
        if(pos >= 29){word = "试图掠夺我国的法师资源";}
        if(pos >= 32){word = "众所周知";}
        if(pos >= 33.3){word = "法师在人群中的诞生率约为一万分之一";}
        if(pos >= 37){word = "而在墓之森";}
        if(pos >= 38.2){word = "每一百人中便有一人成为法师";}
        if(pos >= 41.5){word = "近年来";}
        if(pos >= 42.6){word = "在各界人士的帮助下";}
        if(pos >= 45){word = "培养了一批优良法师";}
        if(pos >= 48.2){word = "法师逐渐成为我国国防主力";}
        if(pos >= 52){word = "为此";}
        if(pos >= 53){word = "世界各国都瞄准了墓之森";}
        if(pos >= 56.5){word = "在会议上";}
        if(pos >= 57.5){word = "对于割让墓之森的无理要求";}
        if(pos >= 58.6){word = "莎瑟·娜蕾女王严词拒绝";}
        if(pos >= 60.5){word = "墓之森自古以来就是奥伦娜的领土";}
        if(pos >= 64){word = "接下来为您播报";}
        if(pos >= 65.8){word = "近日维克中学的最新研究报告";}

        if(Content.text != word){Content.text = word;}
    }
    
}
