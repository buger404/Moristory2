using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundPlayer
{
    public static List<AudioClip> audios = new List<AudioClip>();
    static SoundPlayer(){
        object[] audio = Resources.LoadAll("Sounds");
        foreach(AudioClip au in audio)
            audios.Add(au);
    }
    public static void Play(string tar){
        GameObject go = new GameObject("Audio: " + tar);
        go.transform.position = Vector3.zero;
        go.transform.parent = Camera.main.transform;
        
        AudioSource source = go.AddComponent<AudioSource>();
        source.clip = audios.Find(m => m.name == tar);
        source.volume = 0.9f + Random.Range(-0.2f,0.2f);
        source.Play();
        GameObject.Destroy(go, source.clip.length);
    }
}