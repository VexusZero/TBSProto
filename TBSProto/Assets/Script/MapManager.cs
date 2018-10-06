using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
	public static MapManager _Instance; //singleton declaration

	public List<GameObject> terrainList;
	[SerializeField] public List<ObjectData> objectList;

	public GameObject terrainObject;
	public GameObject wallObject;
	public GameObject eenemyObject;
	public Material[] materialPool;

	public int sizeX;
	public int sizeY;
	public float gridOffset;

	private Vector3 correctedPosition = new Vector3(-4.5f, 0f, -4.5f);

	void Awake()
	{
		_Instance = this; // singleton asign.
	}

	// Use this for initialization
	void Start ()
	{
		GenerateTileMap ();
		GenerateObjects ();
	}

	void GenerateTileMap()
	{
		for (int i=0;  i< sizeX; i++)
		{
			for(int j=0; j< sizeY; j++)
			{
				GameObject go = GameObject.Instantiate (terrainObject, correctedPosition, transform.rotation);
				TerrainCubeData tempData = go.GetComponent<TerrainCubeData> ();
				terrainList.Add (go);
				tempData.gridPosition.posX = i;
				tempData.gridPosition.posY = j;
				correctedPosition = new Vector3 (correctedPosition.x, 0f, correctedPosition.z + gridOffset);
			}
			correctedPosition = new Vector3 (correctedPosition.x + gridOffset, 0f, -4.5f);
		}
	}

	void GenerateObjects()
	{
		for(int i = 0; i< objectList.Count; i++)
		{
			CreateMapObject (objectList[i].targetObject, objectList[i].facing , objectList[i].posX, objectList[i].posY);
		}
	}

	// (!) Generate test block in Coord (3,3) and (4,2).

	void CreateMapObject(GameObject inputObject, ObjectFacing inputFacing , int inputPosX, int inputPosY)
	{

		foreach (GameObject target in terrainList)
		{
			TerrainCubeData tempData = target.GetComponent<TerrainCubeData> ();

			if(tempData.gridPosition.posX == inputPosX && tempData.gridPosition.posY == inputPosY && CheckTile(inputPosX, inputPosY))
			{

				print ("Generating: " + inputObject.name + " in " + inputPosX + "," + inputPosY);
				GameObject go = GameObject.Instantiate (inputObject, Vector3.zero, transform.rotation);
				go.transform.SetParent (target.transform, false);
				tempData.occupant = go;
				ObjectPostConfig (go, inputFacing);
			}
		}
	}

	void ObjectPostConfig(GameObject target, ObjectFacing inputFacing)
	{
		MapObjectData tempData = target.GetComponent<MapObjectData> ();

		switch(tempData.type)
		{
		case ObjectType.Player:
			// add to list
			MainGameManager._Instance.PlayerList.Add(target);
			break;

		case ObjectType.Enemy:
			// add to list
			MainGameManager._Instance.enemyList.Add(target);
			break;

		case ObjectType.Wall:
			// add to list
			MainGameManager._Instance.wallList.Add(target);
			break;

		case ObjectType.DirectionalMove:
			target.GetComponent<ObjectMovement> ().facingConfig = inputFacing;
			MainGameManager._Instance.enemyList.Add (target);
			break;

		case ObjectType.Goal:
			print ("Goal Established");
			break;

		default:
			Debug.LogError ("ERROR: GENERATED OBJECT DOESN'T HAVE A COMPATIBLE TYPE!");
			break;
		}
	}

	// Check if tile is occupied already
	bool CheckTile(int inputPosX, int inputPosY)
	{
		bool output = false;

		foreach (GameObject target in terrainList)
		{
			TerrainCubeData tempData = target.GetComponent<TerrainCubeData> ();

			if(tempData.gridPosition.posX == inputPosX && tempData.gridPosition.posY == inputPosY)
			{
				if (tempData.occupant != null) {
					output = false;
					break;
				}
				else
				{
					output = true;
					break;
				}
			}
		}

		return output;
	}
		
	public void InitMapTilesMaterial()
	{
		foreach (GameObject target in terrainList)
		{
			target.GetComponent<TerrainCubeData> ().InitMaterial ();
		}
	}

	public GameObject RequestMapTile(int inputX, int InputY)
	{
		GameObject output = null;

		foreach (GameObject target in terrainList)
		{
			TerrainCubeData tempData = target.GetComponent<TerrainCubeData> ();

			if(tempData.gridPosition.posX == inputX && tempData.gridPosition.posY == InputY)
			{
				output = target;
				break;
			}
		}

		return output;
	}
}
