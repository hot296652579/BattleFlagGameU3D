using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMoveCommand : BaseCommand
{
    Enemy enemy;
    _BFS bfs;
    List<_BFS.Point> paths;
    _BFS.Point current;
    int pathIndex;
    ModelBase target;

    public AIMoveCommand(Enemy enemy) : base(enemy)
    {
        this.enemy = enemy;
        bfs = new _BFS(GameApp.MapMgr.RowCount, GameApp.MapMgr.ColCount);
        paths = new List<_BFS.Point>();
    }

    public override void Do()
    {
        base.Do();

        target = GameApp.FightWorldMgr.GetMinDisHero(enemy);

        if(target == null)
        {
            isFinish = true;
        }
        else
        {
            paths = bfs.FindMinPath(this.enemy, this.enemy.Step, target.RowIndex, target.ColIndex);

            if(paths == null)
            {
                isFinish = true;//没路 随机一个点做移动
            }
            else
            {
                //将当前敌人位置 设置null 可移动
                GameApp.MapMgr.ChangeBlockType(this.enemy.RowIndex, this.enemy.ColIndex, BlockType.Null);
            }
        }
    }

    public override bool Update(float dt)
    {
        if(paths.Count == 0)
        {
            return base.Update(dt);
        }
        else
        {
            current = paths[pathIndex];
            if(model.Move(current.RowIndex,current.ColIndex,dt * 5) == true)
            {
                pathIndex++;
                if(pathIndex > paths.Count - 1)
                {
                    enemy.PlayAni("idle");
                    GameApp.MapMgr.ChangeBlockType(enemy.RowIndex, enemy.ColIndex, BlockType.Obstacle);
                    return true;
                }
            }
        }
        model.PlayAni("move");
        return false;
    }
}
