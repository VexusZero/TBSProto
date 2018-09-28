using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Button Functions Repository, this is where ALL button functions should be stored. Please Ensure this is assigned to a 
"Main Canvas" GameObject ;) */

public class ButtonFunctions : MonoBehaviour
{
	public void OnAbilityButtonPressed()
	{
		// Assuming this is the locally assigned player 1 (?)

		MainGameManager._Instance.PlayerList [0].GetComponent<PlayerData> ().InputToggleCharacterAbility ();
	}
}
