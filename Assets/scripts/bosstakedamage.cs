using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bosstakedamage : MonoBehaviour {
    public bossstuff Bosshealth;
    public player Player;
    public Rigidbody2D PlayerRB;
    bool hit;
    public bool invincible;
    // Use this for initialization
    void Start() {
        Player = FindObjectOfType<player>();
        //GetComponentInParent<bossstuff>();
        PlayerRB = Player.GetComponent<Rigidbody2D>();
	}
    private void OnCollisionEnter2D(Collision2D collision)
    {
            if (collision.gameObject.CompareTag("player"))
            {
                hit = true;
            }
     }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            hit = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("slug") && invincible == false) {
            print("BOSS tAKE DAMAGE: "+this.gameObject.name+ " COLLIDER NAME: "+collision.gameObject.name);
            Bosshealth.bosshealth -= 5;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            hit = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            hit = false;
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        print("TRIGGER ENTER");
        if (collision.gameObject.CompareTag("slug") && invincible == false)
        {
            print("BOSS tAKE DAMAGE");
            Bosshealth.bosshealth -= 5;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (hit == true/* && Player.invincible == false*/)
        {
            Player.health -= 1;
            //Player.invincible = true;
        }
    }
}
