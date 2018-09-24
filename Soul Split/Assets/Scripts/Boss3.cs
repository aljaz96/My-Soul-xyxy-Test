using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss3 : MonoBehaviour {

    public GameObject player;
    public GameObject currentRoom;
    public GameObject playerRoom;
    public GameObject bullet;
    public GameObject bulletBomb;
    public GameObject bulletSprinkler;
    public bool active = false;
    public float atackTimer = 0;
    public float actionTimer = 2;
    Vector3 originalPosition;
    public int atack;
    public int atackPhase;
    int bulletNumber;
    float bulletTimer = 0.2f;
    float x = 1;
    float y = 1;
    int currentPos = 1;
    bool defeated = false;
    float ShotDelay;
    Vector3 playerPos;


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
        if (active && !defeated)
        {
            //do stuff
            if (actionTimer < 0)
            {
                atack = Random.Range(1, 5);
                if (atack == 1)
                {
                    int p = Random.Range(1, 4);
                    do
                    {
                        p = Random.Range(1, 4);
                    } while (p == currentPos);

                    currentPos = p;
                    actionTimer = 1;
                    atackTimer = 0.5f;
                    atackPhase = 1;
                }
                if (atack == 2)
                {
                    actionTimer = 5;
                    atackTimer = 3f;
                    atackPhase = 1;
                    ShotDelay = 2.5f;
                    playerPos = player.transform.position;
                }
                if (atack == 3)
                {
                    actionTimer = 5;
                    atackTimer = 3.5f;
                    atackPhase = 1;
                    ShotDelay = 3f;
                    playerPos = player.transform.position;
                }
                if (atack == 4)
                {
                    actionTimer = 6;
                    atackTimer = 5f;
                    atackPhase = 1;
                    ShotDelay = 1f;
                    playerPos = player.transform.position;
                }
            }
            if (atack == 1)
            {
                Teleport(currentPos);
            }
            if (atack == 2)
            {
                SnipeShots();
            }
            if (atack == 3)
            {
                MultiShots();
            }
            if (atack == 4)
            {
                BombShot();
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

    void Teleport(int pos)
    {
        if (atackTimer < 0)
        {
            GameObject newPosition = GameObject.Find("Boss3P" + pos);
            transform.position = newPosition.transform.position;
            atack = 0;
        }
    }

    void MultiShots()
    {
        if (atackTimer < ShotDelay)
        {
            ShotDelay -= 0.3f;
            float r = Random.Range(0.9f, 1.3f);
            for (int i = 0; i < 5; i++)
            {
                GameObject b1 = Instantiate(bullet, transform.position, Quaternion.identity);
                Vector3 direction = playerPos;

                direction.x = r * direction.x;
                direction.y = r * direction.y;
                switch (i)
                {
                    case 0:
                        break;
                    case 1:
                        direction = GetMultiShotDirection(1.16f, direction);
					    break;
                    case 2:
                        direction = GetMultiShotDirection(1.32f, direction);
						break;
                    case 3:
                        direction = GetMultiShotDirection(0.84f, direction);
						break;
                    case 4:
                        direction = GetMultiShotDirection(0.68f, direction);
						break;
                }
                direction -= transform.position;
                b1.GetComponent<Rigidbody2D>().velocity = direction / 10;
                playerPos = player.transform.position;
            }
        }
        if (atackTimer < 0)
        {
            atack = 0;
        }
    }

    Vector3 GetMultiShotDirection(float val, Vector3 dir)
    {
        if (currentPos == 2 || currentPos == 3)
        {
            dir.y = dir.x * val;
        }
        else
        {
            dir.x = dir.y * val;
        }
        return dir;
    }

    void SnipeShots()
    {
        if (atackTimer < ShotDelay)
        {
            ShotDelay -= 0.5f;
            GameObject b1 = Instantiate(bullet, transform.position, Quaternion.identity);
            Vector3 direction = playerPos - transform.position;
            b1.GetComponent<Rigidbody2D>().velocity = direction * 2.5f;
            playerPos = player.transform.position;
        }
        if (atackTimer < 0)
        {
            atack = 0;
        }
    }

    void BombShot()
    {
        if ((atackTimer < 4 && atackPhase == 1) || (atackTimer < 3 && atackPhase == 2))
        {
            GameObject b1 = Instantiate(bulletBomb, transform.position, Quaternion.identity);
            Vector3 direction = playerPos - transform.position;
            direction.x = direction.x * (Random.Range(0.8f, 1.2f));
            direction.y = direction.y * (Random.Range(0.8f, 1.2f));
            b1.GetComponent<Rigidbody2D>().velocity = direction * 1.5f;
            playerPos = player.transform.position;
            atackPhase++;
        }
        else if (atackTimer < 2 && atackPhase == 3)
        {
            GameObject b1 = Instantiate(bulletSprinkler, transform.position, Quaternion.identity);
            Vector3 direction = playerPos - transform.position;
            direction.x = direction.x * (Random.Range(0.9f, 1.1f));
            direction.y = direction.y * (Random.Range(0.9f, 1.1f));
            b1.GetComponent<Rigidbody2D>().velocity = direction * 1f;
            playerPos = player.transform.position;
            atackPhase++;
        }
        if (atackTimer < 0)
        {
            atack = 0;
        }
    }
}
