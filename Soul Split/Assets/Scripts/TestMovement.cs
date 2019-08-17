using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestMovement : MonoBehaviour {


    // Use this for initialization
    Rigidbody2D player;
    AudioSource audioData;
    SpriteRenderer charRenderer;
    SpriteRenderer scytheRenderer;
    Animator anim;
    public GameObject scythe;
    public GameObject atack;
    public Animator animator;
    public float timer = 0;
    float dodgeTimer = 0;
    float invulnerability = 0;
    float angle;
    Vector2 vec2;
    Vector2 startPos;
    Vector2 atackPos;
    public float active = 0;
    public float moved;
    public bool right = true;
    public float speed;
    public Vector3 oldPos, newPos;
    int running = 0;
    bool r = false;

    void Start()
    {
        atack = (GameObject)Resources.Load("Prefabs/Slash" + CharacterStats.weaponType, typeof(GameObject));
        anim = GetComponent<Animator>();
        audioData = GetComponent<AudioSource>();
        transform.TransformPoint(Vector3.zero);
        player = GetComponent<Rigidbody2D>();
        charRenderer = GetComponent<SpriteRenderer>();
        scytheRenderer = scythe.GetComponent<SpriteRenderer>();
        oldPos = newPos = gameObject.transform.position;
        
        // if (SceneManager.GetActiveScene().buildIndex == 0)
        //{ 
        //CharacterStats.ResetStats();
        // }
    }

    void FixedUpdate()
    {
        if (active < 0)
        {
            running = 0;
            if (Input.GetKey(KeyCode.A))
            {
                transform.position += Vector3.left * CharacterStats.speed * Time.deltaTime;
                running = 1;
                r = true;
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.position += Vector3.right * CharacterStats.speed * Time.deltaTime;
                running = 1;
                r = true;
            }
            if (Input.GetKey(KeyCode.W))
            {
                transform.position += Vector3.up * CharacterStats.speed * Time.deltaTime;
                running = 1;
                r = true;
            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.position += Vector3.down * CharacterStats.speed * Time.deltaTime;
                running = 1;
                r = true;
            }
            if (Input.GetKey(KeyCode.Space) && timer < 0)
            {
                CharacterStats.energy = CharacterStats.total_energy;
                CharacterStats.hp = CharacterStats.total_hp;
            }
            if (Input.GetKey(KeyCode.LeftShift) && dodgeTimer < 0)
            {
                invulnerability = 0.5f;
                dodgeTimer = 5;
            }
            if (Input.GetKey(KeyCode.Alpha1))
            {
                CharacterStats.bulletType = 1;
                CharacterStats.weaponType = 1;
                atack = (GameObject)Resources.Load("Prefabs/Slash1", typeof(GameObject));
            }
            if (Input.GetKey(KeyCode.Alpha2))
            {
                CharacterStats.bulletType = 2;
                CharacterStats.weaponType = 2;
                atack = (GameObject)Resources.Load("Prefabs/Slash2", typeof(GameObject));
            }
            if (Input.GetKey(KeyCode.Alpha3))
            {
                CharacterStats.bulletType = 3;
                CharacterStats.weaponType = 3;
                atack = (GameObject)Resources.Load("Prefabs/Slash3", typeof(GameObject));
            }
            if (Input.GetKey(KeyCode.Alpha4))
            {
                CharacterStats.bulletType = 4;
            }
            if (Input.GetKey(KeyCode.Alpha5))
            {
                CharacterStats.bulletType = 5;
              
            }
            if (Input.GetKey(KeyCode.Alpha6))
            {
                CharacterStats.bulletType = 6;

            }
            if (Input.GetKey(KeyCode.M))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            if (Input.GetKey(KeyCode.K))
            {
                GameObject r = transform.parent.gameObject;
                foreach (Transform child in r.transform.Find("Enemies").transform)
                {
                    MonsterStats ms = child.GetComponent<MonsterStats>();
                    ms.hp -= 600;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        newPos = gameObject.transform.position;
        if (CharacterStats.hp < 0)
        {
            Camera_script cs = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera_script>();
            Vector3 v3 = transform.position;
            v3.y += 0.5f;
            GameObject p = Instantiate(Resources.Load("Prefabs/Poof", typeof(GameObject)) as GameObject, v3, Quaternion.identity);
            Destroy(p, 0.85f);
            cs.PlayerDied();
            Destroy(gameObject);
            Destroy(this);
        }
        //anim.SetFloat("Speed", (oldPos-newPos).magnitude);
        if (running == 0 && r == true)
        {
            r = false;
            anim.SetInteger("Running", 0);
        }
        else if(running == 1 && r == true)
        {
            anim.SetInteger("Running", 1);
        }
        else
        {
            anim.SetInteger("Running", -1);
        }
     
        
        speed = oldPos.x - newPos.x;
        ChangeDirection(speed);
        active -= Time.deltaTime;
        timer -= Time.deltaTime;
        moved -= Time.deltaTime;
        invulnerability -= Time.deltaTime;
        player.velocity = new Vector3(0, 0, 0);
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        var relativePos = mousePos - transform.position;
        if (right)
        {
            angle = (Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg) + 200;
        }
        else
        {
            angle = (Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg) + 20;
        }
        
        var rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        scythe.transform.rotation = rotation;
        oldPos = newPos;
        if (Input.GetMouseButtonDown(0) && timer < 0)
        {
            animator.SetTrigger("HasAtacked");
            switch (CharacterStats.weaponType)
            {
                case 1:
                    timer = CharacterStats.atackSpeed;
                    break;
                case 2:
                    timer = CharacterStats.atackSpeed + CharacterStats.atackSpeed / 2;
                    break;
                case 3:
                    timer = CharacterStats.atackSpeed / 4;
                    break;
                case 4:
                    timer = CharacterStats.atackSpeed * 3;
                    break;
            }
            startPos.x = transform.position.x;
            startPos.y = transform.position.y;
            double distance = Math.Sqrt(Math.Pow((mousePos.x - startPos.x), 2) + Math.Pow((mousePos.y - startPos.y), 2));
            double T = CharacterStats.range / distance;
            atackPos.x = (float)((1 - T) * startPos.x + T * mousePos.x);
            atackPos.y = (float)((1 - T) * startPos.y + T * mousePos.y);
            angle = (Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg);
            rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            GameObject slash = Instantiate(atack, new Vector3(atackPos.x, atackPos.y, 0), rotation);
            audioData.Play();
        }

        //Quaternion target = Quaternion.Euler(0, 0, angle);
        //scythe.transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 5);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy" && invulnerability < 0)
        {
            invulnerability = 0.5f;
            CharacterStats.hp -= col.gameObject.GetComponent<MonsterStats>().damage;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "EnemySpell" && invulnerability < 0)
        {
            invulnerability = 0.5f;
            CharacterStats.hp -= collision.gameObject.GetComponent<EnemyProjectile>().damage;
        }
        if (collision.tag == "EnemyBullet" && invulnerability < 0)
        {
            invulnerability = 0.5f;
            CharacterStats.hp -= collision.gameObject.GetComponent<EnemyProjectile>().damage;
        }
    }

    public void passing()
    {
        active = 1;
    }

    void ChangeDirection(float diff)
    {
        if (diff > 0.001f)
        {
            right = true;
            charRenderer.flipX = false;
            scytheRenderer.flipX = true;
        }
        if (diff < -0.001f)
        {
            right = false;
            charRenderer.flipX = true;
            scytheRenderer.flipX = false;
        }
    }
}
