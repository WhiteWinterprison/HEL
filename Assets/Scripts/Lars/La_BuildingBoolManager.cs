using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

using Hashtable = ExitGames.Client.Photon.Hashtable; //This line need to be on every script that uses the Hashtable!!

public class La_BuildingBoolManager : MonoBehaviour
{
    [SerializeField]
    Hashtable buildingPlaced = new Hashtable() { { "building1", false }, { "building2", false }, { "building3", false }, { "building4", false }, { "building5", false } };

    private void Awake()
    {
        //create "building" properties
        buildingPlaced["building1"] = false;
        buildingPlaced["building2"] = false;
        buildingPlaced["building3"] = false;
        buildingPlaced["building4"] = false;
        buildingPlaced["building5"] = false;
        PhotonNetwork.CurrentRoom.SetCustomProperties(buildingPlaced);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(PhotonNetwork.CurrentRoom.CustomProperties.ToString());
    }

    public void SetBuildingToPlaced(GameObject build)
    {
        if (build.gameObject.tag == "Building1")
        {
            buildingPlaced["building1"] = true;
        }
        if (build.gameObject.tag == "Building2")
        {
            buildingPlaced["building2"] = true;
        }
        if (build.gameObject.tag == "Building3")
        {
            buildingPlaced["building3"] = true;
        }
        if (build.gameObject.tag == "Building4")
        {
            buildingPlaced["building4"] = true;
        }
        if (build.gameObject.tag == "Building5")
        {
            buildingPlaced["building5"] = true;
        }
        PhotonNetwork.CurrentRoom.SetCustomProperties(buildingPlaced);
    }
}
