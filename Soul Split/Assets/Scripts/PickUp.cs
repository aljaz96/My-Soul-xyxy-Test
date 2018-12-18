using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour {

    GameObject E;
    public string name;
    bool active;
    Vector3 v3;
    bool moving = false;
    float timer = 0;
    // Use this for initialization
    void Start()
    {
        Vector3 v2 = new Vector3();
        v2.x = 0;
        v2.y = 0;
        transform.localScale = v2;
        E = transform.Find("e").gameObject;
        name = gameObject.name;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (transform.localScale.x < 0.02f)
        {
            transform.localScale += new Vector3(0.001f, 0.001f, 0);
        }
        if(transform.localScale.x >= 0.02f && !moving)
        {
            moving = true;
            Vector3 v1 = new Vector3();
            v1.y = -0.2f;
            GetComponent<Rigidbody2D>().velocity = v1;
            timer = 1;
        }
        if(moving && timer < 0)
        {
            Vector3 v1 = new Vector3();
            GetComponent<Rigidbody2D>().velocity = v1;
        }
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.E) && active)
        {
            //Open Chest
            //Play animation
            E.SetActive(false);
            Destroy(gameObject);
        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            E.SetActive(true);
            active = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            E.SetActive(false);
            active = false;
        }
    }
}
