using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_bomb : MonoBehaviour {

    public float x;
    bool active = true;
    float destroyTimer = 1f;
    public GameObject bullet;
    public int numberOfBullets = 20;


    void start()
    {
        x = transform.localScale.x;
    }

    void update()
    {

        x = transform.localScale.x;
        while (x > 0 && !active)
        {
            x -= 0.001f;
            Vector3 v3 = new Vector3();
            v3.x = x;
            v3.y = x;
            transform.localScale = v3;
        }
        if (!active)
        {
            destroyTimer -= Time.deltaTime;
        }
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Wall" || col.tag == "Prop" || col.tag == "Enemy" || col.tag == "Boss")
        {
            active = false;
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
