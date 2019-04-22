using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vase : MonoBehaviour {

    Animator anim;
    public GameObject blob;
    public GameObject bigBlob;
    public GameObject phantom;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        string o = other.gameObject.tag;
        if (o == "Bullet" || o == "Bullet" || o == "Slash")
        {
            anim.SetTrigger("break");
            StartCoroutine(Spawn(0.3f));
            Destroy(gameObject.GetComponent<BoxCollider2D>());
            Destroy(this, 0.5f);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        string o = other.gameObject.tag;
        if (o == "Bullet" || o == "Bullet" || o == "Slash")
        {
            anim.SetTrigger("break");
            StartCoroutine(Spawn(0.3f));
            Destroy(gameObject.GetComponent<BoxCollider2D>());
            Destroy(this, 0.5f);
        }
    }

    IEnumerator Spawn(float f)
    {
        yield return new WaitForSeconds(f);
        int r = Random.Range(1, 101);
        if (r >= 75 && r < 85)
        {
            GameObject b1 = Instantiate(blob, transform.position, Quaternion.identity);
            b1.transform.SetParent(transform.parent.transform.parent.transform.Find("Enemies"));
            b1.GetComponent<AIPath>().enabled = true;
            b1.GetComponent<Seeker>().enabled = true;
            b1.GetComponent<AIDestinationSetter>().enabled = true;
            b1.GetComponent<AIPath>().maxSpeed = blob.GetComponent<MonsterStats>().speed;
        }
        else if (r >= 85 && r < 95)
        {
            GameObject b1 = Instantiate(phantom, transform.position, Quaternion.identity);
            b1.transform.SetParent(transform.parent.transform.parent.transform.Find("Enemies"));
        }
        else if (r >= 95 && r <= 99)
        {
            GameObject b1 = Instantiate(bigBlob, transform.position, Quaternion.identity);
            b1.transform.SetParent(transform.parent.transform.parent.transform.Find("Enemies"));
            b1.GetComponent<AIPath>().enabled = true;
            b1.GetComponent<Seeker>().enabled = true;
            b1.GetComponent<AIDestinationSetter>().enabled = true;
            b1.GetComponent<AIPath>().maxSpeed = blob.GetComponent<MonsterStats>().speed;
        }
        else if(r == 100)
        {
            int i = Random.Range(0, CharacterStats.all_items.Count);
            string item = CharacterStats.all_items[i];
            CharacterStats.all_items.RemoveAt(i);
            GameObject g = Instantiate(Resources.Load("Prefabs/Items/" + item, typeof(GameObject)) as GameObject, transform.position, Quaternion.identity);
            g.name = item;
        }
    }
}
