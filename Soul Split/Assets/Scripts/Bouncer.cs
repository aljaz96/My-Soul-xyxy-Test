using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncer : MonoBehaviour {

    // Use this for initialization
    public GameObject Bullet;
    Rigidbody2D Rb;
    MonsterStats stats;
    float speed;
    float minSpeed;
    float maxSpeed;
    float xSpeed;
    float ySpeed;
    bool moved = false;
    public int type;
    float timer;


    void Start()
    {
        stats = gameObject.GetComponent<MonsterStats>();
        speed = stats.speed;
        minSpeed = speed - (speed / 10);
        maxSpeed = speed + (speed / 10);
        Rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!moved && stats.active)
        {
            firstMove();
            moved = true;
        }
        if (stats.active)
        {
            xSpeed = Rb.velocity.x;
            ySpeed = Rb.velocity.y;
            keepMovementXY();
            timer -= Time.deltaTime;
            if (timer <= 0 && type == 3)
            {
                SpawnManyBullets();
                timer = Random.Range(1.5f, 3.0f);
            }
        }
        
    }

    void firstMove()
    {
        int d = Random.Range(1, 5);
        switch (d)
        {
            case 1:
                Rb.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, speed);
                break;
            case 2:
                Rb.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, -speed);
                break;
            case 3:
                Rb.GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, speed);
                break;
            case 4:
                Rb.GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, -speed);
                break;
        }
        timer = Random.Range(1.5f, 3.0f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (type == 2)
        {
            SpawnManyBullets();
        }
    }

    void SpawnBullets()
    {
        GameObject b1 = Instantiate(Bullet, transform.position, Quaternion.identity);
        GameObject b2 = Instantiate(Bullet, transform.position, Quaternion.identity);
        GameObject b3 = Instantiate(Bullet, transform.position, Quaternion.identity);
        GameObject b4 = Instantiate(Bullet, transform.position, Quaternion.identity);
        Vector3 v = new Vector3();
        v.x = 1;
        v.y = 1;
        b1.GetComponent<Rigidbody2D>().velocity = v * 3; //1,1
        v.y = -1;
        b2.GetComponent<Rigidbody2D>().velocity = v * 3; //1,-1
        v.x = -1;
        b3.GetComponent<Rigidbody2D>().velocity = v * 3; //-1,-1
        v.y = 1;
        b4.GetComponent<Rigidbody2D>().velocity = v * 3; //-1,1
    }

    void SpawnManyBullets()
    {
        GameObject b1 = Instantiate(Bullet, transform.position, Quaternion.identity);
        GameObject b2 = Instantiate(Bullet, transform.position, Quaternion.identity);
        GameObject b3 = Instantiate(Bullet, transform.position, Quaternion.identity);
        GameObject b4 = Instantiate(Bullet, transform.position, Quaternion.identity);
        GameObject b5 = Instantiate(Bullet, transform.position, Quaternion.identity);
        GameObject b6 = Instantiate(Bullet, transform.position, Quaternion.identity);
        GameObject b7 = Instantiate(Bullet, transform.position, Quaternion.identity);
        GameObject b8 = Instantiate(Bullet, transform.position, Quaternion.identity);
        Vector3 v = new Vector3();
        v.x = 1;
        v.y = 1;
        b1.GetComponent<Rigidbody2D>().velocity = v * 2; //1,1
        v.y = -1;
        b2.GetComponent<Rigidbody2D>().velocity = v * 2; //1,-1
        v.x = -1;
        b3.GetComponent<Rigidbody2D>().velocity = v * 2; //-1,-1
        v.y = 1;
        b4.GetComponent<Rigidbody2D>().velocity = v * 2; //-1,1
        v.x = 0;
        b5.GetComponent<Rigidbody2D>().velocity = v * 3; //0,1
        v.y = -1;
        b6.GetComponent<Rigidbody2D>().velocity = v * 3; //0,-1
        v.y = 0;
        v.x = 1;
        b7.GetComponent<Rigidbody2D>().velocity = v * 3; //1,0
        v.x = -1;
        b8.GetComponent<Rigidbody2D>().velocity = v * 3; //-1,0
    }

    void keepMovementXY()
    {
        if (Rb.GetComponent<Rigidbody2D>().velocity.x < minSpeed && xSpeed > 0.001f || xSpeed > maxSpeed && xSpeed > 0.001f)
        {
            Rb.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, ySpeed);
        }
        if (Rb.GetComponent<Rigidbody2D>().velocity.x > -minSpeed && xSpeed < -0.001f || xSpeed < -maxSpeed && xSpeed < -0.001f)
        {
            Rb.GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, ySpeed);
        }
        if (Rb.GetComponent<Rigidbody2D>().velocity.y < minSpeed && ySpeed > 0.001f || ySpeed > maxSpeed && ySpeed > 0.001f)
        {
            Rb.GetComponent<Rigidbody2D>().velocity = new Vector2(xSpeed, speed);
        }
        if (Rb.GetComponent<Rigidbody2D>().velocity.y > -minSpeed && ySpeed < -0.001f || ySpeed < -maxSpeed && ySpeed < 0.001f)
        {
            Rb.GetComponent<Rigidbody2D>().velocity = new Vector2(xSpeed, -speed);
        }
    }
}
