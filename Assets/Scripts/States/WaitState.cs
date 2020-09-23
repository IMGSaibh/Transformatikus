using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using OpenCVMarkerBasedAR;
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
        
        //When cube is placed on table enter game transformatics
        if (Input.GetKeyDown(KeyCode.S))
            stateMachineOwner.ChangeState(new LoadingSceneState());
        
        Debug.Log("im WaitState");
    }
}
