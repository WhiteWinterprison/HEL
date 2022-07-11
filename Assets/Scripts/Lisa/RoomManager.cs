//+++++++++++++++++++++++++++++++++++++++++++++++++++++//
//Lisa Fröhlich Gabra, Expanded Realities, Semester 6th//
//Group 1: HEL                                         //
//+++++++++++++++++++++++++++++++++++++++++++++++++++++//


//Script: Handling the Multiplayer Setup inside the room


//What it do:
// - registers and unregisters the new multiplayer scene
// - spawns the currently needed player when the new multiplayer scene is loaded


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

//deriving from MonoBehaviour Callbacks instead of MonoBehaviour for more PUN specific functionality
public class RoomManager : MonoBehaviourPunCallbacks
{
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
        Singleton.Instance.GetComponent<PlayerSetup>().SpawnMyPlayer();
    }

    #endregion
}
