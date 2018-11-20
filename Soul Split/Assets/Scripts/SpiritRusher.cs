using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritRusher : MonoBehaviour {

    GameObject player;
    GameObject currentRoom;
    GameObject playerRoom;
    public GameObject bullet;
    public bool active = false;
    public float movementTimer;
    public float speed = 2;
    public float rushTimer = 0;
    public float rushCooldown = 3;
    public float bulletTimer = 0;
    public float bulletCooldown = 0.15f;
    public bool rush = false;
    float player_X;
    float player_Y;
    float enemy_X;
    float enemy_Y;
    int side = -1;
    GameObject raycasted;
    public float velocity;
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        currentRoom = new GameObject();
        currentRoom = transform.parent.gameObject.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        rushTimer -= Time.deltaTime;
        bulletTimer -= Time.deltaTime;
        movementTimer -= Time.deltaTime;
        velocity = gameObject.GetComponent<Rigidbody2D>().velocity.magnitude;
        if (active)
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
        else
        {
            playerRoom = player.transform.parent.gameObject;

            if (playerRoom.name == currentRoom.name)
            {
                active = true;
            }
        }
    }


    void OnCollisionEnter2D(Collision2D col)
    {
        rush = false;
        changeDirection();
    }

    void RushPlayer(float x, float y)
    {
        rush = true;
        GetComponent<Rigidbody2D>().velocity = new Vector3(x, y, 0);
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
                break;
            case 2:
                GetComponent<Rigidbody2D>().velocity = new Vector3(-speed, 0, 0);
                break;
            case 3:
                GetComponent<Rigidbody2D>().velocity = new Vector3(0, -speed, 0);
                break;
            case 4:
                GetComponent<Rigidbody2D>().velocity = new Vector3(0, speed, 0);
                break;
        }
        movementTimer = UnityEngine.Random.Range(1, 6);
    }

    void spawnBullets(int decision)
    {
        GameObject b1 = Instantiate(bullet, transform.position, Quaternion.identity);
        GameObject b2 = Instantiate(bullet, transform.position, Quaternion.identity);
        if (decision == 1 || decision == 2)
        {
            b1.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 1, 0) * 3;
            b2.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -1, 0) * 3;
        }
        else
        {
            b1.GetComponent<Rigidbody2D>().velocity = new Vector3(1, 0, 0) * 3;
            b2.GetComponent<Rigidbody2D>().velocity = new Vector3(-1, 0, 0) * 3;
        }
    }
}
