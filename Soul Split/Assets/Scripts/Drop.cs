using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour {

    // Use this for initialization
    Transform t;
    SpriteRenderer sr;
    float size;
    float colorReducer = 0.021f;

	void Start () {
        t = GetComponent<Transform>();
        sr = GetComponent<SpriteRenderer>();
        size = Random.Range(0.60f, 2.10f);
        colorReducer = colorReducer / size;
    }
	
	// Update is called once per frame
	void Update () {
		if(t.localScale.x < size)
        {
            t.localScale += new Vector3(0.02f, 0.02f);
            Color c = sr.color;
            c.a -= colorReducer;
            sr.color = c;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
