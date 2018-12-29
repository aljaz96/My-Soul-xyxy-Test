using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileScript : MonoBehaviour {

    // Use this for initialization
    public float timer = 10;
    public AudioSource audioData;
    public GameObject destroyedEffect;
    public GameObject bullet;
    float angle;
    Vector3 startPos;
    Vector3 endPos;
    float damage;
    public int type;
    float bulletTimer = 0;
    int phase = 0;
    MissileProjectile mp;


    void Start()
    {
        startPos = transform.position;
        audioData = GetComponent<AudioSource>();
        audioData.Play();
        switch (type)
        {
            case 1:
                damage = CharacterStats.damage / 5;
                break;
            case 2:
                damage = CharacterStats.damage / 5;
                break;
            case 3:
                damage = CharacterStats.damage * 4;
                break;
            case 4:
                damage = CharacterStats.damage * 2;
                break;
            case 5:
                damage = CharacterStats.damage * 4;
                break;
            case 6:
                damage = CharacterStats.damage * 0;
                break;
            case 7:
                damage = CharacterStats.damage / 2;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        bulletTimer -= Time.deltaTime;
        if (timer < 0)
        {
            Destroy(gameObject);
        }
        if (type == 4 && bulletTimer <= 0)
        {
            BulletSprinkler(phase);
        }
        if(type == 5)
        {
            Vector3 vel = GetComponent<Rigidbody2D>().velocity;
            vel.x += (mp.StartingX * -1) / 10;
            vel.y += (mp.StartingY * -1) / 10;
            GetComponent<Rigidbody2D>().velocity = vel;
        }
        if(type == 6)
        {
            transform.position = GameObject.FindGameObjectWithTag("Vessel").transform.position;
            if(transform.localScale.y != 2.5f)
            {
                transform.localScale += new Vector3(0, 0.04f, 0);
            }
            if(transform.localScale.y >= 2.5f)
            {
                GameObject b1 = Instantiate(bullet, transform.position, transform.rotation);
                b1.transform.SetParent(transform.parent);
                Destroy(gameObject);
            }
        }
        if(type == 7)
        {
            transform.position = GameObject.FindGameObjectWithTag("Vessel").transform.position;
            if (timer < 8)
            {
                transform.localScale -= new Vector3(0, 0.05f, 0);
            }
            if(transform.localScale.y <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wall")
        {
            if (type == 3)
            {
                BulletBomb(50);
            }
            endPos = transform.position;
            DestroyProjectile();
        }
        else if (collision.tag == "Enemy")
        {
            if (type == 3)
            {
                BulletBomb(50);
            }
            collision.gameObject.GetComponent<MonsterStats>().RecieveDamage(damage);
            DestroyProjectile();
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            other.gameObject.GetComponent<MonsterStats>().RecieveDamage(damage);
           // transform.localScale += new Vector3(0, 0.10f, 0);
        }
    }

    void DestroyProjectile()
    {
        if (type != 7)
        {
            endPos = transform.position;
            var relativePos = startPos - endPos;
            angle = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg;
            var rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            GameObject effect = Instantiate(destroyedEffect, endPos, rotation);
            Destroy(gameObject);
        }
    }

    private void BulletBomb(int numBullets)
    {
        for (int i = 0; i < numBullets; i++)
        {
            GameObject b1 = Instantiate(bullet, transform.position, Quaternion.identity);
            b1.GetComponent<projectileScript>().SetType(1);
            Vector3 v3 = new Vector3();
            v3.x += Random.Range(-4f, 4f);
            v3.y += Random.Range(-4f, 4f);
            v3.Normalize();
            b1.GetComponent<Rigidbody2D>().velocity = v3 * 8;
        }
    }


    private void BulletSprinkler(int p)
    {
        GameObject b1 = Instantiate(bullet, transform.position, Quaternion.identity);
        b1.GetComponent<projectileScript>().SetType(1);
        Vector3 v3 = new Vector3();
        switch (p)
        {
            case 0:
                v3 = new Vector3(2, 0, 0);
                break;
            case 1:
                v3 = new Vector3(2, 0.5f, 0);
                break;
            case 2:
                v3 = new Vector3(2, 1, 0);
                break;
            case 3:
                v3 = new Vector3(2, 1.5f, 0);
                break;
            case 4:
                v3 = new Vector3(2, 2, 0);
                break;
            case 5:
                v3 = new Vector3(1.5f, 2, 0);
                break;
            case 6:
                v3 = new Vector3(1, 2, 0);
                break;
            case 7:
                v3 = new Vector3(0.5f, 2, 0);
                break;
            case 8:
                v3 = new Vector3(0, 2, 0);
                break;
            case 9:
                v3 = new Vector3(-0.5f, 2, 0);
                break;
            case 10:
                v3 = new Vector3(-1f, 2, 0);
                break;
            case 11:
                v3 = new Vector3(-1.5f, 2, 0);
                break;
            case 12:
                v3 = new Vector3(-2, 2, 0);
                break;
            case 13:
                v3 = new Vector3(-2, 1.5f, 0);
                break;
            case 14:
                v3 = new Vector3(-2, 1, 0);
                break;
            case 15:
                v3 = new Vector3(-2, 0.5f, 0);
                break;
            case 16:
                v3 = new Vector3(-2, 0, 0);
                break;
            case 17:
                v3 = new Vector3(-2, -0.5f, 0);
                break;
            case 18:
                v3 = new Vector3(-2, -1, 0);
                break;
            case 19:
                v3 = new Vector3(-2, -1.5f, 0);
                break;
            case 20:
                v3 = new Vector3(-2, -2, 0);
                break;
            case 21:
                v3 = new Vector3(-1.5f, -2, 0);
                break;
            case 22:
                v3 = new Vector3(-1, -2, 0);
                break;
            case 23:
                v3 = new Vector3(-0.5f, -2, 0);
                break;
            case 25:
                v3 = new Vector3(0, -2, 0);
                break;
            case 26:
                v3 = new Vector3(0.5f, -2, 0);
                break;
            case 27:
                v3 = new Vector3(1, -2, 0);
                break;
            case 28:
                v3 = new Vector3(1.5f, -2, 0);
                break;
            case 29:
                v3 = new Vector3(2, -2, 0);
                break;
            case 30:
                v3 = new Vector3(2, -1.5f, 0);
                break;
            case 31:
                v3 = new Vector3(2, -1, 0);
                break;
            case 32:
                v3 = new Vector3(2, -0.5f, 0);
                break;
        }
        phase++;
        v3.Normalize();
        b1.GetComponent<Rigidbody2D>().velocity = v3 * 8;
        if (phase == 33)
        {
            phase = 0;
        }
        bulletTimer = 0.02f;

    }


    public void SetType(int t)
    {
        type = t;
        switch (type)
        {
            case 1:
                damage = CharacterStats.damage / 5;
                break;
            case 2:
                damage = CharacterStats.damage / 5;
                break;
            case 3:
                damage = CharacterStats.damage * 4;
                break;
            case 4:
                damage = CharacterStats.damage * 2;
                break;
        }
    }


    public void SetStartSpeed(float x, float y)
    {
        mp = new MissileProjectile();
        mp.StartingX = x;
        mp.StartingY = y;
        mp.CurrentX = x;
        mp.CurrentY = y;
    }

    class MissileProjectile
    {
        public float StartingX { get; set; }
        public float StartingY { get; set; }
        public float CurrentX { get; set; }
        public float CurrentY { get; set; }
    }
}
