using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CharacterStats : MonoBehaviour
{

    // Use this for initialization
    public static int hp;
    public static int total_hp;
    public static int energy;
    public static int total_energy;
    public static int stamina;
    public static int total_stamina;
    public static float speed;
    public static float damage;
    public static float range;
    public static float atackSpeed;
    public static float bulletSpeed;
    public static int energyRegen;
    public static int bulletType;
    public static int weaponType;
    public static List<string> all_items;
    public static List<string> used_items;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void ResetStats()
    {
        hp = 100;
        total_hp = 100;
        energy = 100;
        total_energy = 100;
        stamina = 100;
        total_stamina = 100;
        speed = 4;
        damage = 5;
        range = 1f;
        atackSpeed = 1;
        bulletSpeed = 5;
        energyRegen = 5;
        bulletType = 1;
        weaponType = 1;
        ResetItems();
    }

    public static void ResetItems()
    {
        used_items = new List<string>();
        all_items = new List<string>();
        all_items.Add("Apple");
        all_items.Add("Pear");
        all_items.Add("Enegy_drink");
        all_items.Add("Pills");
        all_items.Add("Running_shoes");
        all_items.Add("Syringe");
        all_items.Add("Bandage");
        all_items.Add("Halo");
        all_items.Add("Pillow");
        all_items.Add("Wings");
        all_items.Add("Peace");
        all_items.Add("Cross");
        all_items.Add("Wine");
        all_items.Add("Totem");
        all_items.Add("Rune_of_death");
        all_items.Add("Skull");
        all_items.Add("Enegy_bar");
        all_items.Add("Sugar");
        all_items.Add("Broken_bone");
        all_items.Add("Rune_of_life");
        all_items.Add("Rune_of_rebirth");
        all_items.Add("Rune_of_hell");
        all_items.Add("Rune_of_heaven");
        all_items.Add("Rune_of_concentration");
        all_items.Add("Rune_of_action");
        all_items.Add("Rune_of_pain");
        all_items.Add("Rune_of_death");
        all_items.Add("Working_gloves");
        all_items.Add("Hope");
        all_items.Add("Rune_of_range");
        all_items.Add("Rune_of_mellee");
        all_items.Add("Rune_of_destruction");
        all_items.Add("Rune_of_speed");
        all_items.Add("Rune_of_evasion");
        all_items.Add("Dagger");
        all_items.Add("Spear");
        all_items.Add("Claw");
        all_items.Add("ScatterShot");
        all_items.Add("ClusterShot");
        all_items.Add("Sprinkler");
        all_items.Add("Missile");
        all_items.Add("Laser");
    }
}
