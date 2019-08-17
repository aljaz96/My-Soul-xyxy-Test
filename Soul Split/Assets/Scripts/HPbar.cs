using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HPbar : MonoBehaviour {

    // Use this for initialization
   // public int totalHP;
   // public int currentHP;
    public float totalWidth;
    public float currentWidth;
    public float color;

	void Start () {
        try
        {
            //totalHP = CharacterStats.total_hp;
            //currentHP = CharacterStats.hp;
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
        if(CharacterStats.hp > CharacterStats.total_hp)
        {
            CharacterStats.hp = CharacterStats.total_hp;
        }
        color = 1.7f / (float)CharacterStats.total_hp;
        transform.localScale = new Vector3(((totalWidth / CharacterStats.total_hp) * CharacterStats.hp), transform.localScale.y, transform.localScale.z);
        GetComponent<SpriteRenderer>().color = new Color((float)(color * CharacterStats.hp), 0, 0);
    
    }

    public void PlayerDied()
    {
        GameObject camera = GameObject.Find("MainCamera");
        Camera_script cs = camera.GetComponent<Camera_script>();
        cs.DisappearB();
        StartCoroutine(LoadMenu());
    }


    public IEnumerator LoadMenu()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(0);
    }
}
