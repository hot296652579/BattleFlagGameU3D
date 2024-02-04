using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public enum BlockDirection
{
    none = -1,
    down,
    horizontal,
    left,
    left_down,
    left_up,
    right,
    right_down,
    right_up,
    up,
    vertical,
    max
}

public class MapMgr 
{
    private Tilemap tileMap;
    public Block[,] mapArr;

    public int RowCount;
    public int ColCount;

    public List<Sprite> dirSpArr;

    public void Init()
    {
        dirSpArr = new List<Sprite>();
        for(int i = 0; i < (int)BlockDirection.max; i++)
        {
            dirSpArr.Add(Resources.Load<Sprite>($"Icon/{(BlockDirection)i}"));
        }

        tileMap = GameObject.Find("Grid/ground").GetComponent<Tilemap>();

        //地图大小
        RowCount = 12;
        ColCount = 20;

        mapArr = new Block[RowCount, ColCount];
        List<Vector3Int> tempPosArr = new List<Vector3Int>();

        foreach(var pos in tileMap.cellBounds.allPositionsWithin)
        {
            if (tileMap.HasTile(pos))
            {
                tempPosArr.Add(pos);
            }
        }

        //将一维数组位置转换成二维数组的block 进行存储
        Object prefabObj = Resources.Load("Model/block");
        for (int i = 0; i< tempPosArr.Count;i++)
        {
            int row = i / ColCount;
            int col = i % ColCount;
            Block b = (Object.Instantiate(prefabObj) as GameObject).AddComponent<Block>();
            b.RowIndex = row;
            b.ColIndex = col;
            b.transform.position = tileMap.CellToWorld(tempPosArr[i]) + new Vector3(0.5f,0.5f,0);
            mapArr[row, col] = b;
        }
    }

    public Vector3 GetBlockPos(int row,int col)
    {
        return mapArr[row, col].transform.position;
    }

    public BlockType GetBlockType(int row,int col)
    {
        return mapArr[row, col].Type;
    }

    public void ChangeBlockType(int row,int col,BlockType type)
    {
        mapArr[row, col].Type = type;
    }

    //显示移动区域
    public void ShowStepGrid(ModelBase model,int step)
    {
        _BFS bfs = new _BFS(RowCount, ColCount);
        List<_BFS.Point> points = bfs.Search(model.RowIndex, model.ColIndex, step);
        for(int i = 0;i < points.Count; i++)
        {
            mapArr[points[i].RowIndex, points[i].ColIndex].ShowGrid(Color.blue);
        }
    }
    //隐藏移动区域
    public void HideStepGrid(ModelBase model,int step)
    {
        _BFS bfs = new _BFS(RowCount, ColCount);
        List<_BFS.Point> points = bfs.Search(model.RowIndex, model.ColIndex, step);
        for (int i = 0; i < points.Count; i++)
        {
            mapArr[points[i].RowIndex, points[i].ColIndex].HideGrid();
        }
    }

    public void SetBlockDir(int rowIndex,int colIndex,BlockDirection dir,Color color)
    {
        mapArr[rowIndex, colIndex].SetDirSp(dirSpArr[(int)dir], color);
    }

    //开始点 和 下一点 算出方向
    public BlockDirection GetDirection1(AStarPoint start,AStarPoint next)
    {
        int row_offset = next.RowIndex - start.RowIndex;
        int col_offset = next.ColIndex - start.ColIndex;

        if(row_offset == 0)
        {
            return BlockDirection.horizontal;
        }
        else if(col_offset == 0)
        {
            return BlockDirection.vertical;
        }

        return BlockDirection.none;
    }

    //终点 和 前一点 算出方向
    public BlockDirection GetDirection2(AStarPoint end,AStarPoint pre)
    {
        int row_offset = end.RowIndex - pre.RowIndex;
        int col_offset = end.ColIndex - pre.ColIndex;

        if(row_offset == 0 && col_offset > 0)
        {
            return BlockDirection.right;
        }else if (row_offset == 0 && col_offset < 0)
        {
            return BlockDirection.left;
        }
        else if (col_offset == 0 && row_offset > 0)
        {
            return BlockDirection.up;
        }
        else if (col_offset == 0 && row_offset < 0)
        {
            return BlockDirection.down;
        }

        return BlockDirection.none;
    }

    //三个点 算出方向
    public BlockDirection GetDirection3(AStarPoint pre,AStarPoint current,AStarPoint end)
    {
        BlockDirection dir = BlockDirection.none;

        int row_offset_1 = pre.RowIndex - current.RowIndex;
        int col_offset_1 = pre.ColIndex - current.ColIndex;
        int row_offset_2 = end.RowIndex - current.RowIndex;
        int col_offset_2 = end.ColIndex - current.ColIndex;

        int sum_row_offset = row_offset_1 + row_offset_2;
        int sum_col_offset = col_offset_1 + col_offset_2;

        if(sum_row_offset == 1 && sum_col_offset == -1)
        {
            dir = BlockDirection.left_up;
        } else if (sum_row_offset == 1 && sum_col_offset == 1)
        {
            dir = BlockDirection.right_up;
        }
        else if (sum_row_offset == -1 && sum_col_offset == -1)
        {
            dir = BlockDirection.left_down;
        }
        else if (sum_row_offset == -1 && sum_col_offset == 1)
        {
            dir = BlockDirection.right_down;
        }
        else
        {
            if(row_offset_1 == 0)
            {
                dir = BlockDirection.horizontal;
            }
            else
            {
                dir = BlockDirection.vertical;
            }
        }

        return dir;
    }

    public void ShowAttackStep(ModelBase model,int attckStep,Color color)
    {
        int minRow = model.RowIndex - attckStep >= 0 ? model.RowIndex - attckStep : 0;
        int minCol = model.ColIndex - attckStep >= 0 ? model.ColIndex - attckStep : 0;

        int maxRow = model.RowIndex + attckStep > RowCount - 1 ? RowCount - 1 : model.RowIndex + attckStep;
        int maxCol = model.ColIndex + attckStep > ColCount - 1 ? ColCount - 1 : model.ColIndex + attckStep;

        for(int row = minRow; row <= maxRow; row++)
        {
            for(int col = minCol;col <= maxCol; col++)
            {
                if(Mathf.Abs(model.RowIndex - row) + Mathf.Abs(model.ColIndex - col) <= attckStep)
                {
                    mapArr[row, col].ShowGrid(color);
                }
            }
        }
    }

    public void HideAttackStep(ModelBase model, int attckStep)
    {
        int minRow = model.RowIndex - attckStep >= 0 ? model.RowIndex - attckStep : 0;
        int minCol = model.ColIndex - attckStep >= 0 ? model.ColIndex - attckStep : 0;

        int maxRow = model.RowIndex + attckStep > RowCount - 1 ? RowCount - 1 : model.RowIndex + attckStep;
        int maxCol = model.ColIndex + attckStep > ColCount - 1 ? ColCount - 1 : model.ColIndex + attckStep;

        for (int row = minRow; row <= maxRow; row++)
        {
            for (int col = minCol; col <= maxCol; col++)
            {
                if (Mathf.Abs(model.RowIndex - row) + Mathf.Abs(model.ColIndex - col) <= attckStep)
                {
                    mapArr[row, col].HideGrid();
                }
            }
        }
    }
}
