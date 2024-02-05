using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightEnemyUnit : FightUnitBase
{
    public override void Init()
    {
        base.Init();
        GameApp.FightWorldMgr.ResetHeros();
        GameApp.ViewMgr.Open(ViewType.TipView, "敌人回合");

        GameApp.CommandMgr.AddComand(new WaitCommand(1.25f));

        //等待一段时间 切换到玩家回合
        GameApp.CommandMgr.AddComand(new WaitCommand(0.2f, delegate ()
        {
            GameApp.FightWorldMgr.ChangeState(GameState.Player);
        }));
    }
}
