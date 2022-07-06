//+++++++++++++++++++++++++++++++++++++++++++++++++++++//
//Lisa Fröhlich Gabra, Expanded Realities, Semester 6th//
//Group 1: HEL                                         //
//+++++++++++++++++++++++++++++++++++++++++++++++++++++//


//Script: Handling the Main Menu behaviour and providing functions for its UI elements


//What it do:
// - store the relevant variables for updating the UI inside the lobby room
// - set the starting state of the UI
// - update the UI every time the player setup changes
// - provide functions for the buttons of the UI


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class MainMenuManager : MonoBehaviour
{
    #region Variables

    public IntReference playerSetup;
    public BoolReference connectionStatus;
    public ListReference roomNames;

    [Header("The Button to connect to the Server and the Text to display")]
    [SerializeField]
    private GameObject serverButton;
    [SerializeField]
    private string connectToServer;
    [SerializeField]
    private string connectedToServer;

    [Header("Switching between the Player Setups")]
    [SerializeField]
    private GameObject switchButton;
    [SerializeField]
    private string cave;
    [SerializeField]
    private string vr;
    private int _switchInt = 1;

    [Header("The button to join a room and its variables")]
    [SerializeField]
    private GameObject joinButton;
    [SerializeField]
    private string noRoom = "";
    private int _roomIndex = 0;

    [Header("The button to create a room")]
    [SerializeField]
    private GameObject createButton;

    #endregion

    #region First State

    private void Awake()
    {
        //------------------------------------------//
        //create the starting state of the main menu//
        //------------------------------------------//

        //set the buttons to the correct state with the correct text
        serverButton.GetComponentInChildren<TextMeshProUGUI>().text = connectToServer;
        createButton.SetActive(false);
        joinButton.SetActive(false);
        joinButton.GetComponentInChildren<TextMeshProUGUI>().text = noRoom;
    }

    // Start is called before the first frame update
    void Start()
    {
        //based on the player setup set the text in the setup switch button
        switch (playerSetup.Value)
        {
            case 1: switchButton.GetComponentInChildren<TextMeshProUGUI>().text = cave; _switchInt = 1; break;
            case 2: switchButton.GetComponentInChildren<TextMeshProUGUI>().text = vr; _switchInt = 2; break;
            default: break;
        }
    }

    #endregion

    #region Dependencies and Functionality without Interactions

    private void Update()
    {
        //--------------------------------------------------------------------------//
        //handle the feedback about server connection and the ability to join a room//
        //--------------------------------------------------------------------------//

        if (connectionStatus.Value && !joinButton.activeSelf)
        {
            joinButton.SetActive(true);
            createButton.SetActive(true);
            serverButton.GetComponentInChildren<TextMeshProUGUI>().text = connectedToServer;
        }
        else if (!connectionStatus.Value && joinButton.activeSelf)
        {
            joinButton.SetActive(false);
            createButton.SetActive(false);
            serverButton.GetComponentInChildren<TextMeshProUGUI>().text = connectToServer;
        }

        //------------------------------------------------------------------//
        //handle the user feedback about the currently selected player setup//
        //------------------------------------------------------------------//

        if (playerSetup.Value != _switchInt)
        {
            switch (playerSetup.Value)
            {
                case 1: switchButton.GetComponentInChildren<TextMeshProUGUI>().text = cave; _switchInt = 1; break;
                case 2: switchButton.GetComponentInChildren<TextMeshProUGUI>().text = vr; _switchInt = 2; break;
                default: break;
            }
        }
    }

    #endregion

    #region Provided Functions

    //function provided for the server button
    public void ConnectToServer()
    {
        Debug.Log("Start connection process...");

        //use default setting to connect to server (settings defined in 'Photon Server Settings')
        PhotonNetwork.ConnectUsingSettings();
    }

    //function provided for the quit button
    public void QuitApplication()
    {
        Application.Quit();
    }

    //function provided for the create button
    public void CreateRoom()
    {
        Debug.Log("Start creation process...");

        //call the function from the Network Manager to create a new room
        Singleton.Instance.GetComponent<NetworkManager>().CreateNewRoom();
    }

    //function provided for the join button
    public void JoinRoom(TextMeshProUGUI roomName)
    {
        Debug.Log("Start joining room " + roomName.text + "...");

        PhotonNetwork.JoinRoom(roomName.text);
    }

    //function provided for the switching buttons next to the join button
    public void ShowNextRoom(bool right)
    {
        //--------------------------------------------//
        //what to display when which button is clicked//
        //--------------------------------------------//

        if (right) //the right button was clicked
        {
            _roomIndex++;
            if (_roomIndex < roomNames.Content.Count)
            {
                joinButton.GetComponentInChildren<TextMeshProUGUI>().text = roomNames.Content[_roomIndex];
            }
            else //if the index exceeds the list start from the beginning
            {
                _roomIndex = 0;
                joinButton.GetComponentInChildren<TextMeshProUGUI>().text = roomNames.Content[_roomIndex];
            }
        }
        else //the left button was clicked
        {
            _roomIndex--;
            if (_roomIndex >= 0)
            {
                joinButton.GetComponentInChildren<TextMeshProUGUI>().text = roomNames.Content[_roomIndex];
            }
            else //if the index exceeds the list start from the end
            {
                _roomIndex = roomNames.Content.Count - 1;
                joinButton.GetComponentInChildren<TextMeshProUGUI>().text = roomNames.Content[_roomIndex];
            }
        }
    }

    //function provided for the setup changer button
    public void ChangeSetup()
    {
        switch (playerSetup.Value)
        {
            case 1: playerSetup.Variable.Value = 2; break;
            case 2: playerSetup.Variable.Value = 1; break;
            default: break;
        }
    }

    #endregion
}
