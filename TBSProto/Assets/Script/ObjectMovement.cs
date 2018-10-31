using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles movement on Unmanned objects. This shouldn't be used as an automated script, this only should store movement based functions for objects

public class ObjectMovement : MonoBehaviour
{
	public ObjectFacing facingConfig = ObjectFacing.North; // default set to north -just in case-.

	public int positionX;
	public int positionY;

	public int previousPositionX;
	public int previousPositionY;

	// Use this for initialization
	void Start ()
	{
		positionX = GetComponentInParent<TerrainCubeData> ().gridPosition.posX;
		positionY = GetComponentInParent<TerrainCubeData> ().gridPosition.posY;
		UpdateFacing ();
	}

	void UpdatePosition()
	{
		if (!ObjectFunctions.CheckObjectOutOfBounds (positionX, positionY))
		{
			GameObject targetTerrain = MapManager._Instance.RequestMapTile (positionX, positionY);
			TerrainCubeData tempData = targetTerrain.GetComponent<TerrainCubeData> ();

			GetComponentInParent<TerrainCubeData> ().occupant = null; // sets tile "object-free"! - PREEMPTIVE ACTION TO AVOID MAP BUGGING

			if (CheckOccupiedTerrain (positionX, positionY)) {
				transform.parent = null;
				transform.position = Vector3.zero;

				transform.SetParent (targetTerrain.transform, false);
				transform.localPosition = new Vector3 (0f, MapManager._Instance.objectOffset, 0f);
				tempData.occupant = gameObject;

			}
		}
		else
		{
//			GetComponent<EnemyData> ().DestroyEnemy ();

			ObjectFunctions.DestroyObject(gameObject);
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

    public void UpdateFacing()
    {
        switch (facingConfig)
        {
            case ObjectFacing.North:
                transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                break;

            case ObjectFacing.South:
                transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
                break;

            case ObjectFacing.East:
                transform.localRotation = Quaternion.Euler(0f, 90f, 0f);
                break;

            case ObjectFacing.West:
                transform.localRotation = Quaternion.Euler(0f, 270f, 0f);
                break;

            default:
                break;
        }
    }

    public void ApplyPushMovement(GameObject instigatorObject)
	{
		int distance;

		previousPositionX = positionX;
		previousPositionY = positionY;

		MapObjectData tempMapData = instigatorObject.GetComponent<MapObjectData> ();

		switch(tempMapData.type)
		{
		case ObjectType.Player:
			PlayerMovement tempMovementData = instigatorObject.GetComponent<PlayerMovement> ();

			// CrossCheck for X-Axis Movement
			if(tempMovementData.positionY == positionY)
			{
				print ("X-Axis");

				distance = Mathf.Abs (tempMovementData.positionX - positionX);
				print ("Relative Distance: " + distance);

				for (int i = distance; i>0; i--)
				{


					print ("REPEAT");
					if (positionX < tempMovementData.positionX)
					{
						print ("-X");
						if(PostPushCheck(ObjectFacing.West))
						{
							print ("Break Movement");
							break;
						}
						positionX -= 1;
						UpdatePosition ();
					}

					if(positionX > tempMovementData.positionX)
					{
						print ("+X");
						if(PostPushCheck(ObjectFacing.East))
						{
							print ("Break Movement");
							break;
						}
						positionX += 1;
						UpdatePosition ();
					}
				}
			}

			// CrossCheck for Y-AxisMovement
			if(tempMovementData.positionX == positionX)
			{
				print ("Y Axis");

				distance = Mathf.Abs (tempMovementData.positionY - positionY);
				print("Relative Distance: " + distance);

				for (int i = distance; i>0; i--)
				{

					if(positionY < tempMovementData.positionY )
					{
						print ("-Y");
						if(PostPushCheck(ObjectFacing.South))
						{
							print ("Break Movement");
							break;
						}
						positionY -= 1;
						UpdatePosition ();
					}

					if(positionY > tempMovementData.positionY)
					{
						print ("+Y");
						if(PostPushCheck(ObjectFacing.North))
						{
							print ("Break Movement");
							break;
						}
						positionY += 1;
						UpdatePosition ();
					}
				}
			}

			break;

		default:
			break;
		}
	}

	bool PostPushCheck(ObjectFacing inputFacing)
	{
		GameObject tempMapObject;
		bool output = false;

		// WHAT WILL I DO HERE?!
		switch(inputFacing)
		{
		case ObjectFacing.North:
			tempMapObject = MapManager._Instance.RequestMapTile (positionX, positionY+1);
			if(tempMapObject != null)
			{
				if(tempMapObject.GetComponent<TerrainCubeData>().occupant != null)
				{
					if(tempMapObject.GetComponent<TerrainCubeData>().occupant.GetComponent<MapObjectData>().type == ObjectType.Wall)
					{
						output = true;
					}
				}
			}

			break;
		case ObjectFacing.South:
			tempMapObject = MapManager._Instance.RequestMapTile (positionX, positionY -1);
			if(tempMapObject != null)
			{
				if(tempMapObject.GetComponent<TerrainCubeData>().occupant != null)
				{
					if(tempMapObject.GetComponent<TerrainCubeData>().occupant.GetComponent<MapObjectData>().type == ObjectType.Wall)
					{
						output = true;
					}
				}
			}
			break;
		case ObjectFacing.East:
			tempMapObject = MapManager._Instance.RequestMapTile (positionX +1, positionY);
			if(tempMapObject != null)
			{
				if(tempMapObject.GetComponent<TerrainCubeData>().occupant != null)
				{
					if(tempMapObject.GetComponent<TerrainCubeData>().occupant.GetComponent<MapObjectData>().type == ObjectType.Wall)
					{
						output = true;
					}
				}
			}
			break;
		case ObjectFacing.West:
			tempMapObject = MapManager._Instance.RequestMapTile (positionX -1, positionY);
			if(tempMapObject != null)
			{
				if(tempMapObject.GetComponent<TerrainCubeData>().occupant != null)
				{
					if(tempMapObject.GetComponent<TerrainCubeData>().occupant.GetComponent<MapObjectData>().type == ObjectType.Wall)
					{
						output = true;
					}
				}
			}
			break;

		default:
			break;
		}

		return output;
	}

}
