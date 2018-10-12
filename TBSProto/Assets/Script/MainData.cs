using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TEST-PENDING - Define terrain type for specific behaviors like lava or healing tiles, or whatever it comes to mind.
public enum TerrainType
{
	Debug,
	Normal,
	Damage,
	Heal,
}

public enum ObjectFacing // EXPERIMENTAL, For now only used for specific movable objects (this might be used later for character facing!)
{
    North,
    South,
    East,
    West,
}

// Define Object specifit type for certain behaviours
public enum ObjectType
{
	Player,
	Enemy,
	Wall,
    DirectionalMove, // Not a pretty name but it's the kind of block that you can move from one side.
	Goal,
}

// Unnamed characters are displayed as "slots". Change them AS SOON AS new characters are available from degisn.
public enum CharacterType 
{
	DebugCharacter, // Setup for debugging and doing whatever the shit you might come up with without breaking a proper character (?).
	Torito,
	Slot2,
	Slot3,
	Slot4,
}

// struct that should store position on the map
[System.Serializable]
public struct TerrainPosition
{
	public int posX;
	public int posY;
}

// Information struct that specifies what object and which configuration should be taken!
[System.Serializable]
public struct ObjectData
{
	[SerializeField] public ObjectFacing facing;
	[SerializeField] public GameObject targetObject;
	[SerializeField] public int posX;
	[SerializeField] public int posY;
}

public class MainData
{
}
