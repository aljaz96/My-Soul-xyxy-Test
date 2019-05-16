using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour {

    // Use this for initialization
    SpriteRenderer sr;
    GameObject camera;
    bool play = false;
    GameObject black;
    float size;

	void Start () {
        sr = GetComponent<SpriteRenderer>();
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        black = GameObject.Find("B");
        size = transform.localScale.x;
	}
	
	// Update is called once per frame
	void Update () {
        if (play && black.GetComponent<Renderer>().material.color.a == 1)
        {
            CharacterStats.ResetStats();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
	}

    void OnMouseDown()
    {
        if(gameObject.name == "Start")
        {
            camera.GetComponent<Camera_script>().AppearB();
            play = true;
        }
        if (gameObject.name == "Return")
        {
            Camera_script  cs = camera.GetComponent<Camera_script>();
            cs.target = new Vector3(0, 0, -10);
            StartCoroutine(cs.Transition());
        }
        if (gameObject.name == "Credits")
        {
            Camera_script cs = camera.GetComponent<Camera_script>();
            cs.target = new Vector3(-28, 0, -10);
            StartCoroutine(cs.Transition());
        }
        if (gameObject.name == "HowToPlay")
        {
            Camera_script cs = camera.GetComponent<Camera_script>();
            cs.target = new Vector3(24.5f, 0, -10);
            StartCoroutine(cs.Transition());
        }
        if (gameObject.name == "Exit")
        {
            Application.Quit();
        }
    }
    void OnMouseUp()
    {
        
    }
    void OnMouseOver()
    {
        if (transform.localScale.x <= size * 1.4f)
        {
            transform.localScale = transform.localScale * 1.01f;
        }
    }

    void OnMouseExit()
    {
        transform.localScale = new Vector3(size, size, 0);
    }
}
