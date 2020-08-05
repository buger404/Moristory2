using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    public struct Skill{
        public int MP;
        public float Strength;
        public TeamController.JOB Job;
        public string Name;
        public string Describe;
        public Skill(string N,int M,float S,TeamController.JOB J,string Des){
            MP = M;Strength = S;Job = J;
            Name = N;Describe = Des;
        }        
    }
    public static List<Skill> S = new List<Skill>();
    static SkillManager(){
        S.Add(new Skill("能力学习",5,0f,TeamController.JOB.Academy,"复制对方下一回合的魔法"));
        S.Add(new Skill("光能爆破",10,1.2f,TeamController.JOB.Master,"释放巨大的光束攻击对方"));
        S.Add(new Skill("魔法分析",5,0f,TeamController.JOB.Academy,"分析对方的魔法，回避一回合攻击"));
        S.Add(new Skill("魈魆花舞",10,1.0f,TeamController.JOB.Master,"无数的花朵像幽灵一般舞动袭击对方"));
        S.Add(new Skill("恶魔歌姬",30,1.5f,TeamController.JOB.Monster,"将巨大的魔法能量附加在歌声中\n可能导致对方害怕"));
        S.Add(new Skill("光合作用",10,-1f,TeamController.JOB.Master,"利用光能补充自身体力"));
        S.Add(new Skill("记忆碎片",50,0f,TeamController.JOB.Normal,"未知，没有任何关于这种魔法的资料"));
        S.Add(new Skill("结点冰封",20,1.2f,TeamController.JOB.Master,"瞬间降低对方周围的温度，可能冻伤对方"));
        S.Add(new Skill("寒冰冲击",30,1.5f,TeamController.JOB.Battle,"释放巨大的冰柱攻击对方"));
        S.Add(new Skill("幽静能量",10,1.0f,TeamController.JOB.Master,"释放诡异的魔法能量，可能导致对方害怕"));
        S.Add(new Skill("催眠咒语",20,0.0f,TeamController.JOB.Master,"当对方睡着时，可以提升30信任度"));
        S.Add(new Skill("睡梦气息",10,0.0f,TeamController.JOB.Master,"30%使对方陷入睡眠2回合"));
        
        
    
    }
    
}