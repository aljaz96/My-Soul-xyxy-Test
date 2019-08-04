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
    public float size = 0.8f;
    // Use this for initialization
    void Start () {
        enemies = transform.parent.transform.Find("Enemies").gameObject;
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        if (gameObject.name == "Portal")
        {
            transform.localScale = new Vector3(0, 0, 0);
        }
        else if(gameObject.name == "Echo")
        {
            Color c = GetComponent<Renderer>().material.color;
            c.a = 0;
            GetComponent<Renderer>().material.color = c;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (enemies.transform.childCount == 0 && !open)
        {
            if (gameObject.name == "Portal")
            {
                if (transform.localScale.x < size)
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
            if(gameObject.name == "Echo")
            {
                if (GetComponent<Renderer>().material.color.a < 1)
                {
                    var c = GetComponent<Renderer>().material.color;
                    c.a += 0.01f;
                    GetComponent<Renderer>().material.color = c;
                }
                else
                {
                    open = true;
                }
            }
        }
        if(ported == true)
        {
            if (camera.GetComponent<Camera_script>().B.GetComponent<Renderer>().material.color.a == 1)
            {
                if (gameObject.name == "Portal")
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
                else if (gameObject.name == "Echo")
                {
                    SceneManager.LoadScene(0);
                }
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
        if (col.gameObject.tag == "Player" && transform.localScale.x >= size)
        {
            E.SetActive(true);
            active = true;
        }
        else if(col.gameObject.tag == "Player" && gameObject.name == "Echo")
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
