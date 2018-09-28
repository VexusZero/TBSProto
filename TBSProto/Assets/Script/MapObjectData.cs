using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapObjectData : MonoBehaviour
{
	public ObjectType type;


	// Use this for initialization
	void Start ()
	{
		InitObject ();
		InitDebugVFX ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		
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
		switch (type)
		{
		case ObjectType.Enemy:
			GameObject go = GameObject.Instantiate (MainGameManager._Instance.VFXObject, transform.parent);
			go.transform.SetParent (gameObject.transform, false);
			GetComponent<EnemyData> ().VFXObject = go;
			break;

		default:
			break;
		}
	}
}
