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
        if (collision.tag == "Player" && isOpen == true)
        {
            collision.transform.position = new Vector2(findPosition.X, findPosition.Y);
            TestMovement t = collision.gameObject.GetComponent<TestMovement>();
            parent = exitPoint.transform.parent.gameObject;
            collision.transform.parent = parent.transform;
            parent.transform.TransformPoint(Vector3.zero);
            GameObject middle = parent.transform.Find("Mid").gameObject;
            GameObject camera = GameObject.Find("Main Camera");
            Camera_script s = camera.GetComponent<Camera_script>();
            s.target = new Vector3(middle.transform.position.x , middle.transform.position.y, -10);
            StartCoroutine(camera.GetComponent<Camera_script>().Transition());
            t.passing();
        }
    }

    public void getProperPosition()
    {
        findPosition = exitPoint.GetComponent<checkLocal>();
    }

}
