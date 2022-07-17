//+++++++++++++++++++++++++++++++++++++++++++++++++++++//
//Lisa Fröhlich Gabra, Expanded Realities, Semester 6th//
//Group 1: HEL                                         //
//+++++++++++++++++++++++++++++++++++++++++++++++++++++//


//Script: Disabling the other users cameras to avoid rendering problems


//Component of UI game object/prefab


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SetEventCamera : MonoBehaviour
{
    private void Start()
    {
        SetCameraToEventCamera();
    }

    private void OnEnable()
    {
        SetCameraToEventCamera();
    }

    public void SetCameraToEventCamera()
    {
        //check first if there is the need to differenciate between multiple users
        if (PhotonNetwork.InRoom)
        {
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Panel")) //find all possible UIs
            {
                if (obj.GetComponent<PhotonView>().IsMine) //find the one that belongs to this user
                {
                    //and set the camera
                    foreach (Canvas canvas in obj.GetComponentsInChildren<Canvas>())
                    {
                        canvas.worldCamera = this.GetComponentInChildren<Camera>();
                        Debug.Log("camera is now world camera");
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
                    Debug.Log("camera is now world camera");
                }
            }
        }
    }
}
