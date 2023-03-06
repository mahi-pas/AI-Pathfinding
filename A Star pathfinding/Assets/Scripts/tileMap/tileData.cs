using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class tileData
{
    [System.Serializable]
    public struct tilePosition
    {
        [System.Serializable]
        public enum tileToSpawn
        {
            Tree,
            Period,
            At,
        }
        public Vector2 pos;
        public tileToSpawn tile;
    }

    [System.Serializable]
    public struct rowData
    {

        public tilePosition[] cols;
    }

    public rowData[] rows = new rowData[0];

    public rowData[] makeRowData(int height, int width)
    {
        rowData[] myRows = new rowData[height];
        rows = myRows;

        for (int i = 0; i < height; i++)
        {
            rows[i].cols = new tilePosition[width];
        }

        return rows;
    }
}
