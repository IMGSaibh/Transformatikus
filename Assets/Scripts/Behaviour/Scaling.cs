using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaling : MonoBehaviour
{
    private Vector3 scale;
    
    public GameObject pseudoWorldCoordinateSystem;

    /// <summary>
    /// Physics Update.
    /// </summary>
    private void FixedUpdate()
    {
    }

    public void Scale(Transform koordsystem, float scaleX, float scaleY, float scaleZ) 
    {
        this.transform.SetParent(koordsystem);
        Vector3 scale = koordsystem.localScale;
        scale.x = scaleX;
        scale.y = scaleY;
        scale.z = scaleZ;

        this.scale = scale;

        koordsystem.localScale = this.scale; 
    }
    
    public void Scale(Transform koordsystem, Vector3 scale) 
    {
        this.scale = scale;
        this.transform.SetParent(koordsystem);

        koordsystem.localScale = this.scale; 
    }
    
    public void Scale(Vector3 scale) 
    {
        this.scale = scale;
        this.transform.SetParent(pseudoWorldCoordinateSystem.transform);

        pseudoWorldCoordinateSystem.transform.localScale = this.scale; 
    }
    
    public void ScaleAround(Vector3 newScale)
    {
        Vector3 A = transform.localPosition;
        Vector3 B = pseudoWorldCoordinateSystem.transform.localPosition;
        //Vector3 B = pseudoWorldCoordinateSystem.transform.position;
     
        Vector3 C = A - B; // diff from object pivot to desired pivot/origin
     
        //TODO: hier besser einstellen, weil z.B. 1.Skalierung --> 2; 2.Skalierung 0,25 --> 0.25 anstatt 0.25*2
        float RS = newScale.x / transform.localScale.x; // relative scale factor
     
        // calc final position post-scale
        Vector3 FP = B + C * RS;
     
        // finally, actually perform the scale/translation
        transform.localScale = newScale;
        transform.localPosition = FP;
    }
}
