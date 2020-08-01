using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Xml;

public class DataCenter
{
    /**--森之物语2 存档协定-----------------------
        Money：玩家金钱数量
        Pt：玩家学分
        name：玩家名字
        scene：存档所在的场景名
        scenecode：存档的场景数据
        mapdirection：玩家地图朝向
        mapx：玩家X坐标
        mapz：玩家Z坐标
        savesentence：存档句子
        AP：剧情进度
        其他字段：RPG脚本任意使用
    --**/
    [System.Serializable]
    public struct Key{
        public string Name;
        public string Content;
        public void Change(string Value){
            Content = Value;
        }
    }
    [System.Serializable]
    public struct KeyCollection{
        public List<Key> Data;
    }
    public static KeyCollection Saves;
    public static string Get(string Key,string Defaults = ""){
        int index = Saves.Data.FindIndex(m => m.Name == Key);
        if(index == -1) return Defaults;
        Debug.Log("fetched " + index + " with " + Saves.Data[index].Content);
        return Saves.Data[index].Content;
    }
    public static void Put(string Key,string Text){
        int index = Saves.Data.FindIndex(m => m.Name == Key);
        if(index == -1) Saves.Data.Add(new Key{Name = Key, Content = Text});
        if(index != -1) {
            Key key = Saves.Data[index];
            key.Content = Text;
            Saves.Data[index] = key;
            Debug.Log("changed " + index + " with " + Text);
        }
        if(index == -1) Debug.Log("pushed " + Key + " with " + Text);
    }
    public static void Load(){
        string[] Data = PlayerPrefs.GetString("global","").Split('丨');
        try{
            JsonUtility.FromJsonOverwrite(Data[0], ItemSystem.PItems);
            JsonUtility.FromJsonOverwrite(Data[1], Saves);
            JsonUtility.FromJsonOverwrite(Data[2], TeamController.Team);
        }catch{
            MessageCreator.CreateMsg("存档损坏","≧ ﹏ ≦ 存档因为不明原因无法读取！");
        }
        Debug.Log("reload " + ItemSystem.PItems.Data.Count + " items, " + Saves.Data.Count + " value.");
    }
    public static void Save(){
        string Items = JsonUtility.ToJson(ItemSystem.PItems);
        string Keys = JsonUtility.ToJson(Saves);
        string Teams = JsonUtility.ToJson(TeamController.Team);
        string Data = Items + "丨" + Keys + "丨" + Teams;
        Debug.Log("save order:" + Data);
        PlayerPrefs.SetString("global", Data);
        PlayerPrefs.Save();
    }

    static DataCenter(){
        Saves.Data = new List<Key>();
        Debug.Log("init datacenter");
    }
}