using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AStarPoint
{
    public int RowIndex;
    public int ColIndex;
    public int G;//当前节点到开始点的距离
    public int H;//当前节点到终点的距离
    public int F; //F = G + H
    public AStarPoint Parent;

    public AStarPoint(int row,int col)
    {
        this.RowIndex = row;
        this.ColIndex = col;
        Parent = null;
    }

    public AStarPoint(int row, int col,AStarPoint parent)
    {
        this.RowIndex = row;
        this.ColIndex = col;
        this.Parent = parent;
    }

    public int GetG()
    {
        int _g = 0;
        AStarPoint parent = this.Parent;
        while (parent != null)
        {
            _g = _g + 1;
            parent = parent.Parent;
        }
        return _g;
    }

    public int GetH(AStarPoint end)
    {
        return Mathf.Abs(RowIndex - end.RowIndex) + Mathf.Abs(ColIndex - end.ColIndex);
    }
}

public class AStar
{
    private int rowCount;
    private int colCount;
    private List<AStarPoint> open;
    private Dictionary<string, AStarPoint> close;
    private AStarPoint start;
    private AStarPoint end;


    public AStar(int rowCount,int colCount)
    {
        this.rowCount = rowCount;
        this.colCount = colCount;
        open = new List<AStarPoint>();
        close = new Dictionary<string, AStarPoint>();
    }

    //找到open表的路径点
    public AStarPoint IsInOpen(int rowIndex,int colIndex)
    {
        for(int i = 0;i < open.Count; i++)
        {
            if(open[i].RowIndex == rowIndex && open[i].ColIndex == colIndex)
            {
                return open[i];
            }
        }

        return null;
    }

    //某个点是否在关闭close表
    public bool IsInClose(int rowIndex, int colIndex)
    {
        if (close.ContainsKey($"{rowIndex}_{colIndex}"))
        {
            return true;
        }
        return false;
    }

    /*A星思路
     * 1.将起点添加到open表
     * 2.查找open表中 f值最小的路径点
     * 3.将此路径点 从open移除，并添加到close表
     * 4.再将当前路径点周围的点添加到open表中
     * 5.判断终点是否再open表中，不在就从步骤2继续执行
     */
    public bool FindPath(AStarPoint start,AStarPoint end,System.Action<List<AStarPoint>> findCallBack)
    {
        this.start = start;
        this.end = end;
        open = new List<AStarPoint>();
        close = new Dictionary<string, AStarPoint>();

        //1.将起点添加到open
        open.Add(start);
        while (true)
        {
            //2.找到open中最小f值的路径点
            AStarPoint current = GetMinFFromInOpen();

            if(current == null)
            {
                return false;//没路了
            }
            else
            {
                //3.从open中移除 添加到close
                open.Remove(current);
                close.Add($"{current.RowIndex}_{current.ColIndex}",current);

                //4.将当前点的周围点添加到open表
                AddAroundInOpen(current);
                //5.判断终点是否再open
                AStarPoint endPoint = IsInOpen(end.RowIndex, end.ColIndex);
                if(endPoint != null)
                {
                    findCallBack(GetPath(endPoint));
                    return true;//找到了路径
                }

                open.Sort(OpenSort);
            }
        }
    }

    public List<AStarPoint> GetPath(AStarPoint point)
    {
        List<AStarPoint> paths = new List<AStarPoint>();
        paths.Add(point);
        AStarPoint parent = point.Parent;
        while(parent != null)
        {
            paths.Add(parent);
            parent = parent.Parent;
        }

        paths.Reverse();
        return paths;
    }


    public int OpenSort(AStarPoint a,AStarPoint b)
    {
        return a.F - b.F;
    }

    public void AddAroundInOpen(AStarPoint current)
    {
        //up
        if(current.RowIndex - 1 >= 0)
        {
            AddOpen(current, current.RowIndex - 1, current.ColIndex);
        }

        //down
        if(current.RowIndex + 1 < rowCount)
        {
            AddOpen(current, current.RowIndex + 1, current.ColIndex);
        }
        //left
        if(current.ColIndex - 1 >= 0)
        {
            AddOpen(current, current.RowIndex, current.ColIndex - 1);
        }
        //right
        if(current.ColIndex + 1 < colCount)
        {
            AddOpen(current, current.RowIndex, current.ColIndex + 1);
        }
    }

    public void AddOpen(AStarPoint current,int row, int col)
    {
        if(IsInClose(row,col) == false && IsInOpen(row,col) == null && GameApp.MapMgr.GetBlockType(row, col) == BlockType.Null)
        {
            AStarPoint newPoint = new AStarPoint(row, col, current);
            newPoint.G = newPoint.GetG();
            newPoint.H = newPoint.GetH(end);
            newPoint.F = newPoint.G + newPoint.H;
            open.Add(newPoint);
        }
    }

    public AStarPoint GetMinFFromInOpen()
    {
        if(open.Count == 0)
        {
            return null;
        }

        return open[0];
    }
}
