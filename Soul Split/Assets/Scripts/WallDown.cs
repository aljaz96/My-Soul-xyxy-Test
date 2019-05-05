using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDown : MonoBehaviour {

    // Use this for initialization

    new SpriteRenderer renderer;

	void Start () {
        try
        {
            renderer = gameObject.GetComponent<SpriteRenderer>();
        }
        catch
        {
            renderer = null;
        }
    }
	
	// Update is called once per frame
	void Update () {
		if (renderer == null)
        {
            try
            {
                renderer = gameObject.GetComponent<SpriteRenderer>();
            }
            catch
            {
                renderer = null;
            }
        }
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Player")
        {
            Color c = renderer.color;
            c.a = 0.5f;
            renderer.color = c;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Player")
        {
            try
            {
                Color c = renderer.color;
                c.a = 0.5f;
                renderer.color = c;
            }
            catch
            {
                //do nothing;
            }
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Player")
        {
            try
            {
                Color c = renderer.color;
                c.a = 1f;
                renderer.color = c;
            }
            catch
            {
                //do nothing;
            }
        }
    }
}
