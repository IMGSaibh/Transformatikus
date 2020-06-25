using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : BaseState
{
    private Vector3 targetPosition;
    //private Vector2 targetRotation;
    public override void PrepareState()
    {
        base.PrepareState();

        //start position of IP-Paket-Piece

        targetPosition = new Vector3(1, 0, 0);
    }

    public override void UpdateState()
    {
        base.UpdateState();
        // Calculating the movement of IP-Packet-Piece
        Vector3 direction = targetPosition - stateMachineOwner.transform.position;
        if (direction.magnitude > 1)
        {
            direction.Normalize();
        }

        // Passing calculation to Movement component
        stateMachineOwner.TranslationObject.MoveTo(direction);

        // Destination reached!
        if (direction.magnitude < 0.1f)
        {
            // Now wait!
            stateMachineOwner.ChangeState(new WaitState());
        }
    }

}
