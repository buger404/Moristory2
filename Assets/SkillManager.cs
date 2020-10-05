using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    [System.Serializable]
    public struct Buff{
        public string Name;
        public object Tag;
        public int Round;
    }
    [System.Serializable]
    public struct Skill{
        public int MP;
        public float Strength;
        public TeamController.JOB Job;
        public string Name;
        public string Describe;
        public string Animate;
        public float CD;
        public float CDa;
        public Skill(string N,int M,float S,TeamController.JOB J,float cd,string Des,string Ani){
            MP = M;Strength = S;Job = J;
            Name = N;Describe = Des;Animate = Ani;
            CD = cd;CDa = 0;
        }        
    }
    public static List<GameObject> FireworkPrefabs = new List<GameObject>();
    public static List<GameObject> AniPrefabs = new List<GameObject>();
    public static List<Skill> S = new List<Skill>();
    public static BattleController AcB;
    // F1：使用者，F2=被使用者
    public static void ExtraSkill(string name,FighterController F1,FighterController F2){
        if(name == "能力学习"){
            F2.BindMember.buffs.Add(new Buff{Name = "能力学习", Round = 1, Tag = F1});
            AcB.Msg.Add(F1.BindMember.Name + "开始专注学习对方的招式...");
        }
        if(name == "恶魔歌姬"){
            if(Random.Range(1,100) <= 30){
                F2.BindMember.buffs.Add(new Buff{Name = "害怕", Round = 1});
                F2.State = FighterController.BattleState.BadHurt;
                AcB.Msg.Add(F2.BindMember.Name + "害怕了！");
                AcB.Rounds.Remove(F2);
            }
            if(Random.Range(1,100) <= 5){
                F2.BindMember.HP = 0;
                AcB.Msg.Add(F1.BindMember.Name + "魔法的致死效果奏效了！");
            }
        }
        if(name == "结点冰封"){
            if(Random.Range(1,100) <= 30){
                F2.BindMember.buffs.Add(new Buff{Name = "冻伤", Round = 3});
                F2.State = FighterController.BattleState.BadHurt;
                AcB.Msg.Add(F2.BindMember.Name + "被冻伤了！");
            }
        }
        if(name == "幽静能量"){
            if(Random.Range(1,100) <= 30){
                F2.BindMember.buffs.Add(new Buff{Name = "害怕", Round = 1});
                F2.State = FighterController.BattleState.BadHurt;
                AcB.Msg.Add(F2.BindMember.Name + "害怕了！");
                AcB.Rounds.Remove(F2);
            }
        }
        if(name == "睡梦气息"){
            if(Random.Range(1,100) <= 30){
                F2.BindMember.buffs.Add(new Buff{Name = "睡眠", Round = 2});
                F2.State = FighterController.BattleState.Sleep;
                AcB.Msg.Add(F2.BindMember.Name + "呼呼大睡起来了...");
                AcB.Rounds.Remove(F2);
            }
        }
        if(name == "魔法分析"){
            F1.BindMember.buffs.Add(new Buff{Name = "回避", Round = 1, Tag = F1.BindMember.DEF});
            F1.BindMember.DEF = 9999999f;
            AcB.Msg.Add(F1.BindMember.Name + "看透了对方接下来的行动！");
        }
    }
    // Time：0-所有回合开始 1-回合后 2-回合前
    public static void BuffProcess(FighterController F1,int Time){
        bool Worked = false;
        for(int i = 0;i < F1.BindMember.buffs.Count;i++){
            Buff b = F1.BindMember.buffs[i];
            Worked = false;
            if(b.Name == "能力学习" && Time == 0){
                Worked = true;
                FighterController F = (FighterController)b.Tag;
                AcB.Msg.Add(F.BindMember.Name + "的能力学习起效果了！");
                AcB.Msg.Add(F.BindMember.Name + "学会了" + F1.BindMember.LastSkill.Name + "！");
                for(int j = 0;j < 3;j++){
                    if(F.BindMember.Magics[j] == "能力学习"){
                        F.BindMember.Magics[j] = F1.BindMember.LastSkill.Name;
                    }
                }
            }
            if(b.Name == "回避" && Time == 1){
                Worked = true;
                AcB.Msg.Add(F1.BindMember.Name + "撑过了这个回合！");
                F1.BindMember.DEF = (float)b.Tag;
            }
            if(b.Name == "睡眠" && Time == 0){
                Worked = true;
                F1.State = FighterController.BattleState.Sleep;
                AcB.Msg.Add(F1.BindMember.Name + "正睡得香甜...");
                AcB.Rounds.Remove(F1);
            }
            if(b.Name == "害怕" && Time == 0){
                Worked = true;
                F1.State = FighterController.BattleState.BadHurt;
                AcB.Msg.Add(F1.BindMember.Name + "被敌方震慑了，不能行动！");
                AcB.Rounds.Remove(F1);
            }
            if(b.Name == "冻伤" && Time == 0){
                Worked = true;
                F1.State = FighterController.BattleState.BadHurt;
                AcB.Msg.Add(F1.BindMember.Name + "冻伤的伤口发作了！");
                float hp = F1.BindMember.HP;
                F1.BindMember.HP *= 0.75f;
                F1.BindMember.HP = Mathf.Ceil(F1.BindMember.HP);
                AcB.Msg.Add(F1.BindMember.Name + "受到了" + Mathf.Floor(F1.BindMember.HP - hp) + "点伤害。");
            }
            if(Worked) b.Round--;
            F1.BindMember.buffs[i] = b;
        }
        F1.BindMember.buffs.RemoveAll(m => m.Round <= 0);
    }
    static SkillManager(){
        //Assets/Resources/Epic Toon FX/Prefabs/Combat/Nova/Lightning/NovaLightningBlue.prefab
        S.Add(new Skill("能力学习",5,0f,TeamController.JOB.Academy,5f,
        "复制对方下一回合的魔法\n然后替换该招式","Combat\\Nova\\Lightning\\NovaLightningBlue"));
        S.Add(new Skill("光能爆破",10,1.2f,TeamController.JOB.Master,3f,
        "释放巨大的光束攻击对方","Combat/Explosions/LightningExplosion/LightningExplosionYellow"));
        S.Add(new Skill("魔法分析",5,0f,TeamController.JOB.Academy,5f,
        "分析对方的魔法，回避一回合攻击","Combat\\Nova\\Lightning\\NovaLightningBlue"));
        S.Add(new Skill("魈魆花舞",10,1.0f,TeamController.JOB.Master,3.6f,
        "无数的花朵像幽灵一般舞动袭击对方","Combat/Magic/PillarBlast/MagicPillarBlastGreen"));
        S.Add(new Skill("恶魔歌姬",30,1.5f,TeamController.JOB.Monster,4f,
        "将巨大的魔法能量附加在歌声中\n可能导致对方害怕,5%的概率致死","Combat/Blood/Red/BloodExplosion"));
        S.Add(new Skill("光合作用",10,-1f,TeamController.JOB.Master,7f,
        "利用光能补充自身体力","Combat/Magic/Charge/MagicChargeYellow"));
        S.Add(new Skill("记忆碎片",50,0f,TeamController.JOB.Normal,1000000f,
        "未知，没有任何关于这种魔法的资料","Combat/Magic/Buff/MagicBuffYellow"));
        S.Add(new Skill("结点冰封",20,1.2f,TeamController.JOB.Master,1f,
        "瞬间降低对方周围的温度，可能冻伤对方","Combat/Explosions/FrostExplosion/FrostExplosionBlue"));
        S.Add(new Skill("寒冰冲击",30,1.5f,TeamController.JOB.Battle,1f,
        "释放巨大的冰柱攻击对方，必定降雪","Combat/Nova/Frost/NovaFrost"));
        S.Add(new Skill("幽静能量",10,1.0f,TeamController.JOB.Master,1f,
        "释放诡异的魔法能量，可能导致对方害怕","Combat/Muzzleflash/SoulMuzzle/SoulMuzzleCrimson"));
        S.Add(new Skill("催眠咒语",20,0.0f,TeamController.JOB.Master,1f,
        "当对方睡着时，可以提升30信任度","Combat/Muzzleflash/MysticMuzzle/MysticMuzzleWhite"));
        S.Add(new Skill("睡梦气息",10,0.0f,TeamController.JOB.Master,1f,
        "30%使对方陷入睡眠2回合","Combat/Muzzleflash/MysticMuzzle/MysticMuzzleWhite"));
        S.Add(new Skill("水立方",15,1.0f,TeamController.JOB.Normal,8f,
        "攻击对手并使对手失去一回合机会","Combat/Magic/Aura/MagicAuraBlue"));
        S.Add(new Skill("圣羽之刃",20,2.0f,TeamController.JOB.Recovery,3f,
        "攻击对方全体","Combat/Muzzleflash/SoulMuzzle/SoulMuzzleCrimson"));
        S.Add(new Skill("星烁",20,2.0f,TeamController.JOB.Master,2.6f,
        "攻击对方全体","Combat/Explosions/LaserExplosion/LaserExplosionFire"));
        S.Add(new Skill("猎风斩",10,2.0f,TeamController.JOB.Battle,2f,
        "攻击对方全体","Combat/Sword/MagicSwordHit/MagicSwordHitGreen"));
        S.Add(new Skill("雷炎阵",10,2.0f,TeamController.JOB.Master,3f,
        "攻击对方全体","Combat/Death/ElectricDeathYellow"));
    }
    
    public static int MinMP(string[] ma){
        int Min = int.MaxValue;
        foreach(string s in ma){
            Skill sk = S.Find(m => m.Name == s);
            if(sk.MP < Min) Min = sk.MP;
        }
        return Min;
    }
    public static GameObject Fire(string name,Vector3 p,Skill ma,GameObject Owner){
        int index = FireworkPrefabs.FindIndex(m => m.name == name);
        GameObject fab;
        if(index == -1){
            fab = (GameObject)Resources.Load("Firework\\Prefab\\" + name);
            FireworkPrefabs.Add(fab);
        }else{
            fab = FireworkPrefabs[index];
        }
        GameObject obj = Instantiate(fab,p,Quaternion.identity);
        obj.GetComponent<Firework>().BindS = ma;
        obj.GetComponent<Firework>().Owner = Owner;
        obj.SetActive(true);
        return obj;
    }
    public static Vector3 RandomVector(Vector3 or,float min,float max){
        return new Vector3(or.x * Random.Range(min,max),or.y * Random.Range(min,max),or.z * Random.Range(min,max));
    }
    public static Vector3 RandomVector2(Vector3 or,float min,float max){
        float p = Random.Range(min,max);
        return new Vector3(or.x * p,or.y * p,or.z * p);
    }
    public static void PlaySkillAni(Vector3 p,string name){
        int index = AniPrefabs.FindIndex(m => m.name == name);
        GameObject fab;
        if(index == -1){
            fab = (GameObject)Resources.Load("Epic Toon FX\\Prefabs\\" + name);
            AniPrefabs.Add(fab);
        }else{
            fab = AniPrefabs[index];
        }
        GameObject Obj = Instantiate(fab, p, Quaternion.identity);
        Obj.transform.localEulerAngles = new Vector3(-90f,0,0);
        Obj.SetActive(true);
        Destroy(Obj,4.0f);
    }
    public static void MakeFireworks(Skill ma,Vector3 p,GameObject Owner){
        if(ma.Name == "光能爆破"){
            int c = Random.Range(10,20);
            float A = Random.Range(0,360);
            for(int i = 0;i < c;i++){
                GameObject f = Fire("Light",p,ma,Owner);
                f.transform.localScale = RandomVector2(f.transform.localScale,0.3f,0.8f);
                SinFirework sf = f.GetComponent<SinFirework>();
                sf.Speed = Random.Range(0.2f,0.6f);
                sf.Angle = Random.Range(0,360);
                sf.XD = (Random.Range(0f,1f) < 0.5f ? 1 : -1);
                sf.YD = (Random.Range(0f,1f) < 0.5f ? 1 : -1);
                sf.Life = Random.Range(0.05f,0.15f);
            }
        }
        if(ma.Name == "恶魔歌姬"){
            SoundPlayer.Play("Magic3");
            Fire("SongFirework",p,ma,Owner);
        }
        if(ma.Name == "光合作用"){
            Fire("Recovery",p,ma,Owner);
        }
        if(ma.Name == "能力学习"){
            Fire("Refletor",p,ma,Owner);
        }
        if(ma.Name == "魔法分析"){
            Fire("Voider",p,ma,Owner);
        }
        if(ma.Name == "水立方"){
            SoundPlayer.Play("Water1");
            Vector3 p2 = p;
            p2.y = -3f;
            Fire("WaterCube",p2,ma,Owner);
        }
        if(ma.Name == "圣羽之刃"){
            SoundPlayer.Play("Flash2");
            for(int i = 0;i < 10;i++){
                Fire("Feather", 
                new Vector3(p.x + Random.Range(-5f,5f),5f + Random.Range(15f,30f),p.z + Random.Range(-5f,5f))
                ,ma,Owner);
            }
        }
        if(ma.Name == "星烁"){
            SoundPlayer.Play("Fire2");
            for(int i = 0;i < 10;i++){
                Fire("Star", 
                new Vector3(p.x + Random.Range(-5f,5f),p.y,p.z + Random.Range(-5f,5f))
                ,ma,Owner);
            }
        }
        if(ma.Name == "猎风斩"){
            SoundPlayer.Play("Wind2");
            TowardFirework f = Fire("Wind",p,ma,Owner).GetComponent<TowardFirework>();
            int dir = -1;
            NPC npc;RPGEvent rpg;
            if(Owner.TryGetComponent<NPC>(out npc)) dir = npc.Direction;
            if(Owner.TryGetComponent<RPGEvent>(out rpg)) dir = rpg.Direction;
            if(dir == 0) f.YD = -1;
            if(dir == 1) f.XD = -1;
            if(dir == 2) f.XD = 1;
            if(dir == 3) f.YD = 1;
        }
        if(ma.Name == "雷炎阵"){
            SoundPlayer.Play("Thunder9");
            Firework f = Fire("Thunder",new Vector3(p.x,4.0f,p.z),ma,Owner).GetComponent<Firework>();
            f.NoDelete = true;
        }
        if(ma.Name == "魈魆花舞"){
            SoundPlayer.Play("Darkness5");
            for(int i = 0;i < 10;i++){
                Fire("Dark", 
                new Vector3(p.x + Random.Range(-5f,5f),p.y,p.z + Random.Range(-5f,5f))
                ,ma,Owner);
            }
        }
        PlaySkillAni(p,ma.Animate);
    }
}