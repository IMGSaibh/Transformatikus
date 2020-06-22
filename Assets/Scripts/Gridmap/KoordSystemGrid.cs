using UnityEngine;

public class KoordSystemGrid : MonoBehaviour
{
    public Material mat;

    public int gridSizeX = 30;
    public int gridSizeY = 16;
    public int gridSizeZ;

    [Range(1f,10f)]
    public float stepSize = 1;

    public float startX = -15;
    public float startY = -8;
    public float startZ;

    float unitX = 0;
    float unitY = 0;

    private void Start()
    {
        for (float i = 0; i <= gridSizeX; i++)
        {
            unitX = startX + i;
            CreateWorldText(i.ToString(), new Vector2(unitX, -8),8, Color.black, TextAnchor.UpperCenter);
        }

        for (float i = 0; i <= gridSizeY; i++)
        {
            unitY = startY + i;
            CreateWorldText(i.ToString(), new Vector2(startX,unitY), 8, Color.black, TextAnchor.UpperLeft);
        }
    }

    void OnPostRender()
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

