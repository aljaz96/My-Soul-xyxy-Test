using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chained : MonoBehaviour
{

    MonsterStats stats;
    GameObject player;
    public GameObject bullet;
    public GameObject laser;
    float bulletTimer = 0;
    float timer;
    public int type = 0;
    char dir;
    Animator anim;

    void Start()
    {
        stats = gameObject.GetComponent<MonsterStats>();
        player = GameObject.FindWithTag("Player");
        //anim = GetComponent<Animator>();
    }

    void Update()
    {
        bulletTimer -= Time.deltaTime;
        if (stats.active)
        {
            //do stuff
            if (bulletTimer < 0)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, player.transform.position - transform.position);
                if (hit.transform.gameObject == player)
                {
                    if (type == 1)
                    {
                        StartCoroutine(Spawn3Bullets());
                        bulletTimer = Random.Range(1.5f, 2.5f);
                    }
                    if (type == 2)
                    {
                        StartCoroutine(SpawnManyBullets(0.0f));
                        StartCoroutine(SpawnManyBullets(0.2f));
                        StartCoroutine(SpawnManyBullets(0.4f));
                        bulletTimer = Random.Range(1.5f, 2.5f);
                    }
                    if (type == 3)
                    {
                        Laser();
                        bulletTimer = Random.Range(5.0f, 7.0f);
                    }
                }
            }
        }
    }


    void Laser()
    {
        Vector3 pz = player.transform.position;
        Vector2 direction = pz - transform.position;
        direction.Normalize();
        var relativePos = pz - transform.position;
        var angle = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg;
        var rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        GameObject b1 = Instantiate(laser, transform.position, rotation);
        b1.GetComponent<EnemyProjectile>().type = 6;
        b1.GetComponent<EnemyProjectile>().owner = transform.gameObject;
        b1.transform.SetParent(transform);
    }

    IEnumerator SpawnBullet()
    {
        //anim.SetTrigger("shot");
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
    }

    IEnumerator Spawn3Bullets()
    {
        //anim.SetTrigger("shot");
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
    }

    char GetSide(float x, float y)
    {
        if (Mathf.Abs(x) > Mathf.Abs(y))
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

}
