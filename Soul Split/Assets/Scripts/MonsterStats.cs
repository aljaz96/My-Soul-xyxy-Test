using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStats : MonoBehaviour {

    
    // Use this for initialization
    public float hp = 1;
    public int damage = 10;
    public int p_damage = 5;
    public float speed = 1;
    public float playerDistance = 1;
    public bool active = false;
    float timer = 0.5f;
    GameObject player;
    GameObject currentRoom;
    GameObject playerRoom;
    float invulnerable = 0.1f;
    AIPath aiPath;
    
	void Start () {
        player = GameObject.FindWithTag("Player");
        currentRoom = transform.parent.gameObject.transform.parent.gameObject;
        try
        {
            aiPath = gameObject.GetComponent<AIPath>();
            aiPath.endReachedDistance = playerDistance;
            aiPath.maxSpeed = speed;
        }
        catch
        {
            aiPath = null;
        }
    }
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
        invulnerable -= Time.deltaTime;

        if (hp <= 0)
        {
            Destroy(gameObject);
        }
        if (!active)
        {
            playerRoom = player.transform.parent.gameObject;
        }
        if(timer <= 0 && playerRoom.name == currentRoom.name && !active)
        {
            StartCoroutine(SetActive());
        }
	}

    IEnumerator SetActive()
    {
        yield return new WaitForSeconds(1);
        try
        {
            GetComponent<AIPath>().enabled = true;
            GetComponent<Seeker>().enabled = true;
            GetComponent<AIDestinationSetter>().enabled = true;
        }
        catch
        {
            aiPath = null;
        }
        active = true;
    }

    public void RecieveDamage(float d)
    {
        if (invulnerable < 0)
        {
            hp -= d;
            invulnerable = 0.1f;
        }

    }

}
