using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemydodamage : MonoBehaviour {
    public player Player;
    public enemyshoot enemy;
    // Use this for initialization
    void Start()
    {
        Player = GameObject.FindObjectOfType<player>();
        enemy = this.GetComponent<enemyshoot>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("wall") || collision.gameObject.CompareTag("mutant"))
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("player"))
        {
            Player.health = Player.health - 1;
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
