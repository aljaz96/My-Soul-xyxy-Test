using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {

    // Use this for initialization
    public GameObject player;
    public GameObject enemies;
    public GameObject topDoor;
    public GameObject bottomDoor;
    public GameObject leftDoor;
    public GameObject rightDoor;
    public GameObject pathing;
    Door d;
    bool active = false;
    public string test;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemies = transform.Find("Enemies").gameObject;
        try
        {
            pathing = transform.Find("A_").gameObject;
           
        }
        catch
        {
            pathing = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!active && player.transform.parent.name == gameObject.name && enemies.transform.childCount > 0)
        {
            if (topDoor.activeSelf)
            {
                topDoor.GetComponent<Door>().animator.SetTrigger("Close");
            }
            if (bottomDoor.activeSelf)
            {
                bottomDoor.GetComponent<Door>().animator.SetTrigger("Close");
            }
            if (leftDoor.activeSelf)
            {
                leftDoor.GetComponent<Door>().animator.SetTrigger("Close");
            }
            if (rightDoor.activeSelf)
            {
                rightDoor.GetComponent<Door>().animator.SetTrigger("Close");
            }
            active = true;
            pathing.SetActive(true);
            AstarPath astar = pathing.GetComponent<AstarPath>();
            test = astar.graphs[0].name;
            GridGraph g = (GridGraph)astar.graphs[0];
            g.center = transform.position;
            astar.graphs[0] = g;
            astar.graphs[0].Scan();
            //var m = Matrix4x4.TRS(transform.position, Quaternion.identity, Vector3.one);
            //astar.graphs[0].SetMatrix(m);
            //astar.graphs[0].RelocateNodes(m);
            // astar.graphs[0].
   
            foreach (Transform child in enemies.transform)
            {
                child.GetComponent<AIPath>().enabled = true;
                child.GetComponent<Seeker>().enabled = true;
                child.GetComponent<AIDestinationSetter>().enabled = true;
            }
        }
        else if (active && enemies.transform.childCount == 0)
        {
            active = false;
            if (topDoor.activeSelf)
            {
                d = topDoor.GetComponent<Door>();
                openDoor();
            }
            if (bottomDoor.activeSelf)
            {
                d = bottomDoor.GetComponent<Door>();
                openDoor();
            }
            if (leftDoor.activeSelf)
            {
                d = leftDoor.GetComponent<Door>();
                openDoor();
            }
            if (rightDoor.activeSelf)
            {
                d = rightDoor.GetComponent<Door>();
                openDoor();
            }
            pathing.SetActive(false);
            Destroy(this);
        }
    }

    void openDoor()
    {
        d.animator.SetTrigger("Open");
        d.isOpen = true;
    }
}
