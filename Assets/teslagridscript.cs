using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teslagridscript : MonoBehaviour
{
    public GameObject prefab;
    //public BoxCollider2D[] boxes;
    public CompositeCollider2D comp;
    public GameObject colliderholder;
    public Collider2D[] overlap;
    ContactFilter2D filter;
    public GameObject warninglights;
    public GameObject zaplights;
    public float gridtimeactive;
    public player Player;
    public int damage = 1;
    public bool active;
    public bool auto;
    public float auto_wait_time;
    // Start is called before the first frame update
    void Awake()
    {
        Player = GameObject.FindObjectOfType<player>();
        comp.enabled = false;
    }
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    print("TESLA COLLIDE");
    //}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("TESLA TRIGGER");
        if (collision.CompareTag("player"))
        {
            Player.health -= damage;
        }
    }
    //private void OnTriggerStay2D(Collider2D other)
    //{
    //    print("TESLA STAY");
    //}
    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    print("TESLA GRID STAY");
    //}
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    print("TESLA GRID ENTER");
    //    if (collision.CompareTag("player"))
    //    {
    //        //if(Player.invincible == false)
    //        //{
    //        Player.health -= damage;
    //        //    Player.invincible = true;
    //        //}
    //    }

    //}
    public IEnumerator activategrid()
    {
        active = true;
        for (int i = 0; i < 4; i++)
        {
            warninglights.SetActive(true);
            yield return new WaitForSeconds(.2f);
            warninglights.SetActive(false);
            yield return new WaitForSeconds(.2f);
        }
        for(int i = 0; i<8; i++)
        {
            warninglights.SetActive(true);
            yield return new WaitForSeconds(.05f);
            warninglights.SetActive(false);
            yield return new WaitForSeconds(.05f);
        }
        zaplights.SetActive(true);
        print("COMP ENABLED");
        //comp.enabled = true;
        colliderholder.SetActive(true);
        yield return new WaitForSeconds(gridtimeactive);
        colliderholder.SetActive(false);
        comp.enabled = false;
        zaplights.SetActive(false);
        active = false;
    }
    public IEnumerator wait_activate() {
        active = true;
        yield return new WaitForSeconds(auto_wait_time);
        StartCoroutine("activategrid"); 
    }
    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.K))
        //{
        //    StartCoroutine(activategrid());
        //}
        //if (Input.GetKeyDown(KeyCode.J))
        //{
        //    StopAllCoroutines();
        //}
        if (auto && active == false) {
            StartCoroutine("wait_activate");
        }
        if(comp.enabled == true)
        {
            print("ENABLED");
        }
        else
        {
            print("DISABLED");
        }
        int f = comp.OverlapCollider(filter, overlap);
        foreach(Collider2D a in overlap)
        {
            print("COLLIDER NAME: " + a.transform.name);
        }
    }
}
