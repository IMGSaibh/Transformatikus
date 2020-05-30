using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testgrid : MonoBehaviour
{
    public int height;
    public int width;
    Grid grid;
    public Camera main;
    private void Start()
    {
        grid = new Grid(width, height, 10);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            grid.SetValue(GetMouseWorldPosition(),11);
            Debug.Log("Test " + Input.mousePosition);
        }
    }

    public Vector3 GetMouseWorldPosition() 
    {
        Vector3 worldPositionXY = main.ScreenToWorldPoint(Input.mousePosition);
        worldPositionXY.z = 0;
        return worldPositionXY; 
    }
}
