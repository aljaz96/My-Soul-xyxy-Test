using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blob : MonoBehaviour {

    public GameObject LesserBlob;
    public GameObject Bullet;
    MonsterStats stats;
    GameObject player;
    GameObject parent;
    public int hp = 999;
    public int blobSize = 1;
    public float rushTimer;
    float timer = 0.1f;
    public bool rush = false;
    bool right = true;
    float oldXpos;
    float newXpos;
    float oldYpos;
    float newYpos;
    public float differenceX;
    public float differenceY;
    bool died = false;
    string current;
    AIPath AI;
    Animator anim;
    SpriteRenderer sr;

    void Start()
    {
        oldXpos = newXpos = transform.position.x;
        oldYpos = newYpos = transform.position.y;
        rushTimer = Random.Range(3, 5);
        player = GameObject.FindWithTag("Player");
        stats = gameObject.GetComponent<MonsterStats>();
        AI = gameObject.GetComponent<AIPath>();
        parent = gameObject.transform.parent.gameObject;
        anim = GetComponent<Animator>();
        sr = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (stats.active)
        {
            timer -= Time.deltaTime;
            Destroy(GetComponent<PolygonCollider2D>());
            gameObject.AddComponent<PolygonCollider2D>();   
            ChangeSpriteSide();
            rushTimer -= Time.deltaTime;
            if (stats.hp < hp)
            {
                if (blobSize == 1)
                {
                    Destroy(gameObject);
                }
                if (blobSize == 2 || blobSize == 3)
                {
                    MakeBlobs(2);
                    Destroy(gameObject);
                }
                else if (blobSize == 4)
                {
                    SpawnBullets();
                    MakeBlobs(2);
                    Destroy(gameObject);
                }
                else if (blobSize == 5 && !died)
                {
                    rushTimer = 5;
                    died = true;
                    GetComponent<AIPath>().enabled = false;
                    GetComponent<Seeker>().enabled = false;
                    GetComponent<AIDestinationSetter>().enabled = false;
                    Vector3 s = new Vector3(0, 0, 0);
                    GetComponent<Rigidbody2D>().velocity = s;
                    rush = false;
                    BulletExplosion(20);
                    anim.SetTrigger("Died");
                    StartCoroutine(Make3Blobs(0.5f));
                    Destroy(gameObject, 0.8f);
                }
                
            }
            if(blobSize == 5 && rushTimer < 0 && !rush)
            {
                GetComponent<AIPath>().enabled = false;
                GetComponent<Seeker>().enabled = false;
                GetComponent<AIDestinationSetter>().enabled = false;
                rush = true;
                Vector3 p = player.transform.position;
                anim.SetTrigger("Rush");
                StartCoroutine(Rush(p));
            }
        }
    }

    void ChangeSpriteSide()
    {
        newXpos = transform.position.x;
        newYpos = transform.position.y;
        if (newXpos > oldXpos && !right)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1 ,transform.localScale.y);
            right = true;
        }
        else if (newXpos < oldXpos && right)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y);
            right = false;
        }
        if (blobSize != 5 && timer < 0)
        {
            
            differenceX = Mathf.Abs(oldXpos - newXpos) * 100;
            differenceY = Mathf.Abs(oldYpos - newYpos) * 100;
            if (Mathf.Abs(oldYpos - newYpos) > Mathf.Abs(oldXpos - newXpos) && current != "Straight")
            {
                current = "Straight";
                anim.SetTrigger("Straight");
                timer = 0.1f;
            }
            else if (Mathf.Abs(oldYpos - newYpos) < Mathf.Abs(oldXpos - newXpos) && current != "Side")
            {
                current = "Side";
                anim.SetTrigger("Side");
                timer = 0.1f;
            }
        }
        oldXpos = newXpos;
        oldYpos = newYpos;
    }

    IEnumerator Rush(Vector3 player_pos)
    {
        yield return new WaitForSeconds(1);
        anim.SetTrigger("Rushing");
        Vector3 direction = player_pos - transform.position;
        direction.Normalize();
        gameObject.GetComponent<Rigidbody2D>().velocity = direction * 10;
        StartCoroutine(Stop());
    }

    IEnumerator Stop()
    {
        yield return new WaitForSeconds(8);
        GetComponent<AIPath>().enabled = true;
        GetComponent<Seeker>().enabled = true;
        GetComponent<AIDestinationSetter>().enabled = true;
        rushTimer = Random.Range(5, 10);
        Vector3 s = new Vector3(0, 0, 0);
        GetComponent<Rigidbody2D>().velocity = s;
        rush = false;
        anim.SetTrigger("Move");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (blobSize == 5 && collision.gameObject.tag != "Player")
        {
            SpawnManyBullets();
        }
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


    void SpawnBullets()
    {
        GameObject b1 = Instantiate(Bullet, transform.position, Quaternion.identity);
        GameObject b2 = Instantiate(Bullet, transform.position, Quaternion.identity);
        GameObject b3 = Instantiate(Bullet, transform.position, Quaternion.identity);
        GameObject b4 = Instantiate(Bullet, transform.position, Quaternion.identity);
        b1.GetComponent<EnemyProjectile>().damage = stats.p_damage;
        b2.GetComponent<EnemyProjectile>().damage = stats.p_damage;
        b3.GetComponent<EnemyProjectile>().damage = stats.p_damage;
        b4.GetComponent<EnemyProjectile>().damage = stats.p_damage;
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

    void BulletExplosion(int numberOfBullets)
    {
        Vector3 v3 = new Vector3();
        for (int i = 0; i < numberOfBullets; i++)
        {
            GameObject b1 = Instantiate(Bullet, transform.position, Quaternion.identity);
            b1.GetComponent<EnemyProjectile>().damage = stats.p_damage;
            v3.x += (Random.Range(-10f, 11f) / 10) * 4;
            v3.y += (Random.Range(-10f, 11f) / 10) * 4;
            v3.Normalize();
            b1.GetComponent<Rigidbody2D>().velocity = v3 * 4;
        }
    }

    void MakeBlobs(int numOfBlobs)
    {
        Vector3 v = new Vector3();
        for (int i = 0; i < numOfBlobs; i++)
        {
            v.x = transform.position.x + ((Random.Range(-0.500f, 0.501f)));
            v.y = transform.position.y + ((Random.Range(-0.500f, 0.501f)));
            GameObject blob = Instantiate(LesserBlob, v, Quaternion.identity);
            blob.transform.SetParent(parent.transform);
            blob.GetComponent<AIPath>().enabled = true;
            blob.GetComponent<Seeker>().enabled = true;
            blob.GetComponent<AIDestinationSetter>().enabled = true;
            blob.GetComponent<AIPath>().maxSpeed = blob.GetComponent<MonsterStats>().speed;

        }
        Destroy(gameObject);

    }

    IEnumerator Make3Blobs(float t)
    {
        yield return new WaitForSeconds(t);
        Vector3 v = new Vector3();
        v.x = transform.position.x + 2.5f;
        v.y = transform.position.y - 0.25f;
        GameObject blob = Instantiate(LesserBlob, v, Quaternion.identity);
        blob.transform.SetParent(parent.transform);
        blob.GetComponent<AIPath>().enabled = true;
        blob.GetComponent<Seeker>().enabled = true;
        blob.GetComponent<AIDestinationSetter>().enabled = true;
        blob.GetComponent<AIPath>().maxSpeed = blob.GetComponent<MonsterStats>().speed;

        v.x = transform.position.x - 2.5f;
        v.y = transform.position.y - 0.25f;
        blob = Instantiate(LesserBlob, v, Quaternion.identity);
        blob.transform.SetParent(parent.transform);
        blob.GetComponent<AIPath>().enabled = true;
        blob.GetComponent<Seeker>().enabled = true;
        blob.GetComponent<AIDestinationSetter>().enabled = true;
        blob.GetComponent<AIPath>().maxSpeed = blob.GetComponent<MonsterStats>().speed;

        v.x = transform.position.x;
        v.y = transform.position.y - 1;
        blob = Instantiate(LesserBlob, v, Quaternion.identity);
        blob.transform.SetParent(parent.transform);
        blob.GetComponent<AIPath>().enabled = true;
        blob.GetComponent<Seeker>().enabled = true;
        blob.GetComponent<AIDestinationSetter>().enabled = true;
        blob.GetComponent<AIPath>().maxSpeed = blob.GetComponent<MonsterStats>().speed;
    }

}