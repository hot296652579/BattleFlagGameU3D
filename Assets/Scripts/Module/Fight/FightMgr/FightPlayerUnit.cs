using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightPlayerUnit : FightUnitBase
{
    public override void Init()
    {
        base.Init();
        Debug.Log("玩家回合");
        GameApp.ViewMgr.Open(ViewType.TipView, "玩家回合");
    }
}
