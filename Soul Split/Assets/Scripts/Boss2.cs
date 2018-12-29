using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2 : MonoBehaviour {

    GameObject player;
    MonsterStats stats;
    public GameObject boss2;
    public GameObject bullet;
    public float atackTimer = 0;
    public float actionTimer = 1;
    Vector3 originalPosition;
    public int atack;
    public int atackPhase;
    int bulletNumber;
    float bulletTimer = 0.2f;
    float x = 1;
    float y = 1;
    public int movementLimit = 18;
    float f;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        stats = GetComponent<MonsterStats>();
    }

    void Update()
    {
        atackTimer -= Time.deltaTime;
        actionTimer -= Time.deltaTime;
        bulletTimer -= Time.deltaTime;
        if (stats.active)
        {
            //do stuff
            if(boss2 == null && actionTimer < 0)
            {
                for (float f = 0.0f; f < 3.0f; f += 0.1f)
                {
                    StartCoroutine(AtackThree(f));
                }
                actionTimer = 3;
            }
            else if (actionTimer < 0)
            {
                f = Random.Range(-0.10f, 0.10f);
                atack = Random.Range(1, 4);
                if (atack == 1)
                {
                    actionTimer = 5;
                    atackTimer = 4;
                    atackPhase = 1;
                }
                else if (atack == 2)
                {
                    actionTimer = 7;
                    atackTimer = 6;
                    atackPhase = 1;
                    bulletNumber = Random.Range(0,16);
                    x = 1;
                    y = 1;
                }
                else if (atack == 3)
                {
                    for(float f=0.0f; f<3.0f; f += 0.1f)
                    {
                        StartCoroutine(AtackThree(f));
                    }
                    actionTimer = 5;
                    atackTimer = 0;
                    atackPhase = 0;
                }
            }
            if (atack == 1)
            {
                AtackOne();
            }
            if (atack == 2)
            {
                AtackTwo();
            }
        }
    }

    IEnumerator AtackThree(float t)
    {
        yield return new WaitForSeconds(t);
        GameObject b1 = Instantiate(bullet, transform.position, Quaternion.identity);
        b1.GetComponent<EnemyProjectile>().damage = stats.p_damage;
        Vector3 v3 = new Vector3(Random.Range(-1.00f, 1.01f), Random.Range(-1.00f, 1.01f), 0);
        v3.Normalize();
        b1.GetComponent<Rigidbody2D>().velocity = v3 * 3;
    }

    void AtackOne()
    {
        if ((atackPhase == 1 && atackTimer < 4) || (atackPhase == 3 && atackTimer < 3))
        {
           
            for (int i = 0; i < 4; i++)
            {
                GameObject b1 = Instantiate(bullet, transform.position, Quaternion.identity);
                b1.GetComponent<EnemyProjectile>().damage = stats.p_damage;
                Vector3 v3 = new Vector3();
                switch (i)
                {
                    case 0:
                        v3.x = 0 + f;
                        v3.y = 1;
                        break;
                    case 1:
                        v3.x = 1 + f;
                        v3.y = 0;
                        break;
                    case 2:
                        v3.x = 0 + f;
                        v3.y = -1;
                        break;
                    case 3:
                        v3.x = -1 + f;
                        v3.y = 0;
                        break;
                }
                b1.GetComponent<Rigidbody2D>().velocity = v3 * 6;
            }
            atackPhase++;
        }
        if ((atackPhase == 2 && atackTimer < 3.5f) || (atackPhase == 4 && atackTimer < 2.5f))
        {
            for (int i = 0; i < 4; i++)
            {
                GameObject b1 = Instantiate(bullet, transform.position, Quaternion.identity);
                b1.GetComponent<EnemyProjectile>().damage = stats.p_damage;
                Vector3 v3 = new Vector3();
                switch (i)
                {
                    case 0:
                        v3.x = 1 + f;
                        v3.y = 1;
                        break;
                    case 1:
                        v3.x = 1 + f;
                        v3.y = -1;
                        break;
                    case 2:
                        v3.x = -1 + f;
                        v3.y = -1;
                        break;
                    case 3:
                        v3.x = -1 + f;
                        v3.y = 1;
                        break;
                }
                v3.Normalize();
                b1.GetComponent<Rigidbody2D>().velocity = v3 * 6;
            }
            atackPhase++;
        }
        if (atackPhase == 5 && atackTimer < 2f)
        {
            for (int i = 0; i < 8; i++)
            {
                GameObject b1 = Instantiate(bullet, transform.position, Quaternion.identity);
                b1.GetComponent<EnemyProjectile>().damage = stats.p_damage;
                Vector3 v3 = new Vector3();
                switch (i)
                {
                    case 0:
                        v3.x = 0 + f;
                        v3.y = 1;
                        break;
                    case 1:
                        v3.x = 1 + f;
                        v3.y = 1;
                        break;
                    case 2:
                        v3.x = 1 + f;
                        v3.y = 0;
                        break;
                    case 3:
                        v3.x = 1 + f;
                        v3.y = -1;
                        break;
                    case 4:
                        v3.x = 0 + f;
                        v3.y = -1;
                        break;
                    case 5:
                        v3.x = -1 + f;
                        v3.y = -1;
                        break;
                    case 6:
                        v3.x = -1 + f;
                        v3.y = 0;
                        break;
                    case 7:
                        v3.x = -1 + f;
                        v3.y = 1;
                        break;
                }
                v3.Normalize();
                b1.GetComponent<Rigidbody2D>().velocity = v3 * 6;
            }
            atackPhase++;
            atack = 0;
        }
    }

    void AtackTwo()
    {
        if (bulletTimer < 0)
        {
            GameObject b1 = Instantiate(bullet, transform.position, Quaternion.identity);
            b1.GetComponent<EnemyProjectile>().damage = stats.p_damage;
            Vector3 v3 = new Vector3();
            switch (bulletNumber)
            {
                case 0:
                    v3.x = 0;
                    v3.y = y;
                    break;
                case 1:
                    v3.x = x / 2;
                    v3.y = y;
                    break;
                case 2:
                    v3.x = x;
                    v3.y = y;
                    break;
                case 3:
                    v3.x = x;
                    v3.y = y / 2;
                    break;
                case 4:
                    v3.x = x;
                    v3.y = 0;
                    break;
                case 5:
                    v3.x = x;
                    v3.y = -y / 2;
                    break;
                case 6:
                    v3.x = x;
                    v3.y = -y;
                    break;
                case 7:
                    v3.x = x / 2;
                    v3.y = -y;
                    break;
                case 8:
                    v3.x = 0;
                    v3.y = -y;
                    break;
                case 9:
                    v3.x = -x / 2;
                    v3.y = -y;
                    break;
                case 10:
                    v3.x = -x;
                    v3.y = -y;
                    break;
                case 11:
                    v3.x = -x;
                    v3.y = -y / 2;
                    break;
                case 12:
                    v3.x = -x;
                    v3.y = 0;
                    break;
                case 13:
                    v3.x = -x;
                    v3.y = y / 2;
                    break;
                case 14:
                    v3.x = -x;
                    v3.y = y;
                    break;
                case 15:
                    v3.x = -x / 2;
                    v3.y = y;
                    break;
            }
            v3.x += f;
            v3.Normalize();
            b1.GetComponent<Rigidbody2D>().velocity = v3 * 4;
            bulletTimer = 0.1f;
            bulletNumber++;
            if (bulletNumber == 16)
            {
                bulletNumber = 0;
                atackPhase++;
                if (atackPhase == 2)
                {
                    x = 0.9f;
                }
                else if (atackPhase == 3)
                {
                    x = 0.8f;
                }
                else if (atackPhase == 4)
                {
                    atack = 0;
                }
            }
        }
    }
}
