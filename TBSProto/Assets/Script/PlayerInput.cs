using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
	PlayerMovement movementReference;
	PlayerData playerReference;

	// Use this for initialization
	void Start ()
	{
		movementReference = gameObject.GetComponent<PlayerMovement> ();
		playerReference = gameObject.GetComponent<PlayerData> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(Input.GetMouseButtonDown(0))
		{
			if(movementReference.canPerformMovement)
			{
				movementReference.OnPointAction (Input.mousePosition);
			}

			if(playerReference.isAbilityActive)
			{
				playerReference.OnCharacterAbilityClickAction (Input.mousePosition);
			}
		}

		if(Input.touchCount == 1)
		{
			if(movementReference.canPerformMovement)
			{
				movementReference.OnPointAction (Input.touches[0].position);
			}

			if(playerReference.isAbilityActive)
			{
				playerReference.OnCharacterAbilityClickAction (Input.touches[0].position);
			}
		}

		if(Input.GetKeyDown(KeyCode.D))
		{
			CameraManager._Instance.RetrieveSceneCamera ();
		}
	}
}
