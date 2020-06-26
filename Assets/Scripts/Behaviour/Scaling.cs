using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaling : MonoBehaviour
{
    private Vector3 scale;
    
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
}
