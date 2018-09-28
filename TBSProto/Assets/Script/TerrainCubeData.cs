using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainCubeData : MonoBehaviour 
{
	public TerrainType terrain;
	public GameObject occupant;

	[SerializeField] public TerrainPosition gridPosition;

	// Use this for initialization
	void Start ()
	{
		InitMaterial ();
	}

	public void ChangeMaterial(Material inputMaterial, bool isRevert)
	{
		if(isRevert)
		{
			InitMaterial ();
		}
		else
		{
			GetComponent<MeshRenderer> ().material = inputMaterial;	
		}
	}

	public void InitMaterial()
	{
		/*-Material List-

		Black - 0
		Blue - 1
		Gray - 2
		Green - 3
		Red - 4
		Yellow - 5
		Cyan - 6
		Orange - 7

		*/
		switch (terrain)
		{
		case TerrainType.Debug:
			GetComponent<MeshRenderer>().material = MapManager._Instance.materialPool[0];
			break;

		case TerrainType.Normal:
			GetComponent<MeshRenderer> ().material = MapManager._Instance.materialPool [5];
			break;

		case TerrainType.Damage:
			GetComponent<MeshRenderer> ().material = MapManager._Instance.materialPool [4];
			break;

		case TerrainType.Heal:
			GetComponent<MeshRenderer> ().material = MapManager._Instance.materialPool [3];
			break;

		default:
			break;
		}
	}
}
