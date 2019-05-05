using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_bomb : MonoBehaviour {

    public float x;
    public GameObject bullet;
    public int numberOfBullets = 20;


    void Start()
    {
        x = transform.localScale.x;
    }

    void Update()
    {

    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Wall" || col.tag == "Enemy")
        {
            for (int i = 0; i < numberOfBullets; i++)
            {
                GameObject b1 = Instantiate(bullet, transform.position, Quaternion.identity);
                Vector3 v3 = new Vector3();
                v3.x += Random.Range(-4f, 4f);
                v3.y += Random.Range(-4f, 4f);
                b1.GetComponent<Rigidbody2D>().velocity = v3;

            }
            Vector3 v = new Vector3();
            v.x = 0;
            v.y = 0;
            GetComponent<Rigidbody2D>().velocity = v;
        }
    }
}
