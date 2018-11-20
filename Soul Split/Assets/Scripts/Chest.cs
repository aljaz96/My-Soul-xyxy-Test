﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour {

    // Use this for initialization
    GameObject player;
    public GameObject E;
    bool active = false;
    bool opened = false;
    GameObject item;
    public Animator animator;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.E) && active && !opened)
        {
            //Open Chest
            //Play animation
            animator.SetTrigger("Open");
            //Spawn item
            //Instantiate(item, new Vector3(0, 0, 0), Quaternion.identity);
            opened = true;
            E.SetActive(false);
            active = false;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" && !opened)
        {
            E.SetActive(true);
            active = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !opened)
        {
            E.SetActive(true);
            active = true;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            E.SetActive(false);
            active = false;
        }
    }
}
