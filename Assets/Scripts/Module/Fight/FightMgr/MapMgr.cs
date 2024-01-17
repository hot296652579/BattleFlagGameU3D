using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapMgr 
{
    private Tilemap tileMap;
    public Block[,] mapArr;

    public int RowCount;
    public int ColCount;

    public void Init()
    {
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
}
