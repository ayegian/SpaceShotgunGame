using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simpleenemyai: enemyai {
    public enemyshoot enemyshoot;
    private enemyscript enemy;
    public LayerMask mask;
    public float range;
    Vector2 playerpos;
    public int random;
    private player Player;
    Vector2 playersize;
    RaycastHit2D hit2;
    RaycastHit2D hit;
    public SpriteRenderer playerrend;
    public float movespeed;
    public float rotatespeed;
    public float randommove;
   
    
	// Use this for initialization
	void Start () {
        if (this.gameObject.CompareTag("charger"))
        {
            enemyshoot = this.GetComponent<enemyshoot>();
            range = 1;
        }
        else
        {
            enemyshoot = this.GetComponent<enemyshoot>();
            range = (enemyshoot.bulletspeed * enemyshoot.timealive)-7;
            print(range);
        }
        inrange = false;
        enemy = this.GetComponent<enemyscript>();
        Player = FindObjectOfType<player>();
        playerrend = Player.GetComponent<SpriteRenderer>();
        playersize = new Vector2(playerrend.size.x / 2, playerrend.size.y / 2);
        print(playersize);
        mask = LayerMask.GetMask("Default");
        print(mask);
	}
	// Update is called once per frame
	void Update () {
        playerpos = Player.transform.position;
        hit2 = Physics2D.Raycast(this.transform.position + transform.right * 1.2f + transform.up * .5f, transform.right, range + 1, mask);
        hit = Physics2D.Raycast(this.transform.position+ transform.right*1.2f-transform.up*.5f, transform.right, range + 1, mask);
        if ((Mathf.Abs(hit.point.x - playerpos.x) <= 1) && (Mathf.Abs(hit.point.y - playerpos.y) <= 1)&& (Mathf.Abs(hit2.point.x - playerpos.x) <= 1) && (Mathf.Abs(hit2.point.y - playerpos.y) <= 1))
        {
            inrange = true;
        }
        else
        {
            inrange = false;
        }
        Debug.DrawLine(this.transform.position + transform.right * 1.2f + transform.up * .5f, hit.point, Color.magenta);
        Debug.DrawLine(this.transform.position+transform.right*1.2f-transform.up*.5f, hit.point, Color.magenta);
    }
}
