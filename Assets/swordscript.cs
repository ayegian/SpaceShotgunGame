using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordscript : MonoBehaviour
{
    // Start is called before the first frame update
    public float damage;
    public player Player;
    void Awake()
    {
        Player = FindObjectOfType<player>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            //if (Player.invincible == false)
            //{
                Player.health = Player.health - damage;
                //Player.invincible = true;
            //}
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
