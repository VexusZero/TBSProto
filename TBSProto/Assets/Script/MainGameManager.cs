using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameManager : MonoBehaviour
{
	public static MainGameManager _Instance;

	public List<GameObject> PlayerList;
	public List<GameObject> enemyList;
	public List<GameObject> wallList;

	public GameObject VFXObject;

	void Awake()
	{
		// Singleton Assignation

		_Instance = this;
	}

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
