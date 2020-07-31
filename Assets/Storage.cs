using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Linq.Expressions;
using System.Data;
using UnityEngine;
public class Storage
{
    private static DataTable dt = new DataTable();
    public static string Get(string name){
        return DataCenter.Get(name,"");
    }
    public static void Set(string name,string value){
        DataCenter.Put(name,value);
    }
    /// <summary>
    /// 计算表达式
    /// </summary>
    /// <param name="expression">表达式</param>
    /// <returns>结算结果</returns>
    public static object Compute(string expression){
        return dt.Compute(expression.Replace("!=","<>"), null);
    }
    /// <summary>
    /// 根据存档数据格式化后计算表达式
    /// </summary>
    /// <param name="expression">源表达式</param>
    /// <returns>计算结果</returns>
    public static object Condition(string expression){   
        return Compute(Deepin(expression));
    }
    /// <summary>
    /// 将文本使用存档数据格式化
    /// </summary>
    /// <param name="expression">源文本</param>
    /// <returns>格式化结果</returns>
    public static string Deepin(string expression){
        string s = expression;
        //常量替换
        s = s.Replace("[step]","1.435");
        s = s.Replace("FACE_RIGHT","2");
        s = s.Replace("FACE_LEFT","1");
        s = s.Replace("FACE_UP","3");
        s = s.Replace("FACE_DOWN","0");
        s = s.Replace("FACE",GameConfig.FACE.ToString());
        if(GameConfig.Controller != null){
            s = s.Replace("[face]",GameConfig.Controller.GetComponent<RPGEvent>().Direction.ToString());
        }

        //根据存档替换
        string[] t = s.Replace("]","[").Split('[');
        s="";
        for(int i = 0;i < t.Length;i++){
            if(i%2 == 0){
                s += t[i];
            }else{
                s += DataCenter.Get(t[i],"0");
            }
        }    
        t = s.Replace("}","{").Split('{');
        s = "";
        for(int i = 0;i < t.Length;i++){
            if(i%2 == 0){
                s += t[i];
            }else{
                s += DataCenter.Get(t[i],"");
            }
        }    
        Debug.Log("Replaced:" + s);
        return s;
    }
}