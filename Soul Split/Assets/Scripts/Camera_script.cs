using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Camera_script : MonoBehaviour {

    //https://answers.unity.com/questions/49542/smooth-camera-movement-between-two-targets.html

    public float transitionDuration = 1f;
    public Vector3 target;
    bool black = false;
    public GameObject B;
    GameObject mainRoom;
    public bool menu = false;
    // Use this for initialization
    void Start()
    {
        try
        {
            mainRoom = GameObject.Find("StartingRoom");
            B = transform.Find("B").gameObject;
            Vector3 v3 = mainRoom.transform.Find("Mid").gameObject.transform.position;
            v3.z = -10;
            transform.position = v3;
        }
        catch
        {
            //do nothing
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(mainRoom == null && !menu)
        {
            FindMainRoom();
        }
        if (black == false && B.GetComponent<Renderer>().material.color.a > 0)
        {
            var c = B.GetComponent<Renderer>().material.color;
            c.a -= 0.01f;
            B.GetComponent<Renderer>().material.color = c;
        }
        if (black == true && B.GetComponent<Renderer>().material.color.a < 1)
        {
            var c = B.GetComponent<Renderer>().material.color;
            c.a += 0.01f;
            B.GetComponent<Renderer>().material.color = c;
        }
    }


    public void PlayerDied()
    {
        AppearB();
        StartCoroutine(LoadMenu());
    }


    public IEnumerator LoadMenu()
    {
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene(0);
    }

    public IEnumerator Transition()
    {
        float t = 0.0f;
        Vector3 startingPos = transform.position;
        while (t < 1.0f)
        {
            t += Time.deltaTime * (Time.timeScale / transitionDuration);
            transform.position = Vector3.Lerp(startingPos, target, t);
            yield return 0;
        }
    }

    public void DisappearB()
    {
        black = false;
    }

    public void AppearB()
    {
        black = true;
    }

    void FindMainRoom()
    {
        mainRoom = GameObject.Find("StartingRoom");
        B = transform.Find("B").gameObject;
        Vector3 v3 = mainRoom.transform.Find("Mid").gameObject.transform.position;
        v3.z = -10;
        transform.position = v3;
    }
}
