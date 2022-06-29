//+++++++++++++++++++++++++++++++++++++++++++++++++++++//
//Lisa Fröhlich Gabra, Expanded Realities, Semester 6th//
//Group 1: HEL                                         //
//+++++++++++++++++++++++++++++++++++++++++++++++++++++//


//Script: Handling the Player Setup (or implementing the Setup States)


//What it do:
// - activate all available displays
// - check for the setup of the player
// - provides functions to change the setup
// - provides an unity event to invoke when changing the setup
// - spawns/respawns the player whenever necessary


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Photon.Pun;

public class Li_PlayerSetup : MonoBehaviour
{
    #region Variables

    //variables for the player Setup

    Li_SetupStates currentState;

    private int playerSetup;

    //the variables and refs for spawning the player

    [Header("The different Camera Prefabs")]
    [SerializeField]
    private GameObject defaultCamera;
    [SerializeField]
    private GameObject caveSetup;
    [SerializeField]
    private GameObject vrSetup;

    [Header("The Variable to pass the correct refs to")]
    [SerializeField]
    private GameObject myPrefab;

    [Header("The Variables for calculating the Positions for the Prefabs to spawn in")]
    [SerializeField]
    private Transform defaultPoint;
    [SerializeField]
    private Transform cavePoint;
    [SerializeField]
    private Transform vrPoint;

    //making the user setup possible

    [Header("How many Displays for the CAVE Setup")]
    [SerializeField]
    private int displayCount = 6; //counting starts at 0, so in the code pls add -1 (wanted to make it easier for the person inputting the display count into the inspector, since there are technically 6 screens)

    #endregion

    #region Handleing the Setup States

    private void Awake()
    {
        //activate all available dispalys
        ActivateDisplays();

        //check for the users setup
        if (Display.displays.Length >= displayCount - 1) //if there is a HMD present but there are also 6 displays
        {
            playerSetup = 2; //its the CAVE setup
        }
        else //or there is nothing special
        {
            playerSetup = 2; //and its the default/desktop setup
        }
    }

    private void Start()
    {
        switch (playerSetup)
        {
            case 1: currentState = new DefaultState(defaultCamera, caveSetup, vrSetup, defaultPoint, cavePoint, vrPoint); break; //set the first state as the default setup
            case 2: currentState = new CaveState(defaultCamera, caveSetup, vrSetup, defaultPoint, cavePoint, vrPoint); break; //set the first state as the cave setup
            case 3: currentState = new VrState(defaultCamera, caveSetup, vrSetup, defaultPoint, cavePoint, vrPoint); break; //set the first state as the vr setup
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

    //function to see which player setup it is supposed to be
    public int GetPlayerSetup()
    {
        return playerSetup;
    }

    //function to call inside the MainMenuManager function for the Switch Button
    public void ChangePlayerSetup()
    {
        switch (playerSetup)
        {
            case 1: playerSetup = 2; break; //switch to the default setup
            case 2: playerSetup = 3; break; //switch to the CAVE setup
            case 3: playerSetup = 1; break; //switch to the VR setup
            default: Debug.Log("Wrong playerSetup loaded"); break;
        }
    }

    //function provided for the event to be able to destroy
    public void DestroyMyPlayer()
    {
        if (GameObject.FindGameObjectsWithTag("Player") != null)
        {
            if (PhotonNetwork.InRoom)
            {
                foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Player"))
                {
                    if (obj.GetComponent<PhotonView>().IsMine)
                    {
                        Destroy(obj);
                    }
                }
            }
            else
            {
                Destroy(GameObject.FindGameObjectWithTag("Player"));
            }
        }

        Debug.Log("Player Destroyed");
    }

    //function provided for the event to be able to instantiate
    public void SpawnMyPlayer()
    {
        //Vector3 spawnPos = new Vector3(0, 1.7f, 0); ---> should not be need anymore if the state machine is doing its job

        if (PhotonNetwork.InRoom)
        {
            //Instantiates by NAME, be carefull with spelling
            PhotonNetwork.Instantiate(currentState.GetPrefab().name, currentState.GetSpawn().position, Quaternion.identity);
        }
        else
        {
            //Instantiates by NAME, be carefull with spelling
            Instantiate(currentState.GetPrefab(), defaultPoint.position, Quaternion.identity);
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
