using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashEffect : MonoBehaviour {

    // Use this for initialization
    float timer = 0.1f;
    //public GameObject destroyedEffect;

    void Start()
    {
        Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = pz - transform.position;
        direction.Normalize();
        GetComponent<Rigidbody2D>().velocity = direction * 0.1f;

    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            if (collision.gameObject.GetComponent<MonsterStats>().active == true)
            {
                float damage = 0;
                switch (CharacterStats.weaponType)
                {
                    case 1:
                        damage = CharacterStats.damage;
                        break;
                    case 2:
                        damage = CharacterStats.damage;
                        break;
                    case 3:
                        damage = CharacterStats.damage * 0.8f;
                        break;
                    case 4:
                        damage = CharacterStats.damage * 0.9f;
                        break;
                }
                collision.gameObject.GetComponent<MonsterStats>().hp -= damage;
                CharacterStats.energy += CharacterStats.energyRegen;
                if(CharacterStats.energy > CharacterStats.total_energy)
                {
                    CharacterStats.energy = CharacterStats.total_energy;
                }
            }
        }
    }
}
