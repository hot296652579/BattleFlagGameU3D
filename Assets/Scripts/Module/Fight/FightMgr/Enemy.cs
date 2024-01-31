using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : ModelBase
{
    public int newSortingOrder = 99;
    protected override void Start()
    {
        base.Start();

        data = GameApp.ConfigMgr.GetConfigDdata("enemy").GetDataById(Id);

        Type = int.Parse(this.data["Type"]);
        Attack = int.Parse(this.data["Attack"]);
        Step = int.Parse(this.data["Step"]);
        MaxHp = int.Parse(this.data["Hp"]);
        CurHp = MaxHp;
    }

    protected override void OnSelectCallBack(object arg)
    {
        base.OnSelectCallBack(arg);
        GameApp.ViewMgr.Open(ViewType.EnemyDesView, this);
    }

    protected override void OnUnSelectCallBack(object arg)
    {
        base.OnUnSelectCallBack(arg);
        GameApp.ViewMgr.Close((int)ViewType.EnemyDesView);
    }
}
