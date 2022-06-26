using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fire : MonoBehaviour {
    public player Player;
    public int damage;
    public float timealive;
	// Use this for initialization
	void Start () {
        Player = FindObjectOfType<player>();
        StartCoroutine(timeactive());
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("player")/* && Player.invincible == false*/)
        {
            Player.health -= damage;
        }
    }
    public IEnumerator timeactive()
    {
        yield return new WaitForSeconds(timealive);
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update () {
		
	}
}
