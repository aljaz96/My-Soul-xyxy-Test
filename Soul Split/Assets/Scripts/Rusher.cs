using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rusher : MonoBehaviour {

    GameObject player;
    MonsterStats stats;
    float movementTimer;
    float speed;
    float rushTimer = 0;
    public float rushCooldown = 3;
    bool rush = false;
    float player_X;
    float player_Y;
    float enemy_X;
    float enemy_Y;
    int side = -1;
    public float velocity;
    // Use this for initialization
    void Start () {
        stats = gameObject.GetComponent<MonsterStats>();
        speed = stats.speed;
        player = GameObject.FindWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {
        rushTimer -= Time.deltaTime;
        movementTimer -= Time.deltaTime;
        velocity = gameObject.GetComponent<Rigidbody2D>().velocity.magnitude;
        if (stats.active)
        {
            //do stuff
            if (movementTimer < 0 && !rush)
            {
                changeDirection();
            }
            if (rushTimer < 0)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, player.transform.position - transform.position);
                if (hit.transform.gameObject == player)
                {
                    player_X = (float)Math.Round(player.transform.position.x, 1, MidpointRounding.ToEven);
                    player_Y = (float)Math.Round(player.transform.position.y, 1, MidpointRounding.ToEven);
                    enemy_X = (float)Math.Round(transform.position.x, 1, MidpointRounding.ToEven);
                    enemy_Y = (float)Math.Round(transform.position.y, 1, MidpointRounding.ToEven);
                    if (enemy_X == player_X && enemy_Y < player_Y)
                    {
                        RushPlayer(0, speed * 4);
                    }
                    else if (enemy_X == player_X && enemy_Y > player_Y)
                    {
                        RushPlayer(0, -speed * 4);  
                    }
                    else if (enemy_X < player_X && enemy_Y == player_Y)
                    {
                        RushPlayer(speed * 4, 0);   
                    }
                    else if (enemy_X > player_X && enemy_Y == player_Y)
                    {
                        RushPlayer(-speed * 4, 0);    
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
}
