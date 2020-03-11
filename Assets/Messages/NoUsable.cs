using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoUsable : MonoBehaviour
{
    private static int NoUse = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseUp() 
    {
        NoUsable.NoUse++;
        switch(NoUsable.NoUse)
        {
            case(1):
                MessageCreator.CreateMsg("啊哦","(⊙﹏⊙)这个功能似乎还没做...");
                break;
            case(2):
                MessageCreator.CreateMsg("啊哦","(⊙﹏⊙)真的没有做啦...");
                break;
            case(3):
                MessageCreator.CreateMsg("傻逼404","┑(￣Д ￣)┍大家都知道shit404很懒呀...");
                break;
            case(4):
                MessageCreator.CreateMsg("生气气","━┳━　━┳━为什么你不愿意听我的劝告呢");
                break;
            case(5):
                MessageCreator.CreateMsg("不理你了哦","你没有更重要的事情要做了吗？-UT");
                break;
            default:
                MessageCreator.CreateMsg("null","气到没有文字");
                break;
        }
        
    }
}
