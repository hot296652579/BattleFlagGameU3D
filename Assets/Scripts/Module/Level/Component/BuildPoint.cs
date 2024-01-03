using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildPoint : MonoBehaviour
{
    public int LevelId;//设置关卡id

    public void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("trigger enter");
        GameApp.MessageCenter.PostEvent(Defines.ShowLevelDesEvent, LevelId);
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log("trigger exit");
        GameApp.MessageCenter.PostEvent(Defines.HideLevelDesEvent);
    }
}
