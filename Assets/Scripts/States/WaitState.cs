using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitState : BaseState
{
    public float minWait = 1;
    public float waitTime;

    // we can use this as level transition
    public override void PrepareState()
    {
        base.PrepareState();
        // Randomize waiting time.
        waitTime = Random.Range(minWait, 2.5f);
    }

    public override void UpdateState()
    {
        base.UpdateState();
        // After each frame we are subtracting time that passed.
        waitTime -= Time.deltaTime;

        // When wait time is over it's time to change state!
        if (waitTime < 0)
            stateMachineOwner.ChangeState(new MoveState());
    }
}
