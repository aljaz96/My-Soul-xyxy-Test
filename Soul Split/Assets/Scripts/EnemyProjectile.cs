using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour {

    // Use this for initialization
    // Use this for initialization
    public float timer = 4;
    public AudioSource audioData;
    public GameObject destroyedEffect;
    float angle;
    Vector3 startPos;
    Vector3 endPos;
    public float damage = 1f;

    void Start()
    {
        startPos = transform.position;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wall" || collision.tag == "Player")
        {
            endPos = transform.position;
            var relativePos = startPos - endPos;
            angle = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg;
            var rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            GameObject effect = Instantiate(destroyedEffect, endPos, rotation);
            Destroy(gameObject);
            if (collision.tag == "Enemy")
            {
                collision.gameObject.GetComponent<MonsterStats>().hp -= damage;
            }
        }
    }
}
