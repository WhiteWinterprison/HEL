//+++++++++++++++++++++++++++++++++++++++++++++++++++++//
//Lisa Fröhlich Gabra, Expanded Realities, Semester 6th//
//Group 1: HEL                                         //
//+++++++++++++++++++++++++++++++++++++++++++++++++++++//


//Script: State Machine to detect and handle the correct Player Setup


//What it do:
// - provides the ground work for a state machine
// - provides the base class for the Setup States with default, CAVE and VR states
// - holds the three state classes from the state machine (default, CAVE and VR)
// - saves the necessary variables for all calculations
// - spawns the correct player
// - destroys the player when the setup changes


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Li_SetupStates
{
    //define possible states as enumeration
    //ENUM always with capital letters
    public enum STATE
    {
        DEFAULT, CAVE, VR
    };

    //the three phases of the states
    public enum EVENT
    {
        ENTER, UPDATE, EXIT
    };

    //what state is it
    public STATE name;

    protected EVENT stage; //phase the state is in
    protected Li_SetupStates nextState; //ref to the state maschine (not ENUM)

    //the general variables used in the calculations
    protected GameObject statePrefab;
    protected Transform stateSpawn;

    //the variables to hand over from the Li_PlayerSetup script
    protected GameObject defaultPrefab;
    protected GameObject cavePrefab;
    protected GameObject vrPrefab;
    protected Transform defaultSpawn;
    protected Transform caveSpawn;
    protected Transform vrSpawn;


    //constructor to create the different states
    public Li_SetupStates(GameObject _defaultPrefab, GameObject _cavePrefab, GameObject _vrPrefab, Transform _defaultSpawn, Transform _caveSpawn, Transform _vrSpawn)
    {
        stage = EVENT.ENTER;
        defaultPrefab = _defaultPrefab;
        cavePrefab = _cavePrefab;
        vrPrefab = _vrPrefab;
        defaultSpawn = _defaultSpawn;
        caveSpawn = _caveSpawn;
        vrSpawn = _vrSpawn;
    }

    //defining the three stages
    public virtual void Enter() { stage = EVENT.UPDATE; } //calling Enter sets the state from the enter to the update stage
    public virtual void Update() { stage = EVENT.UPDATE; } //calling Update makes the state stay in the update stage
    public virtual void Exit() { stage = EVENT.EXIT; } //calling Exit calls the function that should be done to exit the current state

    //this function gets called from the outside to go from one state to the next
    public Li_SetupStates Process()
    {
        if (stage == EVENT.ENTER) Enter(); //if in enter, go to update
        if (stage == EVENT.UPDATE) Update(); //if in update, stay there until further notice
        if (stage == EVENT.EXIT) //if in exit, do the stuff to exit and then go to the next state
        {
            Exit();
            return nextState;
        }
        return this;
    }

    //------------------------------------//
    //HERE: Implement base class functions//
    //------------------------------------//

    public GameObject GetPrefab()
    {
        return statePrefab;
    }

    public Transform GetSpawn()
    {
        return stateSpawn;
    }
}

public class DefaultState : Li_SetupStates
{
    public DefaultState(GameObject _defaultPrefab, GameObject _cavePrefab, GameObject _vrPrefab, Transform _defaultSpawn, Transform _caveSpawn, Transform _vrSpawn)
        : base(_defaultPrefab, _cavePrefab, _vrPrefab, _defaultSpawn, _caveSpawn, _vrSpawn) //hand over the values to the base class
    {
        name = STATE.DEFAULT;
        statePrefab = _defaultPrefab;
        stateSpawn = _defaultSpawn;
    }

    public override void Enter()
    {
        //Li_NetworkManager.Instance.GetComponent<Li_PlayerSetup>().onSetupChanged.Invoke(); //destroy the current player, change the player variables and spawn the new player
        Li_NetworkManager.Instance.GetComponent<Li_PlayerSetup>().SpawnMyPlayer(); //spawn the new player
        Debug.Log("entered default");
        base.Enter();
    }

