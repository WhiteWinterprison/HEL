//+++++++++++++++++++++++++++++++++++++++++++++++++++++//
//Lisa Fröhlich Gabra, Expanded Realities, Semester 6th//
//Group 1: HEL                                         //
//+++++++++++++++++++++++++++++++++++++++++++++++++++++//


//Script: Handling the Multiplayer Setup inside the room


//What it do:
// - create the singleton instance
// - register and unregister the scene from the delegate
// - according to the player setup, spawn the correct player prefab


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

//deriving from MonoBehaviour Callbacks instead of MonoBehaviour for more PUN specific functionality
public class Li_RoomManager : MonoBehaviourPunCallbacks
{
    #region Singleton Pattern

    public static Li_RoomManager Instance { set; get; }

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

    private void OnEnable()
    {
        //register to delegate
        SceneManager.sceneLoaded += OnSceneLoad;

        //set the first states of the data

    }

    private void OnDestroy()
    {
        //unregister from delegate
        SceneManager.sceneLoaded -= OnSceneLoad;
    }

    #region Spawning the Player

    private void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        //reload the player setup to spawn the player prefab correctly
        Li_NetworkManager.Instance.GetComponent<Li_PlayerSetup>().SpawnMyPlayer();
    }

    #endregion
}
