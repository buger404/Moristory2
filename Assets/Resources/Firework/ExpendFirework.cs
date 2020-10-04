using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpendFirework : Firework
{
    public List<Sprite> RandomSprites;
    private void Awake() {
        base.GetComponent<SpriteRenderer>().sprite = 
            RandomSprites[Random.Range(0,RandomSprites.Count)];
        Destroy(base.gameObject,1.5f);
    }
    public override float AttackDeepth(){
        if(base.BindS.Name == "恶魔歌姬"){
            // 概率致死
            if(Random.Range(0,30) == 16) return 100f;
            return 0.3f;
        }
        return 0.3f;
    }
}
