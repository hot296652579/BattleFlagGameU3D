using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//工具类
public static class Tools 
{
    public static void SetIcon(this UnityEngine.UI.Image img,string res)
    {
        img.sprite = Resources.Load<Sprite>($"Icon/{res}");
    }

    //检测鼠标位置是否有2d碰撞物体
    public static void ScreenPointToRay2D(Camera cam,Vector2 mousePos,System.Action<Collider2D> callback)
    {
        Vector3 worldPos = cam.ScreenToViewportPoint(mousePos);
        Collider2D col = Physics2D.OverlapCircle(worldPos, 0.02f);
        callback?.Invoke(col);
    }
}
