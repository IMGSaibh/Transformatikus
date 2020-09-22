using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoordSystemGridCam : MonoBehaviour
{
    public Material mat;

    public int gridSizeX = 30;
    public int gridSizeY = 16;
    public int gridSizeZ = 0;

    [Range(1f, 10f)]
    public float stepSize = 1;

    public float textScale = 0.02f;

    public float startX = 0;
    public float startY = 0;
    public float startZ = 0;

    public int unitLength = 1;

    //public GameObject koordSystem;


    private void Start()
    {

        int unitCountX = 0;
        int unitCountY = 0;

        startX = transform.position.x;
        startY = transform.position.y;
        startZ = transform.position.z;

        int koordOffsetX = (int)Camera.main.transform.position.x;
        int koordOffsetY = (int)Camera.main.transform.position.y;

        //float intervall_X = gridSizeX / unitLength;
        //double intervallLowerBorder_X = -(intervall_X / 2);
        //intervallLowerBorder_X = Math.Round(intervallLowerBorder_X, 0.0f);

        //float intervall_Y = gridSizeY / unitLength;
        //double intervallLowerBorder_Y = -(intervall_Y / 2);
        //intervallLowerBorder_Y = Math.Round(intervallLowerBorder_Y, 0.0f);


        for (float i = 0; i <= gridSizeX - Camera.main.transform.localPosition.x; i += unitLength)
        {
            CreateWorldText(unitCountX.ToString(), new Vector2(koordOffsetX + i, startY - 0.5f), 300, Color.black, TextAnchor.MiddleCenter, textScale);
            unitCountX++;
        }

        for (float i = 0; i < gridSizeY - Camera.main.transform.localPosition.y; i += unitLength)
        {
            CreateWorldText(unitCountY.ToString(), new Vector2(startX - 0.5f, koordOffsetY + i), 300, Color.black, TextAnchor.MiddleCenter, textScale);
            unitCountY++;

        }


        //for (float i = 0; i <= gridSizeX; i += unitLength)
        //{
        //    //CreateWorldText(unitCountX.ToString(), new Vector2(startX + i,startY - 0.5f), 300, Color.black, TextAnchor.MiddleCenter, textScale);
        //    CreateWorldText((intervallLowerBorder_X + unitCountX).ToString(), new Vector2(startX + i, startY - 0.5f), 300, Color.black, TextAnchor.MiddleCenter, textScale);
        //    unitCountX++;
        //}

        //for (float i = 0; i <= gridSizeY; i += unitLength)
        //{
        //    //CreateWorldText(unitCountY.ToString(), new Vector2(startX - 0.5f, startY + i), 300, Color.black, TextAnchor.MiddleCenter, textScale);
        //    CreateWorldText((intervallLowerBorder_Y + unitCountY).ToString(), new Vector2(startX - 0.5f, startY + i), 300, Color.black, TextAnchor.MiddleCenter, textScale);

        //    unitCountY++;

        //}
    }

    private void OnPostRender()
    {
        if (!mat)
        {
            Debug.LogError("Please Assign a material on the inspector");
            return;
        }

        GL.PushMatrix();
        mat.SetPass(0);
        GL.Begin(GL.LINES);
        GL.Color(mat.color);

        //Layers
        for (float j = 0; j <= gridSizeY; j += stepSize)
        {
            //X axis lines
            for (float i = 0; i <= gridSizeZ; i += stepSize)
            {
                GL.Vertex3(startX, startY + j, startZ + i);
                GL.Vertex3(startX + gridSizeX, startY + j, startZ + i);
            }

            //Z axis lines
            for (float i = 0; i <= gridSizeX; i += stepSize)
            {
                GL.Vertex3(startX + i, startY + j, startZ);
                GL.Vertex3(startX + i, startY + j, startZ + gridSizeZ);
            }
        }

        //Y axis lines
        for (float i = 0; i <= gridSizeZ; i += stepSize)
        {
            for (float k = 0; k <= gridSizeX; k += stepSize)
            {
                GL.Vertex3(startX + k, startY, startZ + i);
                GL.Vertex3(startX + k, startY + gridSizeY, startZ + i);
            }
        }


        GL.End();
        GL.PopMatrix();
    }

    public static TextMesh CreateWorldText(string text, Vector2 localPosition, int fontSize, Color color, TextAnchor textAnchor, float scale)
    {
        GameObject gameObject = new GameObject("Worldtext", typeof(TextMesh));
        gameObject.GetComponent<MeshRenderer>().sortingLayerName = "Playersprite";
        gameObject.transform.position = localPosition;
        gameObject.transform.localScale = new Vector3(scale, scale, scale);
        TextMesh textMesh = gameObject.GetComponent<TextMesh>();
        textMesh.anchor = textAnchor;
        textMesh.text = text;
        textMesh.fontSize = fontSize;
        textMesh.color = color;

        return textMesh;
    }
}
