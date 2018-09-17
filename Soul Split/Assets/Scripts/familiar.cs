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
    private float RotateSpeed = 2f;
    private float Radius = 0.5f;
    public GameObject parent;
    public GameObject projectile;
    private Vector2 center;
    private float angle;
    public float projectileSpeed = 5;
    public float numberOfBullets = 5;
    public float atackSpeed = 0.05f;
    public bool isColiding = false;
    public bool backShot = false;
    float timer = 0.02f;

    public int bulletType = 1;        //1 - normal, 2 - shotgun, 3 - special

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
            angle += RotateSpeed * Time.deltaTime;
            var offset = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)) * Radius;
            transform.position = center + offset;
            //transform.RotateAround(center.position, axis, rotationSpeed * Time.deltaTime);
            //var desiredPosition = (transform.position - center.position).normalized * radius + center.position;
            //transform.position = Vector3.MoveTowards(transform.position, desiredPosition, Time.deltaTime * radiusSpeed);
        }
        timer -= Time.deltaTime;
        if (Input.GetMouseButton(1) && timer < 0)
        {
            if (bulletType == 1)
            {
                normal_bullets();
            }
            else if (bulletType == 2)
            {
                shotgun_bullets();
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "OuterWall")
        {
            isColiding = true;
            col.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "OuterWall")
        {
            isColiding = false;
            col.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        }
    }

    void normal_bullets()
    {
        Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = pz - transform.position;
        direction.Normalize();
        GameObject pew = (GameObject)Instantiate(projectile, transform.position, Quaternion.identity);
        pew.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;
        if (backShot)
        {
            GameObject pew2 = (GameObject)Instantiate(projectile, transform.position, Quaternion.identity);
            pew2.GetComponent<Rigidbody2D>().velocity = direction * -projectileSpeed;
        }
        timer = atackSpeed;
    }

    void shotgun_bullets()
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
            GameObject pew = Instantiate(projectile, transform.position, Quaternion.identity);
            pew.GetComponent<Rigidbody2D>().velocity = changedDirection * (float)(projectileSpeed * (Random.Range(0.5f, 0.7f)));
            if (backShot)
            {
                GameObject pew2 = Instantiate(projectile, transform.position, Quaternion.identity);
                pew2.GetComponent<Rigidbody2D>().velocity = changedDirection * (float)(-projectileSpeed * (Random.Range(0.5f, 0.7f)));
            }
        }
        timer = atackSpeed * 10;
    }
}
