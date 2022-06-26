using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossrightside : MonoBehaviour {
    public player Player;
    public Rigidbody2D PlayerRB;
    // Use this for initialization
    void Start()
    {
        Player = FindObjectOfType<player>();
        PlayerRB = Player.GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            Vector2 left = this.transform.right * 1;
            Vector2 down = this.transform.up * -1;
            print("HAPPENING");
            PlayerRB.velocity = left * 7.5f + down * 7.5f;
            print(PlayerRB.velocity);
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
