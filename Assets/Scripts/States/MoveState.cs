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

        //target position of IP-Paket-Piece and position of IP-Packet
        targetPosition = new Vector3(8, 0, 0);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        Debug.Log("Move State");

        // Calculating the movement of IP-Packet-Piece
        Vector3 direction = targetPosition - stateMachineOwner.transform.position;

        // Passing calculation to Movement component
        stateMachineOwner.TranslationObject.MoveTo(direction);

        if (targetPosition == stateMachineOwner.transform.position)
        {
            Debug.Log("Ziel erreicht");
            stateMachineOwner.ChangeState(new WaitState());

        }
    }

}
