using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : ModelBase
{
    public void Init(Dictionary<string,string> data, int row,int col)
    {
        this.data = data;
        this.RowIndex = row;
        this.ColIndex = col;
        Id = int.Parse(this.data["Id"]);
        Type = int.Parse(this.data["Type"]);
        Attack = int.Parse(this.data["Attack"]);
        Step = int.Parse(this.data["Step"]);
        MaxHp = int.Parse(this.data["Hp"]);
        CurHp = MaxHp;
    }

    protected override void OnSelectCallBack(object arg)
    {
        if(GameApp.FightWorldMgr.state == GameState.Player)
        {
            //玩家回合才可以
            if(IsStop == true)
            {
                return;
            }

            if(GameApp.CommandMgr.isRunningCommand == true)
            {
                return;
            }

            GameApp.CommandMgr.AddComand(new ShowPathCommand(this));
            base.OnSelectCallBack(arg);
            GameApp.ViewMgr.Open(ViewType.HeroDesView, this);
        }
    }

    protected override void OnUnSelectCallBack(object arg)
    {
        base.OnUnSelectCallBack(arg);
        GameApp.ViewMgr.Close((int)ViewType.HeroDesView);
    }
}
