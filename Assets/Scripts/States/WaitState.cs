using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitState : BaseState
{

    // we can use this as level transition
    public override void PrepareState()
    {
        base.PrepareState();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        Debug.Log("Wait State");

        //When cube is placed on table do somthing
        if (Input.GetKeyDown(KeyCode.M))
            stateMachineOwner.ChangeState(new MoveState());
        if (Input.GetKeyDown(KeyCode.S))
            stateMachineOwner.ChangeState(new LoadingSceneState());

    }
}
