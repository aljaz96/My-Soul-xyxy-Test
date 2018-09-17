using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHitEffect : MonoBehaviour {

    // Use this for initialization
    public float timer = 0.3f;
    public AudioSource audioData;
    //public GameObject destroyedEffect;

    void Start()
    {
        audioData = GetComponent<AudioSource>();
        audioData.Play();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            Destroy(gameObject);
        }
    }
}
