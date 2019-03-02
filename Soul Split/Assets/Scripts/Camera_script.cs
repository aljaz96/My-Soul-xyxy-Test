using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_script : MonoBehaviour {

    //https://answers.unity.com/questions/49542/smooth-camera-movement-between-two-targets.html

    public float transitionDuration = 1f;
    public Vector3 target;
    bool black = false;
    GameObject B;
    // Use this for initialization
    void Start()
    {
        GameObject mainRoom = GameObject.Find("StartingRoom");
        B = transform.Find("B").gameObject;
        Vector3 v3 = mainRoom.transform.Find("Mid").gameObject.transform.position;
        v3.z = -10;
        transform.position = v3;
    }

    // Update is called once per frame
    void Update()
    {
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

}
