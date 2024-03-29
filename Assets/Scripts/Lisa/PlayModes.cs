//+++++++++++++++++++++++++++++++++++++++++++++++++++++//
//Lisa Fr�hlich Gabra, Expanded Realities, Semester 6th//
//Group 1: HEL                                         //
//+++++++++++++++++++++++++++++++++++++++++++++++++++++//


//Script: Implementation of the states from the ModeStates


//What it do:
// - implements the from ModeStates inherited Play Mode States
// - provides the public event onModeChanged for changes from outside of the mode state
// - handles the network variable for the play mode via CustomProperties fo the room and Hashtables
// - sets the first state of the play mode
// - provides the function for the SwitchModes button in the room UI


//Component of non-Singleton manager game object (Room Manager prefab) in Multiplayer scene


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Photon.Pun;

using Hashtable = ExitGames.Client.Photon.Hashtable; //This line need to be on every script that uses the Hashtable!!

public class PlayModes : MonoBehaviour
{
    #region Variables

    //general variables

    ModeStates currentState;

    public UnityEvent onModeChanged;

    //the setup for a multiplayer boolean

    [SerializeField]
    Hashtable myModeBoolean = new Hashtable() { { "Modes", true } };

    //variables for handing over to the state machine

    [Header("The References need for the State Machine")]
    public PlayModes playModes;
    public StringReference modeText;
    public BoolReference playMode;

    #endregion

    #region First Setup

    private void Awake()
    {
        //register the event
        if (onModeChanged == null)
        {
            onModeChanged = new UnityEvent();
        }

        //create the first "Modes" property
        myModeBoolean["Modes"] = true;
        PhotonNetwork.CurrentRoom.SetCustomProperties(myModeBoolean);
    }

    // Start is called before the first frame update
    void Start()
    {
        //create the first state the users are in
        currentState = new BuildMode(modeText, playMode, playModes);
    }

    #endregion

    // Update is called once per frame
    void Update()
    {
        //call the current state
        currentState = currentState.Process();

        //----------------------------------------------------------------//
        //update the local variable with the network property if necessary//
        //----------------------------------------------------------------//

        if ((bool)PhotonNetwork.CurrentRoom.CustomProperties["Modes"] != playMode.Value)
        {
            playMode.Variable.Value = (bool)PhotonNetwork.CurrentRoom.CustomProperties["Modes"];
        }
    }

    #region Functions for Managing the Modes

    //function provided for the SwitchModes button in the UI
    public void SwitchModes()
    {
        //get the variable from the network
        bool newBool = (bool)PhotonNetwork.CurrentRoom.CustomProperties["Modes"];

        if (newBool)
        {
            newBool = false;
        }
        else
        {
            newBool = true;
        }

        //hand over the variable to the network
        myModeBoolean["Modes"] = newBool;
        PhotonNetwork.CurrentRoom.SetCustomProperties(myModeBoolean);
    }

    #endregion
}
