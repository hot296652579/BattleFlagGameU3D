using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UserInputMgr
{
    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                //点击到了UI
            }
            else
            {
                Tools.ScreenPointToRay2D(Camera.main, Input.mousePosition, delegate (Collider2D col)
                 {
                     if(col != null)
                     {
                         //检测到又碰撞体的物体
                         GameApp.MessageCenter.PostEvent(col.gameObject, Defines.OnSelectEvent);
                     }
                     else
                     {
                         GameApp.MessageCenter.PostEvent(col.gameObject, Defines.OnUnSelectEvent);
                     }
                 });
            }
        }
    }
}
