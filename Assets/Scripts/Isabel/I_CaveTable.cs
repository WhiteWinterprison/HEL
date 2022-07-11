//---------by Isabel Bartelmus-----------
//You need CaveTable & BuildignsManager on 2 obj to make this work
//-----------24.06.22-------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.XR.Interaction.Toolkit;

public class I_CaveTable : MonoBehaviour
{
    //---------Events By this Script--
    public static event Action OnBuildingGiven;

    //----------Reverences-----------
    public static I_CaveTable Instance { set; get; }
    I_CollisionWithSocket i_socketCollision;

    public XRSocketInteractor teleporterSocket;

    //----------Variables-------------
    //private bool buildingEnabled = true;
    public IntObject BuildingNr;
    //---------Code------------------
    private void Awake()
    {
        //Singelton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else Destroy(this.gameObject);

        //get components
        i_socketCollision = GetComponentInChildren<I_CollisionWithSocket>(); //get the script in the child on the teleporter
    }
 
    void Start()
    {
        //Subsicrbe to BuildingManager event
        I_BuildingsManager.OnBuilding_givable += BuildingCanBeGiven;
    }
    
    //Which building is in the teleporter ?
    public  void GetBuildingInTeleporter() //get called in the Enter hover even on Teleporter !!!
    {

        Debug.Log(i_socketCollision.BuildingName, this.gameObject);

        if(i_socketCollision.BuildingName == "Building1")
        {
            BuildingNr.Value = 0;
            //Debug.Log("Building Nr:" + BuildingNr.Value);
            BuildingWasTouched();
        }
        else if(i_socketCollision.BuildingName == "Building2")
        {
            BuildingNr.Value = 1;
            //Debug.Log("Building Nr:" + BuildingNr.Value);
            BuildingWasTouched();
        }
        else if(i_socketCollision.BuildingName == "Building3")
        {
            BuildingNr.Value = 2;
            //Debug.Log("Building Nr:" + BuildingNr.Value);
            BuildingWasTouched();
        }
          else if(i_socketCollision.BuildingName == "Building4")
        {
            BuildingNr.Value = 3;
            //Debug.Log("Building Nr:" + BuildingNr.Value);
            BuildingWasTouched();
        }
          else if(i_socketCollision.BuildingName == "Building5")
        {
            BuildingNr.Value = 4;
           //Debug.Log("Building Nr:" + BuildingNr.Value);
            BuildingWasTouched();
        }
        else
        {
            Debug.LogError("Obj without tag in Teleporter/n check if all buildigns and children are tagged");
        }
    }

    /*
    //---------------------------------------------------------------------------------------
    //---------------------------------------------------------------------------------------
    //-----------------------------HOW to get infor if stuff is inhj socket------------------
    void Update()
    {

        //-----------------------------HOW to get infor if stuff is inhj socket------------------
        IXRSelectInteractable objName = teleporterSocket.GetOldestInteractableSelected();
       
        Debug.Log(objName);
        //--------------------------------------------------------------------------------------
    }
    //-----------------------------------------------------------------------------------------
    //-----------------------------------------------------------------------------------------
    */


    #region Events
     private void BuildingCanBeGiven()
    {
        //Debug.Log("Building can be used Again");
        //buildingEnabled = true;
    }

    public void BuildingWasTouched() //When Building was touched Invoke this funktion
    {
        OnBuildingGiven?.Invoke();
        //buildingEnabled =false;
        //Debug.Log("Building was given to VR user");
    }

    void OnDisable()
    {
        I_BuildingsManager.OnBuilding_givable -= BuildingCanBeGiven;
    }

    #endregion


}
