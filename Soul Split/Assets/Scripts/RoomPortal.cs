using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomPortal : MonoBehaviour {

    public GameObject exitPoint;
    public checkLocal findPosition;
    public GameObject parent;
    public GameObject E;
    GameObject player;
    public bool active = false;
    string roomName;
    public float timer = 1;

    void Start()
    {
        findPosition = exitPoint.GetComponent<checkLocal>();
        player = GameObject.FindGameObjectWithTag("Player");
        roomName = transform.parent.name;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            if (!active && transform.localScale.x > 0.4f)
            {
                transform.localScale -= new Vector3(0.01f, 0.01f, 0);
            }
            else if (active && transform.localScale.x < 0.7f)
            {
                transform.localScale += new Vector3(0.01f, 0.01f, 0);
            }
            if (transform.parent.name == player.transform.parent.name)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                timer = 1.2f;
            }
        }
    }

    void FixedUpdate()
    {

        if (Input.GetKeyDown(KeyCode.E) && active && player.transform.parent.name == roomName && E.activeSelf && timer < 0)
        {
            
            TestMovement t = player.gameObject.GetComponent<TestMovement>();
            player.transform.position = new Vector2(findPosition.X, findPosition.Y);
            parent = exitPoint.transform.parent.gameObject;
            player.transform.parent = parent.transform;
            parent.transform.TransformPoint(Vector3.zero);
            GameObject middle = parent.transform.Find("Mid").gameObject;
            GameObject camera = GameObject.Find("Main Camera");
            Camera_script s = camera.GetComponent<Camera_script>();
            //s.target = parent.transform;
            s.target = new Vector3(middle.transform.position.x, middle.transform.position.y, -10);
            //camera.target = parent;
            StartCoroutine(camera.GetComponent<Camera_script>().Transition());
            t.passing();
            
        }
    }

    public void getProperPosition()
    {
        findPosition = exitPoint.GetComponent<checkLocal>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && active)
        {
            E.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && active)
        {
            E.SetActive(false);
        }
    }


}
