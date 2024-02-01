using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCommand : BaseCommand
{
    private List<AStarPoint> paths;
    private AStarPoint current;
    private int pathIndex;

    //移动前的行列坐标
    private int preRowIndex;
    private int preColIndex;

    public MoveCommand(ModelBase model):base(model)
    {

    }

    public MoveCommand(ModelBase model,List<AStarPoint> paths) : base(model)
    {
        this.paths = paths;
        pathIndex = 0;
    }

    public override void Do()
    {
        base.Do();
        this.preColIndex = this.model.ColIndex;
        this.preRowIndex = this.model.RowIndex;

        GameApp.MapMgr.ChangeBlockType(this.model.RowIndex, this.model.ColIndex, BlockType.Null);
    }

    public override bool Update(float dt)
    {
        current = this.paths[pathIndex];
        if(this.model.Move(current.RowIndex,current.ColIndex,dt * 5))
        {
            pathIndex++;
            if(pathIndex > paths.Count - 1)
            {
                this.model.PlayAni("idle");
                GameApp.MapMgr.ChangeBlockType(this.model.RowIndex, this.model.ColIndex, BlockType.Obstacle);

                //显示选项界面
                GameApp.ViewMgr.Open(ViewType.SelectOptionView, this.model.data["Event"], (Vector2)this.model.transform.position);
                return true;
            }
        }
        this.model.PlayAni("move");
        return false;
    }

    //撤销
    public override void UnDo()
    {
        base.UnDo();

        Vector3 pos = GameApp.MapMgr.GetBlockPos(preRowIndex, preColIndex);
        pos.z = this.model.transform.position.z;
        this.model.transform.position = pos;
        GameApp.MapMgr.ChangeBlockType(this.model.RowIndex, this.model.ColIndex, BlockType.Null);
        this.model.RowIndex = preRowIndex;
        this.model.ColIndex = preColIndex;
        GameApp.MapMgr.ChangeBlockType(this.model.RowIndex, this.model.ColIndex, BlockType.Obstacle);
    }
}
