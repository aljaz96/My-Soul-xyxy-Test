using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss4 : MonoBehaviour {

    public GameObject bullet;
    public GameObject bulletBomb;
    public GameObject bulletSkull;
    public GameObject bulletSprinkler;
    public GameObject laser;
    public GameObject BHand1;
    public GameObject BHand2;
    public GameObject MHand1;
    public GameObject MHand2;
    public GameObject THand1;
    public GameObject THand2;
    public GameObject Eye;
    GameObject player;
    public float actionTimer = 2;
    public float animationTimer;
    public int atack;
    public int atackPhase;
    MonsterStats stats;

    GameObject spawnPointNW;
    GameObject spawnPointNE;
    GameObject spawnPointSW;
    GameObject spawnPointSE;
    GameObject spawnPointN;
    GameObject spawnPointE;
    GameObject spawnPointS;
    GameObject spawnPointW;
    Hand BHand1_script;
    Hand BHand2_script;
    Hand MHand1_script;
    Hand MHand2_script;
    Hand THand1_script;
    Hand THand2_script;
    Animator anim;


    void Start()
    {
        spawnPointNW = GameObject.Find("spawnPointNorthWest");
        spawnPointNE = GameObject.Find("spawnPointNorthEast");
        spawnPointSW = GameObject.Find("spawnPointSouthWest");
        spawnPointSE = GameObject.Find("spawnPointSouthEast");
        spawnPointN = GameObject.Find("spawnPointNorth");
        spawnPointE = GameObject.Find("spawnPointEast");
        spawnPointS = GameObject.Find("spawnPointSouth");
        spawnPointW = GameObject.Find("spawnPointWest");
        stats = GetComponent<MonsterStats>();
        player = GameObject.FindGameObjectWithTag("Player");
        BHand1_script = BHand1.GetComponent<Hand>();
        BHand2_script = BHand2.GetComponent<Hand>();
        MHand1_script = MHand1.GetComponent<Hand>();
        MHand2_script = MHand2.GetComponent<Hand>();
        THand1_script = THand1.GetComponent<Hand>();
        THand2_script = THand2.GetComponent<Hand>();
        anim = Eye.GetComponent<Animator>();
        animationTimer = Random.Range(2.00f, 8.00f);
    }

    //big head, 1 eye, big mouth, 4 arms
    void Update()
    {
        if (stats.active)
        {
            animationTimer -= Time.deltaTime;
            actionTimer -= Time.deltaTime;
            //do stuff
            while (actionTimer < 0)
            {
                atack = Random.Range(1, 8);
                //bulletSprinkler OutOfBounds
                if (atack == 1)
                {
                    StartCoroutine(Sprinkler(0));
                    StartCoroutine(Sprinkler(1));
                    THand2_script.ColorHand(1.5f);
                    actionTimer = 4;
                }
                //spread shot
                if (atack == 2)
                {
                    StartCoroutine(MultiShots(0));
                    StartCoroutine(MultiShots(0.5f));
                    StartCoroutine(MultiShots(1));
                    StartCoroutine(MultiShots(1.5f));
                    StartCoroutine(MultiShots(2));
                    MHand1_script.ColorHand(2);
                    actionTimer = 3.5f;
                }
                //skull shot/spawn
                if (atack == 3)
                {
                    StartCoroutine(SkullShots(0.5f));
                    MHand2_script.ColorHand(1);
                    actionTimer = 2f;
                }
                //lasers
                if (atack == 4)
                {
                    StartCoroutine(Laserbeam(0));
                    StartCoroutine(Laserbeam(0.66f));
                    StartCoroutine(Laserbeam(1.33f));
                    StartCoroutine(Laserbeam(2));
                    BHand1_script.ColorHand(2.5f);
                    BHand2_script.ColorHand(2.5f);
                    anim.SetTrigger("Laser");
                    animationTimer = Random.Range(4.00f, 8.00f);
                    actionTimer = 4.5f;
                }
                //side shot x4
                if (atack == 5)
                {
                    StartCoroutine(SideShotPrep(0));
                    StartCoroutine(SideShotPrep(0.66f));
                    StartCoroutine(SideShotPrep(1.33f));
                    THand1_script.ColorHand(2.5f);
                    actionTimer = 4;
                }
                //increasingBulletsInARow
                if (atack == 6)
                {
                    for (float f = 0.0f; f < 2.5f; f += 0.1f)
                    {
                        StartCoroutine(IncreasingBullets(f, f));
                    }
                    actionTimer = 4;
                    BHand2_script.ColorHand(3f);
                }
                //FallingBullets
                if (atack == 7)
                {
                    StartCoroutine(BulletFallPrep(0.0f));
                    StartCoroutine(BulletFallPrep(3f));
                    StartCoroutine(BulletFallPrep(6f));
                    StartCoroutine(BulletFallPrep(9f));
                    BHand1_script.ColorHand(11);
                    BHand2_script.ColorHand(11);
                    MHand1_script.ColorHand(11);
                    MHand2_script.ColorHand(11);
                    THand1_script.ColorHand(11);
                    THand2_script.ColorHand(11);
                    actionTimer = 14;
                }
            }
            if (animationTimer < 0)
            {
                int a = Random.Range(1, 3);
                if(a == 1)
                {
                    anim.SetTrigger("Look");
                }
                else
                {
                    anim.SetTrigger("Look2");
                }
                a = Random.Range(1, 3);
                if(a == 1)
                {
                    Eye.GetComponent<SpriteRenderer>().flipX = !Eye.GetComponent<SpriteRenderer>().flipX;
                }
                animationTimer = Random.Range(2.00f, 8.00f);
            }
        }
    }

    GameObject FindStartPos(int i)
    {
        switch (i)
        {
            case 1:
                return spawnPointNW;
            case 2:
                return spawnPointNE;
            case 3:
                return spawnPointSW;
            case 4:
                return spawnPointSE;
            case 5:
                return spawnPointW;
            case 6:
                return spawnPointS;
            case 7:
                return spawnPointE;
            case 8:
                return spawnPointN;                
        }
        return null;
    }

    IEnumerator IncreasingBullets(float f, float s)
    {
        yield return new WaitForSeconds(f);
        GameObject b1 = Instantiate(bullet, Eye.transform.position, Quaternion.identity);
        b1.transform.localScale += new Vector3(s / 10, s / 10, 0);
        Vector3 direction = player.transform.position - transform.position;
        direction.Normalize();
        b1.GetComponent<Rigidbody2D>().velocity = direction * 7;
    }


    IEnumerator Sprinkler(float f)
    {
        yield return new WaitForSeconds(f);
        GameObject g = FindStartPos(Random.Range(1, 9));
        GameObject b1 = Instantiate(bulletSprinkler, g.transform.position, Quaternion.identity);
        Vector3 direction = player.transform.position - g.transform.position;
        direction.x = direction.x * Random.Range(0.90f, 1.11f);
        direction.y = direction.y * Random.Range(0.90f, 1.11f);
        direction.Normalize();
        b1.GetComponent<Rigidbody2D>().velocity = direction * 2;
    }

    IEnumerator MultiShots(float f)
    {

        yield return new WaitForSeconds(f);
        float r = Random.Range(-0.40f, 0.40f);
        for (int i = 0; i < 19; i++)
        {
            Vector3 v3 = new Vector3();
            GameObject b1 = Instantiate(bullet, Eye.transform.position, Quaternion.identity);
            switch (i)
            {
                case 0:
                    v3 = new Vector3(0 + r, -2, 0);
                    break;
                case 1:
                    v3 = new Vector3(0.75f + r, -2, 0);
                    break;
                case 2:
                    v3 = new Vector3(1.5f + r, -2, 0);
                    break;
                case 3:
                    v3 = new Vector3(2.25f + r, -2, 0);
                    break;
                case 4:
                    v3 = new Vector3(3f + r, -2, 0);
                    break;
                case 5:
                    v3 = new Vector3(3.75f + r, -2, 0);
                    break;
                case 6:
                    v3 = new Vector3(4.5f + r, -2, 0);
                    break;
                case 7:
                    v3 = new Vector3(5.25f + r, -2, 0);
                    break;
                case 8:
                    v3 = new Vector3(-0.75f + r, -2, 0);
                    break;
                case 9:
                    v3 = new Vector3(-1.5f + r, -2, 0);
                    break;
                case 10:
                    v3 = new Vector3(-2.25f + r, -2, 0);
                    break;
                case 11:
                    v3 = new Vector3(-3f + r, -2, 0);
                    break;
                case 12:
                    v3 = new Vector3(-3.75f + r, -2, 0);
                    break;
                case 13:
                    v3 = new Vector3(-4.5f + r, -2, 0);
                    break;
                case 14:
                    v3 = new Vector3(-5.25f + r, -2, 0);
                    break;
                case 15:
                    v3 = new Vector3(-6f + r, -2, 0);
                    break;
                case 16:
                    v3 = new Vector3(-6.75f + r, -2, 0);
                    break;
                case 17:
                    v3 = new Vector3(6f + r, -2, 0);
                    break;
                case 18:
                    v3 = new Vector3(6.75f + r, -2, 0);
                    break;
            }
            v3.Normalize();
            b1.GetComponent<Rigidbody2D>().velocity = v3 * 5;
        }
    }

    IEnumerator Laserbeam(float f)
    {
        yield return new WaitForSeconds(f);
        Vector3 direction = player.transform.position - transform.position;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        var rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        GameObject b1 = Instantiate(laser, Eye.transform.position, rotation);
        b1.GetComponent<EnemyProjectile>().type = 6;
        b1.GetComponent<EnemyProjectile>().owner = transform.gameObject;
        b1.transform.SetParent(transform.parent);
    }

    IEnumerator SideShotPrep(float f)
    {
        yield return new WaitForSeconds(f);
        int r = Random.Range(1, 5);
        Vector3 desision = new Vector3();
        switch (r)
        {
            case 1:
                desision = spawnPointNW.transform.position;
                break;
            case 2:
                desision = spawnPointNE.transform.position;
                break;
            case 3:
                desision = spawnPointSW.transform.position;
                break;
            case 4:
                desision = spawnPointSE.transform.position;
                break;
        }
        for (float i = 0; i < 2; i += 0.1f)
        {
            StartCoroutine(SideShot(i, desision));
        }
    }

    IEnumerator BulletFallPrep(float f)
    {
        yield return new WaitForSeconds(f);
        int r = Random.Range(1, 5);
        int x = 0;
        int y = 0;
        switch (r)
        {
            case 1:
                y = -1;
                break;
            case 2:
                x = -1;
                break;
            case 3:
                y = 1;
                break;
            case 4:
                x = 1;
                break;
        }
        for (float i = 0; i < 2.5f; i += 0.03f)
        {
            if (y == -1)
            {
                Vector3 v = spawnPointN.transform.position;
                v.x += Random.Range(-8.00f, 8.00f);
                v.y += 15f;
                StartCoroutine(BulletFall(i, v, 0, y));
            }
            else if (x == -1)
            {
                Vector3 v = spawnPointE.transform.position;
                v.y += Random.Range(-8.00f, 8.00f);
                v.x += 15f;
                StartCoroutine(BulletFall(i, v, x, 0));
            }
            else if (y == 1)
            {
                Vector3 v = spawnPointS.transform.position;
                v.x += Random.Range(-8.00f, 8.00f);
                v.y -= 15f;
                StartCoroutine(BulletFall(i, v, 0, y));
               
            }
            else if (x == 1)
            {
                Vector3 v = spawnPointW.transform.position;
                v.y += Random.Range(-8.00f, 8.00f);
                v.x -= 15f;
                StartCoroutine(BulletFall(i, v, x, 0));
            }
        }
    }

    IEnumerator BulletFall(float f, Vector3 pos, int x, int y)
    {
        yield return new WaitForSeconds(f);
        Vector3 v3 = new Vector3(x, y, 0);
        GameObject b1 = Instantiate(bullet, pos, Quaternion.identity);
       // Vector3 direction = player.transform.position - pos;
       // direction.x = direction.x * (Random.Range(0.80f, 1.21f));
       // direction.y = direction.y * (Random.Range(0.80f, 1.21f));
       // direction.Normalize();
        b1.GetComponent<Rigidbody2D>().velocity = v3 * 6;
    }

    IEnumerator SideShot(float f, Vector3 pos)
    {
        yield return new WaitForSeconds(f);
        GameObject b1 = Instantiate(bullet, pos, Quaternion.identity);
        Vector3 direction = player.transform.position - pos;
        direction.x = direction.x * (Random.Range(0.80f, 1.21f));
        direction.y = direction.y * (Random.Range(0.80f, 1.21f));
        direction.Normalize();
        b1.GetComponent<Rigidbody2D>().velocity = direction * 5;
    }

    IEnumerator SkullShots(float f)
    {
        yield return new WaitForSeconds(f);
        GameObject b1 = Instantiate(bulletSkull, Eye.transform.position, Quaternion.identity);
        Vector3 direction = player.transform.position - transform.position;
        direction.Normalize();
        b1.GetComponent<Rigidbody2D>().velocity = direction * 8f;
        b1.GetComponent<EnemyProjectile>().owner = gameObject;
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
        Vector3 direction = player.transform.position - transform.position;
        direction.x = direction.x * (Random.Range(0.9f, 1.1f));
        direction.y = direction.y * (Random.Range(0.9f, 1.1f));
        direction.Normalize();
        b1.GetComponent<Rigidbody2D>().velocity = direction * 1f;
    }
}
