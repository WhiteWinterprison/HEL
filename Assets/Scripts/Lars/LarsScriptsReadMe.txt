Setup:
- add XR grab interactable to buildings for grabbing and placing
- attach La_PlaceBuilding script to buildings
- Add "ground" tag to ground and/or "socket" tag to socket interactors
- Add trigger collider to ground/socket

To change material of building upon pick up and placement:
- add placement material to element 0 and pick-up material to element 1 on La_PlaceBuilding script in inspector

To pass variable of which building is placed:
- attach La_BuildingBoolManager script to GameObject named "RoomManager"
- To each building, add one of these tags ("Building1", "Building2", "Building3", "Building4" and "Building5") to identify which building is being placed 