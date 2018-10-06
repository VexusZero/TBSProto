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

	void SelectTargetObjectType(ObjectType inputType, TerrainCubeData inputData, GameObject inputTerrain, ObjectFacing inputFacing)
	{

		switch(inputType)
		{

		// Case doesn't allow movement effect on wall.
		case ObjectType.Wall:

                // Preemptive check to avoid null-reference if Player is adyacent to a wall.
                if (lastObjectChecked != null)
                {
                    selectableTargets.Add(lastObjectChecked.GetComponent<TerrainCubeData>().gridPosition);
                    lastObjectChecked.GetComponent<TerrainCubeData>().ChangeMaterial(MapManager._Instance.materialPool[3], false);
                }
			break;

		case ObjectType.Enemy:
                if (lastObjectChecked != null)
                {
                    selectableTargets.Add(inputTerrain.GetComponent<TerrainCubeData>().gridPosition);
                    inputData.ChangeMaterial(MapManager._Instance.materialPool[3], false);
                }
			break;


		case ObjectType.Goal:
			if (lastObjectChecked != null)
			{
				selectableTargets.Add (inputTerrain.GetComponent<TerrainCubeData> ().gridPosition);
				inputData.ChangeMaterial (MapManager._Instance.materialPool [3], false);
			}
			break;

		case ObjectType.Player:
                if (lastObjectChecked != null)
                {
                    selectableTargets.Add(inputTerrain.GetComponent<TerrainCubeData>().gridPosition);
                    inputData.ChangeMaterial(MapManager._Instance.materialPool[3], false);
                }

			break;

		case ObjectType.DirectionalMove:
			if (CheckFacing (inputFacing, inputTerrain.GetComponent<TerrainCubeData> ().occupant.GetComponent<ObjectMovement> ().facingConfig)) {
				selectableTargets.Add (inputTerrain.GetComponent<TerrainCubeData> ().gridPosition);
				inputData.ChangeMaterial (MapManager._Instance.materialPool [3], false);
			} 
			else
			{
				if(lastObjectChecked != null)
				{
					selectableTargets.Add(lastObjectChecked.GetComponent<TerrainCubeData>().gridPosition);
					lastObjectChecked.GetComponent<TerrainCubeData>().ChangeMaterial(MapManager._Instance.materialPool[3], false);
				}

			}
			break;

		default:
			break;
		}

	}

	public void OnAbilityCheckTerrain()
	{
		ObjectFacing facingSample;

		int moveRangeX = MapManager._Instance.sizeX;
		int moveRangeY = MapManager._Instance.sizeY;

		int tempX = movementReference.positionX;
		int tempY = movementReference.positionY;

        if (selectableTargets.Count != 0)
        {
            selectableTargets.Clear();
        }

        if (playerReference.isAbilityActive)
		{
			
			// X+ Check
			for (int i = tempX; i < moveRangeX; i++)
			{
				facingSample = ObjectFacing.East;

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
						SelectTargetObjectType (tempData.occupant.GetComponent<MapObjectData> ().type, tempData, targetObject, facingSample);
                        lastObjectChecked = null;
						break;
					}
					lastObjectChecked = targetObject;
				}
				else
				{
                    if (lastObjectChecked != null)
                    {
                        lastObjectChecked.GetComponent<TerrainCubeData>().ChangeMaterial(MapManager._Instance.materialPool[3], false);
                        selectableTargets.Add(lastObjectChecked.GetComponent<TerrainCubeData>().gridPosition);
                        lastObjectChecked = null; // Last object cleanup after finishing line sweep.
                        break;
                    }

				}


			}

			// Y+ Check
			for(int i = tempY; i < moveRangeY; i++)
			{
				facingSample = ObjectFacing.North;

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
						SelectTargetObjectType (tempData.occupant.GetComponent<MapObjectData> ().type, tempData, targetObject, facingSample);
                        lastObjectChecked = null;
                        break;
					}

					lastObjectChecked = targetObject;
				}
				else
				{
                    if (lastObjectChecked != null)
                    {
                        lastObjectChecked.GetComponent<TerrainCubeData>().ChangeMaterial(MapManager._Instance.materialPool[3], false);
                        selectableTargets.Add(lastObjectChecked.GetComponent<TerrainCubeData>().gridPosition);
                        lastObjectChecked = null; // Last object cleanup after finishing line sweep.
                        break;
                    }
				}
			}

			// X- Check
			for (int i = tempX; i >= 0; i--)
			{
				facingSample = ObjectFacing.West;

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
						SelectTargetObjectType (tempData.occupant.GetComponent<MapObjectData> ().type, tempData, targetObject, facingSample);
                        lastObjectChecked = null;
                        break;
                    }

					lastObjectChecked = targetObject;
				}
				else
				{
                    if (lastObjectChecked != null)
                    {
                        lastObjectChecked.GetComponent<TerrainCubeData>().ChangeMaterial(MapManager._Instance.materialPool[3], false);
                        selectableTargets.Add(lastObjectChecked.GetComponent<TerrainCubeData>().gridPosition);
                        lastObjectChecked = null; // Last object cleanup after finishing line sweep.
                        break;
                    }
				}
			}

			// Y- Check
			for(int i = tempY; i >= 0; i--)
			{
				facingSample = ObjectFacing.South;

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
						SelectTargetObjectType (tempData.occupant.GetComponent<MapObjectData>().type, tempData, targetObject, facingSample);
                        lastObjectChecked = null;
                        break;
					}

					lastObjectChecked = targetObject;

				}
				else
				{
                    if (lastObjectChecked != null)
                    {
                        lastObjectChecked.GetComponent<TerrainCubeData>().ChangeMaterial(MapManager._Instance.materialPool[3], false);
                        selectableTargets.Add(lastObjectChecked.GetComponent<TerrainCubeData>().gridPosition);
                        lastObjectChecked = null; // Last object cleanup after finishing line sweep.
                        break;
                    }
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

                Transform tempTransform = pointerRayData.transform.parent;

				if(CheckSelectedTerrain(tempData.gridPosition))
				{
					

					OnAbilityInteractWithObject (tempData.occupant);

					movementReference.positionX = pointerRayData.transform.GetComponent<ObjectMovement>().previousPositionX;
					movementReference.positionY = pointerRayData.transform.GetComponent<ObjectMovement>().previousPositionY;

					print ("X: " + movementReference.positionX + " Y: " + movementReference.positionY);

					transform.parent.GetComponent<TerrainCubeData> ().occupant = null;
					transform.parent = null;
					transform.position = Vector3.zero;


					transform.SetParent (tempTransform, false);

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
				targetObject.GetComponent<ObjectMovement>().ApplyPushMovement(gameObject);
				break;

			case ObjectType.DirectionalMove:
				targetObject.GetComponent<ObjectMovement> ().ApplyPushMovement (gameObject);
				break;

			case ObjectType.Goal:
				targetObject.GetComponent<ObjectMovement> ().ApplyPushMovement (gameObject);
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

	bool CheckFacing(ObjectFacing inputFacing, ObjectFacing targetFacing)
	{
		bool output = false;

		switch(inputFacing)
		{
		case ObjectFacing.North:
			if(targetFacing == ObjectFacing.South)
			{
				output = true;
			}
			break;

		case ObjectFacing.South:
			if(targetFacing == ObjectFacing.North)
			{
				output = true;
			}
			break;

		case ObjectFacing.East:
			if(targetFacing == ObjectFacing.West)
			{
				output = true;
			}
			break;

		case ObjectFacing.West:
			if(targetFacing == ObjectFacing.East)
			{
				output = true;
			}
			break;

		default:
			break;
		}

		return output;
	}

}
