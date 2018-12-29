﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour {

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


    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
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
    }
}
