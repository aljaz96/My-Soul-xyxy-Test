using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour {

    // Use this for initialization
    //1,0,33
    public bool animType = false;
    Animator anim;
    Color original;
    SpriteRenderer renderer;  
    void Start () {
        anim = GetComponent<Animator>();
        if(animType == true)
        {
            anim.SetTrigger("Other");
        }
        renderer = GetComponent<SpriteRenderer>();
        original = renderer.color;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ColorHand(float f)
    {
        Color c = original;
        c.r = 1;
        c.g = 0;
        c.b = 0.33f;
        renderer.color = c;
        StartCoroutine(DefaultColor(f));
    }

    IEnumerator DefaultColor(float t)
    {
        yield return new WaitForSeconds(t);
        renderer.color = original;
    }
}
