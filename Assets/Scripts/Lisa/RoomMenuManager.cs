//+++++++++++++++++++++++++++++++++++++++++++++++++++++//
//Lisa Fröhlich Gabra, Expanded Realities, Semester 6th//
//Group 1: HEL                                         //
//+++++++++++++++++++++++++++++++++++++++++++++++++++++//


//Script: Handling the Room UI behaviour and providing functions for its UI elements


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Photon.Pun;
using TMPro;

public class RoomMenuManager : MonoBehaviour
{
    #region Variables

    public IntReference playerSetup;
    public StringReference modeText;

    [Header("The Textfields of the Switch Modes Buttons inside of the different UI Prefabs")]
    [SerializeField]
    private TextMeshProUGUI caveModeText;
    [SerializeField]
    private TextMeshProUGUI vrModeText;

    private TextMeshProUGUI myText;

    [Header("The UIs")]
    [SerializeField]
    private GameObject caveUI;
    [SerializeField]
    private GameObject vrUI;

    #endregion

    private void Awake()
    {
        UpdateUISetup();
    }

    #region Provided Functions

    //function provided for the Leave Button
    public void LeaveRoom()
    {
        Debug.Log("Trying to leave room...");

        PhotonNetwork.LeaveRoom();
    }

    //function provided for the Change Setup Buttons
    public void ChangeSetup()
    {
        //change the player setup
        switch (playerSetup.Value)
        {
            case 1: playerSetup.Variable.Value = 2; break;
            case 2: playerSetup.Variable.Value = 1; break;
            default: break;
        }

        UpdateUISetup();
    }

    //function SwitchModes() called on PlayModes script

    #endregion

    #region Functions for the Events

    public void UpdateUIMode()
    {
        //update the mode text inside of the mode button
        myText.text = modeText.Value;
        Debug.Log("Updated Mode Text");
    }

    public void UpdateUISetup()
    {
        //set the UI objects according to the player setup
        switch (playerSetup.Value)
        {
            case 1: caveUI.SetActive(true); vrUI.SetActive(false); myText = caveModeText; break;
            case 2: caveUI.SetActive(false); vrUI.SetActive(true); myText = vrModeText; break;
            default: break;
        }

        //if setup changed update the text for the mode as well in case it has not been updated yet
        UpdateUIMode();
    }

    #endregion
}
