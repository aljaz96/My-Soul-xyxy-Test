using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomWithPortals : MonoBehaviour {

    // Use this for initialization
    GameObject player;
    GameObject enemies;
    public GameObject topPortal;
    public GameObject bottomPortal;
    public GameObject leftPortal;
    public GameObject rightPortal;
    GameObject pathing;
    GameObject middle;
    public bool active = false;
    public string test;


    void Start () {
        middle = transform.Find("Mid").gameObject;
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

    void Update()
    {
        if (!active && player.transform.parent.name == gameObject.name && enemies.transform.childCount > 0)
        {
            if (topPortal.activeSelf)
            {
                topPortal.GetComponent<RoomPortal>().active = false;
            }
            if (bottomPortal.activeSelf)
            {
                bottomPortal.GetComponent<RoomPortal>().active = false;
            }
            if (leftPortal.activeSelf)
            {
                leftPortal.GetComponent<RoomPortal>().active = false;
            }
            if (rightPortal.activeSelf)
            {
                rightPortal.GetComponent<RoomPortal>().active = false;
            }
            active = true;
            pathing.SetActive(true);
            AstarPath astar = pathing.GetComponent<AstarPath>();
            test = astar.graphs[0].name;
            GridGraph g = (GridGraph)astar.graphs[0];
            g.center = middle.transform.position;
            astar.graphs[0] = g;
            astar.graphs[0].Scan();

        }

        else if (enemies.transform.childCount == 0)
        {
            active = false;
            if (topPortal.activeSelf)
            {
                topPortal.GetComponent<RoomPortal>().active = true;
            }
            if (bottomPortal.activeSelf)
            {
                bottomPortal.GetComponent<RoomPortal>().active = true;
            }
            if (leftPortal.activeSelf)
            {
                leftPortal.GetComponent<RoomPortal>().active = true;
            }
            if (rightPortal.activeSelf)
            {
                rightPortal.GetComponent<RoomPortal>().active = true;
            }
            pathing.SetActive(false);
            //Destroy(this);
        }
    }
}
