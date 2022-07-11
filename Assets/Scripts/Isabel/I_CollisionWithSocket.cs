//-------Isabel Bartelmus---------
//This script can be used to detec what object is righnow inside a socket interactor 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class I_CollisionWithSocket : MonoBehaviour
{
    [TextArea] 
    public string InfoTxt;
    public string BuildingName;
    
    void Awake()
    {   
       
    }

    private void OnTriggerEnter(Collider other)
    {
        BuildingName = other.gameObject.tag;
        //Debug.Log("In Socket: "+ BuildingName);
    }
}
