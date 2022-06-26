using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simpleenemyai2 : enemyai
{
    public bool nosightneeded;
    public enemyshoot2 enemyshoot;
    private enemyscript2 enemy;
    public LayerMask mask;
    public float range;
    Vector3 playerpos;
    public int random;
    private player Player;
    Vector2 playersize;
    RaycastHit hit2;
    RaycastHit hit;
    public SpriteRenderer playerrend;
    public float movespeed;
    public float rotatespeed;
    public float randommove;


    // Use this for initialization
    void Start()
    {
        if (this.gameObject.CompareTag("charger"))
        {
            enemyshoot = this.GetComponent<enemyshoot2>();
            range = 1;
        }
        else
        {
            enemyshoot = this.GetComponent<enemyshoot2>();
            range = enemyshoot.range;
        }
        inrange = false;
        enemy = this.GetComponent<enemyscript2>();
        Player = FindObjectOfType<player>();
        playerrend = Player.GetComponent<SpriteRenderer>();
        playersize = new Vector2(playerrend.size.x / 2, playerrend.size.y / 2);
        print(playersize);
        mask = LayerMask.GetMask("Default");
        print(mask);
    }
    // Update is called once per frame
    void Update()
    {
        playerpos = Player.transform.position;
        if (nosightneeded == false)
        {
            Physics.SphereCast(this.transform.position, .5f, this.transform.right, out hit, range, mask);
            if (hit.collider != null && hit.collider.gameObject.CompareTag("player"))
            {
                print("INRANGE TRUE: " + range + " DIST: " + Vector3.Distance(this.transform.position, Player.transform.position));
                inrange = true;
            }
            else
            {
                inrange = false;
            }
        }
        else
        {
            inrange = (Vector3.Distance(this.transform.position, playerpos) <= range);
        }
    }
}
//print("DIST TO PLAYER: " + Vector3.Distance(this.transform.position, Player.transform.position)+ "THIS POS: "+this.transform.position + " THIS ROOT POS: "+this.transform.root.position+" PLAYER POS: "+Player.transform.position);        //print("DIST TO PLAYER: " + Vector3.Distance(this.transform.position, Player.transform.position)+ "THIS POS: "+this.transform.position + " THIS ROOT POS: "+this.transform.root.position+" PLAYER POS: "+Player.transform.position);
//if ((Mathf.Abs(hit.point.x - playerpos.x) <= 1) && (Mathf.Abs(hit.point.y - playerpos.y) <= 1) && (Mathf.Abs(hit.point.z - playerpos.z) <= 1))
//{
//    print("INRANGE TRUE: " + range + " DIST: " + Vector3.Distance(this.transform.position, Player.transform.position));
//    inrange = true;
//}
//hit2 = Physics2D.Raycast(this.transform.position + transform.right * 1.2f + transform.up * .5f, transform.right, range + 1, mask);
//hit = Physics2D.Raycast(this.transform.position + transform.right * 1.2f - transform.up * .5f, transform.right, range + 1, mask);
//if(hit.collider != null)
//{
//    print("THIS NAME: "+this.transform.root.name+"OBJ HIT: " + hit.collider.gameObject.name+ " HIT POINT: "+hit.point+" PLAYER POS: "+playerpos);
//}
//Debug.DrawLine(this.transform.position + transform.right * 1.2f + transform.up * .5f, hit.point, Color.magenta);
//Debug.DrawLine(this.transform.position + transform.right * 1.2f - transform.up * .5f, hit.point, Color.magenta);