    public override void Update()
    {
        //base.Update();

        //go from this state to another

        if (Li_NetworkManager.Instance.GetComponent<Li_PlayerSetup>().GetPlayerSetup() == 2)
        {
            nextState = new CaveState(defaultPrefab, cavePrefab, vrPrefab, defaultSpawn, caveSpawn, vrSpawn); //the next state is the cave state
            Li_NetworkManager.Instance.GetComponent<Li_PlayerSetup>().DestroyMyPlayer(); //destroy the current player
            stage = EVENT.EXIT; //leave this state
        }
        else if (Li_NetworkManager.Instance.GetComponent<Li_PlayerSetup>().GetPlayerSetup() == 3)
        {
            nextState = new VrState(defaultPrefab, cavePrefab, vrPrefab, defaultSpawn, caveSpawn, vrSpawn); //the next state is the vr state
            Li_NetworkManager.Instance.GetComponent<Li_PlayerSetup>().DestroyMyPlayer(); //destroy the current player
            stage = EVENT.EXIT; //leave this state
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}

public class CaveState : Li_SetupStates
{
    public CaveState(GameObject _defaultPrefab, GameObject _cavePrefab, GameObject _vrPrefab, Transform _defaultSpawn, Transform _caveSpawn, Transform _vrSpawn)
        : base(_defaultPrefab, _cavePrefab, _vrPrefab, _defaultSpawn, _caveSpawn, _vrSpawn) //hand over the values to the base class
    {
        name = STATE.CAVE;
        statePrefab = _cavePrefab;
        stateSpawn = _caveSpawn;
    }

    public override void Enter()
    {
        //Li_NetworkManager.Instance.GetComponent<Li_PlayerSetup>().onSetupChanged.Invoke(); //destroy the current player, change the player variables and spawn the new player
        Li_NetworkManager.Instance.GetComponent<Li_PlayerSetup>().SpawnMyPlayer(); //spawn the new player
        Debug.Log("entered CAVE");
        base.Enter();
    }

    public override void Update()
    {
        //base.Update();

        //go from this state to another

        if (Li_NetworkManager.Instance.GetComponent<Li_PlayerSetup>().GetPlayerSetup() == 1)
        {
            nextState = new DefaultState(defaultPrefab, cavePrefab, vrPrefab, defaultSpawn, caveSpawn, vrSpawn); //the next state is the default state
            Li_NetworkManager.Instance.GetComponent<Li_PlayerSetup>().DestroyMyPlayer(); //destroy the current player
            stage = EVENT.EXIT; //leave this state
        }
        else if (Li_NetworkManager.Instance.GetComponent<Li_PlayerSetup>().GetPlayerSetup() == 3)
        {
            nextState = new VrState(defaultPrefab, cavePrefab, vrPrefab, defaultSpawn, caveSpawn, vrSpawn); //the next state is the vr state
            Li_NetworkManager.Instance.GetComponent<Li_PlayerSetup>().DestroyMyPlayer(); //destroy the current player
            stage = EVENT.EXIT; //leave this state
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}

public class VrState : Li_SetupStates
{
    public VrState(GameObject _defaultPrefab, GameObject _cavePrefab, GameObject _vrPrefab, Transform _defaultSpawn, Transform _caveSpawn, Transform _vrSpawn)
        : base(_defaultPrefab, _cavePrefab, _vrPrefab, _defaultSpawn, _caveSpawn, _vrSpawn) //hand over the values to the base class
    {
        name = STATE.VR;
        statePrefab = _vrPrefab;
        stateSpawn = _vrSpawn;
    }

    public override void Enter()
    {
        //Li_NetworkManager.Instance.GetComponent<Li_PlayerSetup>().onSetupChanged.Invoke(); //destroy the current player, change the player variables and spawn the new player
        Li_NetworkManager.Instance.GetComponent<Li_PlayerSetup>().SpawnMyPlayer(); //spawn the new player
        Debug.Log("entered VR");
        base.Enter();
    }

    public override void Update()
    {
        //base.Update();

        //go from this state to another

        if (Li_NetworkManager.Instance.GetComponent<Li_PlayerSetup>().GetPlayerSetup() == 2)
        {
            nextState = new CaveState(defaultPrefab, cavePrefab, vrPrefab, defaultSpawn, caveSpawn, vrSpawn); //the next state is the cave state
            Li_NetworkManager.Instance.GetComponent<Li_PlayerSetup>().DestroyMyPlayer(); //destroy the current player
            stage = EVENT.EXIT; //leave this state
        }
        else if (Li_NetworkManager.Instance.GetComponent<Li_PlayerSetup>().GetPlayerSetup() == 1)
        {
            nextState = new DefaultState(defaultPrefab, cavePrefab, vrPrefab, defaultSpawn, caveSpawn, vrSpawn); //the next state is the default state
            Li_NetworkManager.Instance.GetComponent<Li_PlayerSetup>().DestroyMyPlayer(); //destroy the current player
            stage = EVENT.EXIT; //leave this state
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
