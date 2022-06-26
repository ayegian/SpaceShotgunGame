using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lineofsight : MonoBehaviour {
    public enemyscript enemy;
    public player Player;
    public Vector2 distance;
    public int hitout;
    public Transform trans;
   
  
	// Use this for initialization
	void Awake () {
        Player = GameObject.FindObjectOfType<player>();
        enemy = GetComponentInParent<enemyscript>();
	}
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("player") && Physics2D.Raycast(this.transform.position, this.transform.eulerAngles, 100f))
        {
            enemy.Aware();
        }
    }
    // Update is called once per frame
    void Update () {        
        if (Physics2D.Raycast(this.transform.position, this.transform.eulerAngles, 1f, hitout))
        {
        }
        trans = GetComponentInChildren<Transform>();
        this.transform.rotation = trans.rotation;
	}
}
