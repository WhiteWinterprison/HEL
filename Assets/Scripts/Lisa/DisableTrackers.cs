//+++++++++++++++++++++++++++++++++++++++++++++++++++++//
//Lisa Fröhlich Gabra, Expanded Realities, Semester 6th//
//Group 1: HEL                                         //
//+++++++++++++++++++++++++++++++++++++++++++++++++++++//


//Script: Disabling the other users tracking to avoid tracking problems

//Component of player prefab


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR.Interaction.Toolkit;
using Photon.Pun;

public class DisableTrackers : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //only needed when in the multiplayer scene already (otherwise there are no multiple players)
        if (PhotonNetwork.InRoom)
        {
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Player")) //find all the players
            {
                if (!obj.GetComponent<PhotonView>().IsMine) //find the ones that are not this player
                {
                    //disable the tracked pose driver (on the head)
                    foreach (TrackedPoseDriver poseDriver in obj.GetComponentsInChildren<TrackedPoseDriver>())
                    {
                        poseDriver.enabled = false;
                    }

                    //disable the xr controllers (inside of the hands)
                    foreach (ActionBasedController controller in obj.GetComponentsInChildren<ActionBasedController>())
                    {
                        controller.enabled = false;
                    }
                }
            }
        }
    }
}
