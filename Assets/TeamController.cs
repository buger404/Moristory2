using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamController
{
    // 职业技能加成表
    public static float[,] JC = new float[8,8]
    {
        {1f     ,0.8f   ,1.2f   ,1.2f   ,1f     ,1f     ,1.1f   ,0f     },
        {0.8f   ,1f     ,1f     ,1.2f   ,1f     ,1f     ,1f     ,0f     },
        {1.2f   ,1f     ,1f     ,0.8f   ,1f     ,1.2f   ,0.8f   ,1.2f   },
        {1.2f   ,1f     ,1.2f   ,0f     ,1f     ,2f     ,0.8f   ,0.8f   },
        {1f     ,1f     ,1f     ,1f     ,1f     ,1f     ,1f     ,1f     },
        {0.8f   ,1f     ,1.2f   ,0.5f   ,1f     ,1.5f   ,0.5f   ,2f     },
        {0.9f   ,1f     ,1.1f   ,0.8f   ,1f     ,1.5f   ,1f     ,1.2f   },
        {1f     ,1f     ,0.8f   ,1.1f   ,1f     ,0.9f   ,0.9f   ,2f     }
    };
    public enum JOB{
        Art = 1,        // 艺术
        Academy = 2,    // 学术
        Master = 3,     // 法师
        Recovery = 4,   // 治愈
        Normal = 5,     // 一般
        Battle = 6,     // 战争
        Defence = 7,    // 坚守
        Monster = 8,    // 魔物
    }
    [System.Serializable]
    public struct Member{
        public int HP, MaxHP, MP, MaxMP;
        public int ATK, DEF, SPD;
        public JOB[] Job;
        public string Name;
        public List<string> Equipments;
        public string[] Magics;
        public Member(string Role){
            Equipments = new List<string>();
            MaxHP = 0; MaxMP = 0;Job = new JOB[]{JOB.Normal,JOB.Normal};
            ATK = 0;DEF = 0;SPD = 0;
            Name = Role;Magics = new string[] {"","","",""};
            if(Role == "世原·安诺"){
                MaxHP = 100; MaxMP = 100;Job = new JOB[]{JOB.Academy,JOB.Normal};
                Magics = new string[] {"能力学习","光能爆破","暗夜袭击","魔法分析"};
            }
            if(Role == "兮·御冯"){
                MaxHP = 80; MaxMP = 90;Job = new JOB[]{JOB.Art,JOB.Recovery};
                Magics = new string[] {"魈魆花舞","恶魔歌姬","光合作用","记忆碎片"};
            }
            HP = MaxHP;MP = MaxMP;
        }
    }
    [System.Serializable]
    public struct Teams{
        public List<Member> Mem;
    }
    public static Teams Team = new Teams();
    static TeamController(){
        Team.Mem = new List<Member>();
    }
}