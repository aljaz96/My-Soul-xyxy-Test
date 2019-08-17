using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{

    GameObject player;
    public int damage = 10;
    public GameObject bullet;
    public GameObject laser;
    float bulletTimer;
    public float bulletCooldown = 1;
    public float bulletStartTime = 1;
    public int x;
    public int y;
    public int type = 0;
    Vector3 v3;
    Vector3 position;
    public string roomName;
    public string playerRoomName;

    void Start()
    {

        bullet = (GameObject)Resources.Load("Prefabs/BigEnemyBullet2");
        if(type == 4 || type == 5)
        {
            bullet = (GameObject)Resources.Load("Prefabs/BigEnemyBullet3");
        }
        laser = (GameObject)Resources.Load("Prefabs/EnemyLaserPreparing");
        player = GameObject.FindWithTag("Player");
        v3 = new Vector3(x, y, 0);
        v3.Normalize();
        position = transform.position;
        position.y += 0.35f;
        roomName = transform.parent.gameObject.transform.parent.name;
    }

    void Update()
    {
        if (player != null)
        {
            playerRoomName = player.transform.parent.name;
            if (roomName == playerRoomName)
            {
                bulletTimer -= Time.deltaTime;
                if (bulletTimer < 0)
                {
                    if (type == 1 || type == 4)
                    {
                        SpawnBullet();
                        bulletTimer = bulletCooldown;
                    }
                    else if (type == 2 || type == 5)
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
            else
            {
                bulletTimer = bulletStartTime;
            }
        }
    }


    void Laser()
    {
        var angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;
        var rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        GameObject b1 = Instantiate(laser, position, rotation);
        b1.GetComponent<EnemyProjectile>().type = 6;
        b1.GetComponent<EnemyProjectile>().owner = transform.gameObject;
        b1.transform.SetParent(transform.parent);
    }

    void SpawnBullet()
    {
        //anim.SetTrigger("shot");
        GameObject b1 = Instantiate(bullet, position, Quaternion.identity);
        b1.GetComponent<EnemyProjectile>().damage = damage;
        b1.GetComponent<Rigidbody2D>().velocity = v3 * 4;
    }

    void Spawn3Bullets()
    {
        //anim.SetTrigger("shot");
        GameObject b1 = Instantiate(bullet, position, Quaternion.identity);
        b1.GetComponent<EnemyProjectile>().damage = damage;
        GameObject b2 = Instantiate(bullet, position, Quaternion.identity);
        b2.GetComponent<EnemyProjectile>().damage = damage;
        GameObject b3 = Instantiate(bullet, position, Quaternion.identity);
        b3.GetComponent<EnemyProjectile>().damage = damage;
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
