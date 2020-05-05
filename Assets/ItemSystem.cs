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
        public GameItemInfo(string name,string describe,long cost,string icon,long hp,long mp,bool extend,params string[] love){
            Name = name;Describe = describe;Cost = cost;
            Icon = icon;HP = hp;MP = mp;Extend = extend;
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
            5,"cake.png",       //花费，图标
            10,0,               //HP点数，MP点数
            false,              //是否有扩展效果
            "谭娜·域零"          //有加成效果的成员名单
        ));

        GItems.Add(new GameItemInfo(
            "咖啡",
            "可以微微提神，回复5点体力，10点魔力",
            9,"coffee-cup.png",       
            5,10,               
            false,              
            "雪郎·梦亭","域零·占也","艾伦·思瑞","莎瑟·娜蕾"          
        ));

        GItems.Add(new GameItemInfo(
            "奶茶",
            "奶味和茶香味扑面而来，可回复12点体力，12点魔力",
            14,"iced-tea.png",       
            12,12,               
            true,              
            "兮·御冯","世原·安诺","布莱·昴斯","域零·占也"          
        ));

        GItems.Add(new GameItemInfo(
            "纯牛奶",
            "校长的推荐，可回复10点体力，5点魔力",
            7,"milk.png",       
            10,5,               
            false,              
            "兮·御冯","谭娜·霓悦"          
        ));

        GItems.Add(new GameItemInfo(
            "鲜橙汁",
            "可能不是鲜橙？可回复5点魔力",
            4,"orange-juice.png",       
            0,5,               
            false,              
            "布莱·昴斯","閠橘·芙莱"          
        ));

        GItems.Add(new GameItemInfo(
            "汽水",
            "香橙味。可回复1点体力，10点魔力",
            6,"soft-drink.png",       
            1,10,               
            false,              
            "布莱·昴斯","艾伦·思瑞","埃斯·洛里"          
        ));

        GItems.Add(new GameItemInfo(
            "矿泉水",
            "谁知道是不是自来水呢？可回复5点体力",
            2,"water.png",       
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