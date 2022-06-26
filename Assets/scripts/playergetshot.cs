using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playergetshot : MonoBehaviour {
    public player Player;
    public enemyshoot enemy;
	// Use this for initialization
	void Start () {
        print("LASER APPEAR");
        Player = GameObject.FindObjectOfType<player>();
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("HAPPY");
        if(collision.gameObject.CompareTag("wall")|| collision.gameObject.CompareTag("mutant"))
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("player") /*&& Player.invincible == false*/)
        {
            Destroy(gameObject);
            //Player.invincible = true;
            print("INVINCIBLEASKSFD");
            Player.health = Player.health - enemy.damage;
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}
