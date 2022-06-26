using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosionscript : MonoBehaviour {
    public player Player;
    public float damage;
    public SpriteRenderer explosion;
	// Use this for initialization
	void Start () {
        explosion.gameObject.SetActive(true);
        Player = FindObjectOfType<player>();
        StartCoroutine(destroythis());
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            Player.health -= damage;
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            Player.health -= damage;
        }
    }
    IEnumerator destroythis()
    {
        yield return new WaitForSeconds(.2f);
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update () {
        //this.transform.position = this.transform.position + (transform.up*.2f);
	}
}
