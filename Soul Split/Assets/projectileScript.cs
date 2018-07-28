using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileScript : MonoBehaviour {

    // Use this for initialization
    public float timer = 4;
    public AudioSource audioData;

	void Start () {
        audioData = GetComponent<AudioSource>();
        audioData.Play();
    }
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
        if(timer < 0)
        {
            Destroy(gameObject);
        }
	}
}
