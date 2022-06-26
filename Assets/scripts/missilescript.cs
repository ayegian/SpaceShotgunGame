using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class missilescript : MonoBehaviour {
    player Player;
    public int damage;
    public GameObject explosion;
	// Use this for initialization
	void Start () {
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
            GameObject a = Instantiate(explosion, this.transform.position, this.transform.rotation);
            a.transform.parent = null;

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
            GameObject a = Instantiate(explosion, this.transform.position, this.transform.rotation);
            a.transform.parent = null;

            //}
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("wall"))
        {
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update () {
        if(menuscript.Menuscript.ispaused == false)
        {
            this.transform.eulerAngles = new Vector3(0, 0, (Mathf.Atan2((Player.transform.position.y - this.transform.position.y), (Player.transform.position.x - this.transform.position.x)) * Mathf.Rad2Deg));
            this.transform.position = this.transform.position + (transform.right * .085f);
        }
    }
}
