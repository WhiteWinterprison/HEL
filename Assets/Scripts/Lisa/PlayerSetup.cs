//+++++++++++++++++++++++++++++++++++++++++++++++++++++//
//Lisa Fröhlich Gabra, Expanded Realities, Semester 6th//
//Group 1: HEL                                         //
//+++++++++++++++++++++++++++++++++++++++++++++++++++++//


//Script: Implementing the Setup States


//What it do:
// - implements the from SetupStates inherited Player Setup States
// - provides a public event onSetupChanged for all the changes that need to be handled from outside of the setup states
// - activates all displays in the setup
// - sets the first player setup state
// - provides functions for the Setup States


//Component of Singleton manager game object (Network Manager prefab) in Lobby scene


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Photon.Pun;

public class PlayerSetup : MonoBehaviour
{
    #region Variables

    //variables for the player Setup

    SetupStates currentState;

    public IntReference playerSetup;

    public UnityEvent onSetupChanged;

    //the variables and refs for spawning the player

    [Header("The different Player Prefabs")]
    [SerializeField]
    private GameObject caveSetup;
    [SerializeField]
    private GameObject vrSetup;

    [Header("The Points for Spawning the player in the correct position")]
    public Vector3Reference defaultPoint;
    public Vector3Reference cavePoint;
    public Vector3Reference vrPoint;

    //making the user setup possible

    [Header("How many Displays are in the CAVE setup?")]
    [SerializeField]
    private int displayCount = 6; //counting starts at 0, so in the code pls add -1 (wanted to make it easier for the person inputting the display count into the inspector, since there are technically 6 screens)

    #endregion

    #region Handling the Setup States

    private void Awake()
    {
        //activate all available dispalys
        ActivateDisplays();

        //check for the users setup
        if (Display.displays.Length >= displayCount - 1) //if there are 6 displays
        {
            playerSetup.Variable.Value = 1; //its the CAVE setup
        }
        else //or there are not that many displays
        {
            playerSetup.Variable.Value = 2; //its the VR setup
        }

        //make sure there is a Unity Event for onSetupChanged
        if (onSetupChanged == null)
        {
            onSetupChanged = new UnityEvent();
        }
    }

    private void Start()
    {
        switch (playerSetup.Variable.Value)
        {
            case 1: currentState = new CaveState(caveSetup, vrSetup, defaultPoint, cavePoint, vrPoint); break; //set the first state as the cave setup
            case 2: currentState = new VrState(caveSetup, vrSetup, defaultPoint, cavePoint, vrPoint); break; //set the first state as the vr setup
            default: Debug.Log("Wrong playerSetup"); break;
        }
    }

    private void Update()
    {
        //call the current state
        currentState = currentState.Process();
    }

    #endregion

    #region Functions for the Player Setup

    //function provided for the event to be able to destroy
    public void DestroyMyPlayer()
    {
        if (GameObject.FindGameObjectsWithTag("Player") != null) //check if there even is a player
        {
            if (PhotonNetwork.InRoom)
            {
                foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Player")) //find all players
                {
                    if (obj.GetComponent<PhotonView>().IsMine) //find this one
                    {
                        //and destroy it
                        Destroy(obj);
                    }
                }
            }
            else
            {
                //destroy the player
                Destroy(GameObject.FindGameObjectWithTag("Player"));
            }
        }

        Debug.Log("Player Destroyed");
    }

    //function provided for the event to be able to instantiate
    public void SpawnMyPlayer()
    {
        if (PhotonNetwork.InRoom)
        {
            //Instantiates by NAME, be carefull with spelling
            PhotonNetwork.Instantiate(currentState.GetPrefab().name, currentState.GetSpawn().Value, Quaternion.identity);
        }
        else
        {
            //Instantiates by NAME, be carefull with spelling
            Instantiate(currentState.GetPrefab(), defaultPoint.Value, Quaternion.identity);
        }

        Debug.Log(currentState.GetPrefab().name + " spawned");
    }

    #endregion

    #region Other needed Functions

    //function to activate all available displays
    private void ActivateDisplays()
    {
        Debug.Log(Display.displays.Length);

        for (int i = 1; i < Display.displays.Length; i++)
            Display.displays[i].Activate();
    }

    #endregion
}
