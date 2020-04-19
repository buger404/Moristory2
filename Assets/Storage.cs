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
        return PlayerPrefs.GetString(name,"");
    }
    public static void Set(string name,string value){
        PlayerPrefs.SetString(name,value);
    }
    public static object Compute(string expression){
        return dt.Compute(expression.Replace("!=","<>"), null);
    }
    public static object Condition(string expression){   
        return Compute(Deepin(expression));
    }
    public static string Deepin(string expression){
        string[] t = expression.Replace("]","[").Split('[');
        string s = "";
        for(int i = 0;i < t.Length;i++){
            if(i%2 == 0){
                s += t[i];
            }else{
                s += PlayerPrefs.GetString(t[i],"0");
            }
        }    
        t = s.Replace("}","{").Split('{');
        s = "";
        for(int i = 0;i < t.Length;i++){
            if(i%2 == 0){
                s += t[i];
            }else{
                s += PlayerPrefs.GetString(t[i],"");
            }
        }    
        Debug.Log("Replaced:" + s);
        return s;
    }
}