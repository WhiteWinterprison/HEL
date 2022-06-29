//+++++++++++++++++++++++++++++++++++++++++++++++++++++//
//Lisa Fröhlich Gabra, Expanded Realities, Semester 6th//
//Group 1: HEL                                         //
//+++++++++++++++++++++++++++++++++++++++++++++++++++++//


//Script: Enableing the Teleportation for the VR user


//What it do:
// - find the ground and sets the teleportation provider of its teleportation area to be the players teleportation provider


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Li_EnableTeleportation : MonoBehaviour
{
    private TeleportationArea teleportArea;

    [Header("This Prefabs Teleportation Provider")]
    [SerializeField]
    private TeleportationProvider teleportProvider;

    // Start is called before the first frame update
    void Start()
    {
        teleportArea = GameObject.FindGameObjectWithTag("Ground").GetComponent<TeleportationArea>();

        teleportArea.teleportationProvider = teleportProvider;
    }
}
