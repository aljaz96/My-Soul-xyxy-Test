using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashEffect : MonoBehaviour {

    // Use this for initialization
    public float timer = 0.3f;
    public AudioSource audioData;
    //public GameObject destroyedEffect;

    void Start()
    {
        audioData = GetComponent<AudioSource>();
        Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = pz - transform.position;
        direction.Normalize();
        GetComponent<Rigidbody2D>().velocity = direction * 0.1f;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            audioData.Play();
        }
    }
}
