using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_script : MonoBehaviour {

    //https://answers.unity.com/questions/49542/smooth-camera-movement-between-two-targets.html

    public float transitionDuration = 1f;
    public Vector3 target;
    // Use this for initialization
    void Start()
    {
        GameObject mainRoom = GameObject.Find("StartingRoom");
        Vector3 v3 = mainRoom.transform.Find("Mid").gameObject.transform.position;
        v3.z = -10;
        transform.position = v3;
    }

    // Update is called once per frame
    void Update()
    {
       
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

}
