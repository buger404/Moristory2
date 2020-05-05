using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaySystem : MonoBehaviour
{
    public bool Disabled = true;
    public static List<ItemSystem.GameItemInfo> Shopping;
    public static int NowShop;
    public static string Owner;
    public static string Post;
    public static void CreateShop(string shops,string cuts,string owner,string post){
        Shopping = new List<ItemSystem.GameItemInfo>();
        NowShop = 0;Owner = owner;Post = post;
        string[] cut = cuts.Split(';'),shop = shops.Split(';');

        for(int i = 0;i < shop.Length;i++){
            ItemSystem.GameItemInfo gmi = ItemSystem.GItems.Find(m => m.Name == shop[i]);
            gmi.Cut = float.Parse(cut[i]);
            Shopping.Add(gmi);
        }  

        GameObject fab = (GameObject)Resources.Load("Prefabs\\PayCanvas");
        GameObject box = Instantiate(fab,new Vector3(0,0,0),Quaternion.identity);
		box.SetActive(true);
    }

    private void OnMouseUp() {
        if(Disabled) return;

        if(this.name == "NextBtn"){
            if(NowShop + 1 >= Shopping.Count) return;
            NowShop++; UpdateUI();
        }
        if(this.name == "PrevBtn"){
            if(NowShop - 1 < 0) return;
            NowShop--; UpdateUI();
        }
        if(this.name == "Pay"){
            GameObject fab = (GameObject)Resources.Load("Prefabs\\Fallen");
            GameObject box = Instantiate(fab,new Vector3(0,0,0),Quaternion.identity);
            box.GetComponent<Image>().sprite = this.transform.Find("Icon").GetComponent<Image>().sprite;
            box.SetActive(true);
            Destroy(box,2.5f);
        }  
        if(this.name == "NoWay"){
            this.GetComponent<Animator>().SetFloat("Speed",-1);
            this.GetComponent<Animator>().Play("PayCan",0,1.0f);
        }  
    }

    public void AniEnter(){
        if(this.GetComponent<Animator>().GetFloat("Speed") == -1)
            Destroy(this.gameObject);
    }
    public void AniEnd(){
        if(this.GetComponent<Animator>().GetFloat("Speed") != -1)
            Disabled = false;
    }

    void Awake()
    {
        UpdateUI();
    }

    public void UpdateUI(){
        if(this.name != "PayCanvas") return;

        this.transform.Find("PrevBtn").gameObject.SetActive(NowShop > 0 && Shopping.Count > 1);
        this.transform.Find("NextBtn").gameObject.SetActive(NowShop < Shopping.Count - 1 && Shopping.Count > 1);
        
        ItemSystem.GameItemInfo gmi = Shopping[NowShop];

        this.transform.Find("CutOff").gameObject.SetActive(gmi.Cut != 1);
        this.transform.Find("Cuts").gameObject.SetActive(gmi.Cut != 1);

        string Cut = gmi.Cut.ToString().Replace("0.","");
        string[] Num1 = {"1","2","3","4","5","6","7","8","9"};
        string[] Num2 = {"一","二","三","四","五","六","七","八","九"};
        for(int i = 0;i < Num1.Length;i++) Cut = Cut.Replace(Num1[i],Num2[i]);
        Cut += "折优惠";

        this.transform.Find("Cuts").GetComponent<Text>().text = Cut;
        
        this.transform.Find("Exchange").GetComponent<Text>().text = 
                $"<b>{Owner}</b>想要和你交易“<b>{gmi.Name}x1</b>”";
        this.transform.Find("Tips").GetComponent<Text>().text = 
                $"{gmi.Describe}";
        this.transform.Find("Say").GetComponent<Text>().text = 
                $"{Post}";

        this.transform.Find("PayTips").GetComponent<Text>().text = 
                $"G{(int)(gmi.Cost * gmi.Cut * 10) / 10}";

        /** TODO
        this.transform.Find("Coins").GetComponent<Text>().text = 
                $"余额 G{Coins}";   
        **/

         string LovePerson = "";
         /** TODO
            查找道具的喜爱者然后从队伍中筛选出
         **/

        this.transform.Find("LikeBar").gameObject.SetActive(LovePerson != "");
        this.transform.Find("Role").gameObject.SetActive(LovePerson != "");
        this.transform.Find("DislikeBar").gameObject.SetActive(LovePerson == ""); 

        this.transform.Find("Role").GetComponent<WalkingAnimate>().character = LovePerson;

        this.transform.Find("People").GetComponent<Text>().text = 
                LovePerson != "" ?
                $"队伍里的{LovePerson}表示很想要！" :
                $"队伍里没有人特别想要。";
    }
    // Update is called once per frame
    void Update()
    {
        
    }


}
