using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class familiar : MonoBehaviour {

    // Use this for initialization
    //public Transform center;
    //public Vector3 axis;
    //public float radius = 2.0f;
    //public float radiusSpeed = 0.5f;
    //public float rotationSpeed = 80.0f;
    private float RotateSpeed = 2f;
    private float Radius = 0.5f;
    public GameObject parent;
    public GameObject projectile;
    private Vector2 center;
    private float angle;
    public float projectileSpeed = 5;
    public float atackSpeed = 0.05f;

    public bool backShot = false;
    float timer = 0.02f;

    void Start () {
        //axis = Vector3.up;
        //_centre = parent.transform.position;
        // transform.position = (transform.position - center.position).normalized * radius + center.position;
    }
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
        center = parent.transform.position;
        angle += RotateSpeed * Time.deltaTime;
        var offset = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)) * Radius;
        transform.position = center + offset;
        //transform.RotateAround(center.position, axis, rotationSpeed * Time.deltaTime);
        //var desiredPosition = (transform.position - center.position).normalized * radius + center.position;
        //transform.position = Vector3.MoveTowards(transform.position, desiredPosition, Time.deltaTime * radiusSpeed);

        if (Input.GetMouseButton(1) && timer < 0)
        {
            Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = pz - transform.position;
            direction.Normalize();
            GameObject pew = (GameObject)Instantiate(projectile, transform.position, Quaternion.identity);
            pew.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;
            if (backShot)
            {
                GameObject pew2 = (GameObject)Instantiate(projectile, transform.position, Quaternion.identity);
                pew2.GetComponent<Rigidbody2D>().velocity = direction * -projectileSpeed;
            }
            timer = atackSpeed;
        }
    }
}
