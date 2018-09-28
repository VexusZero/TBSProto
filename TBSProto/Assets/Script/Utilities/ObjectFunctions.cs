using System.Collections;
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
}
