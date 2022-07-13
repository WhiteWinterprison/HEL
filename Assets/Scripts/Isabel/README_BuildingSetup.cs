//-----------------Building Manager Setup Guid----------------------
//---------------------by Isabel Bartelmus--------------------------
//


// In A scene you need the buildign Manager , The Cave(cave table & teleporter) and the VR player to make it work.

// The BuildingManager:
//     1. The lusit of Sockets will be created in runtime so no need to set up stuff there
//     2. In the Building List add the buildings that you want to be spawned for the VR player 
//         2.1 Here Make sure that the amount of builings is the same as you have sockets 
//         2.2 Also Make sure that the buildigns in the list and on the Table in the cave are the same (just for convinice)
//     3.Add the Teleporter from the cave in the empty Teleporter field

// The Cave Table:
// 0. Tag with CaveTable
//     1. It needs the I_CaveTableScript
//     2. Add the BuildingNr Scriptable obj if not already there
//     3. Add as many Sockets as children as you want different buildigns 
//         3.1 Make sure all Cave Sockets are taged with  "CaveSocket"
    
//     Teleporter.
//     1. Make sure that on the teleporter there is the I_CollisioNWithSocket Script -> this will tell you what building is in the teleporter
//         1.1 Make sure that the Collider is set to trigger !!
//     2. !!!!Call the Funktion GetBuildingInforamtion into the Enter Hover event on the Telepoerte socket interactor!!!!!!!!!


// The VR Belt:

//     1.The Vr player needs a New Obj Called Belt -> Tag With VRBelt
//     2.On there have the I_VRBelt Script
//         2.1 Make sure the VR Belt hase enough sockets as children for the amount of differnt buildigns you have
//         2.2 The List of sockets will be created on runtime if you dicede to add more as children
//     3.Make Sure to Tag the Sockets for the VR Belt With "VRSocket"!!!

// The Buildigns:
    
//     1. Make sure arr buildings have a GrabInteractable
//     2. Make sure all buildigns are Taged correctly -> Building1 ,Buildign2 , etc




