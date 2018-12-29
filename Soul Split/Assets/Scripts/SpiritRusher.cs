using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritRusher : MonoBehaviour {

    GameObject player;
    MonsterStats stats;
    public GameObject bullet;
    float movementTimer;
    float speed;
    float rushTimer = 0;
    public float rushCooldown = 3;
    float bulletTimer = 0;
    public float bulletCooldown = 0.15f;
    bool rush = false;
    float player_X;
    float player_Y;
    float enemy_X;
    float enemy_Y;
    int side = -1;
    GameObject raycasted;
    float velocity;
    string turned = "left";
    Animator anim;
    // Use this for initialization
    void Start()
    {
        stats = gameObject.GetComponent<MonsterStats>();
        speed = stats.speed;
        player = GameObject.FindWithTag("Player");
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        rushTimer -= Time.deltaTime;
        bulletTimer -= Time.deltaTime;
        movementTimer -= Time.deltaTime;
        velocity = gameObject.GetComponent<Rigidbody2D>().velocity.magnitude;
        if (stats.active)
        {
            //do stuff
            if (movementTimer < 0 && !rush)
            {
                changeDirection();
            }
            if (rush && bulletTimer < 0)
            {
                spawnBullets(side);
                bulletTimer = bulletCooldown;
            }
            if (rushTimer < 0)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, player.transform.position - transform.position);
                raycasted = hit.transform.gameObject;
                if (hit.transform.gameObject == player)
                {
                    player_X = (float)Math.Round(player.transform.position.x, 0, MidpointRounding.ToEven);
                    player_Y = (float)Math.Round(player.transform.position.y, 0, MidpointRounding.ToEven);
                    enemy_X = (float)Math.Round(transform.position.x, 0, MidpointRounding.ToEven);
                    enemy_Y = (float)Math.Round(transform.position.y, 0, MidpointRounding.ToEven);
                    if (enemy_X == player_X && enemy_Y < player_Y)
                    {
                        RushPlayer(0, speed * 4);
                        side = 4;
                    }
                    else if (enemy_X == player_X && enemy_Y > player_Y)
                    {
                        RushPlayer(0, -speed * 4);
                        side = 3;
                    }
                    else if (enemy_X < player_X && enemy_Y == player_Y)
                    {
                        RushPlayer(speed * 4, 0);
                        side = 1;
                    }
                    else if (enemy_X > player_X && enemy_Y == player_Y)
                    {
                        RushPlayer(-speed * 4, 0);
                        side = 2;
                    }
                }
            }
            if (velocity == 0)
            {
                changeDirection();
            }
        }
    }


    void OnCollisionEnter2D(Collision2D col)
    {
        rush = false;
        changeDirection();
    }

    void SetAnim(float speedX, float speedY)
    {
        if (speedY > 0.1f && !rush)
        {
            anim.SetTrigger("Up");
        }
        else if (speedY > 0.1f && rush)
        {
            anim.SetTrigger("RushUp");
        }
        else if (speedY < -0.1f && !rush)
        {
            anim.SetTrigger("Down");
        }
        else if (speedY < -0.1f && rush)
        {
            anim.SetTrigger("RushDown");
        }
        else if ((speedX < -0.1f && !rush) || (speedX > 0.1f && !rush))
        {
            anim.SetTrigger("Side");
        }
        else if ((speedX < -0.1f && rush) || (speedX > 0.1f && rush))
        {
            anim.SetTrigger("RushSide");
        }
        if (speedX < 0 && turned == "left")
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
            turned = "right";
        }
        else if (speedX > 0 && turned == "right")
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
            turned = "left";
        }
    }

    void RushPlayer(float x, float y)
    {
        rush = true;
        GetComponent<Rigidbody2D>().velocity = new Vector3(x, y, 0);
        SetAnim(x, y);
        movementTimer = 10;
        rushTimer = rushCooldown;
    }

    void changeDirection()
    {
        int decision = decision = UnityEngine.Random.Range(1, 5);
        while (decision == side)
        {
            decision = UnityEngine.Random.Range(1, 5);
        }
        side = decision;

        switch (decision)
        {
            case 1:
                GetComponent<Rigidbody2D>().velocity = new Vector3(speed, 0, 0);
                SetAnim(speed, 0);
                break;
            case 2:
                GetComponent<Rigidbody2D>().velocity = new Vector3(-speed, 0, 0);
                SetAnim(-speed, 0);
                break;
            case 3:
                GetComponent<Rigidbody2D>().velocity = new Vector3(0, -speed, 0);
                SetAnim(0, -speed);
                break;
            case 4:
                GetComponent<Rigidbody2D>().velocity = new Vector3(0, speed, 0);
                SetAnim(0, speed);
                break;
        }
        movementTimer = UnityEngine.Random.Range(1, 6);
    }

    void spawnBullets(int decision)
    {
        GameObject b1 = Instantiate(bullet, transform.position, Quaternion.identity);
        b1.GetComponent<EnemyProjectile>().damage = stats.p_damage;
        GameObject b2 = Instantiate(bullet, transform.position, Quaternion.identity);
        b2.GetComponent<EnemyProjectile>().damage = stats.p_damage;
        if (decision == 1 || decision == 2)
        {
            b1.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 1, 0) * 4;
            b2.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -1, 0) * 4;
        }
        else
        {
            b1.GetComponent<Rigidbody2D>().velocity = new Vector3(1, 0, 0) * 4;
            b2.GetComponent<Rigidbody2D>().velocity = new Vector3(-1, 0, 0) * 4;
        }
    }
}
