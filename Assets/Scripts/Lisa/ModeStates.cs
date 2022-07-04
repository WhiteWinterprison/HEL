//+++++++++++++++++++++++++++++++++++++++++++++++++++++//
//Lisa Fröhlich Gabra, Expanded Realities, Semester 6th//
//Group 1: HEL                                         //
//+++++++++++++++++++++++++++++++++++++++++++++++++++++//


//Script: State Machine to control the Switch between Building and Simulating Mode


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using Photon.Pun;
public class ModeStates
{
    //define possible states as enumeration
    //ENUM always with capital letters
    public enum STATE
    {
        BUILD, SIMULATE
    };

    //the three phases of the states
    public enum EVENT
    {
        ENTER, UPDATE, EXIT
    };

    //what state is it
    public STATE name;

    protected EVENT stage; //phase the state is in
    protected ModeStates nextState; //ref to the state maschine (not ENUM)
    protected PlayModes playModes; //ref to the script that implements the state machine
    protected StringReference modeText; //ref to the string variable used by the UI
    protected BoolReference playMode; //ref to the boolean variable used to determine the mode we should currently be in

    //constructor to create the different states
    public ModeStates(StringReference _modeText, BoolReference _playMode, PlayModes _playModes)
    {
        stage = EVENT.ENTER;
        playModes = _playModes;
        modeText = _modeText;
        playMode = _playMode;
    }

    //defining the three stages
    public virtual void Enter() { stage = EVENT.UPDATE; } //calling Enter sets the state from the enter to the update stage
    public virtual void Update() { stage = EVENT.UPDATE; } //calling Update makes the state stay in the update stage
    public virtual void Exit() { stage = EVENT.EXIT; } //calling Exit calls the function that should be done to exit the current state

    //this function gets called from the outside to go from one state to the next
    public ModeStates Process()
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
}

public class BuildMode : ModeStates
{
    public BuildMode(StringReference _modeText, BoolReference _playMode, PlayModes _playModes)
        : base(_modeText, _playMode, _playModes) //hand over the values to the base class
    {
        name = STATE.BUILD;
        playModes = _playModes;
        modeText = _modeText;
        playMode = _playMode;
    }

    public override void Enter()
    {
        modeText.Variable.Value = "Build Mode";
        playModes.onModeChanged.Invoke();
        Debug.Log("entered Build Mode");
        base.Enter();
    }

    public override void Update()
    {
        //base.Update();

        //-----------------------------------------//
        //do your simulation mode funcionality here//
        //-----------------------------------------//

        //switch the mode if button was clicked
        if (!playMode.Value)
        {
            nextState = new SimulationMode(modeText, playMode, playModes); //the next state is the angry state
            stage = EVENT.EXIT; //leave this state
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}

public class SimulationMode : ModeStates
{
    public SimulationMode(StringReference _modeText, BoolReference _playMode, PlayModes _playModes)
        : base(_modeText, _playMode, _playModes) //hand over the values to the base class
    {
        name = STATE.SIMULATE;
        playModes = _playModes;
        modeText = _modeText;
        playMode = _playMode;
    }

    public override void Enter()
    {
        modeText.Variable.Value = "Simulation Mode";
        playModes.onModeChanged.Invoke();
        Debug.Log("entered Simulation Mode");
        base.Enter();
    }

    public override void Update()
    {
        //base.Update();

        //-----------------------------------------//
        //do your simulation mode funcionality here//
        //-----------------------------------------//

        //switch the mode if button was clicked
        if (playMode.Value)
        {
            nextState = new BuildMode(modeText, playMode, playModes); //the next state is the angry state
            stage = EVENT.EXIT; //leave this state
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
