using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEditor;

public class tileMap : MonoBehaviour
{
    private int mHeight, mWidth;

    public GameObject tree, at, period;

    string smallMap;
    string largeMap;

    public bool useSmallMap, useLargeMap;

    public tileData t;

    GameObject mapTile;

    GameObject[,] mapArray;


    /* UNITY FUNCTIONS */
    void Awake()
    {

    }

    void Start()
    {
      

               
    }

    void Update()
    {

    }


    /* DATA MANAGEMENT */

    [ContextMenu("Create Map")]
    void loadMap( )
    {
        smallMap = File.ReadAllText(Application.dataPath + "/Maps/lak104d.map");
        largeMap = File.ReadAllText(Application.dataPath + "/Maps/AR0011SR.map");

        string fileInput = smallMap;
        bool isMap = false;

        if (useSmallMap)
        {
            fileInput = smallMap;
        }
        if (useLargeMap)
        {
            fileInput = largeMap;
        }
      

        string[] f = fileInput.Split(new string[] { "\n", "\r", "\r\n" }, System.StringSplitOptions.RemoveEmptyEntries);

        Debug.Log(f[1]);
        Debug.Log(f[2]);

        string height = new string(f[1].Where(char.IsDigit).ToArray());
        string width = new string(f[2].Where(char.IsDigit).ToArray());

        if (f[1].Length > 0 && f[2].Length > 0)
        {
            int.TryParse(height, out mHeight);
            int.TryParse(width, out mWidth);
        }

        t.makeRowData(mHeight,mWidth);

        if (mWidth > 0 && mHeight > 0)
        {
            mapArray = new GameObject[mHeight, mWidth];
            int y = 0, x = 0;
            foreach (string row in f)
            {
                isMap = false;
                x = 0;
                foreach (char col in row)
                {
                    if(!(col.Equals('@') || col.Equals('T') || col.Equals('.'))){
                        Debug.Log(row);
                        continue;
                    }
                    else
                    {
                        isMap = true;
                    }

                    GameObject tile;

                    if (col.Equals('@'))
                    {
                        t.rows[y].cols[x].pos = new Vector2(x, -y);
                        t.rows[y].cols[x].tile = tileData.tilePosition.tileToSpawn.At;
                        tile = PrefabUtility.InstantiatePrefab(at) as GameObject;
                        tile.transform.position = t.rows[y].cols[x].pos;
                    }
                    else if (col.Equals('T'))
                    {
                        t.rows[y].cols[x].pos = new Vector2(x, -y);
                        t.rows[y].cols[x].tile = tileData.tilePosition.tileToSpawn.Tree;
                        tile = PrefabUtility.InstantiatePrefab(tree) as GameObject;
                        tile.transform.position = t.rows[y].cols[x].pos;
                    }
                    else
                    {
                        t.rows[y].cols[x].pos = new Vector2(x, -y);
                        t.rows[y].cols[x].tile = tileData.tilePosition.tileToSpawn.Period;
                        tile = PrefabUtility.InstantiatePrefab(period) as GameObject;
                        tile.transform.position = t.rows[y].cols[x].pos;
                    }

                    x++;

                    /*if (int.Parse(col.Trim()) < 8)
                    {
                        mapArray[y, x] = Tiles[int.Parse(col.Trim())];
                        mapTile = mapArray[y, x];
                        GameObject.Instantiate(mapTile, new Vector3(x, mHeight - y, 0),
                                                Quaternion.Euler(0, 0, 0));
                        x++;
                        isMap = true;
                    }
                    else { isMap = false; }*/
                }
                if (isMap)
                {
                    y++;
                }
            }
        }
    }
}
