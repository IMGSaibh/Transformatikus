using System.Collections.Generic;
using MarkerBasedARExample.MarkerBasedAR;
using OpenCVForUnity.XimgprocModule;
using UnityEngine;
using UnityEngine.UI;

public class GameStateMachine : StateMachine
{
    /// <summary>
    /// Bestätigungsbutton.
    /// </summary>
    public Button button;
        
    public bool confirmed = false;
    
    // Reference to movement script of IP-Paket-Piece.
    [SerializeField]
    private Translation translationObject;
    public Translation TranslationObject => translationObject;
    
    // Reference to rotation script of IP-Paket-Piece.
    [SerializeField]
    private Rotation rotationObject;
    public Rotation RotationObject => rotationObject;
    
    // Reference to scaling script of IP-Paket-Piece.
    [SerializeField]
    private Scaling scalingObject;
    public Scaling ScalingObject => scalingObject;

    public GameObject Ipaket_target;
    public GameObject Ipaket;


    private new void Start()
    {
        base.Start();
        
        //add button listener
        button.onClick.AddListener(delegate()
        {
            this.ButtonClicked();
        });

        //Change to gameState
        GameState gameState = new GameState();
        base.ChangeState(gameState);
    }
    
    private new void Update()
    {
        base.Update();
    }

    public void ButtonClicked () {
        this.confirmed = !this.confirmed;
        //Debug.Log("clicked!");
    }


}
