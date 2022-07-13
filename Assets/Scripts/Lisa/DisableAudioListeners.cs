//+++++++++++++++++++++++++++++++++++++++++++++++++++++//
//Lisa Fröhlich Gabra, Expanded Realities, Semester 6th//
//Group 1: HEL                                         //
//+++++++++++++++++++++++++++++++++++++++++++++++++++++//


//Script: Disabling the other users audio listeners to avoid audio problems

//Component of player prefab


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DisableAudioListeners : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //this only needs to be done when in the Multiplayer scene already (otherwise there are no multiple players)
        if (PhotonNetwork.InRoom)
        {
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Player")) //find all players in the scene
            {
                if (!obj.GetComponent<PhotonView>().IsMine) //find the ones that are not this player
                {
                    //and disable the audio listeners (inside the cameras)
                    foreach (AudioListener audioListener in obj.GetComponentsInChildren<AudioListener>())
                    {
                        audioListener.enabled = false;
                    }
                }
            }
        }
    }
}
