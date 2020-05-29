using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : BaseState
{
    private Vector2 targetPosition;
    //private Vector2 targetRotation;
    public override void PrepareState()
    {
        base.PrepareState();

        //start position of IP-Paket-Piece

        targetPosition = new Vector2(-10, 2);
    }

    public override void UpdateState()
    {
        base.UpdateState();
        // Calculating the movement of IP-Packet-Piece
        Vector2 direction = targetPosition; // - openCV input position
        if (direction.magnitude > 1)
        {
            direction.Normalize();
        }

        // Passing calculation to Movement component
        stateMachineOwner.Movement.MoveTo(direction);

        // Destination reached!
        if (direction.magnitude < 0.2f)
        {
            // Now wait!
            stateMachineOwner.ChangeState(new WaitState());
        }
    }

}
