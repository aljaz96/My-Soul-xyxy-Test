using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2 : MonoBehaviour {

    public GameObject player;
    public GameObject currentRoom;
    public GameObject playerRoom;
    public GameObject bullet;
    public bool active = false;
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


    void Start()
    {
        player = GameObject.Find("box");
        currentRoom = new GameObject();
        currentRoom = transform.parent.gameObject;
        originalPosition = transform.position;
    }

    void Update()
    {
        atackTimer -= Time.deltaTime;
        actionTimer -= Time.deltaTime;
        bulletTimer -= Time.deltaTime;
        if (active)
        {
            //do stuff
            if (actionTimer < 0)
            {
                atack = Random.Range(1, 4);
                if (atack == 1)
                {
                    int x = Random.Range(-movementLimit, movementLimit);
                    int y = Random.Range(-movementLimit, movementLimit);
                    Vector3 newPos = new Vector3 {
                        x = x / 10,
                        y = y / 10
                    };
                    GetComponent<Rigidbody2D>().velocity = newPos;
                    actionTimer = 2;
                    atackTimer = 2;
                }
                if (atack == 2)
                {
                    actionTimer = 5;
                    atackTimer = 4;
                    atackPhase = 1;
                    stopMoving();
                }
                if (atack == 3)
                {
                    actionTimer = 7;
                    atackTimer = 6;
                    atackPhase = 1;
                    bulletNumber = 0;
                    x = 1;
                    y = 1;
                    stopMoving();
                }
            }
            if (atack == 2)
            {
                atackOne();
            }
            if (atack == 3)
            {
                atackTwo();
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
        /* if (transform.position.x > movementLimit || transform.position.x < -movementLimit || transform.position.y > movementLimit || transform.position.y < -movementLimit)
        {
            Vector3 v3 = new Vector3
            {
                x = 0,
                y = 0
            };
            GetComponent<Rigidbody2D>().velocity = v3;
        }
        */

    }

    void atackOne()
    {
        if ((atackPhase == 1 && atackTimer < 4) || (atackPhase == 3 && atackTimer < 3))
        {
            for (int i = 0; i < 4; i++)
            {
                GameObject b1 = Instantiate(bullet, transform.position, Quaternion.identity);
                Vector3 v3 = new Vector3();
                switch (i)
                {
                    case 0:
                        v3.x = 0;
                        v3.y = 1;
                        break;
                    case 1:
                        v3.x = 1;
                        v3.y = 0;
                        break;
                    case 2:
                        v3.x = 0;
                        v3.y = -1;
                        break;
                    case 3:
                        v3.x = -1;
                        v3.y = 0;
                        break;
                }
                b1.GetComponent<Rigidbody2D>().velocity = v3;
            }
            atackPhase++;
        }
        if ((atackPhase == 2 && atackTimer < 3.5f) || (atackPhase == 4 && atackTimer < 2.5f))
        {
            for (int i = 0; i < 4; i++)
            {
                GameObject b1 = Instantiate(bullet, transform.position, Quaternion.identity);
                Vector3 v3 = new Vector3();
                switch (i)
                {
                    case 0:
                        v3.x = 1;
                        v3.y = 1;
                        break;
                    case 1:
                        v3.x = 1;
                        v3.y = -1;
                        break;
                    case 2:
                        v3.x = -1;
                        v3.y = -1;
                        break;
                    case 3:
                        v3.x = -1;
                        v3.y = 1;
                        break;
                }
                b1.GetComponent<Rigidbody2D>().velocity = v3;
            }
            atackPhase++;
        }
        if (atackPhase == 5 && atackTimer < 2f)
        {
            for (int i = 0; i < 8; i++)
            {
                GameObject b1 = Instantiate(bullet, transform.position, Quaternion.identity);
                Vector3 v3 = new Vector3();
                switch (i)
                {
                    case 0:
                        v3.x = 0;
                        v3.y = 1;
                        break;
                    case 1:
                        v3.x = 1;
                        v3.y = 1;
                        break;
                    case 2:
                        v3.x = 1;
                        v3.y = 0;
                        break;
                    case 3:
                        v3.x = 1;
                        v3.y = -1;
                        break;
                    case 4:
                        v3.x = 0;
                        v3.y = -1;
                        break;
                    case 5:
                        v3.x = -1;
                        v3.y = -1;
                        break;
                    case 6:
                        v3.x = -1;
                        v3.y = 0;
                        break;
                    case 7:
                        v3.x = -1;
                        v3.y = 1;
                        break;
                }
                b1.GetComponent<Rigidbody2D>().velocity = v3;
            }
            atackPhase++;
            atack = 0;
        }
    }

    void atackTwo()
    {
        if (bulletTimer < 0)
        {
            GameObject b1 = Instantiate(bullet, transform.position, Quaternion.identity);
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
            b1.GetComponent<Rigidbody2D>().velocity = v3;
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

    void stopMoving()
    {
        Vector3 v3 = new Vector3
        {
            x = 0,
            y = 0
        };
        GetComponent<Rigidbody2D>().velocity = v3;
    }
}
