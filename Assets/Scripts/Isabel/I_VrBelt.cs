//---------by Isabel Bartelmus-----------
//You need BuildingsManager & VrBelt on 2 obj to make this work
//-----------24.06.22-------------------using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Photon.Pun;
using Photon.Realtime;

public class I_VrBelt : MonoBehaviour
{
    //Singelton -> We only want 1 Manager
    #region  Singelton
    [SerializeField] public static I_VrBelt Instance { set; get; }

    #endregion

    //Event Created by this Instance
    public static event Action OnBuildingPlaced;

    //----------variables-----------------
    [SerializeField]private List<GameObject> vrSockets; 
    //private bool buildEnabled = false;
    public IntObject BuildingNr;
    public IntObject BeltCounter;
    private GameObject BuildingToSpawn;
    private bool IsAllowedToSapwn = true;

    //----------Rev---------------------
    

//---------------------------------------------------------------------------------
//-------get the info from BuildingsManager what building needs to be placed-------
//-------get Int from Scriptable obj to know which socket should be used for spawn-
//---------------------------------------------------------------------------------
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else Destroy(this.gameObject);      
        
    }
    void Start()
    {
        
        foreach(GameObject tempObj in GameObject.FindGameObjectsWithTag("VRSocket"))
        {
            vrSockets.Add(tempObj);
        } 

        //Subscribe to BuildingManager event
        I_BuildingsManager.OnBuilding_placable += BuildingCanBePlaced;
    }

    //Instatiate obj on right Socket
    private void InstantateObjOnSocket()
    {
        //dependign on what int we have get differnt transform to Instantiate the obj
        int i = BeltCounter.Value;

        vrSockets[i].GetComponent<Transform>();

        Quaternion B_rotation = vrSockets[i].GetComponent<Transform>().rotation;
        Vector3 B_position = vrSockets[i].GetComponent<Transform>().position;

        //Get the right obj
        if(I_BuildingsManager.Instance!= null)
        {
            BuildingToSpawn =I_BuildingsManager.Instance.GetBuilding();
            //Debug.Log("VR Belt Holds the model"+BuildingToSpawn);
        }

        // GameObject BuildingClone = Instantiate(BuildingToSpawn, B_position, B_rotation);
        // Debug.LogWarning (BuildingClone);
       
        //PhotonNetwork.Instantiate(BuildingName, B_position, Quaternion.identity, 0);
        PhotonNetwork.Instantiate(BuildingToSpawn.name , B_position, Quaternion.identity, 0);
        IsAllowedToSapwn = false;

    }

    #region Events 
    private void BuildingCanBePlaced()
    {
        if(IsAllowedToSapwn == true)
        {
            InstantateObjOnSocket(); //get called to fotern right now
            //Debug.Log("Building waiting to be placed");
        }
    }

    public void BuildWasPlaced()
    {
        OnBuildingPlaced?.Invoke();
        IsAllowedToSapwn = true;
        //Debug.Log("Building was placed can be given again");
    }

    void OnDisable()
    {
        I_BuildingsManager.OnBuilding_placable -= BuildingCanBePlaced;
    }

    #endregion
}
