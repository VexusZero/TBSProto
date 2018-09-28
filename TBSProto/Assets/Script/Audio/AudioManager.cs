using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	public static AudioManager _Instance;

	public AudioClip[] audioPool;

	[SerializeField] Camera activeCamera;

	// Use this for initialization
	void Awake ()
	{
		_Instance = this;
	}

	void Start()
	{
		activeCamera = Camera.main;
	}

	public void PlayIndexedSound(int audioID)
	{
        /*Documented Audio IDs - Please, when adding a new clip to the AudioPool write down new entry in here.

		0 - SMRPG - Enemy Flee - DEBUG SOUND.

		*/
        if (!activeCamera.GetComponent<AudioSource>().isPlaying)
        {
            activeCamera.GetComponent<AudioSource>().PlayOneShot(audioPool[audioID]);
        }
    }

}
