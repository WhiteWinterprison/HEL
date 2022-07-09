// Script by Lars Skogseide

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class La_PlaceBuilding : MonoBehaviour
{ 
    Renderer rend;
    public Material[] Materials; //place materials PutDown in element 0 and PickUp in element 1
    GameObject buildingBool;

    //script for VR interaction

    private void Awake()
    {
        //making sure the building renderer is recognized for changing material
        rend = GetComponent<Renderer>();
        rend.enabled = true;

        buildingBool = GameObject.Find("RoomManager");
    }

    void Start()
    {

    }

    void Update()
    {

    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Ground" || other.gameObject.tag == "Socket")
        {
            rend.material = Materials[0]; //change material to be put down
            GameObject build = this.gameObject;
            buildingBool.GetComponent<La_BuildingBoolManager>().SetBuildingToPlaced(build);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ground" || other.gameObject.tag == "Socket")
        {
            rend.material = Materials[1]; //change material to be picked up
        }
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