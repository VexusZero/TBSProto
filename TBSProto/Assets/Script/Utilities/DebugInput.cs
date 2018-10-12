using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugInput : MonoBehaviour
{	
	// Update is called once per frame
	void FixedUpdate () 
	{
		#if UNITY_EDITOR_WIN

		// PANIC BUTTON - Press this to reload Menu Scene.
		if(Input.GetKeyDown(KeyCode.Alpha9))
		{
			SceneManager.LoadScene("MainMenu");
		}

		#endif
	}
}
