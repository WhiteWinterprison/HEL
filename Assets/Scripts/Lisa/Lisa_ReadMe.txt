//----------------------//
//ReadMe for Lisa's Code//
//----------------------//

//Lobby Scene

- put the build index for the Lobby_Scene into the scriptable object "lobbyIndex" in Assets>>Scripts>>Lisa>>ScriptableObjects
- set the Vector3 scriptable object "defaultSpawn" in Assets>>Scripts>>Lisa>>ScriptableObjects to match a suitable position for your player to spawn in
- get the NetworkManager prefab into the scene from Assets>>Prefabs>>Multiplayer
	- the prefab needs to have the scripts Singleton, NetworkManager and PlayerSetup
	- all scriptable objects for the NetworkManager and PlayerSetup script have already been created and are in Assets>>Scripts>>Lisa>>ScriptableObjects
		(if in question never tick "Use Constant")
	- there need to be two player prefabs in Assets>>Resources that need to be put into the PlayerSetup script
	- the Display Count for the PlayerSetup script is 6
- there needs to be the XRInteractionManager prefab from Assets>>Prefabs>>Interactions in the scene
	- the prefab needs to have the components XRInteractionManager script, InputActionManager and PlayerInput
	- the InputActionManager needs two Action Assets: "RI Default Input Actions" and "Player Input Actions"
	- the PlayerInput needs the "Player Input Actions" as the Actions and the Behaviour needs to be set to "Invoke Unity Events"
- add the MainMenuManager prefab to the scene from Assets>>Prefabs>>UI
	- the prefab needs to have the MainMenuManager script
	- add the necessary scriptable objects from Assets>>Scripts>>Lisa>>ScriptableObjects
	- the button game objects needed are children from the MainMenu prefab (will come to that in the next points)
	- the two strings required for "Connect to Server" and "Connected to Server" are meant as messages on the connection button as user feedback and can also just be what their variable names already suggest
	- "No Room" is also a string and should either state that there is no room or be empty
- add the MainMenu prefab from Assets>>Prefabs>>UI
	- it needs to have the SetEventCamera script
	- if there was no EventSystem in the scene or has not been created automatically in the last step please add one manually now
	- the buttons and their necessary functions:
		- server_button -> ConnectToServer() on MainMenuManager
		- room_button -> CreateRoom() on MainMenuManager
		- join_button -> JoinRoom() on MainMenuManager
		- two children from join_button right and left -> ShowNextRoom() on MainMenuManager (tick the required boolean only for the right button)
		- quit_button -> QuitApplication() on MainMenuManager

//Multiplayer Scene

- put the build index for the Multiplayer_Scene into the scriptable object "roomIndex" in Assets>>Scripts>>Lisa>>ScriptableObjects
- set the Vector3 scriptable objects "caveSpawn" and "vrSpawn" in Assets>>Scripts>>Lisa>>ScriptableObjects to match the positions you want the two players to spawn in
- put a RoomManager prefab from Assets>>Prefabs>>Multiplayer in the scene
	- the prefab needs to have the RoomManager script and the PlayModes script
	- in the public event OnModeChanged() in the PlayModes script add the function UpdateUI() from the RoomMenuManager (I will come to the setup of that game object later)
	- add the PlayModes script from the RoomManager itself to the "Play Modes" in the PlayModes script
	- add the necessary scriptable objects from Assets>>Scripts>>Lisa>>ScriptableObjects
- add the XRInteractionManager prefab just as described a few lines earlier
- add the RoomMenuManager prefab from Assets>>Prefabs>>UI
	- this prefab needs to have the RoomMenuManager script
	- add all necessary scriptable objects from Assets>>Scripts>>Lisa>>ScriptableObjects
	- add the three UI element prefabs CAVEUI, VRUI, DataUI to the scene
	- create a DataNet game object by creating a plane and adding the Wave Shader from Assets>>Materials>>Shaders to its Mesh Renderer
	- input the three UI prefabs into the RoomMenuManager script and add the new DataNet game object to the required place in the script as well
- Components and functions for CAVEUI and VRUI:
	- both UI prefabs need to have the SetEventCamera script
	- the SwitchMode_Button needs the SwitchMode() function from the RoomManager game object
	- the LeaveRoom_Button needs the LeaveRoom() function from the RoomMenuManager game object
