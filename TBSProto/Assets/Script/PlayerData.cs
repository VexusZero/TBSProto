using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
	public CharacterType character;

	public bool isAbilityActive = true;

	// Use this for initialization
	void Awake ()
	{
		SetCharacterClass ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	// add all functions from derived character as modular and separate script for EACH character.
	private void SetCharacterClass()
	{
		switch(character)
		{
		case CharacterType.Torito:
			ToritoFunctions add = gameObject.AddComponent (typeof(ToritoFunctions)) as ToritoFunctions;
			break;

		default:
			break;
		}
	}

	// perform specific ability call for given character (only use this function as call, don't code more than that!)
	private void OnCharacterAbilityStart()
	{
		switch(character)
		{
		case CharacterType.Torito:
			gameObject.GetComponent<ToritoFunctions> ().OnAbilityCheckTerrain ();
			break;

		default:
			break;
		}
	}

	public void OnCharacterAbilityClickAction(Vector3 pointVector)
	{
		switch(character)
		{
		case CharacterType.Torito:
			gameObject.GetComponent<ToritoFunctions> ().OnAbilityMoveToTarget (pointVector);
			break;

		default:
			break;
		}
	}

	public void InputToggleCharacterAbility()
	{
		PlayerMovement tempReference = gameObject.GetComponent<PlayerMovement> ();

		isAbilityActive = true;
        tempReference.canPerformMovement = false;
        // Movement lockup setup while ability is active IF-Less variant.
        // tempReference.canPerformMovement = !tempReference.canPerformMovement;
		tempReference.ShowMovableTerrain ();
		OnCharacterAbilityStart ();

	}
}
