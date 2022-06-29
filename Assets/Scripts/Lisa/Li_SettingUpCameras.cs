//+++++++++++++++++++++++++++++++++++++++++++++++++++++//
//Lisa Fröhlich Gabra, Expanded Realities, Semester 6th//
//Group 1: HEL                                         //
//+++++++++++++++++++++++++++++++++++++++++++++++++++++//


//Script: Disableing the other users cameras to avoid rendering problems


//What it do:
// - checks for the user being inside of the room
// - searches for all other players than the users
// - disables all the other players cameras
// - set the players camera to be the event camera of the UI

//backup commit test

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Li_SettingUpCameras : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.InRoom)
        {
            //disable the other players cameras
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Player"))
            {
                if (!obj.GetComponent<PhotonView>().IsMine)
                {
                    foreach (Camera cam in obj.GetComponentsInChildren<Camera>())
                    {
                        cam.enabled = false;
                    }
                }
            }

            //assign the users own camera to the UI
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Panel"))
            {
                if (obj.GetComponent<PhotonView>().IsMine)
                {
                    foreach (Canvas canvas in obj.GetComponentsInChildren<Canvas>())
                    {
                        canvas.worldCamera = this.GetComponentInChildren<Camera>();
                    }
                }
            }
        }
        else
        {
            //assign the users own camera to the UI
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Panel"))
            {
                foreach (Canvas canvas in obj.GetComponentsInChildren<Canvas>())
                {
                    canvas.worldCamera = this.GetComponent<Camera>();
                }
            }
        }
    }
}
