using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_kill_enemies : MonoBehaviour {

    GameObject topDoor;
    GameObject bottomDoor;
    GameObject leftDoor;
    GameObject rightDoor;
    public bool L;
    public bool R;
    public bool B;
    public bool T;
    AIDestinationSetter ai;

    void Start()
    {
        ai = gameObject.GetComponent<AIDestinationSetter>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        ai.target = player.transform;
        GameObject room = gameObject.transform.parent.gameObject.transform.parent.gameObject;
        topDoor = room.transform.Find("TopDoor").gameObject;
        bottomDoor = room.transform.Find("BottomDoor").gameObject;
        leftDoor = room.transform.Find("LeftDoor").gameObject;
        rightDoor = room.transform.Find("RightDoor").gameObject;

        if (L && leftDoor.tag == "EnterDoor")
        {
            Destroy(gameObject);
        }
        if (R && rightDoor.tag == "EnterDoor")
        {
            Destroy(gameObject);
        }
        if (T && topDoor.tag == "EnterDoor")
        {
            Destroy(gameObject);
        }
        if (B && bottomDoor.tag == "EnterDoor")
        {
            Destroy(gameObject);
        }
        Destroy(this);
        //StartCoroutine(DestroyGameObject());
    }


    IEnumerator DestroyGameObject()
    {
        yield return new WaitForSeconds(1);
        GameObject room = gameObject.transform.parent.gameObject.transform.parent.gameObject;
        topDoor = room.transform.Find("TopDoor").gameObject;
        bottomDoor = room.transform.Find("BottomDoor").gameObject;
        leftDoor = room.transform.Find("LeftDoor").gameObject;
        rightDoor = room.transform.Find("RightDoor").gameObject;

        if (L && leftDoor.tag == "EnterDoor")
        {
            Destroy(gameObject);
        }
        if (R && rightDoor.tag == "EnterDoor")
        {
            Destroy(gameObject);
        }
        if (T && topDoor.tag == "EnterDoor")
        {
            Destroy(gameObject);
        }
        if (B && bottomDoor.tag == "EnterDoor")
        {
            Destroy(gameObject);
        }
        Destroy(this);
    }



    void Update()
    {

    }
}
