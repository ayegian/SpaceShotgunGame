using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playergetshotlaser : MonoBehaviour {
    public player Player;
    public enemyshoot enemy;
    public float damage;
    public float seconds;
    public bool stayintactaftercollision;
    public bool destroy_on_boss;
    // Use this for initialization
    void Awake () {
        Player = FindObjectOfType<player>();
        if(enemy != null)
        {
            seconds = enemy.timealive;
            StartCoroutine(timealive());
        }
	}
    IEnumerator timealive()
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
        print("DESTROYED");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("COLLIDE: "+collision.gameObject.name+" TAG: "+collision.gameObject.tag);
        if (collision.gameObject.CompareTag("wall") || collision.gameObject.CompareTag("mutant")|| collision.gameObject.CompareTag("enemy")|| (destroy_on_boss && collision.gameObject.CompareTag("boss")))
        {
            print("HIT: "+ collision.gameObject.tag);
            if(stayintactaftercollision == false)
            {
                print("DESTROY");
                Destroy(gameObject);
            }
        }
        if (collision.gameObject.CompareTag("player"))
        {
            if(enemy != null)
            {
                //if (Player.invincible == false)
                //{
                    Player.health = Player.health - enemy.damage;
                //    Player.invincible = true;
                //}
                if (!enemy.gameObject.CompareTag("charger"))
                {
                    if (stayintactaftercollision == false)
                    {
                        Destroy(gameObject);
                    }
                }
            }
            else
            {
                //if (Player.invincible == false)
                //{
                    print("DAMAGE: " + damage);
                    Player.health = Player.health - damage;
                //    Player.invincible = true;
                //}
                if (stayintactaftercollision == false)
                {
                    Destroy(gameObject);
                }
            }
            if (stayintactaftercollision == false)
            {
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("wall") || collision.gameObject.CompareTag("mutant") || collision.gameObject.CompareTag("enemy") || (destroy_on_boss && collision.gameObject.CompareTag("boss")))
        {
            if (stayintactaftercollision == false)
            {
                print("DESTROYER: " + collision.tag + collision.name);
                Destroy(gameObject);
            }
        }
        if (collision.gameObject.CompareTag("player"))
        {
            if (enemy != null)
            {
                //if (Player.invincible == false)
                //{
                Player.health = Player.health - enemy.damage;
                //    Player.invincible = true;
                //}
                if (!enemy.gameObject.CompareTag("charger"))
                {
                    if (stayintactaftercollision == false)
                    {
                        Destroy(gameObject);
                    }
                }
            }
            else
            {
                //if (Player.invincible == false)
                //{
                print("DAMAGE: " + damage);
                Player.health = Player.health - damage;
                //    Player.invincible = true;
                //}
                if (stayintactaftercollision == false)
                {
                    Destroy(gameObject);
                }
            }
            if (stayintactaftercollision == false)
            {
                Destroy(gameObject);
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(stayintactaftercollision == false)
        {
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}
