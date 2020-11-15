using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PVPCharacters : MonoBehaviour
{
    public enum PVPT{       //作用对象
        P = 0, PR = 1, PH = 2, PV = 3, PA = 4, PF = 5, PM = 6, PL = 7,
        //我方：本体，随机，横排，纵列，全体，前列，中列，尾列
        E = 8, ER = 9, EH = 10, EV = 11, EA = 12, EF = 13, EM = 14, EL = 15,
        //敌方：本体，随机，横排，纵列，全体，前列，中列，尾列
        PN = 16,EN = 17
        //我方指定名字，敌方指定名字
    }
    public enum PVPCard{    //牌类
        Blue = 0,Purple = 1,Orange = 2
    }
    public struct PVPE{     //数值效果器
        public PVPT TAR;    //作用对象
        public string TARN; //指定作用对象的名字（一般为空）
        public string ABR;  //作用能力值
        public string SRND; //特殊持续回合标记
        public int RND;     //持续回合数标记
        public string EXP;  //作用表达式
    }
    public struct PVPR{     //回合效果器
        public PVPT TAR;    //作用对象
        public string TARN; //指定作用对象的名字（一般为空）
        public string EN;   //效果器名称
    }
    public struct PVPO{     //技能单次攻击结构体
        public float SAT;   //能量条加成
        public float STR;   //威力（攻击%）
        public PVPT TAR;    //作用对象
        public List<PVPE> E;     //技能拥有的数值效果器
        public List<PVPR> R;     //技能拥有的回合效果器
    }
    public struct PVPS{     //技能结构体
        public string Name; //名字
        public string Des;  //给玩家看的介绍
        public List<PVPO> O;
        //技能包含的攻击总数
    }
    public struct PVPB{     //羁绊结构体
        public List<PVPE> E;     //数值效果器
        public List<PVPR> R;     //回合效果器
        public int RND;     //当前羁绊回合
    }
    public struct PVPC{     //角色结构体
        public string Name; //角色名
        public PVPCard Card;//所属卡类
        public float SAT;   //大招能量条储存变量
        public int LEVEL;   //储存强化级别
        public PVPB B;                 //羁绊设定
        public List<PVPL> L;     //各强化级别的能力设定
        public List<PVPE> E;     //被附加的数值效果器
        public List<PVPR> R;     //被附加的回合效果器
    }
    public struct PVPL{     //不同强化级别角色能力设定结构体
        public float ATK;   //攻击
        public float DEF;   //防御
        public float HP;    //血量
        public float SPD;   //速度
        public float ARD;   //破甲
        public float CRI;   //暴击
        public float MIS;   //闪避
        public List<PVPS> S;     //持有的技能
    }
    public struct PVPTEAM{     //PVP队伍
        public PVPC[] C;    //3*2
    }
    //全部角色的原始设定
    public static List<PVPC> Charcters = new List<PVPC>();
    //我方队伍
    public static PVPTEAM Team = new PVPTEAM();
    //计算伤害值
    public static float CalulateATK(PVPO O,PVPL Src,PVPL Des){
        float SA = Src.ATK - Des.DEF * (1 - Src.ARD) - O.STR;
        if(SA < 0) SA = 0;
        SA *= (Random.Range(0f,1f) < Des.MIS ? 0 : 1);
        SA *= (Random.Range(0f,1f) < Des.CRI ? 2 : 1);
        return SA;
    }
}
