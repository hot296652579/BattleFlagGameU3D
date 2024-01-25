using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelBase : MonoBehaviour
{
    public int Id;
    public Dictionary<string, string> data;
    public int Step;
    public int Attack;
    public int Type;
    public int MaxHp;
    public int CurHp;

    public int RowIndex;
    public int ColIndex;
    public SpriteRenderer bodySp;
    public GameObject stopObj;
    public Animator ani;

    private void Awake()
    {
        bodySp = transform.Find("body").GetComponent<SpriteRenderer>();
        stopObj = transform.Find("stop").gameObject;
        ani = transform.Find("body").GetComponent<Animator>();
    }

    protected virtual void Start()
    {
        AddEvents();
    }

    protected virtual void AddEvents()
    {
        GameApp.MessageCenter.AddEvent(gameObject, Defines.OnSelectEvent, OnSelectCallBack);
        GameApp.MessageCenter.AddEvent(Defines.OnUnSelectEvent, OnUnSelectCallBack);
    }

    protected virtual void OnDestory()
    {
        GameApp.MessageCenter.RemoveEvent(gameObject, Defines.OnSelectEvent, OnSelectCallBack);
        GameApp.MessageCenter.RemoveEvent(Defines.OnUnSelectEvent, OnUnSelectCallBack);
    }

    //选中回掉
    protected virtual void OnSelectCallBack(System.Object arg)
    {
        Debug.Log("ModelBase OnSelectCallBack+++++");
      
        GameApp.MessageCenter.PostEvent(Defines.OnUnSelectEvent);
        GameApp.MapMgr.ShowStepGrid(this, Step);
    }

    //未选中回掉
    protected virtual void OnUnSelectCallBack(System.Object arg)
    {
        //bodySp.color = Color.white;
        GameApp.MapMgr.HideStepGrid(this, Step);
    }
}
