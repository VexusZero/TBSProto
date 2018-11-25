﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Function Repository, code here functions that should be called on an "Object-Based Script Component"

public class ObjectFunctions
{
	public static bool CheckObjectOutOfBounds(int inputX, int InputY)
	{
		bool output = false;

		if(inputX < 0 || inputX >= MapManager._Instance.sizeX)
		{
			output = true;
		}

		if(InputY < 0 || InputY >= MapManager._Instance.sizeY)
		{
			output = true;
		}

		return output;
	}

	// Based on the enemy destroy-function. EXPERIMENTAL, if this works this should be the "standarized" destroy function for EVERY object.
	public static void DestroyObject(GameObject targetObject)
	{
		if(targetObject.GetComponent<MapObjectData>().type == ObjectType.Goal)
		{
			MainGameManager._Instance.OnVictoryAchieved ();
		}

		targetObject.GetComponent<MapObjectData> ().VFXObject.GetComponent<ParticleSystem> ().Play ();
		AudioManager._Instance.PlayIndexedSound (0); // might not work in a class-only environment!
		GameObject.Destroy(targetObject, 1f);
	}

    public static Vector3 FacingToVector(ObjectFacing inputFacing)
    {

        Debug.Log("Facing: " + inputFacing);
        Vector3 output = Vector3.zero;

        switch (inputFacing)
        {
            case ObjectFacing.North:
                output = Vector3.forward;
                break;
            case ObjectFacing.South:
                output = Vector3.back;
                break;
            case ObjectFacing.East:
                output = Vector3.right;
                break;
            case ObjectFacing.West:
                output = Vector3.left;
                break;
        }

        return output;
    }
}
