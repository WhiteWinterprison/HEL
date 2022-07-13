//+++++++++++++++++++++++++++++++++++++++++++++++++++++//
//Lisa Fröhlich Gabra, Expanded Realities, Semester 6th//
//Group 1: HEL                                         //
//+++++++++++++++++++++++++++++++++++++++++++++++++++++//


//Script: Providing a Singleton Pattern for the Network Manager GameObject for better visibility what object is a Singleton


//What it do:
// - provide a singleton pattern for a singleton object so we can easily see which object is a singleton in our scenes


//Component of ONE Singleton Manager (Network Manager prefab)


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    #region Singleton Pattern

    public static Singleton Instance { set; get; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else Destroy(this.gameObject);
    }

    #endregion
}
