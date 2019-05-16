using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charger : MonoBehaviour {

    GameObject player;
    MonsterStats stats;
    public GameObject bullet;
    float rushTimer;
    bool rush = false;
    public bool boss = false;
    Animator anim;
    string turned = "right";
    Rigidbody2D rg;
    // Use this for initialization
    void Start()
    {
        stats = gameObject.GetComponent<MonsterStats>();
        player = GameObject.FindWithTag("Player");
        rushTimer = Random.Range(1.00f, 3.00f);
        anim = GetComponent<Animator>();
        rg = GetComponent<Rigidbody2D>();
        if (boss)
        {
            rushTimer = Random.Range(3.00f, 6.00f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (stats.active)
        {
            rushTimer -= Time.deltaTime;
            ChangeSide(rg.velocity.x);
            if (rushTimer < 0 && !rush)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, player.transform.position - transform.position);
                if (hit.transform.gameObject == player && !rush)
                {
                    Vector3 v = new Vector3(0, 0, 0);
                    GetComponent<Rigidbody2D>().velocity = v;
                    GetComponent<AIPath>().enabled = false;
                    GetComponent<Seeker>().enabled = false;
                    GetComponent<AIDestinationSetter>().enabled = false;
                    anim.SetTrigger("Thinking");
                    if (boss)
                    {
                        int r = Random.Range(1, 3);
                        if(r == 1)
                        {
                            StartCoroutine(RushPlayer());
                        }
                        if(r == 2)
                        {
                            StartCoroutine(Puke());
                        }
                    }
                    else
                    {
                        StartCoroutine(RushPlayer());
                    }
                    rush = true;
                }
            }
        }
    }


    void OnCollisionEnter2D(Collision2D col)
    {
        if (rush)
        {
            rush = false;
            rushTimer = Random.Range(3.0f, 6.0f);
            if (boss)
            {
                SpawnBullers();
                rushTimer = Random.Range(2.0f, 5.0f);
            }
            StartCoroutine(Move(0));
        }
    }

    void ChangeSide(float x)
    {
        if (x > 0 && turned == "left")
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
            turned = "right";
        }
        else if (x < 0 && turned == "right")
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
            turned = "left";
        }
    }

    IEnumerator Move(float f)
    {
        yield return new WaitForSeconds(f);
        rush = false;
        GetComponent<AIPath>().enabled = true;
        GetComponent<Seeker>().enabled = true;
        GetComponent<AIDestinationSetter>().enabled = true;
        anim.SetTrigger("Reset");
    }

    IEnumerator RushPlayer()
    {
        yield return new WaitForSeconds(1);
        anim.SetTrigger("Rush");
        Vector3 v3 = player.transform.position - transform.position;
        v3.Normalize();
        if (boss)
        {
            GetComponent<Rigidbody2D>().velocity = v3 * 11;
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = v3 * 8;
        }
    }

    void SpawnBullers()
    {
        for(int i=0; i<10; i++)
        {
            GameObject b1 = Instantiate(bullet, transform.position, Quaternion.identity);
            b1.GetComponent<EnemyProjectile>().damage = stats.p_damage;
            Vector3 v3 = new Vector3(Random.Range(-1.00f, 1.00f), Random.Range(-1.00f, 1.00f), 0);
            v3.Normalize();
            b1.GetComponent<Rigidbody2D>().velocity = v3 * 2;
        }
    }

    IEnumerator Puke()
    {
        yield return new WaitForSeconds(1);
        anim.SetTrigger("Shot");
        for (int i=0; i<10; i++)
        {
            GameObject b1 = Instantiate(bullet, transform.position, Quaternion.identity);
            b1.GetComponent<EnemyProjectile>().damage = stats.p_damage;
            Vector2 direction = player.transform.position - transform.position;
            direction.x += direction.x * (Random.Range(0.7f, 1.3f));
            direction.y += direction.y * (Random.Range(0.7f, 1.3f));
            direction.Normalize();
            b1.GetComponent<Rigidbody2D>().velocity = direction * Random.Range(4.0f, 6.0f);
        }
        rushTimer = Random.Range(2.0f, 4.0f);
        StartCoroutine(Move(0.5f));
    }



}
