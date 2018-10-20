using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour 
{
	public static LevelManager _Instance;

	float countdownTime = 2f;
	[SerializeField] float restartCountDown;

	bool isCountdownActive = false;
	static bool isObjectProtected;

	void Awake()
	{
		_Instance = this;

		print ("Current Bool State: " + isObjectProtected);

		if(!isObjectProtected)
		{
			GameObject.DontDestroyOnLoad (gameObject);
			isObjectProtected = true;
		}

	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		if(isCountdownActive)
		{
			restartCountDown -= Time.deltaTime;
		}

		if(restartCountDown < 0f)
		{
			isCountdownActive = false;
			restartCountDown = 0.1f; // Failsafe Set.
			OnReturnToMainMenu ();
		}
	}

	public void StartCountdown()
	{
		restartCountDown = countdownTime;
		isCountdownActive = true;
	}

	public void OnReturnToMainMenu()
	{
		SceneManager.LoadScene ("MainMenu");
	}

	public void OnLevelSelectRequest(string inputStage)
	{
		SceneManager.LoadScene (inputStage);
	}
}
