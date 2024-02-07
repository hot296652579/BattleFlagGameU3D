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

        //敌人移动 使用技能等
        for(int i = 0; i < GameApp.FightWorldMgr.enemys.Count; i++)
        {
            Enemy enemy = GameApp.FightWorldMgr.enemys[i];
            GameApp.CommandMgr.AddComand(new WaitCommand(0.25f));
            GameApp.CommandMgr.AddComand(new AIMoveCommand(enemy));
            GameApp.CommandMgr.AddComand(new WaitCommand(0.25f));
            GameApp.CommandMgr.AddComand(new SkillCommand(enemy));
        }

        //等待一段时间 切换到玩家回合
        GameApp.CommandMgr.AddComand(new WaitCommand(0.2f, delegate ()
        {
            GameApp.FightWorldMgr.ChangeState(GameState.Player);
        }));
    }
}
