using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour {
    public float lastknownhealth;
    public float health;
    public player Player;
    public GameObject spawn;
    public levelmanager Levelmanager;
    private bool invincible;
    public GameObject Menu;
    public bool ispaused;
    public float invincibletime;
    public AudioSource hit_sound;
    public Animator anim;
    public GameObject death_timer;
    public GameObject death_body_parts;
    public GameObject main_cam;
    // Use this for initialization
    //SOMETHING WRONG WITH ZOMBIE HAND 2 ATTACKS HAPPENING AT SAME TIME;(Why was this in player script lol, i think its solved now)
    void Awake () {
        health = FindObjectOfType<difficulty_script>().health_difficulty[FindObjectOfType<difficulty_script>().difficulty];
        lastknownhealth = health;
        Levelmanager = GameObject.FindObjectOfType<levelmanager>();
        Player = GameObject.FindObjectOfType<player>();
        invincible = false;
    }
    IEnumerator Invincible()
    {
        invincible = true;
        yield return new WaitForSeconds(invincibletime);
        invincible = false;
        //StopAllCoroutines();
    }
    // Update is called once per frame
    void Update () {
        print("SET ANIMS FALSE");
        if (lastknownhealth <= 0)
        {
            //dead_timer.SetActive(true);
            death_body_parts.transform.parent = null;
            death_body_parts.SetActive(true);
            main_cam.transform.parent = null;
            Destroy(gameObject);
        }
        if (lastknownhealth != health)
        {
            if(invincible == false)
            {
                //if(health <= 0)
                //{
                //    anim.SetBool("idle", false);
                //    anim.SetBool("dead", true);
                //}
                hit_sound.Play();
                print("DO HURT ANIM");
                anim.Play("Hurt", -1, 0);
                lastknownhealth -= 1;
                health = lastknownhealth;
                invincible = true;
                StartCoroutine(Invincible());
            }
            else if(invincible == true)
            {
                health = lastknownhealth;
            }
        }        
	}

}
