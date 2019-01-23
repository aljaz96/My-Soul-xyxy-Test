using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{

    // Use this for initialization
    // Use this for initialization
    float timer = 20;
    public GameObject destroyedEffect;
    public GameObject bullet;
    public GameObject owner;
    float angle;
    Vector3 startPos;
    Vector3 endPos;
    public int damage = 5;
    public int type = 1;
    float bulletTimer;
    int phase;

    void Start()
    {
        startPos = transform.position;
        phase = Random.Range(0, 32);
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        bulletTimer -= Time.deltaTime;
        if (type == 3 && bulletTimer <= 0)
        {
            BulletSprinkler(phase);
        }
        if (type == 6)
        {
            transform.position = owner.transform.position;
            if (transform.localScale.y != 36f)
            {
                transform.localScale += new Vector3(0, 0.8f, 0);
            }
            if (transform.localScale.y >= 36f)
            {
                GameObject b1 = Instantiate(bullet, transform.position, transform.rotation);
                b1.transform.SetParent(transform.parent);
                b1.GetComponent<EnemyProjectile>().owner = owner;
                b1.transform.SetParent(owner.transform);
                Destroy(gameObject);
            }
        }
        if (type == 7)
        {
            transform.position = owner.transform.position;
            if (timer < 17)
            {
                transform.localScale -= new Vector3(0, 0.6f, 0);
            }
            if (transform.localScale.y <= 0)
            {
                Destroy(gameObject);
            }
        }
        if (timer < 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wall")
        {
            if (type == 2)
            {
                BulletBomb(20);
            }
            endPos = transform.position;
            DestroyProjectile();
        }
        else if (collision.tag == "Player")
        {
            if (type == 2)
            {
                BulletBomb(20);
            }
            DestroyProjectile();
        }
    }

    void DestroyProjectile()
    {
        if (type != 7)
        {
            endPos = transform.position;
            var relativePos = startPos - endPos;
            angle = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg;
            var rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            GameObject effect = Instantiate(destroyedEffect, endPos, rotation);
            Destroy(gameObject);
        }
    }

    private void BulletSprinkler(int p)
    {
        GameObject b1 = Instantiate(bullet, transform.position, Quaternion.identity);
        Vector3 v3 = new Vector3();
        switch (p)
        {
            case 0:
                v3 = new Vector3(2, 0, 0);
                break;
            case 1:
                v3 = new Vector3(2, 0.5f, 0);
                break;
            case 2:
                v3 = new Vector3(2, 1, 0);
                break;
            case 3:
                v3 = new Vector3(2, 1.5f, 0);
                break;
            case 4:
                v3 = new Vector3(2, 2, 0);
                break;
            case 5:
                v3 = new Vector3(1.5f, 2, 0);
                break;
            case 6:
                v3 = new Vector3(1, 2, 0);
                break;
            case 7:
                v3 = new Vector3(0.5f, 2, 0);
                break;
            case 8:
                v3 = new Vector3(0, 2, 0);
                break;
            case 9:
                v3 = new Vector3(-0.5f, 2, 0);
                break;
            case 10:
                v3 = new Vector3(-1f, 2, 0);
                break;
            case 11:
                v3 = new Vector3(-1.5f, 2, 0);
                break;
            case 12:
                v3 = new Vector3(-2, 2, 0);
                break;
            case 13:
                v3 = new Vector3(-2, 1.5f, 0);
                break;
            case 14:
                v3 = new Vector3(-2, 1, 0);
                break;
            case 15:
                v3 = new Vector3(-2, 0.5f, 0);
                break;
            case 16:
                v3 = new Vector3(-2, 0, 0);
                break;
            case 17:
                v3 = new Vector3(-2, -0.5f, 0);
                break;
            case 18:
                v3 = new Vector3(-2, -1, 0);
                break;
            case 19:
                v3 = new Vector3(-2, -1.5f, 0);
                break;
            case 20:
                v3 = new Vector3(-2, -2, 0);
                break;
            case 21:
                v3 = new Vector3(-1.5f, -2, 0);
                break;
            case 22:
                v3 = new Vector3(-1, -2, 0);
                break;
            case 23:
                v3 = new Vector3(-0.5f, -2, 0);
                break;
            case 24:
                v3 = new Vector3(0, -2, 0);
                break;
            case 25:
                v3 = new Vector3(0.5f, -2, 0);
                break;
            case 26:
                v3 = new Vector3(1, -2, 0);
                break;
            case 27:
                v3 = new Vector3(1.5f, -2, 0);
                break;
            case 28:
                v3 = new Vector3(2, -2, 0);
                break;
            case 29:
                v3 = new Vector3(2, -1.5f, 0);
                break;
            case 30:
                v3 = new Vector3(2, -1, 0);
                break;
            case 31:
                v3 = new Vector3(2, -0.5f, 0);
                break;
        }
        phase++;
        v3.Normalize();
        b1.GetComponent<Rigidbody2D>().velocity = v3 * 4;
        if (phase == 32)
        {
            phase = 0;
        }
        bulletTimer = 0.2f;
    }

    private void BulletBomb(int numBullets)
    {
        for (int i = 0; i < numBullets; i++)
        {
            GameObject b1 = Instantiate(bullet, transform.position, Quaternion.identity);
            Vector3 v3 = new Vector3();
            v3.x += Random.Range(-4f, 4f);
            v3.y += Random.Range(-4f, 4f);
            v3.Normalize();
            b1.GetComponent<Rigidbody2D>().velocity = v3 * 8;
        }
    }
}
