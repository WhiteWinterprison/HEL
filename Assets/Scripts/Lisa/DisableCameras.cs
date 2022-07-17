//+++++++++++++++++++++++++++++++++++++++++++++++++++++//
//Lisa Fröhlich Gabra, Expanded Realities, Semester 6th//
//Group 1: HEL                                         //
//+++++++++++++++++++++++++++++++++++++++++++++++++++++//


//Script: Disabling the other users cameras to avoid rendering problems

//Component of player prefab


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DisableCameras : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //only do this when in the multiplayer scene (otherwise there are no multiple players and no need to fix anything)
        if (PhotonNetwork.InRoom)
        {
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Player")) //find all players
            {
                if (!obj.GetComponent<PhotonView>().IsMine) //find the ones that are not this player
                {
                    //and disable their cameras
                    foreach (Camera cam in obj.GetComponentsInChildren<Camera>())
                    {
                        cam.enabled = false;
                    }
                }
            }
        }
    }
}
