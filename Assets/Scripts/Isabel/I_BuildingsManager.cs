//---------by Isabel Bartelmus-----------
//You need CaveTable & VrBelt on 2 obj to make this work
//-----------24.06.22-------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.XR.Interaction.Toolkit;
using Photon.Pun;

using Hashtable = ExitGames.Client.Photon.Hashtable; //need for photon Hash

public class I_BuildingsManager : MonoBehaviour
{
    //Singelton -> We only want 1 Manager

    //------------------Events------------------
     public static event Action OnBuilding_givable;
     public static event Action OnBuilding_placable;

    //------------------Variabels------------------
    [TextArea]
    public String SetupInfo;
    [SerializeField]private List<GameObject> CaveSockets;
    [SerializeField]private List<GameObject> VRSockets; 
    [SerializeField]private List<GameObject> Buildings; 
    [SerializeField] private GameObject Teleporter;
    private bool canBePlaced= false;
    private bool CanBeGiven=false ;
    public IntObject BuildingNr;
    public IntObject BeltCounter;


    private GameObject BuildingToInstantiate{get; set;}


    //---------------Reverences--------------------
    //singelton
    public static I_BuildingsManager Instance { set; get; }


    //--------------Code-----------------------

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
        BeltCounter.Value = 0;
        foreach(GameObject tempObj in GameObject.FindGameObjectsWithTag("CaveSocket"))
        {
            CaveSockets.Add(tempObj);
        }
        foreach(GameObject tempObj in GameObject.FindGameObjectsWithTag("VRSocket"))
        {
            VRSockets.Add(tempObj);
        }


        //Subscribing as a Listener to following events:
        I_CaveTable.OnBuildingGiven += BuildingIsGiven;
        I_VrBelt.OnBuildingPlaced += BuildingIsPlaced;

        // BuildingManager shall assign each socketpair which building will be used
        if (VRSockets.Count != CaveSockets.Count)
        {
            Debug.LogError("Mismatch on socket count between Cave and VR\n Check if Sockets are tagged");
        }

        for (int i = 0; i < VRSockets.Count; i++)
        {
            if(i >= Buildings.Count)
            {   
                Debug.LogWarning("There are not enough buildings in the list of the BuildingsManager!");
                break;
            }
            VRSockets[i].GetComponent<XRSocketInteractor>().startingSelectedInteractable = Buildings[i].GetComponent<XRGrabInteractable>();
            CaveSockets[i].GetComponent<XRSocketInteractor>().startingSelectedInteractable = Buildings[i].GetComponent<XRGrabInteractable>();
        }



    }

   void Update()
   {    
        RemovedFromBelt();
        if(canBePlaced == true)
        {
            OnBuilding_placable?.Invoke(); //If the event exist do it
           // Debug.Log("BuildingManager: Place It");
        }

        if(CanBeGiven == true)
        {
            OnBuilding_givable?.Invoke();
           // Debug.Log("BuildinManager: Given");
        }
   }

   public void spawnBuilding()
   {    
        string interactable = Teleporter.GetComponent<XRSocketInteractor>().attachTransform.ToString();
        //Debug.Log(interactable);
   }

   private void RemovedFromBelt()
   {
        //if lars bool == true int -1
        if((bool)PhotonNetwork.CurrentRoom.CustomProperties["BuildingYeeted"] == true)
        {
           BeltCounter.Value -= 1; 
           Debug.Log("Building was yeeted from ze Belt");
        }
        if(BeltCounter.Value < 1)
        {
            canBePlaced = false;
        }

   }


#region Events
   private void BuildingIsGiven()   // Building is teleported to the VR
   {
        //Get int from I_CaveTable which building was used 
        //use the info to tell what building from List Buildings should be given over 
        for(int i = 0; i < Buildings.Count; i++)
        {
            if(i == BuildingNr.Value)
            {
                //set BuildingToInstantiate to the right buildign nr  
                BuildingToInstantiate = Buildings[i]; 
                //Debug.Log("Building"+ BuildingNr.Value +" is Ready for VR use");
                //Debug.LogWarning("Manager Chosen Building" + Buildings[i]+ "for Vr user");
                
                canBePlaced = true;
                BeltCounter.Value += 1;

                if(BeltCounter.Value >= VRSockets.Count) //stuff on belt not allowed to be more than sockets exsist
                {
                    CanBeGiven = false;
                }
                else //if less builings than sockets you can teleport buildings
                {   
                    // TODO: Maybe redundant
                    CanBeGiven = true;
                }
                
            }

        }       
   }

   public GameObject GetBuilding()
    {
      return BuildingToInstantiate;
    }


   private void BuildingIsPlaced()
   {
        //Debug.Log("Vr User used building, Bulidings can be given out again");
        canBePlaced = false;
        CanBeGiven = true;
   }




  private void OnDisable()
  {
    //unsubscribe to following events if theye are Distroyed:
    I_CaveTable.OnBuildingGiven -= BuildingIsGiven;
    I_VrBelt.OnBuildingPlaced -= BuildingIsPlaced;
  }
  #endregion 

}

