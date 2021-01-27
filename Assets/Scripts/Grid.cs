using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
public class Grid
{
    private int width;
    private int height;
    private int[,] gridArray;
    private float cellSize;
    private TextMesh[,] debugArray;
    private Vector3 originPosition;


    public Grid(int width, int height, float cellSize, Vector3 origin)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = origin;

        //creates array for the grid
        gridArray = new int[width, height];
        debugArray = new TextMesh[width, height];

        Debug.Log(width + " " + height);

        for (int x =0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                Debug.Log(x + ", " + y);
                debugArray[x,y] = UtilsClass.CreateWorldText(gridArray[x, y].ToString(), null, GetWorldPosition(x, y), 8, Color.white, TextAnchor.UpperLeft);
                Debug.DrawLine(GetWorldPosition(x,y-1),GetWorldPosition(x + 1, y-1), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(x, y-1), GetWorldPosition(x, y), Color.white, 100f);

            }

        }

        SetValue(2, 1, 2);
    }

    //Get's the position for each grid point
    private Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellSize + originPosition + new Vector3(0,1,0);//0,0 world point is centre of map
    }

    private Vector2Int GetXY (Vector3 worldPos)
    {
        //Have to adjust value by half width as 0,0 world point is centre
        int x = Mathf.FloorToInt((worldPos.x / cellSize) - originPosition.x);
        int y = Mathf.FloorToInt((worldPos.y / cellSize) - originPosition.y);
        Debug.Log("Got " + x + ", " + y);
        return new Vector2Int(x, y);

    }

    public void SetValue(int x, int y, int value)
    {
        gridArray[x, y] = value;
        debugArray[x, y].text = gridArray[x, y].ToString();
    }

    public void SetValue(Vector3 worldPos, int value)
    {
        Vector2Int xy = GetXY(worldPos);
        SetValue(xy.x, xy.y, value);
    }

    public int GetValue(int x, int y)
    {
        if (x >= 0 && y>=0 && x <= width && y <= height)
        {
            return gridArray[x, y];
        } else
        {
            return -1;
        }
    }

    public int GetValue(Vector3 worldPos)
    {
        Vector2Int xy = GetXY(worldPos);
        return GetValue(xy.x, xy.y);
    }
}