using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPbar : MonoBehaviour {

    // Use this for initialization
    public int totalHP;
    public int currentHP;
    public float totalWidth;
    public float currentWidth;
    public float color;

	void Start () {
        try
        {
            totalHP = CharacterStats.total_hp;
            currentHP = CharacterStats.hp;
            totalWidth = transform.localScale.x;
            currentWidth = transform.localScale.x;
        }
        catch
        {
            //do nothing;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (totalHP == 0)
        {
            totalHP = CharacterStats.total_hp;
            currentHP = CharacterStats.hp;
        }
        else
        {
            color = 1.7f / (float)totalHP;
            currentHP = CharacterStats.hp;
            transform.localScale = new Vector3(((totalWidth / totalHP) * currentHP), transform.localScale.y, transform.localScale.z);
            GetComponent<SpriteRenderer>().color = new Color((float)(color * currentHP), 0, 0);
        }
    
    }
}
