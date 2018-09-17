using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rusher : MonoBehaviour {

    public GameObject player;
    public GameObject currentRoom;
    public GameObject playerRoom;
    public bool active = false;
    public float movementTimer;
    public float speed = 2;
    public float rushTimer = 0;
    public float rushCooldown = 3;
    public bool rush = false;

    // Use this for initialization
    void Start () {
        player = GameObject.Find("box");
        currentRoom = new GameObject();
        currentRoom = transform.parent.gameObject;
    }
	
	// Update is called once per frame
	void Update () {
        rushTimer -= Time.deltaTime;
        movementTimer -= Time.deltaTime;
        if (active)
        {
            //do stuff
            if (movementTimer < 0 && !rush)
            {
                changeDirection();
            }
            if (rushTimer < 0)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, player.transform.position - transform.position);
                if (hit.transform == player)
                {
                    if (transform.position.x == player.transform.position.x && transform.position.y < player.transform.position.y)
                    {
                        RushPlayer(0, speed * 4);
                    }
                    else if (transform.position.x == player.transform.position.x && transform.position.y > player.transform.position.y)
                    {
                        RushPlayer(0, -speed * 4);  
                    }
                    else if (transform.position.x < player.transform.position.x && transform.position.y == player.transform.position.y)
                    {
                        RushPlayer(speed * 4, 0);   
                    }
                    else if (transform.position.x > player.transform.position.x && transform.position.y == player.transform.position.y)
                    {
                        RushPlayer(-speed * 4, 0);    
                    }
                }
            }
        }
        else
        {  
            playerRoom = player.transform.parent.gameObject;
            
            if (playerRoom.name == currentRoom.name)
            {
                active = true;
            }
        }
    }


    void OnCollisionEnter2D(Collision2D col)
    {
        rush = false;
        changeDirection();
    }

    void RushPlayer(float x, float y)
    {
        rush = true;
        GetComponent<Rigidbody2D>().velocity = new Vector3(x, y, 0);
        movementTimer = 10;
        rushTimer = rushCooldown;
    }

    void changeDirection()
    {
        int decision = Random.Range(1, 5);
        switch (decision)
        {
            case 1:
                GetComponent<Rigidbody2D>().velocity = new Vector3(speed, 0, 0);
                break;
            case 2:
                GetComponent<Rigidbody2D>().velocity = new Vector3(-speed, 0, 0);
                break;
            case 3:
                GetComponent<Rigidbody2D>().velocity = new Vector3(0, -speed, 0);
                break;
            case 4:
                GetComponent<Rigidbody2D>().velocity = new Vector3(0, speed, 0);
                break;
        }
        movementTimer = Random.Range(1, 6);
    }
}
