using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blob : MonoBehaviour {

    public GameObject LesserBlob;
    public GameObject Bullet;
    MonsterStats stats;
    GameObject parent;
    public int hp = 999;
    public int blobSize = 1;

    void Start()
    {
        stats = gameObject.GetComponent<MonsterStats>();
        parent = gameObject.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (stats.hp < hp)
        {
            if (blobSize == 2 || blobSize == 3)
            {
                MakeBlobs(2);
            }
            else if (blobSize == 4)
            {
                SpawnBullets();
                MakeBlobs(2);
            }
            else if (blobSize == 5)
            {
                BulletExplosion(20);
                MakeBlobs(2);
            }
            Destroy(gameObject);
        }
    }


    void SpawnBullets()
    {
        GameObject b1 = Instantiate(Bullet, transform.position, Quaternion.identity);
        GameObject b2 = Instantiate(Bullet, transform.position, Quaternion.identity);
        GameObject b3 = Instantiate(Bullet, transform.position, Quaternion.identity);
        GameObject b4 = Instantiate(Bullet, transform.position, Quaternion.identity);
        Vector3 v = new Vector3();
        v.x = 1;
        v.y = 1;
        b1.GetComponent<Rigidbody2D>().velocity = v * 3; //1,1
        v.y = -1;
        b2.GetComponent<Rigidbody2D>().velocity = v * 3; //1,-1
        v.x = -1;
        b3.GetComponent<Rigidbody2D>().velocity = v * 3; //-1,-1
        v.y = 1;
        b4.GetComponent<Rigidbody2D>().velocity = v * 3; //-1,1
    }

    void BulletExplosion(int numberOfBullets)
    {
        Vector3 v3 = new Vector3();
        for (int i = 0; i < numberOfBullets; i++)
        {
            GameObject b1 = Instantiate(Bullet, transform.position, Quaternion.identity);
            v3.x += (Random.Range(-10f, 11f) / 10) * 4;
            v3.y += (Random.Range(-10f, 11f) / 10) * 4;
            b1.GetComponent<Rigidbody2D>().velocity = v3;
        }
    }

    void MakeBlobs(int numOfBlobs)
    {
        Vector3 v = new Vector3();
        for (int i = 0; i < numOfBlobs; i++)
        {
            v.x = transform.position.x + ((Random.Range(-0.500f, 0.501f)));
            v.y = transform.position.y + ((Random.Range(-0.500f, 0.501f)));
            GameObject blob = Instantiate(LesserBlob, v, Quaternion.identity);
            blob.transform.SetParent(parent.transform);
            blob.GetComponent<AIPath>().enabled = true;
            blob.GetComponent<Seeker>().enabled = true;
            blob.GetComponent<AIDestinationSetter>().enabled = true;

        }
        Destroy(gameObject);

    }

}