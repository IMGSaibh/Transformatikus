using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    private Rigidbody2D rb;
    // Storing input for movement in FixedUpdate
    private Vector2 input;

    // Movement speed
    [SerializeField]
    private float moveSpeed = 10;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.gravityScale = 0;
    }


    /// <summary>
    /// Physics Update.
    /// </summary>
    private void FixedUpdate()
    {
        input *= moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + input);
    }

    /// <summary>
    /// Passing direction as input where IP-Paket-Piece should go.
    /// </summary>
    /// <param name="input">Input - direciton.</param>
    public void MoveTo(Vector2 input)
    {
        if (input.magnitude > 1)
            input.Normalize();

        this.input = input;
    }

}
