using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public bool canPerformMovement = true; // true for default as it on the start you should be able to move right away (?)

	public int moveRange = 1;

	public int positionX;
	public int positionY;

	[SerializeField] public List<TerrainPosition> movablePositions;

	Ray pointerRay;
	RaycastHit pointerData;

	PlayerData playerReference;

	// Use this for initialization
	void Start ()
	{
		playerReference = gameObject.GetComponent<PlayerData> ();

		positionX = GetComponentInParent<TerrainCubeData> ().gridPosition.posX;
		positionY = GetComponentInParent<TerrainCubeData> ().gridPosition.posY;
		ShowMovableTerrain();
	}

	public void ShowMovableTerrain()
	{

		switch (playerReference.character)
		{
		case CharacterType.Torito:

			DefineMovableTerrain ();

			MapManager._Instance.InitMapTilesMaterial (); // Force Tiling to Cleanup before applying movable terrains

			foreach (GameObject target in MapManager._Instance.terrainList) {
				for (int i = 0; i < movablePositions.Count; i++) {
					TerrainCubeData tempData = target.GetComponent<TerrainCubeData> ();

					if (tempData.gridPosition.posX == movablePositions [i].posX && tempData.gridPosition.posY == movablePositions [i].posY) {
						tempData.ChangeMaterial (MapManager._Instance.materialPool [6], false);
					}
				}
			}
			break;

		default:
			break;
		}

	}

	void DefineMovableTerrain()
	{
		if (movablePositions != null)
		{
			movablePositions.Clear ();
		}

		if (canPerformMovement)
		{

			for (int i = moveRange; i > 0; i--) {
				int tempX;
				int TempY;

				tempX = positionX + i;
				TempY = positionY;

				if (CheckOccupiedTerrain (tempX, TempY))
					movablePositions.Add (new TerrainPosition{ posX = tempX, posY = TempY });
		

				tempX = positionX;
				TempY = positionY + i;

				if (CheckOccupiedTerrain (tempX, TempY))
					movablePositions.Add (new TerrainPosition{ posX = tempX, posY = TempY });
			

				tempX = positionX - i;
				TempY = positionY;

				if (CheckOccupiedTerrain (tempX, TempY))
					movablePositions.Add (new TerrainPosition{ posX = tempX, posY = TempY });
			

				tempX = positionX;
				TempY = positionY - i;

				if (CheckOccupiedTerrain (tempX, TempY))
					movablePositions.Add (new TerrainPosition{ posX = tempX, posY = TempY });
			
			}
		}
	}
	
	public void OnPointAction(Vector3 pointVector)
	{
		pointerRay = Camera.main.ScreenPointToRay (pointVector);
//		print ("RAY");

		if(Physics.Raycast(pointerRay.origin, pointerRay.direction * 10, out pointerData))
		{
			Debug.Log(pointerData.transform.name);
			TerrainCubeData tempData = pointerData.transform.GetComponent<TerrainCubeData> ();

			if(pointerData.transform.tag == "MapTerrain")
			{
				if(CheckSelectedTerrain(tempData.gridPosition))
				{
					transform.parent.GetComponent<TerrainCubeData> ().occupant = null;
					transform.parent = null;
					transform.position = Vector3.zero;

					transform.SetParent (pointerData.transform, false);
					tempData.occupant = gameObject;

					positionX = tempData.gridPosition.posX;
					positionY = tempData.gridPosition.posY;

					ShowMovableTerrain ();
				}
			}
		}
	}

	bool CheckOccupiedTerrain(int posX, int posY)
	{
		bool output = true;

		foreach (GameObject target in MapManager._Instance.terrainList)
		{
			TerrainPosition tempPosition = target.GetComponent<TerrainCubeData> ().gridPosition;

			if(tempPosition.posX == posX && tempPosition.posY == posY)
			{
				if(target.GetComponent<TerrainCubeData>().occupant != null)
				{
					output = false;
					break;
				}
			}
		}

		return output;
	}

	bool CheckSelectedTerrain(TerrainPosition inputSelection)
	{
		bool output = false;

		foreach (TerrainPosition target in movablePositions)
		{
			if (target.posX == inputSelection.posX && target.posY == inputSelection.posY) {
				output = true;
				break;
			} 
		}

		return output;
	}
}
