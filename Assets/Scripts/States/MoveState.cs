using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : BaseState
{
    private Vector3 cubeInputVector;
    //private Vector2 targetRotation;
    public override void PrepareState()
    {
        base.PrepareState();

        //target position of IP-Paket-Piece and position of IP-Packet
        //targetPosition = new Vector3(8, 0, 0);
        cubeInputVector = stateMachineOwner.GetComponent<StateMachine>().targetObject.transform.position;
    }

    public override void UpdateState()
    {
        base.UpdateState();

        Debug.Log("Move State");

        // Calculating the movement of IP-Packet-Piece
        Vector3 direction = cubeInputVector - stateMachineOwner.transform.position;

        // Passing calculation to Movement component
        stateMachineOwner.TranslationObject.Add(direction);

        if (cubeInputVector == stateMachineOwner.transform.position)
        {
            Debug.Log("Ziel erreicht");
            stateMachineOwner.ChangeState(new WaitState());

        }
    }

}
