using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomWithPortals : MonoBehaviour {

    // Use this for initialization
    GameObject player;
    GameObject enemies;
    public GameObject topPortal;
    public GameObject bottomPortal;
    public GameObject leftPortal;
    public GameObject rightPortal;
    Color newColor;
    GameObject pathing;
    GameObject middle;
    public bool active = false;
    public string test;

    public string sceneName;


    void Start () {
        middle = transform.Find("Mid").gameObject;
        player = GameObject.FindGameObjectWithTag("Player");
        enemies = transform.Find("Enemies").gameObject;
        newColor.r = 0.65F;
        newColor.b = 0.65F;
        newColor.g = 0.65F;
        newColor.a = 1;
        try
        {
            pathing = transform.Find("A_").gameObject;

        }
        catch
        {
            pathing = null;
        }

        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;

        if (sceneName == "Stage3")
        {
            GameObject walls = gameObject.transform.Find("Walls").gameObject;
            foreach (Transform child in walls.transform)
            {
                SpriteRenderer r = child.gameObject.GetComponent<SpriteRenderer>();
                r.color = newColor;
            }
            GameObject floors = gameObject.transform.Find("Floors").gameObject;
            foreach (Transform child in floors.transform)
            {
                SpriteRenderer r = child.gameObject.GetComponent<SpriteRenderer>();
                r.color = newColor;
            }
            GameObject objects = gameObject.transform.Find("Objects").gameObject;
            foreach (Transform child in objects.transform)
            {
                if (child.name.Contains("Block"))
                {
                    foreach (Transform c in child.transform)
                    {
                        SpriteRenderer r = c.gameObject.GetComponent<SpriteRenderer>();
                        r.color = newColor;
                        /* if (child.name.Contains("Block"))
                         {
                             SpriteRenderer r = child.gameObject.GetComponent<SpriteRenderer>();
                             r.color = Color.grey;
                         }*/
                    }
                }
            }
        }
    }

    void Update()
    {
        if (player != null)
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
}
