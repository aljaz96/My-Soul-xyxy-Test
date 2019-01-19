using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hatchling : MonoBehaviour {

    // Use this for initialization
    Animator anim;
    bool right = true;
    float oldXpos;
    float newXpos;

    SpriteRenderer sr;

	void Start () {
        oldXpos = transform.position.x;
        anim = gameObject.GetComponent<Animator>();
        sr = gameObject.GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        newXpos = transform.position.x;
        if (newXpos > oldXpos && !right)
        {
            sr.flipX = false;
            right = true;
        }
        else if(newXpos < oldXpos && right)
        {
            sr.flipX = true;
            right = false;
        }
        oldXpos = newXpos;
	}
}
