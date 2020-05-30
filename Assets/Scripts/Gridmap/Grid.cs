using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class Grid
{

    private int gridWidth;
    private int gridHeight;
    private int gridCellSize;
    private Vector3 gridOriginPosition;

    private int[,] gridArray;
    private TextMesh[,] debugTextArray;

    public Grid(int width, int height, int cellSize, Vector3 originPosition) 
    {
        gridWidth = width;
        gridHeight = height;
        gridCellSize = cellSize;
        gridOriginPosition = originPosition;

        gridArray = new int[gridWidth, gridHeight];
        debugTextArray = new TextMesh[gridWidth, gridHeight];

        for (int x = 0; x < gridArray.GetLength(0); x++)
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                debugTextArray[x,y] = CreateWorldText(gridArray[x, y].ToString(), GetWorldPosition(x, y) + new Vector3(cellSize, cellSize) * 0.5f, 20, Color.white, TextAnchor.MiddleCenter);

                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
            }

        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
        Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);

    }


  

    private Vector3 GetWorldPosition(int x, int y) 
    {
        return new Vector3(x, y) * gridCellSize + gridOriginPosition;
    }

    private void GetXY(Vector3 worldPosition, out int x, out int y) 
    {
        x = Mathf.FloorToInt((worldPosition - gridOriginPosition).x / gridCellSize);
        y = Mathf.FloorToInt((worldPosition - gridOriginPosition).y / gridCellSize);

    }

    public void SetValue(int x, int y, int value)
    {
        if (x >= 0 && y >= 0 && x < gridWidth && y < gridHeight)
        {
            gridArray[x, y] = value;
            debugTextArray[x, y].text = gridArray[x, y].ToString();

        }
    }
    public void SetValue(Vector3 worldPosition, int value) 
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        SetValue(x, y, value);
    }

    public int GetValue(int x, int y) 
    {
        if (x >= 0 && y >= 0 && x < gridWidth && y < gridHeight)
            return gridArray[x, y];
        else
            return 0;
    }

    public int GetValue(Vector3 worldPosition) 
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return GetValue(x, y);
    
    }




    public static TextMesh CreateWorldText(string text, Vector2 localPosition, int fontSize, Color color, TextAnchor textAnchor) 
    {
        GameObject gameObject = new GameObject("Worldtext", typeof(TextMesh));
        gameObject.GetComponent<MeshRenderer>().sortingLayerName = "Playersprite";
        gameObject.transform.position = localPosition;
        TextMesh textMesh = gameObject.GetComponent<TextMesh>();
        textMesh.anchor = textAnchor;
        textMesh.text = text;
        textMesh.fontSize = fontSize;
        textMesh.color = color;

        return textMesh;
    }

}
