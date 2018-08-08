using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMovement : MonoBehaviour {


    // Use this for initialization
    Rigidbody2D player;
    public GameObject scythe;
    public GameObject atack;
    public float speed = 2;
    public float atackSpeed = 0.5f;
    public Animator animator;
    public float timer = 0;
    public float dodgeTimer;
    public float invulnerability = 0;
    public float mouse_X_position;
    public float mouse_Y_position;
    public float angle;
    Vector2 vec2;
    Vector2 startPos;
    Vector2 atackPos;
    float range = 0.5f;

    void Start()
    {
        transform.TransformPoint(Vector3.zero);
        player = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += Vector3.down * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Space) && timer < 0)
        {
            //   animator.SetTrigger("HasAtacked");
            //   timer = 1f;
        }
        if (Input.GetKey(KeyCode.LeftShift) && dodgeTimer < 0)
        {
            invulnerability = 0.5f;
            dodgeTimer = 5;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        var relativePos = mousePos - transform.position;
        angle = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg;
        var rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        if (Input.GetMouseButtonDown(0) && timer < 0)
        {
            animator.SetTrigger("HasAtacked");
            timer = atackSpeed;
            startPos.x = transform.position.x;
            startPos.y = transform.position.y;
            double distance = Math.Sqrt(Math.Pow((mousePos.x - startPos.x), 2) + Math.Pow((mousePos.y - startPos.y), 2));
            double T = range / distance;
            atackPos.x = (float)((1 - T) * startPos.x + T * mousePos.x);
            atackPos.y = (float)((1 - T) * startPos.y + T * mousePos.y);
            GameObject slash = Instantiate(atack, new Vector3(atackPos.x, atackPos.y, 0), rotation);
            //pew.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;
        }
        scythe.transform.rotation = rotation;
        //Quaternion target = Quaternion.Euler(0, 0, angle);
        //scythe.transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 5);
    }
}
