// Script by Lars Skogseide

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

using Hashtable = ExitGames.Client.Photon.Hashtable; //This line need to be on every script that uses the Hashtable!!

public class La_PlaceBuilding : MonoBehaviour
{ 
    Renderer rend;
    public Material[] Materials; //place materials PutDown in element 0 and PickUp in element 1

    //public bool buildingPlaced;

    [SerializeField]
    Hashtable buildingPlaced = new Hashtable() { { "building1", false }, { "building2", false }, { "building3", false }, { "building4", false }, { "building5", false }, { "building?", false } };

    //script for VR interaction

    private void Awake()
    {
        //create "building" properties
        buildingPlaced["building1"] = false;
        buildingPlaced["building2"] = false;
        buildingPlaced["building3"] = false;
        buildingPlaced["building4"] = false;
        buildingPlaced["building5"] = false;
        buildingPlaced["building?"] = false;
        PhotonNetwork.CurrentRoom.SetCustomProperties(buildingPlaced);
    }

    void Start()
    {
        //making sure the building renderer is recognized for changing material
        rend = GetComponent<Renderer>();
        rend.enabled = true;
    }

    void OnTriggerEnter(Collider other)
    {
            if (other.gameObject.tag == "Ground" || other.gameObject.tag == "Socket")
            {
                rend.material = Materials[0]; //change material to be put down
                SetBuildingToPlaced();
            }
    }

    void OnTriggerExit(Collider other)
    {
            if (other.gameObject.tag == "Ground" || other.gameObject.tag == "Socket")
            {
                rend.material = Materials[1]; //change material to be picked up
            }
    }

    public void SetBuildingToPlaced()
    {
        if(this.gameObject.tag == "Building1")
        {
            buildingPlaced["building1"] = true;
        }
        if (this.gameObject.tag == "Building2")
        {
            buildingPlaced["building2"] = true;
        }
        if (this.gameObject.tag == "Building3")
        {
            buildingPlaced["building3"] = true;
        }
        if (this.gameObject.tag == "Building4")
        {
            buildingPlaced["building4"] = true;
        }
        if (this.gameObject.tag == "Building5")
        {
            buildingPlaced["building5"] = true;
        }
        else
        {
            buildingPlaced["building?"] = true;
        }
        PhotonNetwork.CurrentRoom.SetCustomProperties(buildingPlaced);
    }
}


#region archive code

// script for mouse interaction where grey building is replaced with textured building

//RaycastHit hit;

// Start is called before the first frame update
/*void Start()
{
    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

    if (Physics.Raycast(ray, out hit, 50000.0f, (1 << 3)))
    {
        transform.position = hit.point;
    }
}

// Update is called once per frame
void Update()
{
    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

    if(Physics.Raycast(ray, out hit, 50000.0f, (1 << 3)))
    {
        transform.position = hit.point;
    }

    if(Input.GetMouseButton(0))
    {
            Instantiate(prefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
    }
}
*/

#endregion