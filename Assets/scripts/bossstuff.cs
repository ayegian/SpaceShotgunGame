using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class bossstuff : MonoBehaviour {
    public int bosshealth;
    public int last_known_health;
    public int bossmaxhealth;
    public levelmanager levelman;
    public int bossnum;
    public string bossname;
    public bool phase2;
    public int phase2healthbarrier;
    public bool multiboss = false;
    public GameObject[] otherbosses;
    public GameObject[] hurt_sprites;
    public int num_flash;
    public float between_flash;
    public AudioSource hit_hurt;
    public GameObject dead_timer;
    public GameObject dead_stuff;

    // Use this for initialization
    void Awake () {
        bossmaxhealth = bosshealth;
        last_known_health = bosshealth;
	}
    IEnumerator hurt()
    {
        hit_hurt.Play();
        for (int i = 0; i < num_flash; i++)
        {
            foreach (GameObject sprite in hurt_sprites)
            {
                sprite.SetActive(true);
            }
            yield return new WaitForSeconds(between_flash);
            foreach (GameObject sprite in hurt_sprites)
            {
                sprite.SetActive(false);
            }
            yield return new WaitForSeconds(between_flash);
        }
    }
    // Update is called once per frame
    void Update () {
        if(bosshealth < last_known_health)
        {
            StopAllCoroutines();
            StartCoroutine(hurt());
            last_known_health = bosshealth;
        }
		if(bosshealth <= 0)
        {
            dead_timer.SetActive(true);
            dead_stuff.transform.parent = null;
            dead_stuff.SetActive(true);
           // main_cam.transform.parent = null;
            Destroy(gameObject);
        }
        phase2 = bosshealth <= phase2healthbarrier;
    }
}
