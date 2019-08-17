using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energybar : MonoBehaviour {

    // Use this for initialization
    //public int totalEnergy;
    //public int currentEnergy;
    public float totalWidth;
    public float currentWidth;
    public float totalHeight;
    public float currentHeight;
    public float color;

    void Start()
    {
        try
        {
            //totalEnergy = CharacterStats.total_energy;
            //currentEnergy = CharacterStats.energy;
            totalWidth = transform.localScale.x;
            currentWidth = transform.localScale.x;
            totalHeight = transform.localScale.y;
            currentHeight = transform.localScale.y;
        }
        catch
        {
            //do nothing;
        }
    }

    // Update is called once per frame
    void Update()
    {
       // if (totalEnergy == 0)
       // {
       //     totalEnergy = CharacterStats.total_energy;
       //     currentEnergy = CharacterStats.energy;
       // }
       // else
       // {
            color = (float)CharacterStats.energy / ((float)CharacterStats.total_energy - 40);
            //currentEnergy = CharacterStats.energy;
            //totalEnergy = CharacterStats.total_energy;
            if (CharacterStats.energy >= CharacterStats.total_energy)
            {
                CharacterStats.energy = CharacterStats.total_energy;
            }
            transform.localScale = new Vector3(((totalWidth / CharacterStats.total_energy) * CharacterStats.energy), ((totalHeight / CharacterStats.total_energy) * CharacterStats.energy), transform.localScale.z);
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, color);
       // }

    }
}
