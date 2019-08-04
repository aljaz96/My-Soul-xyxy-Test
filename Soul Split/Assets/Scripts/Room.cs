using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Room : MonoBehaviour {

    // Use this for initialization
    public GameObject player;
    public GameObject enemies;
    public GameObject topDoor;
    public GameObject bottomDoor;
    public GameObject leftDoor;
    public GameObject rightDoor;
    public GameObject pathing;
    GameObject middle;
    Door d;
    bool active = false;
    public string test;
    public string sceneName;

    void Start()
    {
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

        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;

        if (sceneName == "Stage3")
        {
            GameObject walls = gameObject.transform.Find("Walls").gameObject;
            foreach (Transform child in walls.transform)
            {
                SpriteRenderer r = child.gameObject.GetComponent<SpriteRenderer>();
                r.color = Color.grey;
            }
            GameObject floors = gameObject.transform.Find("Floors").gameObject;
            foreach (Transform child in floors.transform)
            {
                SpriteRenderer r = child.gameObject.GetComponent<SpriteRenderer>();
                r.color = Color.grey;
            }
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
                topDoor.GetComponent<Door>().isOpen = false;
            }
            if (bottomDoor.activeSelf)
            {
                bottomDoor.GetComponent<Door>().animator.SetTrigger("Close");
                bottomDoor.GetComponent<Door>().isOpen = false;
            }
            if (leftDoor.activeSelf)
            {
                leftDoor.GetComponent<Door>().animator.SetTrigger("Close");
                leftDoor.GetComponent<Door>().isOpen = false;
            }
            if (rightDoor.activeSelf)
            {
                rightDoor.GetComponent<Door>().animator.SetTrigger("Close");
                rightDoor.GetComponent<Door>().isOpen = false;
            }
            active = true;
            pathing.SetActive(true);
            AstarPath astar = pathing.GetComponent<AstarPath>();
            test = astar.graphs[0].name;
            GridGraph g = (GridGraph)astar.graphs[0];
            g.center = middle.transform.position;
            astar.graphs[0] = g;
            astar.graphs[0].Scan();
            
            /*
            foreach (Transform child in enemies.transform)
            {
                try
                {
                    child.GetComponent<AIPath>().enabled = true;
                    child.GetComponent<Seeker>().enabled = true;
                    child.GetComponent<AIDestinationSetter>().enabled = true;
                }
                catch
                {
                   // do nothing;
                }
            }
            */
        }
        else if (active && enemies.transform.childCount == 0)
        {
            active = false;
            if (topDoor.activeSelf)
            {
                d = topDoor.GetComponent<Door>();
                d.isOpen = true;
                openDoor();
            }
            if (bottomDoor.activeSelf)
            {
                d = bottomDoor.GetComponent<Door>();
                d.isOpen = true;
                openDoor();
            }
            if (leftDoor.activeSelf)
            {
                d = leftDoor.GetComponent<Door>();
                d.isOpen = true;
                openDoor();
            }
            if (rightDoor.activeSelf)
            {
                d = rightDoor.GetComponent<Door>();
                d.isOpen = true;
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
