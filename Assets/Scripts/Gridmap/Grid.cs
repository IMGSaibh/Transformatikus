using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    //public GameObject plane;

    public bool showMain = true;
    public bool showSub = false;

    public int gridSizeX;
    public int gridSizeY;
    public int gridSizeZ;

    public float smallStep;
    public float largeStep;

    public float startX;
    public float startY;
    public float startZ;

    public Color mainColor = new Color(0f, 1f, 0f, 1f);
    public Color subColor = new Color(0f, 0.5f, 0f, 1f);

    private void Start()
    {
        
        Vector3 p = GetComponent<Camera>().ScreenToWorldPoint(new Vector3(0, GetComponent<Camera>().pixelHeight, GetComponent<Camera>().nearClipPlane));
        startX = p.x;
        startY = -p.y;
    }

    void OnPostRender()
    {
        if (smallStep < 0.1f)
        {
            smallStep = 0.1f;
        }

        if (largeStep < 0.1f)
        {
            largeStep = 0.1f;
        }
        //CreateLineMaterial();
        // set the current material
        //lineMaterial.SetPass(0);

        GL.Begin(GL.LINES);

        if (showSub)
        {
            GL.Color(subColor);

            //Layers
            for (float j = 0; j <= gridSizeY; j += smallStep)
            {
                //X axis lines
                for (float i = 0; i <= gridSizeZ; i += smallStep)
                {
                    GL.Vertex3(startX, startY + j, startZ + i);
                    GL.Vertex3(startX + gridSizeX, startY + j, startZ + i);
                }

                //Z axis lines
                for (float i = 0; i <= gridSizeX; i += smallStep)
                {
                    GL.Vertex3(startX + i, startY + j, startZ);
                    GL.Vertex3(startX + i, startY + j, startZ + gridSizeZ);
                }
            }

            //Y axis lines
            for (float i = 0; i <= gridSizeZ; i += smallStep)
            {
                for (float k = 0; k <= gridSizeX; k += smallStep)
                {
                    GL.Vertex3(startX + k, startY, startZ + i);
                    GL.Vertex3(startX + k, startY + gridSizeY, startZ + i);
                }
            }
        }

        if (showMain)
        {
            GL.Color(mainColor);

            //Layers
            for (float j = 0; j <= gridSizeY; j += largeStep)
            {
                //X axis lines
                for (float i = 0; i <= gridSizeZ; i += largeStep)
                {
                    GL.Vertex3(startX, startY + j, startZ + i);
                    GL.Vertex3(startX + gridSizeX, startY + j, startZ + i);
                }

                //Z axis lines
                for (float i = 0; i <= gridSizeX; i += largeStep)
                {
                    GL.Vertex3(startX + i, startY + j, startZ);
                    GL.Vertex3(startX + i, startY + j, startZ + gridSizeZ);
                }
            }

            //Y axis lines
            for (float i = 0; i <= gridSizeZ; i += largeStep)
            {
                for (float k = 0; k <= gridSizeX; k += largeStep)
                {
                    GL.Vertex3(startX + k, startY, startZ + i);
                    GL.Vertex3(startX + k, startY + gridSizeY, startZ + i);
                }
            }
        }


        GL.End();
    }
}

