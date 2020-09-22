using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using OpenCVMarkerBasedAR;
using UnityEngine;

public class IdleState : BaseState
{
    public override void PrepareState()
    {
        base.PrepareState();
    }
    
    public override void UpdateState()
    {
        base.UpdateState();
        
        IdleStateMachine idle = stateMachineOwner.GetComponent<IdleStateMachine>();
        
        if (SortedCubesListScript.sortedCubes != null
            && SortedCubesListScript.sortedCubes.Count == 1)
        {
            if (SortedCubesListScript.sortedCubes[0].Value.transformationClass.transformationMatrix.elementType ==
                IntMatrix.ElementTypes.Operation)
            {
                if (SortedCubesListScript.sortedCubes[0].Value.transformationClass.transformationMatrix.operation == "%")
                {
                    //Debug.Log("Eintritt!");
                    idle.text.text = "Eintritt!";

                    idle.StartCoroutine(Wait(5));
                    
                    //change State
                    //TODO: oder doch eher in die IdleStatemachine packen?
                    //base.stateMachineOwner.ChangeState(new WaitState());
                    idle.ChangeState(new LoadingSceneState());
                }
            }
        }
    }
    
    IEnumerator Wait(float duration)
    {
        yield return new WaitForSeconds(duration);   //Wait
    } 

}
