using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{

    GameObject E;
    public string name;
    bool active;
    Vector3 v3;
    bool moving = false;
    float timer = 0;
    // Use this for initialization
    void Start()
    {
        Vector3 v2 = new Vector3();
        v2.x = 0;
        v2.y = 0;
        transform.localScale = v2;
        E = transform.Find("e").gameObject;
        name = gameObject.name;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (transform.localScale.x < 1f)
        {
            transform.localScale += new Vector3(0.025f, 0.025f, 0);
        }
        if (transform.localScale.x >= 1f && !moving)
        {
            moving = true;
            Vector3 v1 = new Vector3();
            v1.y = -0.2f;
            GetComponent<Rigidbody2D>().velocity = v1;
            timer = 1;
        }
        if (moving && timer < 0)
        {
            Vector3 v1 = new Vector3();
            GetComponent<Rigidbody2D>().velocity = v1;
        }
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.E) && active)
        {
            //Open Chest
            //Play animation
            E.SetActive(false);
            ItemEffects(gameObject.name);
            Destroy(gameObject);
        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            E.SetActive(true);
            active = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            E.SetActive(false);
            active = false;
        }
    }

    void ItemEffects(string name)
    {
        switch (name)
        {
            case "Apple":
                CharacterStats.hp += 25;
                CharacterStats.total_hp += 25;
                break;
            case "Pear":
                CharacterStats.energy += 25;
                CharacterStats.total_energy += 25;
                break;
            case "Enegy_drink":
                CharacterStats.energy += 100;
                CharacterStats.total_energy += 10;
                CharacterStats.speed += 0.1f;
                CharacterStats.energyRegen += 1;
                break;
            case "Pills":
                CharacterStats.hp += 15;
                CharacterStats.total_hp += 15;
                CharacterStats.energy += 15;
                CharacterStats.total_energy += 15;
                break;
            case "Running_shoes":
                CharacterStats.speed += 0.3f;
                break;
            case "Syringe":
                CharacterStats.speed += 0.2f;
                CharacterStats.damage += 2;
                break;
            case "Bandage":
                CharacterStats.hp += 15;
                CharacterStats.total_hp += 15;
                CharacterStats.damage += 3;
                break;
            case "Halo":
                CharacterStats.hp += 10;
                CharacterStats.atackSpeed -= 0.2f;
                break;
            case "Pillow":
                CharacterStats.energy += 100;
                CharacterStats.energyRegen += 2;
                CharacterStats.total_energy += 10;
                break;
            case "Wings":
                CharacterStats.speed += 0.2f;
                CharacterStats.atackSpeed -= 0.1f;
                break;
            case "Peace":
                CharacterStats.energyRegen += 3;
                break;
            case "Cross":
                CharacterStats.atackSpeed -= 0.1f;
                CharacterStats.damage += 2;
                break;
            case "Wine":
                CharacterStats.hp += 10;
                CharacterStats.total_hp += 10;
                CharacterStats.energy += 10;
                CharacterStats.total_energy += 10;
                CharacterStats.damage += 2;
                CharacterStats.speed -= 0.1f;
                break;
            case "Totem":
                CharacterStats.damage += 2;
                CharacterStats.energyRegen += 2;
                break;
            case "Rune_of_death":
                CharacterStats.damage += 7;
                CharacterStats.atackSpeed -= 0.1f;
                CharacterStats.hp -= 10;
                break;
            case "Skull":
                CharacterStats.damage += 5;
                break;
            case "Enegy_bar":
                CharacterStats.energyRegen += 2;
                CharacterStats.energy += 50;
                CharacterStats.total_energy += 10;
                break;
            case "Sugar":
                CharacterStats.atackSpeed -= 0.2f;
                CharacterStats.speed += 0.1f;
                break;
            case "Broken_bone":
                CharacterStats.damage += 5;
                CharacterStats.hp -= 15;
                break;
            case "Rune_of_life":
                CharacterStats.total_energy += 50;
                CharacterStats.total_hp += 50;
                break;
            case "Rune_of_rebirth":
                CharacterStats.total_hp += 100;
                CharacterStats.hp += 100;
                CharacterStats.atackSpeed += 0.1f;
                CharacterStats.damage -= 2;
                break;
            case "Rune_of_hell":
                CharacterStats.damage = CharacterStats.damage * 2;
                CharacterStats.total_hp = CharacterStats.total_hp / 2;
                break;
            case "Rune_of_heaven":
                CharacterStats.atackSpeed -= 0.4f;
                break;
            case "Rune_of_concentration":
                CharacterStats.energyRegen += 5;
                CharacterStats.total_energy -= 35;
                break;
            case "Rune_of_action":
                CharacterStats.atackSpeed -= 0.3f;
                CharacterStats.speed += 0.2f;
                CharacterStats.total_hp -= 35;
                break;
            case "Rune_of_pain":
                CharacterStats.damage += 6;
                CharacterStats.atackSpeed -= 0.2f;
                CharacterStats.total_hp -= 40;
                break;
            case "Rune_of_the_devil":
                CharacterStats.damage += 666;
                CharacterStats.total_hp = 1;
                break;
            case "Working_gloves":
                CharacterStats.atackSpeed -= 0.2f;
                break;
            case "Hope":
                CharacterStats.total_hp += 10;
                CharacterStats.hp += 10;
                CharacterStats.total_energy += 10;
                CharacterStats.energy += 10;
                CharacterStats.speed += 0.1f;
                CharacterStats.energyRegen += 1;
                CharacterStats.damage += 1;
                CharacterStats.atackSpeed -= 0.1f;
                break;
            case "Rune_of_range":
                CharacterStats.total_energy += 50;
                CharacterStats.energyRegen += 5;
                CharacterStats.hp -= 70;
                CharacterStats.atackSpeed -= 0.1f;
                CharacterStats.damage -= 2;
                break;
            case "Rune_of_mellee":
                CharacterStats.total_hp += 50;
                CharacterStats.hp += 50;
                CharacterStats.energy -= 70;
                CharacterStats.energyRegen -= 3;
                CharacterStats.damage += 10;
                CharacterStats.atackSpeed -= 0.1f;
                break;
            case "Rune_of_destruction":
                CharacterStats.damage = CharacterStats.damage * 3;
                CharacterStats.atackSpeed = CharacterStats.atackSpeed * 3;
                break;
            case "Rune_of_speed":
                CharacterStats.atackSpeed = 0.4f;
                CharacterStats.damage = CharacterStats.damage / 3;
                break;
            case "Rune_of_evasion":
                CharacterStats.speed += 0.5f;
                CharacterStats.total_hp -= 40;
                break;
            case "Dagger":
                CharacterStats.weaponType = 3;
                break;
            case "Spear":
                CharacterStats.weaponType = 2;
                break;
            case "Claw":
                CharacterStats.weaponType = 4;
                break;
            case "ScatterShot":
                CharacterStats.bulletType = 2;
                break;
            case "ClusterShot":
                CharacterStats.bulletType = 3;
                break;
            case "Sprinkler":
                CharacterStats.bulletType = 4;
                break;
            case "Missile":
                CharacterStats.bulletType = 5;
                break;
            case "Laser":
                CharacterStats.bulletType = 6;
                break;
        }
    }
}
