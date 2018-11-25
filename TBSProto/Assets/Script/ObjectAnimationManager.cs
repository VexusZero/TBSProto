using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAnimationManager : MonoBehaviour
{
    ObjectFacing facingReference;

    public Animator objectAnimator;
    public Vector3 targetPosition;

     public bool isPlaying;

	// Use this for initialization
	void Start ()
    {
        isPlaying = false;
        objectAnimator = GetComponent<Animator>();
	}

    public void IdleReset()
    {
        objectAnimator.SetBool("isPlaying", false);
    }

    public IEnumerator PlayDirectionAnimation(ObjectFacing inputFacing)
    {
        print("playing");
        isPlaying = true;
        GetComponent<ObjectMovement>().facingConfig = inputFacing;
        GetComponent<ObjectMovement>().UpdateFacing();
        objectAnimator.SetTrigger("isRolling");
        objectAnimator.SetBool("isPlaying", true);
        yield return new WaitForSeconds(ApplyTranslation());
        while (isPlaying) { yield return null; }
        print("end animation");
        isPlaying = false;
    }

    float ApplyTranslation()
    {
        // iTween.MoveTo(gameObject, iTween.Hash("position", (transform.position + targetPosition), "time", 1, "easeType", iTween.EaseType.easeInOutQuad));
        return 1f;
    }

    public IEnumerator PlayActionAnimation(string inputAnimState)
    {
        print("playing");
        isPlaying = true;
        objectAnimator.SetTrigger(inputAnimState);
        objectAnimator.SetBool("isPlaying", true);
        while (isPlaying) { yield return null; }
        print("end animation");
        isPlaying = false;
        yield return null;
    }

    void Update()
    {
        isPlaying = objectAnimator.GetBool("isPlaying");

        if (Input.GetKeyDown(KeyCode.W))
        {
          //  print("Action W");
          //  StartCoroutine (PlayDirectionAnimation(ObjectFacing.North));
            // Insert UpdateFacing from BaseMovement (?)
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
          //  print("Action A");
          //  StartCoroutine (PlayDirectionAnimation(ObjectFacing.West));
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
           // print("Action S");
           // StartCoroutine (PlayDirectionAnimation(ObjectFacing.South));
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
           // print("Action D");
           // StartCoroutine (PlayDirectionAnimation(ObjectFacing.East));
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            print("Action F");
            StartCoroutine(PlayActionAnimation("isAction"));
        }
    }
}
