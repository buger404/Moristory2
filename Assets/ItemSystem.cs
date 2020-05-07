using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSystem{
    public struct GameItem{
        public int Count;
        public string Name;
        public void Give(){
            Count++;
        }
    }
    public struct GameItemInfo{
        public string Name;
        public long Cost;
        public string Describe;
        public string Icon;
        public string Love;
        public long HP;
        public long MP;
        public bool Extend;
        public float Cut;
        public GameItemInfo(string name,string describe,long cost,string icon,long hp,long mp,bool extend,params string[] love){
            Name = name;Describe = describe;Cost = cost;
            Icon = icon;HP = hp;MP = mp;Extend = extend;Cut = 1;
            Love = "";
            foreach(string p in love)
                Love += p + ";";
        }
    }
    public static List<GameItemInfo> GItems;
    static ItemSystem(){
        GItems = new List<GameItemInfo>();
        GItems.Add(new GameItemInfo(
            "蛋糕",
            "一种甜甜的食物，可以回复10点体力。",
            5,"cake",           //花费，图标
            10,0,               //HP点数，MP点数
            false,              //是否有扩展效果
            "谭娜·域零"          //有加成效果的成员名单
        ));

        GItems.Add(new GameItemInfo(
            "香肠",
            "真香，回复0点体力，3点魔力",
            1,"sausage",       
            0,3,               
            false,              
            "布莱·昴斯"          
        ));

        GItems.Add(new GameItemInfo(
            "香肠",
            "真香，回复0点体力，3点魔力",
            1,"sausage",       
            0,3,               
            false,              
            "布莱·昴斯"          
        ));

        GItems.Add(new GameItemInfo(
            "面包",
            "小麦的香气，回复10点体力，0点魔力",
            4,"bread",       
            10,0,               
            false,              
            "谭娜·霓悦"          
        ));

        GItems.Add(new GameItemInfo(
            "炒饭",
            "闻起来很香，回复15点体力，5点魔力",
            10,"fried-rice",       
            15,5,               
            false,              
            "世原·安诺","埃斯·洛里"          
        ));

        GItems.Add(new GameItemInfo(
            "拉面",
            "非常有嚼劲，回复5点体力，15点魔力",
            13,"ramen",       
            5,15,               
            false,              
            "埃斯·洛里","域零·占也"          
        ));

        GItems.Add(new GameItemInfo(
            "炒面",
            "香飘十里，回复20点体力，1点魔力",
            10,"sauerkraut",       
            20,1,               
            false,              
            "艾伦·思瑞","域零·占也"          
        ));

        GItems.Add(new GameItemInfo(
            "牛排套餐",
            "烤牛肉的滋滋声~回复30点体力，10点魔力",
            24,"steak",       
            20,20,               
            false,              
            "世原·安诺"       
        ));

        GItems.Add(new GameItemInfo(
            "鸡腿",
            "可能来自美女鸡？回复5点体力，10点魔力",
            6,"meat",       
            5,5,               
            false,              
            "雪郎·梦亭","艾伦·思瑞","兮·御冯"          
        ));

        GItems.Add(new GameItemInfo(
            "奶茶",
            "香味扑面而来，可回复12点体力，12点魔力",
            14,"iced-tea",       
            12,12,               
            true,              
            "兮·御冯","世原·安诺","布莱·昴斯","域零·占也"          
        ));

        GItems.Add(new GameItemInfo(
            "纯牛奶",
            "校长的推荐，可回复10点体力，5点魔力",
            7,"milk",       
            10,5,               
            false,              
            "兮·御冯","谭娜·霓悦"          
        ));

        GItems.Add(new GameItemInfo(
            "鲜橙汁",
            "可能不是鲜橙？可回复5点魔力",
            4,"orange-juice",       
            0,5,               
            false,              
            "布莱·昴斯","閠橘·芙莱"          
        ));

        GItems.Add(new GameItemInfo(
            "汽水",
            "香橙味。可回复1点体力，10点魔力",
            6,"soft-drink",       
            1,10,               
            false,              
            "布莱·昴斯","艾伦·思瑞","埃斯·洛里"          
        ));

        GItems.Add(new GameItemInfo(
            "矿泉水",
            "谁知道是不是自来水呢？可回复5点体力",
            2,"water",       
            5,0,               
            false,              
            "埃斯·洛里"          
        ));
    }
    public static List<GameItem> Get(){
        string[] temp = PlayerPrefs.GetString("Items","").Split(';');
        List<GameItem> list = new List<GameItem>();
        for(int i = 0;i < temp.Length;i++){
            int index = list.FindIndex(m => m.Name == temp[i]);
            if(index == -1){
                list.Add(new GameItem{Count = 1,Name = temp[i]});
            }else{
                list[index].Give();
            }
        }
        return list;
    }
    public static void UseItem(string Name){
        GameItemInfo gii = GItems.Find(m => m.Name == Name);
        if(gii.Extend){
            //特殊事件
        }

        //持有数量--
        string code = PlayerPrefs.GetString("Items","");
        code = code.Remove(code.IndexOf(Name),Name.Length + 1);
        PlayerPrefs.SetString("Items",code);
    }
}