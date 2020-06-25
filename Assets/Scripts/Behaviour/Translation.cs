using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Translation : MonoBehaviour
{
    // Storing input for movement in FixedUpdate
    private Vector3 input;

    // Movement speed
    [SerializeField]
    private float moveSpeed = 10;



    /// <summary>
    /// Physics Update.
    /// </summary>
    private void FixedUpdate()
    {

        input *= moveSpeed * Time.fixedDeltaTime;
        transform.position += input;

    }

    /// <summary>
    /// Passing direction as input where IP-Paket-Piece should go.
    /// </summary>
    /// <param name="input">Input - direciton.</param>
    public void MoveTo(Vector3 input)
    {
        this.input = input;
    }


}
