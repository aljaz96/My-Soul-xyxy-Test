using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{

    MonsterStats stats;
    GameObject player;
    public GameObject bullet;
    public GameObject laser;
    public float bulletTimer = 0.5f;
    public float bulletCooldown = 1;
    public int x;
    public int y;
    public int type = 0;
    Vector3 v3;
    Animator anim;

    void Start()
    {
        stats = gameObject.GetComponent<MonsterStats>();
        player = GameObject.FindWithTag("Player");
        anim = GetComponent<Animator>();
        v3 = new Vector3(x, y, 0);
        v3.Normalize();
    }

    void Update()
    {
        if (stats.active)
        {
            bulletTimer -= Time.deltaTime;
            if (bulletTimer < 0)
            {
                if (type == 1)
                {
                    SpawnBullet();
                    bulletTimer = bulletCooldown;
                }
                else if (type == 2)
                {
                    Spawn3Bullets();

                        bulletTimer = bulletCooldown;
                }
                else if (type == 3)
                {
                    Laser();
                    bulletTimer = bulletCooldown;
                }
            }
        }
    }


    void Laser()
    {
        var angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;
        var rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        GameObject b1 = Instantiate(laser, transform.position, rotation);
        b1.GetComponent<EnemyProjectile>().type = 6;
        b1.GetComponent<EnemyProjectile>().owner = transform.gameObject;
        b1.transform.SetParent(transform.parent);
    }

    void SpawnBullet()
    {
        //anim.SetTrigger("shot");
        GameObject b1 = Instantiate(bullet, transform.position, Quaternion.identity);
        b1.GetComponent<EnemyProjectile>().damage = stats.p_damage;
        b1.GetComponent<Rigidbody2D>().velocity = v3 * 4;
    }

    void Spawn3Bullets()
    {
        //anim.SetTrigger("shot");
        GameObject b1 = Instantiate(bullet, transform.position, Quaternion.identity);
        b1.GetComponent<EnemyProjectile>().damage = stats.p_damage;
        GameObject b2 = Instantiate(bullet, transform.position, Quaternion.identity);
        b2.GetComponent<EnemyProjectile>().damage = stats.p_damage;
        GameObject b3 = Instantiate(bullet, transform.position, Quaternion.identity);
        b3.GetComponent<EnemyProjectile>().damage = stats.p_damage;
        if (x != 0)
        {
            b1.GetComponent<Rigidbody2D>().velocity = v3 * 4;
            v3.y = 0.25f;
            v3.Normalize();
            b2.GetComponent<Rigidbody2D>().velocity = v3 * 4;
            v3.y = -0.25f;
            v3.Normalize();
            b3.GetComponent<Rigidbody2D>().velocity = v3 * 4;
            v3.y = 0.0f;
            v3.Normalize();
        }
        else if (y != 0)
        {
            b1.GetComponent<Rigidbody2D>().velocity = v3 * 4;
            v3.x = 0.25f;
            v3.Normalize();
            b2.GetComponent<Rigidbody2D>().velocity = v3 * 4;
            v3.x = -0.25f;
            v3.Normalize();
            b3.GetComponent<Rigidbody2D>().velocity = v3 * 4;
            v3.x = 0.0f;
            v3.Normalize();
        }
    }

}
