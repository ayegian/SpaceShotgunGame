
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikescript : MonoBehaviour {
    
    public player Player;
    public enemyscript enemy;
    public bool spikehit;
	// Use this for initialization
	void Start () {
        Player = GameObject.FindObjectOfType<player>();
        
        	}
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            //spikehit = true;
            Player.health -= 1;
            //Player.invincible = true;
        }       
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            //spikehit = true;
            Player.health -= 1;
            //Player.invincible = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            //spikehit = false;
        }
    }
    // Update is called once per frame
    void Update () {
        if (spikehit == true/* && Player.invincible == false*/)
        {
            Player.health -= 5;
            //Player.invincible = true;
            print("DAMAGE");
        }
	}
}
