using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToritoFunctions : MonoBehaviour
{
	PlayerData playerReference;
	PlayerMovement movementReference;

	public GameObject lastObjectChecked;
	[SerializeField] public List<TerrainPosition> selectableTargets;

	// Use this for initialization
	void Start ()
	{
		playerReference = gameObject.GetComponent<PlayerData> ();
		movementReference = gameObject.GetComponent<PlayerMovement> ();
		selectableTargets = new List<TerrainPosition> ();
	}

	void FixedUpdate()
	{

	}

	void SelectTargetObjectType(ObjectType inputType, TerrainCubeData inputData, GameObject inputTerrain)
	{

		switch(inputType)
		{

		// Case doesn't allow movement effect on the wall.
		case ObjectType.Wall:
			selectableTargets.Add (lastObjectChecked.GetComponent<TerrainCubeData>().gridPosition);
			lastObjectChecked.GetComponent<TerrainCubeData> ().ChangeMaterial (MapManager._Instance.materialPool[3], false);
			break;

		case ObjectType.Enemy:
			selectableTargets.Add (inputTerrain.GetComponent<TerrainCubeData>().gridPosition);
			inputData.ChangeMaterial (MapManager._Instance.materialPool[3], false);
			break;

		case ObjectType.Player:
			selectableTargets.Add (inputTerrain.GetComponent<TerrainCubeData>().gridPosition);
			inputData.ChangeMaterial (MapManager._Instance.materialPool[3], false);
			break;

		default:
			break;
		}

	}

	public void OnAbilityCheckTerrain()
	{
		int moveRangeX = MapManager._Instance.sizeX;
		int moveRangeY = MapManager._Instance.sizeY;

		int tempX = movementReference.positionX;
		int tempY = movementReference.positionY;


		if (playerReference.isAbilityActive)
		{
			if(selectableTargets.Count != 0)
			{
				selectableTargets.Clear ();
			}

			for (int i = tempX; i < moveRangeX; i++)
			{
				GameObject targetObject = MapManager._Instance.RequestMapTile (i + 1, tempY);

//			print ("TargetObject: " + targetObject.transform.name);

				if (targetObject != null)
				{
					TerrainCubeData tempData = targetObject.GetComponent<TerrainCubeData> ();

					if (tempData.occupant == null)
					{
						tempData.ChangeMaterial (MapManager._Instance.materialPool [7], false);
					} 
					else
					{
						SelectTargetObjectType (tempData.occupant.GetComponent<MapObjectData> ().type, tempData, targetObject);
						break;
					}
					lastObjectChecked = targetObject;
				}
				else
				{
					lastObjectChecked.GetComponent<TerrainCubeData> ().ChangeMaterial (MapManager._Instance.materialPool[3], false);
					selectableTargets.Add (lastObjectChecked.GetComponent<TerrainCubeData>().gridPosition);

				}


			}

			for(int i = tempY; i < moveRangeY; i++)
			{

				GameObject targetObject = MapManager._Instance.RequestMapTile (tempX, i+1 );

//				print ("Current TilePosition: " + targetObject.GetComponent<TerrainCubeData>().gridPosition.posY);

				if (targetObject != null)
				{
					TerrainCubeData tempData = targetObject.GetComponent<TerrainCubeData> ();

					if (tempData.occupant == null)
					{
						tempData.ChangeMaterial (MapManager._Instance.materialPool [7], false);
					}
					else
					{
						SelectTargetObjectType (tempData.occupant.GetComponent<MapObjectData> ().type, tempData, targetObject);
						break;
					}

					lastObjectChecked = targetObject;
				}
				else
				{
					lastObjectChecked.GetComponent<TerrainCubeData> ().ChangeMaterial (MapManager._Instance.materialPool[3], false);
					selectableTargets.Add (lastObjectChecked.GetComponent<TerrainCubeData>().gridPosition);

				}
			}

			for (int i = tempX; i >= 0; i--)
			{
				GameObject targetObject = MapManager._Instance.RequestMapTile (i - 1, tempY);

				//			print ("TargetObject: " + targetObject.transform.name);

				if (targetObject != null)
				{
					TerrainCubeData tempData = targetObject.GetComponent<TerrainCubeData> ();

					if (tempData.occupant == null)
					{
						tempData.ChangeMaterial (MapManager._Instance.materialPool [7], false);
					} 
					else
					{
						SelectTargetObjectType (tempData.occupant.GetComponent<MapObjectData> ().type, tempData, targetObject);
						break;
					}

					lastObjectChecked = targetObject;
				}
				else
				{
					lastObjectChecked.GetComponent<TerrainCubeData> ().ChangeMaterial (MapManager._Instance.materialPool[3], false);
					selectableTargets.Add (lastObjectChecked.GetComponent<TerrainCubeData>().gridPosition);

				}
			}

			for(int i = tempY; i >= 0; i--)
			{

				GameObject targetObject = MapManager._Instance.RequestMapTile (tempX, i-1 );

				if (targetObject != null)
				{
					TerrainCubeData tempData = targetObject.GetComponent<TerrainCubeData> ();

					if(tempData.occupant == null)
					{
						tempData.ChangeMaterial(MapManager._Instance.materialPool[7], false);
					}
					else
					{
						SelectTargetObjectType (tempData.occupant.GetComponent<MapObjectData>().type, tempData, targetObject);
						break;
					}

					lastObjectChecked = targetObject;

				}
				else
				{
					lastObjectChecked.GetComponent<TerrainCubeData> ().ChangeMaterial (MapManager._Instance.materialPool[3], false);
					selectableTargets.Add (lastObjectChecked.GetComponent<TerrainCubeData>().gridPosition);

				}


			}
		}
	}

	public void OnAbilityMoveToTarget(Vector3 pointVector)
	{
		Ray pointerRay = Camera.main.ScreenPointToRay(pointVector);
		RaycastHit pointerRayData;

		if(Physics.Raycast(pointerRay.origin, pointerRay.direction *10, out pointerRayData))
		{
			TerrainCubeData tempData;

			if(pointerRayData.transform.tag == "MapTerrain")
			{
				tempData = pointerRayData.transform.GetComponent<TerrainCubeData> ();

				if(CheckSelectedTerrain(tempData.gridPosition))
				{
					OnAbilityInteractWithObject (tempData.occupant);


					transform.parent.GetComponent<TerrainCubeData> ().occupant = null;
					transform.parent = null;
					transform.position = Vector3.zero;


					transform.SetParent (pointerRayData.transform, false);
					tempData.occupant = gameObject;

					movementReference.positionX = tempData.gridPosition.posX;
					movementReference.positionY = tempData.gridPosition.posY;

					movementReference.canPerformMovement = !movementReference.canPerformMovement;
					playerReference.isAbilityActive = !playerReference.isAbilityActive;

					movementReference.ShowMovableTerrain ();

				}
			}

			if(pointerRayData.transform.tag == "Object")
			{
				tempData = pointerRayData.transform.GetComponentInParent<TerrainCubeData> ();

				// PRE-SET PARENT REFERENCE HERE - OTHERWISE IT WILL BE SET ON LAST ENEMY POSITION YOU DUMBASS!

				if(CheckSelectedTerrain(tempData.gridPosition))
				{
					

					OnAbilityInteractWithObject (tempData.occupant);

					movementReference.positionX = pointerRayData.transform.GetComponent<ObjectMovement>().previousPositionX;
					movementReference.positionY = pointerRayData.transform.GetComponent<ObjectMovement>().previousPositionY;

					print ("X: " + movementReference.positionX + " Y: " + movementReference.positionY);

					transform.parent.GetComponent<TerrainCubeData> ().occupant = null;
					transform.parent = null;
					transform.position = Vector3.zero;


					transform.SetParent (pointerRayData.transform.parent, false);

					tempData.occupant = gameObject;


					movementReference.canPerformMovement = !movementReference.canPerformMovement;
					playerReference.isAbilityActive = !playerReference.isAbilityActive;

					movementReference.ShowMovableTerrain ();


				}
			}
		}
	}

	void OnAbilityInteractWithObject(GameObject targetObject)
	{
		if(targetObject != null)
		{
			ObjectType tempObjectType = targetObject.GetComponent<MapObjectData>().type;

			switch(tempObjectType)
			{
			case ObjectType.Enemy:
//				targetObject.GetComponent<EnemyData> ().DestroyEnemy ();
				targetObject.GetComponent<ObjectMovement>().ApplyPushMovement(gameObject);
				break;

			default:
				break;
			}
		}
	}

	bool CheckSelectedTerrain(TerrainPosition inputSelection)
	{
		bool output = false;

		foreach (TerrainPosition target in selectableTargets)
		{
			if (target.posX == inputSelection.posX && target.posY == inputSelection.posY) {
				output = true;
				break;
			} 
		}

		return output;
	}

}
