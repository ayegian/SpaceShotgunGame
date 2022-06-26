using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class specialenemyai : enemyai {
    public enemyshoot enemyshoot;
    public float range;
    Vector2 playerpos;
    Vector2 position;
    private float distancetoplayer;
    public player Player;
    private float positionx;
    private float positiony;

    // Use this for initialization
    void Start () {
        if (this.CompareTag("missileguy"))
        {
            range = 100;
            enemyshoot = this.GetComponent<enemyshoot>();
        }
        else
        {
            enemyshoot = this.GetComponent<enemyshoot>();
            range = enemyshoot.range;
            print(range);
        }
        Player = FindObjectOfType<player>();
    }
	
	// Update is called once per frame
	void Update () {
        playerpos = Player.transform.position;
        positionx = Mathf.Abs(this.transform.position.x - Player.transform.position.x);
        positiony = Mathf.Abs(this.transform.position.y - Player.transform.position.y);
        distancetoplayer = Mathf.Sqrt((positionx * positionx) + (positiony * positiony));
        position = new Vector2(Mathf.Abs(positionx), Mathf.Abs(positiony));
        if (distancetoplayer < range)
        {
            inrange = true;
        }
        else
        {
            inrange = false;
        }
    }
}
