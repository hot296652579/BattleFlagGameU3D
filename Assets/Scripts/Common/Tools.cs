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
}
