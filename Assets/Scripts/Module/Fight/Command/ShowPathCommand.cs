using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//显示移动路径指令
public class ShowPathCommand : BaseCommand
{
    Collider2D pre; //鼠标之前检测到的2d碰撞盒
    Collider2D current; //鼠标当前检测的2d碰撞盒
    AStar astat;
    AStarPoint start;
    AStarPoint end;
    List<AStarPoint> prePaths;

   public ShowPathCommand(ModelBase model):base(model)
   {
        prePaths = new List<AStarPoint>();
        start = new AStarPoint(model.RowIndex, model.ColIndex);
        astat = new AStar(GameApp.MapMgr.RowCount, GameApp.MapMgr.ColCount);
   }

    public override bool Update(float dt)
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameApp.MessageCenter.PostEvent(Defines.OnUnSelectEvent);
            return true;
        }

        current = Tools.ScreenPointToRay2D(Camera.main, Input.mousePosition);
        if(current != null)
        {
            Debug.Log("Current: " + current);
            Debug.Log("Previous: " + pre);
            //当前盒子和之前碰撞检测的盒子不一样才进行路径检测
            if (!current.Equals(pre))
            {
                pre = current;
                Block b = current.GetComponent<Block>();

                if(b != null)
                {
                    //检测到block脚本进行驯鹿
                    end = new AStarPoint(b.RowIndex, b.ColIndex);
                    astat.FindPath(start, end, UpdatePth);
                }
                else
                {
                    for(int i = 0; i < prePaths.Count; i++)
                    {
                        GameApp.MapMgr.mapArr[prePaths[i].RowIndex, prePaths[i].ColIndex].SetDirSp(null, Color.white);
                    }
                    prePaths.Clear();
                }
            }
        }

        return false;
    }

    private void UpdatePth(List<AStarPoint> paths)
    {
        if(prePaths.Count != 0)
        {
            for(int i = 0; i < prePaths.Count; i++)
            {
                GameApp.MapMgr.mapArr[prePaths[i].RowIndex, prePaths[i].ColIndex].SetDirSp(null, Color.white);
            }
        }

        for(int i = 0; i < paths.Count; i++)
        {
            BlockDirection dir = BlockDirection.down;
            GameApp.MapMgr.SetBlockDir(paths[i].RowIndex, paths[i].ColIndex, dir, Color.yellow);
        }

        prePaths = paths;
    }
}
