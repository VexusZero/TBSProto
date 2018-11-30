using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudManager : MonoBehaviour {

    public GameObject cloudObject;

    public GameObject[] spawns;

    float currentTime = 0;

    float waitTime = 5f;

	// Use this for initialization
	void Start () {
       spawns = GameObject.FindGameObjectsWithTag("Spawn");

        StartCoroutine(SpawnClouds());
	}

    void OnTriggerExit(Collider sensor)
    {
        if (sensor.transform.name == "Cloud")
        {
            print("boom");
            GameObject.Destroy(sensor.transform.gameObject);
            SpawnSingleCloud();
        }
    }

    IEnumerator SpawnClouds()
    {
        int randomIndex = Random.Range(0, spawns.Length);

        for (int i = 0; i < 6; i++) {
         GameObject go = Instantiate(cloudObject, spawns[randomIndex].transform.position,Quaternion.identity) as GameObject;
            yield return new WaitForSeconds(Random.Range(1,3));
        }

        yield return null;
    }

    void SpawnSingleCloud()
    {
        int randomIndex = Random.Range(0, spawns.Length);

        GameObject go = GameObject.Instantiate(cloudObject, spawns[randomIndex].transform.position, Quaternion.identity) as GameObject;
    }
	
	// Update is called once per frame
	void Update () {
        if (currentTime > waitTime)
        {
            StartCoroutine(SpawnClouds());
            currentTime = 0f;
        }
        currentTime += Time.deltaTime;
	}
}
