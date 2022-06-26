using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class piranhascript : MonoBehaviour
{
    //CHANGE UP FROM MISSILE, MAYBE ADD IN BASIC AI LIKE THE MUTANT 
    player Player;
    public int damage;
    public int adjust;
    // Use this for initialization
    void Start()
    {
        Player = GameObject.FindObjectOfType<player>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("slug"))
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("player"))
        {
            //if(Player.invincible == false)
            //{
            //    Player.invincible = true;
            Player.health -= damage;
            //GameObject a = Instantiate(explosion, this.transform.position, this.transform.rotation);
            //a.transform.parent = null;

            //}
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("wall"))
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("slug"))
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("player"))
        {
            //if(Player.invincible == false)
            //{
            //    Player.invincible = true;
            Player.health -= damage;
            //GameObject a = Instantiate(explosion, this.transform.position, this.transform.rotation);
            //a.transform.parent = null;

            //}
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("wall"))
        {
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
    }
}
