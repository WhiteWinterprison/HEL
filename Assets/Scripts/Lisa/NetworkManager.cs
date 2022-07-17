//+++++++++++++++++++++++++++++++++++++++++++++++++++++//
//Lisa Fröhlich Gabra, Expanded Realities, Semester 6th//
//Group 1: HEL                                         //
//+++++++++++++++++++++++++++++++++++++++++++++++++++++//


//Script: Handling the Multiplayer Setup


//What it do:
// - provides the Callbacks for connecting to the server
// - provides the Callbacks for getting into a room
// - provides the Callback for leaving the room

//Component of a Singleton manager game object (Network Manager prefab) in Lobby scene


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

//deriving from MonoBehaviour Callbacks instead of MonoBehaviour for more PUN specific functionality
public class NetworkManager : MonoBehaviourPunCallbacks
{
    #region Variables

    public ListReference roomNames;
    public BoolReference connectionStatus;

    [Header("How many Players are allowed inside of a Room")]
    public ByteReference playerCount;

    [Header("Which Scene to load when Creating a Room")]
    public ByteReference lobbyIndex;

    [Header("Which Scene to load when goind back to the Entrance Scene")]
    public ByteReference roomIndex;

    #endregion

    private void Awake()
    {
        //make sure that the value shows disconnected when starting the game
        if (connectionStatus.Value) connectionStatus.Variable.Value = false;
    }

    #region Connecting to the Server

    //function ConnectToServer() on MainMenuManager

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
        connectionStatus.Variable.Value = true;
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        connectionStatus.Variable.Value = false;
    }

    #endregion

    #region Getting into a Room Room

    //function CreateRoom() on MainMenuManager

    //create a new room and join as the first player
    public void CreateNewRoom()
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
            MaxPlayers = playerCount.Value,
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
        PhotonNetwork.LoadLevel(roomIndex.Value);
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
            roomNames.Content.Add(room.Name);
        }
    }

    #endregion

    #region Leave a room

    //function LeaveRoom() on RoomMenuManager

    //triggered when room has been left
    public override void OnLeftRoom()
    {
        Debug.Log("Left room and load scene...");

        //base.OnLeftRoom();

        //listen to the scene change to spawn the player
        SceneManager.sceneLoaded += OnSceneLoaded;

        //load scene 0
        //instead of the PHOTON scene handling we use the unity scene manager since we do not need to syncronize the scene load for all users
        SceneManager.LoadScene(lobbyIndex.Value); //index must fit the build settings
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Singleton.Instance.GetComponent<PlayerSetup>().SpawnMyPlayer();
        Debug.Log("Player spawned in Lobby again");

        //stop listening to the scene change
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    #endregion
}
