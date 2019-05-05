using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class E_zombie : MonoBehaviour {

    GameObject player;
    private NavMeshAgent navMeshAgent;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        navMeshAgent = this.gameObject.GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
        if (player)
        {
            navMeshAgent.SetDestination(player.transform.position);
        }
	}
}
