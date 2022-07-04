//+++++++++++++++++++++++++++++++++++++++++++++++++++++//
//Lisa Fröhlich Gabra, Expanded Realities, Semester 6th//
//Group 1: HEL                                         //
//+++++++++++++++++++++++++++++++++++++++++++++++++++++//


//Script: Enabling the Teleportation for the VR user


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
        //find the teleportation area in this scene
        teleportArea = GameObject.FindGameObjectWithTag("Ground").GetComponent<TeleportationArea>();

        //and set this prefabs teleportation provider as the one to use
        teleportArea.teleportationProvider = teleportProvider;
    }
}
