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
                // 点击 UI
            }
            else
            {
                Vector3 mousePos = Input.mousePosition;
                Ray ray = Camera.main.ScreenPointToRay(mousePos);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

                if (hit.collider != null)
                {
                    // 检测到碰撞体
                    if (hit.collider.CompareTag("Hero"))
                    {
                        // 如果点击的是英雄，执行选中操作
                        GameApp.MessageCenter.PostEvent(hit.collider.gameObject, Defines.OnSelectEvent);
                    }
                    else if (hit.collider.CompareTag("Block"))
                    {
                        // 如果点击的是 Block，执行取消选中英雄的操作
                        GameApp.MessageCenter.PostEvent(Defines.OnUnSelectEvent);
                    }
                }
            }
        }
    }
}
