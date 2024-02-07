using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class _BFS
{
    public class Point
    {
        public int RowIndex;
        public int ColIndex;
        public Point Father;

        public Point(int row,int col)
        {
            this.RowIndex = row;
            this.ColIndex = col;
        }

        public Point(int row,int col,Point father)
        {
            this.RowIndex = row;
            this.ColIndex = col;
            this.Father = father;
        }
    }

    public int RowCount;
    public int ColCount;

    public Dictionary<string, Point> finds;//存储找到的点的字典（key:点的行列拼接的字符串 value:搜索点)

    public _BFS(int row,int col)
    {
        finds = new Dictionary<string, Point>();
        this.RowCount = row;
        this.ColCount = col;
    }

    //搜索行走区域 row开始点的行坐标 col开始点的列坐标 step步数
    public List<Point> Search(int row,int col,int step)
    {
        //定义搜索集合
        List<Point> searchs = new List<Point>();
        //开始点
        Point startPoint = new Point(row, col);
        //将开始点存储到搜索集合
        searchs.Add(startPoint);
        //默认开始点已找到
        finds.Add($"{row}_{col}", startPoint);

        for (int i=0;i<step;i++)
        {
            //定义一个临时集合 用于存储目前找到满足条件的点
            List<Point> temps = new List<Point>();
            for(int j = 0; j < searchs.Count; j++)
            {
                Point current = searchs[j];
                //查找当前点四周的点
                FindAroundPoints(current, temps);
            }

            if(temps.Count == 0)
            {
                break;//死路了
            }

            searchs.Clear();
            //将临时集合的点添加到搜索集合
            searchs.AddRange(temps);
        }

        //将查找到的点转换成集合
        return finds.Values.ToList();
    }

    public void FindAroundPoints(Point current,List<Point> temps)
    {
        //up
        if(current.RowIndex - 1 >= 0)
        {
            AddFinds(current.RowIndex - 1, current.ColIndex, current,temps);
        }
        //down
        if(current.RowIndex + 1 < RowCount)
        {
            AddFinds(current.RowIndex + 1, current.ColIndex, current, temps);
        }
        //left
        if(current.ColIndex - 1 >= 0)
        {
            AddFinds(current.RowIndex, current.ColIndex - 1, current, temps);
        }
        //right
        if(current.ColIndex + 1 < ColCount)
        {
            AddFinds(current.RowIndex, current.ColIndex + 1, current, temps);
        }
    }

    public void AddFinds(int row,int col,Point father,List<Point> temps)
    {
        //不在查找的字典里 或不是障碍物才加入查找字典
        if (finds.ContainsKey($"{row}_{col}") == false && GameApp.MapMgr.GetBlockType(row,col) != BlockType.Obstacle)
        {
            Point p = new Point(row, col, father);
            //添加到查找到的字典
            finds.Add($"{row}_{col}", p);
            //添加到临时集合 用于下次继续查找
            temps.Add(p);
        }
    }

    //寻找可移动的点 离终点最近的路径集合
    public List<Point> FindMinPath(ModelBase model,int step,int endRowIndex,int endColIndex)
    {
        List<Point> results = Search(model.RowIndex, model.ColIndex, step);
        if(results.Count == 0)
        {
            return null;
        }
        else
        {
            Point minPoint = results[0];
            int min_dis = Mathf.Abs(minPoint.RowIndex - endRowIndex) - Mathf.Abs(minPoint.ColIndex - endColIndex);
            for(int i = 1; i < results.Count; i++)
            {
                int temp_dis = Mathf.Abs(results[i].RowIndex - endRowIndex) + Mathf.Abs(results[i].ColIndex - endColIndex);
                if(temp_dis < min_dis)
                {
                    min_dis = temp_dis;
                    minPoint = results[i];
                }
            }

            List<Point> paths = new List<Point>();
            Point current = minPoint.Father;
            paths.Add(minPoint);
            while (current != null)
            {
                paths.Add(current);
                current = current.Father;
            }
            paths.Reverse();//倒序
            return paths;
        }
    }
}
