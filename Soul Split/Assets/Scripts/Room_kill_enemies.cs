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
    public int type = 1;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        try
        {
            ai = gameObject.GetComponent<AIDestinationSetter>();
            ai.target = player.transform;
        }
        catch
        {
            ai = null;
        }
        GameObject room = gameObject.transform.parent.gameObject.transform.parent.gameObject;
        if (type == 1)
        {
            topDoor = room.transform.Find("TopDoor").gameObject;
            bottomDoor = room.transform.Find("BottomDoor").gameObject;
            leftDoor = room.transform.Find("LeftDoor").gameObject;
            rightDoor = room.transform.Find("RightDoor").gameObject;
        }
        else if(type == 2)
        {
            topDoor = room.transform.Find("TopPortal").gameObject;
            bottomDoor = room.transform.Find("BottomPortal").gameObject;
            leftDoor = room.transform.Find("LeftPortal").gameObject;
            rightDoor = room.transform.Find("RightPortal").gameObject;
        }

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
