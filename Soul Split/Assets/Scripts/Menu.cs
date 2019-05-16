using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour {

    // Use this for initialization
    public GameObject drop;
    float timer = 0.0f;
    public float timerCD = 0.002f;
    Vector3 v3;
    AudioSource audioSource;
    //http://www.orangefreesounds.com/rain-sound-loop/ //rain sound
    //https://www.font-generator.com/fonts/Ruritania/ //text
    void Start () {
        v3 = new Vector3();
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            v3.x = Random.Range(-40.00f, 40.00f);
            v3.y = Random.Range(-8.00f, 8.00f);
            GameObject d = Instantiate(drop, v3, Quaternion.identity);
            d.transform.localScale = new Vector3(0, 0, 0);
            timer = timerCD;
        }
	}
}
