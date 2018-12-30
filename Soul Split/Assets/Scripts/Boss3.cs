using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss3 : MonoBehaviour {

    public GameObject bullet;
    public GameObject bulletBomb;
    public GameObject bulletSprinkler;
    GameObject player;
    public float atackTimer = 0;
    public float actionTimer = 2;
    Vector3 originalPosition;
    public int atack;
    public int atackPhase;
    float bulletTimer = 0.2f;
    int currentPos = 1;
    bool defeated = false;
    float ShotDelay;
    Vector3 playerPos;
    MonsterStats stats;
    bool hasTeleported = false;


    void Start()
    {
        originalPosition = transform.position;
        stats = GetComponent<MonsterStats>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {   
        if (stats.active && !defeated)
        {
            atackTimer -= Time.deltaTime;
            actionTimer -= Time.deltaTime;
            bulletTimer -= Time.deltaTime;
            //do stuff
            CheckIfOutOfBounds();
            if (actionTimer < 0)
            {
                atack = Random.Range(1, 6);
                if (atack == 1)
                {
                    int p = Random.Range(1, 4);
                    do
                    {
                        p = Random.Range(1, 4);
                    } while (p == currentPos);
                    currentPos = p;
                    actionTimer = 2f;
                    hasTeleported = false; 
                }
                if (atack == 2)
                {
                    actionTimer = 2;
                    atackPhase = 1;
                    playerPos = player.transform.position;
                    StartCoroutine(SnipeShots(0.1f, playerPos));
                }
                if (atack == 3)
                {
                    actionTimer = 4;
                    for (float f = 0; f < 2.8f; f += 0.7f)
                    {
                        StartCoroutine(MultiShots(f));
                    }
                }
                if (atack == 4)
                {
                    actionTimer = 1.5f;
                    StartCoroutine(BombShot(0.1f));
                }
                if (atack == 5)
                {
                    actionTimer = 2;
                    StartCoroutine(BulletSprinkler(0.0f));
                }
            }
            if (atack == 1)
            {
                Teleport(currentPos);
            }
        } 
    }

    void Teleport(int pos)
    {
        if (transform.localScale.x > 0 && !hasTeleported)
        {
            transform.localScale -= new Vector3(0.005f, 0, 0);
        }
        else if (transform.localScale.x <= 0 && !hasTeleported)
        {
            GameObject newPosition = GameObject.Find("Boss3P" + pos);
            transform.position = newPosition.transform.position;
            hasTeleported = true;
        }
        else if (transform.localScale.x <= 0.1f && hasTeleported)
        {
            transform.localScale += new Vector3(0.005f, 0, 0);
        }
        else if(transform.localScale.x >= 0.1f && hasTeleported)
        {
            atack = 0;
        }
    }

    void CheckIfOutOfBounds()
    {
        if(currentPos == 1 && player.transform.position.y > transform.position.y && actionTimer < 0)
        {
            actionTimer = 0.5f;
            atackPhase = 1;
            playerPos = player.transform.position;
            StartCoroutine(SnipeShots(0.0f, playerPos));
        }
        if (currentPos == 2 && player.transform.position.x > transform.position.x && actionTimer < 0)
        {
            actionTimer = 0.5f;
            atackPhase = 1;
            playerPos = player.transform.position;
            StartCoroutine(SnipeShots(0.0f, playerPos));
        }
        if (currentPos == 3 && player.transform.position.x < transform.position.x && actionTimer < 0)
        {
            actionTimer = 0.5f;
            atackPhase = 1;
            playerPos = player.transform.position;
            StartCoroutine(SnipeShots(0.0f, playerPos));
        }
    }

    IEnumerator MultiShots(float f)
    {

        yield return new WaitForSeconds(f);
        float r = Random.Range(-0.40f, 1.80f);
        switch (currentPos) {
            case 1:
                for (int i = 0; i < 9; i++)
                {
                    Vector3 v3 = new Vector3();
                    GameObject b1 = Instantiate(bullet, transform.position, Quaternion.identity);
                    switch (i)
                    {
                        case 0:
                            v3 = new Vector3(0,-2 + r, 0);
                            break;
                        case 1:
                            v3 = new Vector3(0.75f , -2 + r, 0);
                            break;
                        case 2:
                            v3 = new Vector3(1.5f, -2 + r, 0);
                            break;
                        case 3:
                            v3 = new Vector3(2.25f, -2 + r, 0);
                            break;
                        case 4:
                            v3 = new Vector3(3f, -2 + r, 0);
                            break;
                        case 5:
                            v3 = new Vector3(-0.75f, -2 + r, 0);
                            break;
                        case 6:
                            v3 = new Vector3(-1.5f, -2 + r, 0);
                            break;
                        case 7:
                            v3 = new Vector3(-2.25f, -2 + r, 0);
                            break;
                        case 8:
                            v3 = new Vector3(-3f, -2 + r, 0);
                            break;
                    }
                    v3.Normalize();
                    b1.GetComponent<Rigidbody2D>().velocity = v3 * 6;
                }
                break;
            case 2:
                for (int i = 0; i < 9; i++)
                {
                    Vector3 v3 = new Vector3();
                    GameObject b1 = Instantiate(bullet, transform.position, Quaternion.identity);
                    switch (i)
                    {
                        case 0:
                            v3 = new Vector3(-2 + r, 0, 0);
                            break;
                        case 1:
                            v3 = new Vector3(-2 + r, 0.75f, 0);
                            break;
                        case 2:
                            v3 = new Vector3(-2 + r, 1.5f, 0);
                            break;
                        case 3:
                            v3 = new Vector3(-2 + r, -0.75f, 0);
                            break;
                        case 4:
                            v3 = new Vector3(-2 + r, -1.5f, 0);
                            break;
                        case 5:
                            v3 = new Vector3(-2 + r, -2.25f, 0);
                            break;
                        case 6:
                            v3 = new Vector3(-2 + r, -3f, 0);
                            break;
                        case 7:
                            v3 = new Vector3(-2 + r, 2.25f, 0);
                            break;
                        case 8:
                            v3 = new Vector3(-2 + r, 3f, 0);
                            break;
                    }
                    v3.Normalize();
                    b1.GetComponent<Rigidbody2D>().velocity = v3 * 6;
                }
                break;
            case 3:
                for (int i = 0; i < 9; i++)
                {
                    Vector3 v3 = new Vector3();
                    GameObject b1 = Instantiate(bullet, transform.position, Quaternion.identity);
                    switch (i)
                    {
                        case 0:
                            v3 = new Vector3(2 + r, 0, 0);
                            break;
                        case 1:
                            v3 = new Vector3(2 + r, 0.75f, 0);
                            break;
                        case 2:
                            v3 = new Vector3(2 + r, 1.5f, 0);
                            break;
                        case 3:
                            v3 = new Vector3(2 + r, -0.75f, 0);
                            break;
                        case 4:
                            v3 = new Vector3(2 + r, -1.5f, 0);
                            break;
                        case 5:
                            v3 = new Vector3(2 + r, -2.25f, 0);
                            break;
                        case 6:
                            v3 = new Vector3(2 + r, -3f, 0);
                            break;
                        case 7:
                            v3 = new Vector3(2 + r, 2.25f, 0);
                            break;
                        case 8:
                            v3 = new Vector3(2 + r, 3f, 0);
                            break;
                    }
                    v3.Normalize();
                    b1.GetComponent<Rigidbody2D>().velocity = v3 * 6;
                }
                break;
        }
    }

    IEnumerator SnipeShots(float f, Vector3 pos)
    {
        yield return new WaitForSeconds(f);
        GameObject b1 = Instantiate(bullet, transform.position, Quaternion.identity);
        Vector3 direction = playerPos - transform.position;
        direction.Normalize();
        b1.GetComponent<Rigidbody2D>().velocity = direction * 12f;
        atackPhase++;
        if (atackPhase != 7)
        {
            playerPos = player.transform.position;
            StartCoroutine(SnipeShots(0.1f, playerPos));
        }
        else
        {
            atackPhase = 0;
            atack = 0;
        }
    
        
    }

    IEnumerator BombShot(float f)
    {
        yield return new WaitForSeconds(f);
        GameObject b1 = Instantiate(bulletBomb, transform.position, Quaternion.identity);
        Vector3 direction = player.transform.position - transform.position;
        direction.x = direction.x * (Random.Range(0.9f, 1.1f));
        direction.y = direction.y * (Random.Range(0.9f, 1.1f));
        direction.Normalize();
        b1.GetComponent<Rigidbody2D>().velocity = direction * 6f;
    }

    IEnumerator BulletSprinkler(float f)
    {
        yield return new WaitForSeconds(f);
        GameObject b1 = Instantiate(bulletSprinkler, transform.position, Quaternion.identity);
        Vector3 direction = playerPos - transform.position;
        direction.x = direction.x * (Random.Range(0.9f, 1.1f));
        direction.y = direction.y * (Random.Range(0.9f, 1.1f));
        direction.Normalize();
        b1.GetComponent<Rigidbody2D>().velocity = direction * 1f;
    }
}
