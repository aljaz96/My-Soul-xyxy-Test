using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {

    // Use this for initialization
    bool isEmpty = false;
    bool doorsOpened = false;
    GameObject enemies;
    Door door;

    void Start()
    {
        enemies = transform.Find("Enemies").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemies.transform.childCount == 0 && !doorsOpened)
        {
            isEmpty = true;
        }
        if (isEmpty && !doorsOpened)
        {
            foreach (Transform child in transform)
            {
                if (child.tag == "Door")
                {
                    door = child.GetComponent<Door>();
                    door.isOpen = true;
                    door.animator.SetBool("Open", true);
                }
            }
            doorsOpened = true;
        }
    }
}
