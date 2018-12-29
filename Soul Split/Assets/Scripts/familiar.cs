using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class familiar : MonoBehaviour
{

    // Use this for initialization
    //public Transform center;
    //public Vector3 axis;
    //public float radius = 2.0f;
    //public float radiusSpeed = 0.5f;
    //public float rotationSpeed = 80.0f;
    float rotateSpeed = 2f;
    float radius = 0.5f;
    public GameObject parent;
    public GameObject normalBullet;
    public GameObject bombBullet;
    public GameObject sprinklerBullet;
    public GameObject rocketBullet;
    public GameObject laser;
    private Vector2 center;
    private float angle;
    public float numberOfBullets = 20;
    public bool isColiding = false;
    public bool backShot = false;
    float timer = 0.02f;

    void Start()
    {
        // axis = Vector3.up;
        // _centre = parent.transform.position;
        // transform.position = (transform.position - center.position).normalized * radius + center.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isColiding)
        {
            center = parent.transform.position;
            angle += rotateSpeed * Time.deltaTime;
            var offset = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)) * radius;
            transform.position = center + offset;
            //transform.RotateAround(center.position, axis, rotationSpeed * Time.deltaTime);
            //var desiredPosition = (transform.position - center.position).normalized * radius + center.position;
            //transform.position = Vector3.MoveTowards(transform.position, desiredPosition, Time.deltaTime * radiusSpeed);
        }
        timer -= Time.deltaTime;
        if (Input.GetMouseButton(1) && timer < 0)
        {
            if (CharacterStats.bulletType == 1 && CharacterStats.energy >= 1)
            {
                NormalBullets();
                CharacterStats.energy -= 1;
            }
            else if (CharacterStats.bulletType == 2 && CharacterStats.energy >= 10)
            {
                ShotgunBullets();
                CharacterStats.energy -= 10;
            }
            else if (CharacterStats.bulletType == 3 && CharacterStats.energy >= 33)
            {
                BombBullet();
                CharacterStats.energy -= 33;
            }
            else if (CharacterStats.bulletType == 4 && CharacterStats.energy >= 40)
            {
                SprinklerBullet();
                CharacterStats.energy -= 40;
            }
            else if (CharacterStats.bulletType == 5 && CharacterStats.energy >= 15)
            {
                ChargeBullet();
                CharacterStats.energy -= 15;
            }
            else if (CharacterStats.bulletType == 6 && CharacterStats.energy >= 60)
            {
                Laser();
                CharacterStats.energy -= 60;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Wall")
        {
            isColiding = true;
            col.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Wall")
        {
            isColiding = false;
            col.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        }
    }

    void NormalBullets()
    {
        Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = pz - transform.position;
        direction.Normalize();
        GameObject b1 = Instantiate(normalBullet, transform.position, Quaternion.identity);
        b1.GetComponent<projectileScript>().SetType(1);
        b1.GetComponent<Rigidbody2D>().velocity = direction * CharacterStats.bulletSpeed;
        if (backShot)
        {
            GameObject b2 = Instantiate(normalBullet, transform.position, Quaternion.identity);
            b2.GetComponent<projectileScript>().SetType(1);
            b2.GetComponent<Rigidbody2D>().velocity = direction * - CharacterStats.bulletSpeed;
        }
        timer = (CharacterStats.atackSpeed / 15);
    }

    void BombBullet()
    {
        Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = pz - transform.position;
        direction.Normalize();
        GameObject b1 = Instantiate(bombBullet, transform.position, Quaternion.identity);
        b1.GetComponent<projectileScript>().SetType(3);
        b1.GetComponent<Rigidbody2D>().velocity = direction * CharacterStats.bulletSpeed;
        timer = (CharacterStats.atackSpeed / 15) * 35;
    }

    void SprinklerBullet()
    {
        Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = pz - transform.position;
        direction.Normalize();
        GameObject b1 = Instantiate(sprinklerBullet, transform.position, Quaternion.identity);
        b1.GetComponent<projectileScript>().SetType(4);
        b1.GetComponent<Rigidbody2D>().velocity = direction * 1;
        timer = (CharacterStats.atackSpeed / 15) * 35;
    }

    void ShotgunBullets()
    {
        Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = pz - transform.position;
        direction.Normalize();
        for (int i = 0; i < numberOfBullets; i++)
        {
            Vector2 changedDirection = direction;
            changedDirection.x = changedDirection.x * (Random.Range(0.7f, 1.3f));
            changedDirection.y = changedDirection.y * (Random.Range(0.7f, 1.3f));
            changedDirection.Normalize();
            GameObject b1 = Instantiate(normalBullet, transform.position, Quaternion.identity);
            b1.GetComponent<projectileScript>().SetType(1);
            b1.GetComponent<Rigidbody2D>().velocity = changedDirection * (float)(CharacterStats.bulletSpeed * (Random.Range(0.5f, 0.7f)));
            if (backShot)
            {
                GameObject b2 = Instantiate(normalBullet, transform.position, Quaternion.identity);
                b2.GetComponent<projectileScript>().SetType(1);
                b2.GetComponent<Rigidbody2D>().velocity = changedDirection * (float)(-CharacterStats.bulletSpeed * (Random.Range(0.5f, 0.7f)));
            }
        }
        timer = (CharacterStats.atackSpeed) / 15 * 10;
    }

    void ChargeBullet()
    {
        Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = pz - transform.position;
        direction.Normalize();
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        var relativePos = mousePos - transform.position;
        angle = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg;
        var rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        GameObject b1 = Instantiate(rocketBullet, transform.position, rotation);
        b1.GetComponent<projectileScript>().SetType(5);
        b1.GetComponent<Rigidbody2D>().velocity = (direction * (CharacterStats.bulletSpeed + 5)) * -1;
        b1.GetComponent<projectileScript>().SetStartSpeed(b1.GetComponent<Rigidbody2D>().velocity.x, b1.GetComponent<Rigidbody2D>().velocity.y);
        timer = (CharacterStats.atackSpeed / 15) * 15;
    }

    void Laser()
    {
        Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = pz - transform.position;
        direction.Normalize();
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        var relativePos = mousePos - transform.position;
        angle = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg;
        var rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        GameObject b1 = Instantiate(laser, transform.position, rotation);
        b1.GetComponent<projectileScript>().SetType(6);
        timer = (CharacterStats.atackSpeed / 15) * 50;
    }
}
