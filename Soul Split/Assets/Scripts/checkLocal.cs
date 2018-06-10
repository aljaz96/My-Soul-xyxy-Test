using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkLocal : MonoBehaviour {

	// Use this for initialization
    public float X;
    public float Y;

	void Start () {
        transform.TransformPoint(Vector3.zero);
    }
	
	// Update is called once per frame
	void Update () {
        X = transform.position.x;
        Y = transform.position.y;
	}
}
