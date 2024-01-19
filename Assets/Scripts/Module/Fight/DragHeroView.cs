using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//拖拽出来的图标
public class DragHeroView : BaseView
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //拖拽中跟随鼠标移动 显示时才进行移动
        if(_canvas.enabled == false)
        {
            return;
        }

        Vector2 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = worldPos;
    }

    public override void Open(params object[] args)
    {
        base.Open(args);
        transform.GetComponent<Image>().SetIcon(args[0].ToString());
    }
}
