
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    public void SwitchToScene(string scene) 
    {
        Debug.Log("ScenSwitch in SceneSwitch-Script");
        SceneManager.LoadScene(scene);
    }
}
