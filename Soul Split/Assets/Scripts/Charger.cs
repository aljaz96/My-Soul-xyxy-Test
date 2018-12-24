using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charger : MonoBehaviour {

    GameObject player;
    MonsterStats stats;
    float rushTimer;
    float rushCooldown = 5;
    bool rush = false;
    // Use this for initialization
    void Start()
    {
        stats = gameObject.GetComponent<MonsterStats>();
        player = GameObject.FindWithTag("Player");
        rushTimer = Random.Range(1.00f, 3.00f);
    }

    // Update is called once per frame
    void Update()
    {
        if (stats.active)
        {
            rushTimer -= Time.deltaTime;
            if (rushTimer < 0 && !rush)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, player.transform.position - transform.position);
                if (hit.transform.gameObject == player && !rush)
                {
                    Vector3 v = new Vector3(0, 0, 0);
                    GetComponent<Rigidbody2D>().velocity = v;
                    GetComponent<AIPath>().enabled = false;
                    GetComponent<Seeker>().enabled = false;
                    GetComponent<AIDestinationSetter>().enabled = false;
                    StartCoroutine(RushPlayer());
                    rush = true;
                }
            }
        }
    }


    void OnCollisionEnter2D(Collision2D col)
    {
        if (rush)
        {
            rush = false;
            rushTimer = rushCooldown;
            StartCoroutine(Move());
        }
    }

    IEnumerator Move()
    {
        yield return new WaitForSeconds(1);
        GetComponent<AIPath>().enabled = true;
        GetComponent<Seeker>().enabled = true;
        GetComponent<AIDestinationSetter>().enabled = true;
    }

    IEnumerator RushPlayer()
    {
        yield return new WaitForSeconds(1);
        Vector3 v3 = player.transform.position - transform.position;
        v3.Normalize();
        GetComponent<Rigidbody2D>().velocity =v3 * 8;
    }

    
}
