using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    // Use this for initialization
    public GameObject exitPoint;
    public checkLocal findPosition;
    public GameObject parent;
    public Animator animator;
    public bool isOpen = false;


    void Start () {
        findPosition = exitPoint.GetComponent<checkLocal>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "box")
        {
            collision.transform.position = new Vector2(findPosition.X, findPosition.Y);
            parent = exitPoint.transform.parent.gameObject;
            parent.transform.TransformPoint(Vector3.zero);
            GameObject camera = GameObject.Find("Main Camera");
            camera.transform.position = new Vector3(parent.transform.position.x, parent.transform.position.y, -10);
        }
    }

    public void getProperPosition()
    {
        findPosition = exitPoint.GetComponent<checkLocal>();
    }

}
