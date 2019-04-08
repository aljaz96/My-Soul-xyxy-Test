using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour {

    bool open = false;
    bool active = false;
    bool ported = false;
    public GameObject E;
    public GameObject Chest;
    new GameObject camera;
    GameObject enemies;
    // Use this for initialization
    void Start () {
        enemies = transform.parent.transform.Find("Enemies").gameObject;
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        transform.localScale = new Vector3(0, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
        if (enemies.transform.childCount == 0 && !open)
        {
            if (transform.localScale.x < 0.8f)
            {
                transform.localScale += new Vector3(0.02f, 0.02f, 0);
            }
            else
            {
                open = true;
                Vector3 v3 = transform.transform.position;
                v3.y -= 2;
                Instantiate(Chest, v3, Quaternion.identity);
            }
        }
        if(ported == true)
        {
            if(camera.GetComponent<Camera_script>().B.GetComponent<Renderer>().material.color.a == 1)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
	}

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.E) && active && open && !ported)
        {
            ported = true;
            //Change camera
            camera.GetComponent<Camera_script>().AppearB();
            GameObject.FindGameObjectWithTag("Player").GetComponent<TestMovement>().active = 10;
            //Go to next lvl
        }
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            E.SetActive(true);
            active = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            E.SetActive(false);
            active = false;
        }
    }
}
