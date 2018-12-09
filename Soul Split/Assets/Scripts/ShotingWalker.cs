using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotingWalker : MonoBehaviour {

    // Use this for initialization
    GameObject player;
    public GameObject bullet;
    MonsterStats stats;
    public float movementTimer;
    public float shotTimer = 0.5f;
    float speed;
    int side = -1;
    float velocity;
    bool shoting = false;
    public int type = 0;
    string turned = "left";
    float turned_x;
    Animator anim;

    void Start () {
        stats = gameObject.GetComponent<MonsterStats>();
        speed = stats.speed;
        player = GameObject.FindWithTag("Player");
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        if (!shoting)
        {
            movementTimer -= Time.deltaTime;
        }
        shotTimer -= Time.deltaTime;
        velocity = GetComponent<Rigidbody2D>().velocity.magnitude;
        if (stats.active)
        {
            //do stuff
            if (movementTimer < 0)
            {
                changeDirection();
            }
            turned_x = GetComponent<Rigidbody2D>().velocity.x;
            ChangeSide(turned_x);
            Vector3 direction = player.transform.position - transform.position;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction);
            if (hit.transform.gameObject == player && shotTimer < 0 && !shoting)
            {
                ChangeSide(direction.x);
                shoting = true;   
                GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
                if (type == 1)
                {
                    anim.SetTrigger("NormalShot");
                    StartCoroutine(Shoot());
                }
                else if(type == 2)
                {
                    StartCoroutine(ShootShell(10));
                }
                else if(type == 3)
                {
                    for(int i=1; i<41; i++)
                    {
                        StartCoroutine(ShootMany((float)i/20));
                    }
                    StartCoroutine(Move(2.5f, 5));
                }
            }   
            if (velocity == 0 && !shoting)
            {
                changeDirection();
            }
        }
    }

    void ChangeSide(float x)
    {
        if (x > 0 && turned == "left")
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
            turned = "right";
        }
        else if (x < 0 && turned == "right")
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
            turned = "left";
        }
    }

    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(0.1f);
        Vector3 pos = transform.position;
        pos.y = pos.y - 0.25f;
        GameObject b1 = Instantiate(bullet, pos, Quaternion.identity);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        b1.GetComponent<Rigidbody2D>().velocity = direction * 6;
        StartCoroutine(Move(0.25f, 2));
    }

    IEnumerator ShootShell(int num)
    {
        yield return new WaitForSeconds(0.3f);
        for (int i = 0; i < num; i++)
        {
            GameObject b1 = Instantiate(bullet, transform.position, Quaternion.identity);
            Vector2 direction = player.transform.position - transform.position;
            direction.x = direction.x + UnityEngine.Random.Range(-0.50f, 0.51f);
            direction.y = direction.y + UnityEngine.Random.Range(-0.50f, 0.51f);
            direction.Normalize();
            float vel = UnityEngine.Random.Range(5.00f, 6.00f);
            b1.GetComponent<Rigidbody2D>().velocity = direction * vel;
        }
        StartCoroutine(Move(0.25f, 4));
    }

    IEnumerator ShootMany(float timer)
    {
        yield return new WaitForSeconds(timer);
        GameObject b1 = Instantiate(bullet, transform.position, Quaternion.identity);
        Vector2 direction = player.transform.position - transform.position;
        direction.x = direction.x + UnityEngine.Random.Range(-0.30f, 0.31f);
        direction.y = direction.y + UnityEngine.Random.Range(-0.30f, 0.31f);
        direction.Normalize();
        b1.GetComponent<Rigidbody2D>().velocity = direction * 6;
    }


    IEnumerator Move(float timer, float cooldown)
    {
        yield return new WaitForSeconds(timer);
        side = -1;
        shotTimer = cooldown;
        shoting = false;
        changeDirection();
    }


    void OnCollisionEnter2D(Collision2D col)
    {
        changeDirection();
    }

    void changeDirection()
    {
        int decision = UnityEngine.Random.Range(1, 5);
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
