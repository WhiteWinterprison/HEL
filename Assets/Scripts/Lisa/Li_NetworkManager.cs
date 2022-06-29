//+++++++++++++++++++++++++++++++++++++++++++++++++++++//
//Lisa Fröhlich Gabra, Expanded Realities, Semester 6th//
//Group 1: HEL                                         //
//+++++++++++++++++++++++++++++++++++++++++++++++++++++//


//Script: Handling the Multiplayer Setup with Photon


//What it do:
// - create the singleton instance
// - provide a function to connect to the server
// - join the lobby if connected to the server
// - provide a function to create a room
// - create and join a room if creating and joining before failed


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

//deriving from MonoBehaviour Callbacks instead of MonoBehaviour for more PUN specific functionality
public class Li_NetworkManager : MonoBehaviourPunCallbacks
{
    #region Variables

    public List<string> roomNames;

    [Header("How many Players are allowed inside of a Room")]
    [SerializeField]
    private byte playerCount = 2;

    [Header("Which Scene to load when Creating a Room")]
    [SerializeField]
    private byte sceneIndex = 1;

    [Header("Which Scene to load when goind back to the Entrance Scene")]
    [SerializeField]
    private byte entranceIndex = 0;

    #endregion

    #region Singleton Pattern

    public static Li_NetworkManager Instance { set; get; }

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

    #region Connection to Server

    //function to call in the function provided for the server button
    public void Interact_StartConnectionToServer()
    {
        Debug.Log("Try to connect to server...");

        //use default setting to connect to server (settings defined in 'Photon Server Settings')
        PhotonNetwork.ConnectUsingSettings();
    }

    //triggered when user is connected to server
    public override void OnConnectedToMaster()
    {
        //base.OnConnectedToMaster();

        Debug.Log("...Connected to server");

        //if connected join the user to lobby
        //lobby: waiting room to join a room
        PhotonNetwork.JoinLobby();
    }

    //triggered when user joined the lobby
    public override void OnJoinedLobby()
    {
        //base.OnJoinedLobby();

        Debug.Log("...Ready to join multiplayer");
    }

    #endregion

    #region Getting into a Room

    //function to call in the function provided for the create room button
    public void Interact_CreateNewRoom()
    {
        CreateNewRoom();
    }

    public void Interact_JoinRoom(string roomName)
    {
        Debug.Log("Trying to join " + roomName + "...");
        
        PhotonNetwork.JoinRoom(roomName);
    }

    //create a new room and join as the first player
    private void CreateNewRoom()
    {
        //--------------------//
        //Set the room options//
        //--------------------//

        //for now a random integer as a room name (plans to change later)
        int randomRoomName = Random.Range(0, 9999);
        //use obj initialisor instead of constructor
        RoomOptions roomOptions = new RoomOptions()
        {
            IsVisible = true,
            IsOpen = true,
            MaxPlayers = playerCount,
            PublishUserId = true //other players can see UID
        };

        //create the new room
        PhotonNetwork.CreateRoom("My Room " + randomRoomName, roomOptions);

        //show status in console
        Debug.Log("My Room " + randomRoomName + " created");
    }

    //triggered when user managed to join a room
    public override void OnJoinedRoom()
    {
        //base.OnJoinedRoom();

        Debug.Log("...Joined room and load scene...");

        //load scene, check build settings for index
        PhotonNetwork.LoadLevel(sceneIndex);
    }

    //triggered when user failed to join a room
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        //base.OnJoinRoomFailed(returnCode, message);

        Debug.Log("...Failed to join requested room, trying to join new room...");

        CreateNewRoom();
    }

    //triggered whenever the room list is updated (new room, changed room, deleted room) while in lobby
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (RoomInfo room in roomList)
        {
            roomNames.Add(room.Name);
        }
    }

    #endregion

    #region Leave a room

    //function to call in the function provided for the leave room button
    public void Interact_BackToMenu()
    {
        Debug.Log("Trying to leave room...");

        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        Debug.Log("Left room and load scene...");

        //base.OnLeftRoom();

        //load scene 0
        //instead of the PHOTON scene handling we use the unity scene manager since we do not need to syncronize the scene load for all users
        SceneManager.LoadScene(entranceIndex); //index must fit the build settings
    }

    #endregion

    #region Handing over Variables

    //function to update another script about the connection status to the server
    public bool GetConnectionStatus()
    {
        return PhotonNetwork.IsConnected;
    }

    //function to hand over the specific name of a room inside the rooms list
    public string GetRoomName(int index)
    {
        return roomNames[index];
    }

    #endregion
}
