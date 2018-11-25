using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialLerper : MonoBehaviour
{
    Material moddedMaterial;

    float currentTime;
    float timeToReverse = 2f;

    bool isReverse;

    void Start()
    {
        moddedMaterial = GetComponent<Renderer>().material;
        isReverse = false;
    }
	
	
	void FixedUpdate ()
    {
        if (currentTime > timeToReverse)
        {
            isReverse = !isReverse;
            currentTime = 0f;
        }
        else
        {
            currentTime += Time.deltaTime;
        }

        ApplyMaterialLerp();
	}

    void ApplyMaterialLerp()
    {
        if (!isReverse)
        {
            moddedMaterial.color = Color.Lerp(moddedMaterial.color, Color.white, Time.deltaTime * 1.5f);
        }
        else
        {
            moddedMaterial.color = Color.Lerp(moddedMaterial.color, Color.red, Time.deltaTime * 1.5f);
        }
    }
}
