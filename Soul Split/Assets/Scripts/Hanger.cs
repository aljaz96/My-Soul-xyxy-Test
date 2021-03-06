﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hanger : MonoBehaviour {

    MonsterStats stats;
    GameObject player;
    GameObject bullet;
    public float moveLimit = 3;
    public float movementTimer = 0;
    float speed;
    float bulletTimer = 0;
    float originalPosition;
    float timer;
    public int type = 0;
    public char dir;
    public Vector3 v;
    Animator anim;
    public bool MAMA = false;

    void Start()
    {
        bullet = (GameObject)Resources.Load("Prefabs/EnemyBullet");
        stats = gameObject.GetComponent<MonsterStats>();
        speed = stats.speed;
        player = GameObject.FindWithTag("Player");
        anim = GetComponent<Animator>();
        originalPosition = transform.position.y;
        MoveRandomly();
    }

    void Update()
    {
        movementTimer -= Time.deltaTime;
        bulletTimer -= Time.deltaTime;
        if (stats.active && player != null)
        {
            //do stuff
            anim.SetFloat("movement", GetComponent<Rigidbody2D>().velocity.y);
            if (bulletTimer < 0)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, player.transform.position - transform.position);
                if (hit.transform.gameObject == player)
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
                    if (type == 1)
                    {
                        StartCoroutine(SpawnBullet());
                    }
                    if(type == 2)
                    {
                        StartCoroutine(Spawn3Bullets());
                    }
                    if(type == 3)
                    {
                        StartCoroutine(SpawnManyBullets(0.0f));
                        StartCoroutine(SpawnManyBullets(0.2f));
                        StartCoroutine(SpawnManyBullets(0.4f));
                    }
                    bulletTimer = Random.Range(1.5f, 2.5f);
                    movementTimer += 1;
                }
            }
            if ((transform.position.y > originalPosition + moveLimit || transform.position.y < originalPosition - moveLimit) && timer - 1 > movementTimer)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
            }
            if(movementTimer < 0)
            {
                MoveRandomly();
                movementTimer = Random.Range(3.00f, 6.00f);
            }
           
        }
    }


    IEnumerator SpawnBullet()
    {
        anim.SetTrigger("shot");
        yield return new WaitForSeconds(0.1f);
        Vector3 v3 = transform.position;
        v3.y -= 0.1f;
        GameObject b1 = Instantiate(bullet, v3, Quaternion.identity);
        b1.GetComponent<EnemyProjectile>().damage = stats.p_damage;
        v3 = player.transform.position - transform.position;
        v3.x += Random.Range(-0.5f, 0.5f);
        v3.y += Random.Range(-0.5f, 0.5f);
        v3.Normalize();
        b1.GetComponent<Rigidbody2D>().velocity = v3 * 4;
        StartCoroutine(Move(0.8f));
    }
    IEnumerator Spawn3Bullets()
    {
        anim.SetTrigger("shot");
        yield return new WaitForSeconds(0.1f);
        GameObject b1 = Instantiate(bullet, transform.position, Quaternion.identity);
        b1.GetComponent<EnemyProjectile>().damage = stats.p_damage;
        GameObject b2 = Instantiate(bullet, transform.position, Quaternion.identity);
        b2.GetComponent<EnemyProjectile>().damage = stats.p_damage;
        GameObject b3 = Instantiate(bullet, transform.position, Quaternion.identity);
        b3.GetComponent<EnemyProjectile>().damage = stats.p_damage;
        Vector3 v3 = (player.transform.position - transform.position) * 1000;
        v3.Normalize();
        dir = GetSide(v3.x, v3.y);
        if (dir == 'x')
        {
            b1.GetComponent<Rigidbody2D>().velocity = v3 * 5;
            v3.x = v3.x * 0.75f;
            v3.y = v3.y * 1.25f;
            v3.Normalize();
            b2.GetComponent<Rigidbody2D>().velocity = v3 * 5;
            v3 = (player.transform.position - transform.position) * 1000;
            v3.x = v3.x * 1.25f;
            v3.y = v3.y * 0.75f;
            v3.Normalize();
            b3.GetComponent<Rigidbody2D>().velocity = v3 * 5;
        }
        else
        {
            b1.GetComponent<Rigidbody2D>().velocity = v3 * 5;
            v3.y = v3.y * 0.75f;
            v3.x = v3.x * 1.25f;
            v3.Normalize();
            b2.GetComponent<Rigidbody2D>().velocity = v3 * 5;
            v3 = (player.transform.position - transform.position) * 1000;
            v3.y = v3.y * 1.25f;
            v3.x = v3.x * 0.75f;
            v3.Normalize();
            b3.GetComponent<Rigidbody2D>().velocity = v3 * 5;
        }
        StartCoroutine(Move(0.8f));
    }

    char GetSide(float x, float y)
    {
        if(Mathf.Abs(x) > Mathf.Abs(y))
        {
            return 'x';
        }
        else
        {
            return 'y';
        }
    }

    IEnumerator SpawnManyBullets(float timer)
    {
        yield return new WaitForSeconds(timer);
        StartCoroutine(Spawn3Bullets());
    }

    IEnumerator Move(float timer)
    {
        yield return new WaitForSeconds(timer);
        MoveRandomly();
    }

    void MoveRandomly()
    {
        int r = Random.Range(0, 2);
        if (r == 0)
        {
            if (transform.position.y < originalPosition + moveLimit)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector3(0, speed, 0);
            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = new Vector3(0, -speed, 0);
            }
        }
        else if(r == 1)
        {
            if(transform.position.y > originalPosition - moveLimit)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector3(0, -speed, 0);
            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = new Vector3(0, speed, 0);
            }
        }
        movementTimer = timer = Random.Range(1.0f, 4.0f);
    }

}
