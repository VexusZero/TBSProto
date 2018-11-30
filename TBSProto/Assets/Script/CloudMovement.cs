using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour {

    float speed;
    Vector3 vectorFactor;

	// Use this for initialization
	void Start () {
        speed = Random.Range(0.05f, 0.1f);
        vectorFactor = new Vector3(0f, 0f, speed);

        GameObject.Destroy(gameObject, 15f);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.position += vectorFactor;
	}
}
