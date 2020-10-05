using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicExchange : MonoBehaviour
{
    // Start is called before the first frame update
    public string ExMagic = "";
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject != GameConfig.Controller) return;
        GameConfig.ExS = ExMagic;
        GameConfig.ExMObj = this.gameObject;
    }
    private void OnTriggerExit(Collider other) {
        if(other.gameObject != GameConfig.Controller) return;
        GameConfig.ExS = "";
    }
    private void Awake() {
        ReLoad();
    }
    public void ReLoad(){
        SkillManager.Skill s = SkillManager.S.Find(m => m.Name == ExMagic);
        this.transform.Find("Icon").GetComponent<SpriteRenderer>().sprite = 
        Resources.Load<Sprite>("Job\\s" + (int)s.Job);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
