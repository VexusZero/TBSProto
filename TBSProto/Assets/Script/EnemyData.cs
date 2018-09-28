using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : MonoBehaviour
{
	public GameObject VFXObject;


	public bool isDebugMode;

	public void DestroyEnemy()
	{
		print ("BOOM GOES THE ENEMY");

		GetComponentInParent<TerrainCubeData> ().occupant = null;

		VFXObject.GetComponent<ParticleSystem> ().Play ();
		AudioManager._Instance.PlayIndexedSound (0);
		GameObject.Destroy (gameObject, 1f);
	}

}
