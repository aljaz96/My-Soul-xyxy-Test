using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStats : MonoBehaviour {

    
    // Use this for initialization
    public float hp = 1;
    public int damage = 10;
    public int p_damage = 5;
    public float speed = 1;
    public float playerDistance = 1;
    public bool active = false;
    public float timer = 0.5f;
    public GameObject player;
    GameObject currentRoom;
    GameObject playerRoom;
    SpriteRenderer sr;
    float invulnerable = 0.1f;
    AIPath aiPath;
    public float wait = 1;
    bool destroy = true;
    bool blink = false;
    Color normal;
    Color invisible;
    float invisiTimer;
    
	void Start () {
        try
        {
            sr = GetComponent<SpriteRenderer>();
            normal = sr.color;
        }
        catch
        {
            //do nothing
        }
        invisible = normal;
        invisible.a = 0;
        player = GameObject.FindWithTag("Player");
        currentRoom = transform.parent.gameObject.transform.parent.gameObject;
        try
        {
            aiPath = gameObject.GetComponent<AIPath>();
            aiPath.endReachedDistance = playerDistance;
            aiPath.maxSpeed = speed;
        }
        catch
        {
            aiPath = null;
        }
    }

    // Update is called once per frame
    void Update() {
        timer -= Time.deltaTime;
        invulnerable -= Time.deltaTime;
       /* 
        invisiTimer -= Time.deltaTime;  
        if(!name.Contains("Egg") && !name.Contains("Echo"))
        if (invulnerable > 0 && blink)
        {
            sr.color = normal;
            blink = false;
        }
        else if (invulnerable > 0 && !blink)
        {
            sr.color = invisible;
            blink = true;
        }
        else if (invulnerable < 0 && blink)
        {
            sr.color = normal;
            blink = false;
        }
        */
        if (hp <= 0)
        {
            MonsterReactionsUponDeath();
            if (destroy)
            {
                if (transform.name.Contains("Egg") == false && transform.name.Contains("Echo") == false)
                {
                    Vector3 v3 = transform.position;
                    v3.y += 1;
                    GameObject p = Instantiate(Resources.Load("Prefabs/Poof", typeof(GameObject)) as GameObject, v3, Quaternion.identity);
                    Destroy(p, 0.85f);
                }
                Destroy(gameObject);
            }
        }
        if (!active && player != null)
        {
            playerRoom = player.transform.parent.gameObject;
        }
        if(timer <= 0 && playerRoom.name == currentRoom.name && !active)
        {
            StartCoroutine(SetActive());
        }
	}

    IEnumerator SetActive()
    {
        yield return new WaitForSeconds(wait);
        try
        {
            GetComponent<AIPath>().enabled = true;
            GetComponent<Seeker>().enabled = true;
            GetComponent<AIDestinationSetter>().enabled = true;
        }
        catch
        {
            aiPath = null;
        }
        active = true;
    }

    public void RecieveDamage(float d, string name)
    {
        if (invulnerable < 0)
        {
            hp -= d;
        }
        if (name.Contains("Slash") && invulnerable < 0)
        {
            invulnerable = 0.12f;
        }
        if (name.Contains("Laser") && invulnerable < 0)
        {
            invulnerable = 0.1f;
        }

    }

    void MonsterReactionsUponDeath()
    {
        if (transform.childCount > 0)
        {
            GameObject ch = transform.GetChild(0).gameObject;
            if (ch.name == "web")
            {
                ch.transform.SetParent(currentRoom.transform);
            }
        }
        if (transform.name.Contains("Hanger"))
        {

            if (GetComponent<Hanger>().MAMA)
            {
                int num = Random.Range(3, 8);
                for (int i = 0; i < num; i++)
                {
                    GameObject gb = Instantiate(Resources.Load("Prefabs/Egg", typeof(GameObject)) as GameObject, transform.position, Quaternion.identity);
                    gb.name = "Egg";
                    gb.transform.SetParent(transform.parent);
                    //gb.GetComponent<CircleCollider2D>().isTrigger = true;
                    float x = Random.Range(-1.0f, 1.1f);
                    float y = Random.Range(-1.0f, 1.1f);
                    gb.GetComponent<Rigidbody2D>().velocity = new Vector3(x, y, 0);
                    gb.transform.localScale = gb.transform.localScale / 2;
                    gb.GetComponent<Egg>().type = 2;
                    gb.GetComponent<Egg>().timer = Random.Range(0.50f, 1.50f);
                    gb.GetComponent<MonsterStats>().timer = 0;
                    gb.GetComponent<MonsterStats>().wait = 0;
                }
            }
        }
        if (transform.name.Contains("Egg"))
        {
            GetComponent<Animator>().SetTrigger("Hatch");
            GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
            Destroy(this);
            Destroy(GetComponent<Egg>());
            Destroy(GetComponent<CircleCollider2D>());
            transform.SetParent(null);
            destroy = false;
        }
    }
}
