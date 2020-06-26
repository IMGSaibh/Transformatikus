using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    private Vector3 axis;
    private float alpha;

    public GameObject pseudoWorldCoordinateSystem;
    
    /// <summary>
    /// Physics Update.
    /// </summary>
    private void FixedUpdate()
    {
    }
    
    /// <summary>
    /// Passing rotation as input where IP-Paket-Piece should rotate to.
    /// </summary>
    /// <param name="axis">Axis - rotation axis</param>
    /// <param name="alpha">Alpha - rotation angle</param>
    public void Rotate(Vector3 axis, float alpha)
    {
        this.axis = axis;
        this.alpha = alpha;
        
        transform.RotateAround(pseudoWorldCoordinateSystem.transform.position, this.axis, this.alpha);
    }

}
