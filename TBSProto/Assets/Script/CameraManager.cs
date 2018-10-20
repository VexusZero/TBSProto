using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour 
{
	public static CameraManager _Instance;

	public float normalSize;
	public float zoomSize;
	public bool isCameraZoom;

	[SerializeField] Camera currentCamera;

	[SerializeField] Vector3 normalPosition;

	void Awake()
	{
		if(_Instance == null)
		{
			_Instance = this;
		}

		isCameraZoom = false;
	}

	// Use this for initialization
	void Start ()
	{
		RetrieveSceneCamera ();
		normalPosition = currentCamera.transform.position;

		print (normalPosition);
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		AutoLerpCamera ();
	}

	public void AutoLerpCamera()
	{
		if (isCameraZoom)
		{
			currentCamera.orthographicSize = Mathf.Lerp (currentCamera.orthographicSize, zoomSize, Time.deltaTime*3);
			currentCamera.transform.position = Vector3.Lerp (currentCamera.transform.position,	CorrectTargetLerp(MainGameManager._Instance.PlayerList[0].transform.position), Time.deltaTime*3);
		}
		else
		{
			currentCamera.orthographicSize = Mathf.Lerp (currentCamera.orthographicSize, normalSize, Time.deltaTime*3);	
			currentCamera.transform.position = Vector3.Lerp (currentCamera.transform.position, normalPosition, Time.deltaTime*3);
		}
	}

	Vector3 CorrectTargetLerp(Vector3 inputVector)
	{
		Vector3 output;

		output = new Vector3 (inputVector.x-2f, inputVector.y+3f, inputVector.z-2f);

		return output;
	}

	public void RetrieveSceneCamera()
	{
		currentCamera = Camera.main;
		normalPosition = currentCamera.transform.position;
		isCameraZoom = false;
	}
}
