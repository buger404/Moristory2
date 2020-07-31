using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamController
{
    [System.Serializable]
    public struct Member{
        public int HP,MaxHP,MP,MaxMP;
        public string Name;
        public List<string> Equipments;
        public string[] Magics;
        public Member(string Role){
            Equipments = new List<string>();
            MaxHP = 0; MaxMP = 0;
            Name = Role;Magics = new string[] {"","","",""};
            if(Role == "世原·安诺"){
                MaxHP = 100; MaxMP = 100;
                Magics = new string[] {"能力学习","光能爆破","暗夜袭击","魔法分析"};
            }
            if(Role == "兮·御冯"){
                MaxHP = 80; MaxMP = 90;
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