using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class specialai : simpleenemyai {
    public float range;
    public bool inrange;
    public player Player;
    public float distoplayer;
    // Use this for initialization
    void Start () {
        Player = FindObjectOfType<player>();
		
	}
	
	// Update is called once per frame
	void Update () {
        distoplayer = Mathf.Sqrt(Mathf.Abs((((this.transform.position.x - Player.transform.position.x) * (this.transform.position.x - Player.transform.position.x)) + ((this.transform.position.y - Player.transform.position.y) * (this.transform.position.y - Player.transform.position.y)))));
        if(range> distoplayer)
        {
            inrange = true;
        }
        else
        {
            inrange = false;
        }
	}
}
