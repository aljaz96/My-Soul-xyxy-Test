using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    public GameObject exitPoint;
    public checkLocal findPosition;
    public GameObject parent;
    public Animator animator;
    public bool isOpen = true;

    void Start()
    {
        animator = GetComponent<Animator>();
        findPosition = exitPoint.GetComponent<checkLocal>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "box" && isOpen == true)
        {
            collision.transform.position = new Vector2(findPosition.X, findPosition.Y);
            TestMovement t = collision.gameObject.GetComponent<TestMovement>();
            parent = exitPoint.transform.parent.gameObject;
            collision.transform.parent = parent.transform;
            parent.transform.TransformPoint(Vector3.zero);
            GameObject camera = GameObject.Find("Main Camera");
            Camera_script s = camera.GetComponent<Camera_script>();
            //s.target = parent.transform;
            s.target = new Vector3(parent.transform.position.x , parent.transform.position.y, -10);
            //camera.target = parent;
            StartCoroutine(camera.GetComponent<Camera_script>().Transition());
            t.passing();
            //camera.transform.position = new Vector3(parent.transform.position.x, parent.transform.position.y, -10);
        }
    }

    public void getProperPosition()
    {
        findPosition = exitPoint.GetComponent<checkLocal>();
    }

}
