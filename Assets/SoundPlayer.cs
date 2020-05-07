using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundPlayer
{
    public static List<AudioClip> audios = new List<AudioClip>();
    static SoundPlayer(){
        string[] files = Directory.GetFiles("Assets/Resources/Sounds/");
        foreach(string f in files){
            string rf = f.Replace("Assets/Resources/Sounds/","").Split('.')[0];
            audios.Add((AudioClip)Resources.Load("Sounds/" + rf));
            Debug.Log("loaded:" + rf);
        }
            
    }
    public static void Play(string tar){
        GameObject go = new GameObject("Audio: " + tar);
        go.transform.position = Vector3.zero;
        go.transform.parent = Camera.main.transform;
        
        AudioSource source = go.AddComponent<AudioSource>();
        source.clip = audios.Find(m => m.name == tar);
        source.Play();
        GameObject.Destroy(go, source.clip.length);
    }
}