using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMovement : MonoBehaviour {

    // Use this for initialization
    Rigidbody2D player;
    public GameObject scythe;
    float speed = 2;
    public Animator animator;
    public float timer = 0;
    public float mouse_X_position;
    public float mouse_Y_position;
    public float angle;
    Vector2 vec2;

	void Start () {
        transform.TransformPoint(Vector3.zero);
        player = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += Vector3.down * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Space) && timer < 0)
        {
            animator.SetTrigger("HasAtacked");
            timer = 1f;
        }
    }

    // Update is called once per frame
    void Update () {
        timer -= Time.deltaTime;
        Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pz.z = 0;
        vec2.x = pz.x;
        vec2.y = pz.y;
        mouse_X_position = vec2.x;
        mouse_Y_position = vec2.y;
        //angle = Vector2.Angle(transform.position, vec2);
        if (Input.GetMouseButtonDown(0) && timer < 0)
        {
            animator.SetTrigger("HasAtacked");
            timer = 0.5f;
        }
        var relativePos = pz - transform.position;
        angle = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg;
        var rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        scythe.transform.rotation = rotation;
        //Quaternion target = Quaternion.Euler(0, 0, angle);
        //scythe.transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 5);
    }
}
