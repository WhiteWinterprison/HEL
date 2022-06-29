//+++++++++++++++++++++++++++++++++++++++++++++++++++++//
//Lisa Fröhlich Gabra, Expanded Realities, Semester 6th//
//Group 1: HEL                                         //
//+++++++++++++++++++++++++++++++++++++++++++++++++++++//


//Script: Disableing the other users audio listeners to avoid audio problems


//What it do:
// - checks for the user being inside the room
// - searches for all other players than the users
// - disables all the other players Audio Listeners


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Li_DisableOtherAudioListeners : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.InRoom)
        {
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Player"))
            {
                if (!obj.GetComponent<PhotonView>().IsMine)
                {
                    //disable the audio listeners (inside the cameras)
                    foreach (AudioListener audioListener in obj.GetComponentsInChildren<AudioListener>())
                    {
                        audioListener.enabled = false;
                    }
                }
            }
        }
    }
}
