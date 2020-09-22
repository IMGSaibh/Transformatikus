using System.Collections.Generic;
using MarkerBasedARExample.MarkerBasedAR;
using UnityEngine;
using UnityEngine.UI;

public class StateMachine : MonoBehaviour
{
    // Reference to currently operating state.
    private BaseState currentState;

    [SerializeField]
    private SceneSwitch sceneSwitch;
    public SceneSwitch SceneSwitch => sceneSwitch;
    
    protected virtual void Start() 
    {
        //create new state like this
        //ChangeState(new WaitState());

        //configure state is also possible
        WaitState waitState = new WaitState();
 
        //last pass the state
        ChangeState(waitState);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (currentState != null)
            currentState.UpdateState();
    }

    /// <summary>
    /// Method used to change state
    /// </summary>
    /// <param name="newState">New state.</param>
    public void ChangeState(BaseState newState)
    {
        // If we currently have state, we need to destroy it!
        if (currentState != null)
            currentState.DestroyState();
        
        // Swap reference to desired state
        currentState = newState;

        // If we passed reference to new state, we should assign owner of that state and initialize it!
        // If we decided to pass null as new state, nothing will happened.
        if (currentState != null)
        {
            currentState.stateMachineOwner = this;
            currentState.PrepareState();
        }
    }
}
