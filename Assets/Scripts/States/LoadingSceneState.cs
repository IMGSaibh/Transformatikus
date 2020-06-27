using UnityEngine;

public class LoadingSceneState : BaseState
{
    public override void PrepareState()
    {
        base.PrepareState();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        Debug.Log("Loading State");
        Debug.Log("Loaded Game");
        if (stateMachineOwner.SceneSwitch != null)
            stateMachineOwner.SceneSwitch.SwitchToScene("Transformatikus");



    }


}
