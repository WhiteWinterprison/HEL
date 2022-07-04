// Script by Lars Skogseide

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class La_PlaceBuilding : MonoBehaviour
{
    //public GameObject prefab;
    //bool placedBuilding;
  
    Renderer rend;
    public Material[] Materials;

    //script for VR interaction
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (rend != null)
        {
            if (other.gameObject.tag == "Ground" || other.gameObject.tag == "Socket")
            {
                rend.material = Materials[0];
            }
        }
        else
            Debug.LogWarning("Building Renderer not found, unable to change material");
    }

    void OnTriggerExit(Collider other)
    {
        if (rend != null)
        {
            if (other.gameObject.tag == "Ground" || other.gameObject.tag == "Socket")
            {
                rend.material = Materials[1];
            }
        }
        else
            Debug.LogWarning("Building Renderer not found, unable to change material");
    }
}

//archive code

// script for mouse interaction

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