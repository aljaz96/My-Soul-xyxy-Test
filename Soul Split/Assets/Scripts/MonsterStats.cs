using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStats : MonoBehaviour {

    // Use this for initialization
    public float hp = 1;
    public float damage = 1;
    public float speed = 1;
    public float playerDistance = 1;
    public bool active = false;
    float timer = 0.5f;
    AIPath aiPath;
	void Start () {
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
		if(hp <= 0)
        {
            Destroy(gameObject);
        }
        if(timer <= 0)
        {
            active = true;
        }
	}
}
