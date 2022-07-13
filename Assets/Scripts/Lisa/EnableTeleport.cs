//+++++++++++++++++++++++++++++++++++++++++++++++++++++//
//Lisa Fröhlich Gabra, Expanded Realities, Semester 6th//
//Group 1: HEL                                         //
//+++++++++++++++++++++++++++++++++++++++++++++++++++++//


//Script: Enabling the Teleportation for the VR user by adding the users teleportation provider as the one to use for the teleportation areas

//Component of VR player prefab


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class EnableTeleport : MonoBehaviour
{
    //the teleportation area we want to be able to teleport on
    private TeleportationArea teleportArea;

    [Header("This Prefabs Teleportation Provider")]
    [SerializeField]
    private TeleportationProvider teleportProvider;

    // Start is called before the first frame update
    void Start()
    {
        //find the teleportation areas in this scene
        foreach (GameObject teleportArea in GameObject.FindGameObjectsWithTag("Ground"))
        {
            //and set this prefabs teleportation provider as the one to use
            teleportArea.GetComponentInChildren<TeleportationArea>().teleportationProvider = teleportProvider;
        }
    }
}
