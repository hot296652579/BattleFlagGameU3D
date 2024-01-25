using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlockType
{
    Null,
    Obstacle
}

//地图单元格
public class Block : MonoBehaviour
{
    public int RowIndex;
    public int ColIndex;
    public BlockType Type;
    private SpriteRenderer selectSp; //选中状态图
    private SpriteRenderer gridSp; //网格图
    private SpriteRenderer dirSp; //移动方向

    private void Awake()
    {
        selectSp = transform.Find("select").GetComponent<SpriteRenderer>();
        gridSp = transform.Find("grid").GetComponent<SpriteRenderer>();
        dirSp = transform.Find("dir").GetComponent<SpriteRenderer>();

        GameApp.MessageCenter.AddEvent(gameObject, Defines.OnSelectEvent, OnSelectCallBack);
    }

    void OnSelectCallBack(System.Object arg)
    {
        GameApp.MessageCenter.PostEvent(Defines.OnUnSelectEvent);
    }

    private void OnDestroy()
    {
        GameApp.MessageCenter.RemoveEvent(gameObject, Defines.OnSelectEvent, OnSelectCallBack);
    }

    private void OnMouseEnter()
    {
        selectSp.enabled = true;
    }

    private void OnMouseExit()
    {
        selectSp.enabled = false;
    }

    public void ShowGrid(Color color)
    {
        gridSp.enabled = true;
        gridSp.color = color;
    }

    public void HideGrid()
    {
        gridSp.enabled = false;
    }
}
