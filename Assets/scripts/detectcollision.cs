using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detectcollision : MonoBehaviour {
    public bool collided;
	// Use this for initialization
	void Start () {
		
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("player")== false || collision.CompareTag("slug")== false)
        {
            print("AKJOINEF");
            collided = true;
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}
