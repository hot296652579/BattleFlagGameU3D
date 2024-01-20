 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HeroItem : MonoBehaviour,IBeginDragHandler,IEndDragHandler,IDragHandler
{
    Dictionary<string, string> data;

    // Start is called before the first frame update
    void Start() 
    {
        transform.Find("icon").GetComponent<Image>().SetIcon(data["Icon"]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(Dictionary<string,string> data)
    {
        this.data = data;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        GameApp.ViewMgr.Open(ViewType.DragHeroView,data["Icon"]);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GameApp.ViewMgr.Close((int)ViewType.DragHeroView);

        //检测拖拽后的位置是否有block
        Tools.ScreenPointToRay2D(eventData.pressEventCamera, eventData.position, delegate (Collider2D col) {
            if(col != null)
            {
                Block b = col.GetComponent<Block>();
                if(b != null)
                {
                    Debug.Log(b);

                    Destroy(gameObject);
                    //创建英雄物体
                    GameApp.FightWorldMgr.AddHero(b, data);
                }
            }
        });
    }

    public void OnDrag(PointerEventData eventData)
    {
        
    }
}
