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
        if(Name == "汉堡"){
            //还不知道可以做啥
        }

        //持有数量--
        string code = PlayerPrefs.GetString("Items","");
        code = code.Remove(code.IndexOf(Name),Name.Length + 1);
        PlayerPrefs.SetString("Items",code);
    }
}