using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

using Hashtable = ExitGames.Client.Photon.Hashtable; //need for photon Hash

public class I_BuildingIsPlaced : MonoBehaviour
{   
    private bool buildingsPlaced = false;
    
    [SerializeField] Hashtable IsBuildingPlaced = new Hashtable() { {"BuildingYeeted", false} }; //if builing is thorwn out of belt !!donst check if it placed just if its no longer in belt!!
    

    private void Start()
    {
        IsBuildingPlaced["BuildingYeeted"] = false;
        PhotonNetwork.CurrentRoom.SetCustomProperties(IsBuildingPlaced);
    }

    public void CalculateCounter()
    {
        buildingsPlaced = true;
        IsBuildingPlaced["BuildingYeeted"] = buildingsPlaced;
        PhotonNetwork.CurrentRoom.SetCustomProperties(IsBuildingPlaced);
    }

    public bool Counter()
    {
        return buildingsPlaced;
    }

    public void Test()
    {
        
    }
}
