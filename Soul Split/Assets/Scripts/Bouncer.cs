using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncer : MonoBehaviour {

    // Use this for initialization
    public GameObject Bullet;
    public GameObject head;
    Animator anim;
    Rigidbody2D Rb;
    MonsterStats stats;
    float speed;
    float minSpeed;
    float maxSpeed;
    float xSpeed;
    float ySpeed;
    bool moved = false;
    public int type;
    float timer = 0;

    void Start()
    {
        stats = gameObject.GetComponent<MonsterStats>();
        speed = stats.speed;
        minSpeed = speed - (speed / 15);
        maxSpeed = speed + (speed / 15);
        Rb = gameObject.GetComponent<Rigidbody2D>();
        anim = head.GetComponent<Animator>();
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
            CheckVelocity();
            xSpeed = Rb.velocity.x;
            ySpeed = Rb.velocity.y;
            KeepMovementXY();
            timer -= Time.deltaTime;
            if (timer <= 0 && type == 3)
            {
                SpawnManyBullets();
                anim.SetTrigger("Shoot");
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
        if (type == 2 && timer < 0)
        {
            SpawnBullets();
            anim.SetTrigger("Shoot");
            timer = 1;
        }
    }

    void SpawnBullets()
    {
        Vector3 v3 = transform.position;
        v3.y -= 0.3f;
        GameObject b1 = Instantiate(Bullet, v3, Quaternion.identity);
        GameObject b2 = Instantiate(Bullet, v3, Quaternion.identity);
        GameObject b3 = Instantiate(Bullet, v3, Quaternion.identity);
        GameObject b4 = Instantiate(Bullet, v3, Quaternion.identity);
        b1.GetComponent<EnemyProjectile>().damage = stats.p_damage;
        b2.GetComponent<EnemyProjectile>().damage = stats.p_damage;
        b3.GetComponent<EnemyProjectile>().damage = stats.p_damage;
        b4.GetComponent<EnemyProjectile>().damage = stats.p_damage;
        Vector3 v = new Vector3();
        v.x = 1;
        v.y = 1;
        b1.GetComponent<Rigidbody2D>().velocity = v * 4; //1,1
        v.y = -1;
        b2.GetComponent<Rigidbody2D>().velocity = v * 4; //1,-1
        v.x = -1;
        b3.GetComponent<Rigidbody2D>().velocity = v * 4; //-1,-1
        v.y = 1;
        b4.GetComponent<Rigidbody2D>().velocity = v * 4; //-1,1
    }

    void SpawnManyBullets()
    {
        Vector3 v3 = transform.position;
        v3.y -= 0.3f;
        GameObject b1 = Instantiate(Bullet, v3, Quaternion.identity);
        GameObject b2 = Instantiate(Bullet, v3, Quaternion.identity);
        GameObject b3 = Instantiate(Bullet, v3, Quaternion.identity);
        GameObject b4 = Instantiate(Bullet, v3, Quaternion.identity);
        GameObject b5 = Instantiate(Bullet, v3, Quaternion.identity);
        GameObject b6 = Instantiate(Bullet, v3, Quaternion.identity);
        GameObject b7 = Instantiate(Bullet, v3, Quaternion.identity);
        GameObject b8 = Instantiate(Bullet, v3, Quaternion.identity);
        b1.GetComponent<EnemyProjectile>().damage = stats.p_damage;
        b2.GetComponent<EnemyProjectile>().damage = stats.p_damage;
        b3.GetComponent<EnemyProjectile>().damage = stats.p_damage;
        b4.GetComponent<EnemyProjectile>().damage = stats.p_damage;
        b5.GetComponent<EnemyProjectile>().damage = stats.p_damage;
        b6.GetComponent<EnemyProjectile>().damage = stats.p_damage;
        b7.GetComponent<EnemyProjectile>().damage = stats.p_damage;
        b8.GetComponent<EnemyProjectile>().damage = stats.p_damage;
        Vector3 v = new Vector3();
        v.x = 1;
        v.y = 1;
        b1.GetComponent<Rigidbody2D>().velocity = v * 3f; //1,1
        v.y = -1;
        b2.GetComponent<Rigidbody2D>().velocity = v * 3f; //1,-1
        v.x = -1;
        b3.GetComponent<Rigidbody2D>().velocity = v * 3f; //-1,-1
        v.y = 1;
        b4.GetComponent<Rigidbody2D>().velocity = v * 3f; //-1,1
        v.x = 0;
        b5.GetComponent<Rigidbody2D>().velocity = v * 4; //0,1
        v.y = -1;
        b6.GetComponent<Rigidbody2D>().velocity = v * 4; //0,-1
        v.y = 0;
        v.x = 1;
        b7.GetComponent<Rigidbody2D>().velocity = v * 4; //1,0
        v.x = -1;
        b8.GetComponent<Rigidbody2D>().velocity = v * 4; //-1,0
    }

    void KeepMovementXY()
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

    void CheckVelocity()
    {
        if (GetComponent<Rigidbody2D>().velocity.x > 0)
        {
            Vector3 v3 = head.transform.position;
            v3.x = transform.position.x + 0.060f;
            v3.y = transform.position.y;
            head.transform.position = v3;
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
            head.GetComponent<SpriteRenderer>().flipX = false;
        }
        if (GetComponent<Rigidbody2D>().velocity.x < 0)
        {
            Vector3 v3 = head.transform.position;
            v3.x = transform.position.x -0.060f;
            v3.y = transform.position.y;
            head.transform.position = v3;
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
            head.GetComponent<SpriteRenderer>().flipX = true;
        }
    }
}
