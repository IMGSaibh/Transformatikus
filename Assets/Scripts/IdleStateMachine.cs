using System.Collections.Generic;
using MarkerBasedARExample.MarkerBasedAR;
using UnityEngine;
using UnityEngine.UI;

public class IdleStateMachine : StateMachine
{
    public Text text;
    
    private new void Start() 
    {
        base.Start();
        
        //switch to idle state
        IdleState idleState = new IdleState();
        
        base.ChangeState(idleState);

    }

    // Update is called once per frame
    private new void Update()
    {
        base.Update();
    }
}
