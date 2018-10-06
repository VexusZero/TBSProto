using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapObjectData : MonoBehaviour
{
	public ObjectType type;

	public GameObject VFXObject; // new reference, should be attached to this rather than an specific data component (ex. EnemyData)

	// Use this for initialization
	void Start ()
	{
		InitObject ();
		InitDebugVFX ();
	}

	void InitObject()
	{
		switch(type)
		{
		case ObjectType.Enemy:
			EnemyData add = gameObject.AddComponent (typeof(EnemyData)) as EnemyData;
			break;

		default:
			break;
		}
	}

	void InitDebugVFX()
	{
		GameObject go;

		switch (type)
		{
		case ObjectType.Player:
			go = GameObject.Instantiate (MainGameManager._Instance.VFXObject, transform.parent);
			go.transform.SetParent (gameObject.transform, false);
			VFXObject = go;
			break;

		case ObjectType.Enemy:
			go = GameObject.Instantiate (MainGameManager._Instance.VFXObject, transform.parent);
			go.transform.SetParent (gameObject.transform, false);
			VFXObject = go;
			break;

		case ObjectType.DirectionalMove:
			go = GameObject.Instantiate (MainGameManager._Instance.VFXObject, transform.parent);
			go.transform.SetParent(gameObject.transform, false);
			VFXObject = go;
			break;

		case ObjectType.Goal:
			go = GameObject.Instantiate (MainGameManager._Instance.VFXObject, transform.parent);
			go.transform.SetParent (gameObject.transform, false);
			VFXObject = go;
			break;

		default:
			break;
		}
	}
}
