//+++++++++++++++++++++++++++++++++++++++++++++++++++++//
//Lisa Fröhlich Gabra, Expanded Realities, Semester 6th//
//Group 1: HEL                                         //
//+++++++++++++++++++++++++++++++++++++++++++++++++++++//


//Script: State Machine to detect and handle the correct Player Setup


//What it do:
// - provide the base class for the player setup state machine
// - provides base class functions to hand over protected variables
// - provides the two derived classes CaveState and VrState
// - spawns and despawns the correct player whenever the state is changed
// - leaves the state into the new correct one if the player setup is changed
// - invokes the event onSetupChanged for other changes that need to happen from outside of the state machine


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SetupStates
{
    //define possible states as enumeration
    //ENUM always with capital letters
    public enum STATE
    {
        CAVE, VR
    };

    //the three phases of the states
    public enum EVENT
    {
        ENTER, UPDATE, EXIT
    };

    //what state is it
    public STATE name;

    protected EVENT stage; //phase the state is in
    protected SetupStates nextState; //ref to the state maschine (not ENUM)

    //the general variables used in the calculations
    protected GameObject statePrefab;
    protected Vector3Reference stateSpawn;

    //the variables to hand over from the Li_PlayerSetup script
    protected GameObject cavePrefab;
    protected GameObject vrPrefab;
    protected Vector3Reference defaultSpawn;
    protected Vector3Reference caveSpawn;
    protected Vector3Reference vrSpawn;


    //constructor to create the different states
    public SetupStates(GameObject _cavePrefab, GameObject _vrPrefab, Vector3Reference _defaultSpawn, Vector3Reference _caveSpawn, Vector3Reference _vrSpawn)
    {
        stage = EVENT.ENTER;
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
    public SetupStates Process()
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

    public Vector3Reference GetSpawn()
    {
        return stateSpawn;
    }
}

public class CaveState : SetupStates
{
    //Contructor
    public CaveState(GameObject _cavePrefab, GameObject _vrPrefab, Vector3Reference _defaultSpawn, Vector3Reference _caveSpawn, Vector3Reference _vrSpawn)
        : base(_cavePrefab, _vrPrefab, _defaultSpawn, _caveSpawn, _vrSpawn) //hand over the values to the base class
    {
        name = STATE.CAVE;
        statePrefab = _cavePrefab;
        stateSpawn = _caveSpawn;
    }

    public override void Enter()
    {
        Singleton.Instance.GetComponent<PlayerSetup>().SpawnMyPlayer(); //spawn the new player
        Singleton.Instance.GetComponent<PlayerSetup>().onSetupChanged.Invoke(); //inform all listeners about the change
        Debug.Log("entered CAVE");
        base.Enter();
    }

    public override void Update()
    {
        //base.Update();

        //go from this state to the vr state

        if (Singleton.Instance.GetComponent<PlayerSetup>().playerSetup.Value == 2)
        {
            nextState = new VrState(cavePrefab, vrPrefab, defaultSpawn, caveSpawn, vrSpawn); //the next state is the vr state
            Singleton.Instance.GetComponent<PlayerSetup>().DestroyMyPlayer(); //destroy the current player
            stage = EVENT.EXIT; //leave this state
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}

public class VrState : SetupStates
{
    //Contructor
    public VrState(GameObject _cavePrefab, GameObject _vrPrefab, Vector3Reference _defaultSpawn, Vector3Reference _caveSpawn, Vector3Reference _vrSpawn)
        : base(_cavePrefab, _vrPrefab, _defaultSpawn, _caveSpawn, _vrSpawn) //hand over the values to the base class
    {
        name = STATE.VR;
        statePrefab = _vrPrefab;
        stateSpawn = _vrSpawn;
    }

    public override void Enter()
    {
        Singleton.Instance.GetComponent<PlayerSetup>().SpawnMyPlayer(); //spawn the new player
        Singleton.Instance.GetComponent<PlayerSetup>().onSetupChanged.Invoke(); //inform all listeners about the change
        Debug.Log("entered VR");
        base.Enter();
    }

    public override void Update()
    {
        //base.Update();

        //go from this state to the cave state

        if (Singleton.Instance.GetComponent<PlayerSetup>().playerSetup.Value == 1)
        {
            nextState = new CaveState(cavePrefab, vrPrefab, defaultSpawn, caveSpawn, vrSpawn); //the next state is the cave state
            Singleton.Instance.GetComponent<PlayerSetup>().DestroyMyPlayer(); //destroy the current player
            stage = EVENT.EXIT; //leave this state
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
