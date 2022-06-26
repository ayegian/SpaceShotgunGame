using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class acidvialscript : MonoBehaviour
{

    public float explodetimer = 99;
    public GameObject vial;
    public spinscript spin;
    public GameObject explosion;
    public float timeexplosionactive = .5f;
    public int damage = 1;
    public Rigidbody2D rigid;
    public bool delay;
    public float delaytimesmall;
    public float delaytimelarge;
    public float explodetime = 0;
    public bool nonexplosive;
    // Start is called before the first frame update
    void Start()
    {
        rigid = this.GetComponentInChildren<Rigidbody2D>();
        if (!nonexplosive)
        {
            explosion.SetActive(false);
        }
        //if(explodetime != 0)
        //{
        //    explodetimer = explodetime;
        //    StartCoroutine(explode());
        //}
    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("player"))
    //    {
    //        player playertemp = collision.GetComponentInChildren<player>();
    //        if (playertemp.invincible == false)
    //        {
    //            playertemp.invincible = true;
    //            playertemp.health -= damage;
    //        }
    //    }
    //}
    void OnTriggerEnter2D(Collider2D collision)
    {
        print("TRIGGER ENTER VIAL");
        if (collision.isTrigger == false)
        {
            print("TRIGGER ENTER IS COLLISION ENTER VIAL");
            StopAllCoroutines();
            rigid.velocity = Vector3.zero;
            //if (delay == true)
            //{
            //    spin.enabled = false;
            //    yield return new WaitForSeconds(Random.Range(delaytimesmall, delaytimelarge));
            //}
            if (!nonexplosive)
            {
                vial.SetActive(false);
                explosion.SetActive(true);
                explosion.transform.parent = null;
            }
            if (!nonexplosive)
            {
                Destroy(this.transform.gameObject);
            }
        }
    }    
    void OnCollisionEnter2D(Collision2D collision)
    {
        print("COllision ENTER VIAL");
        StopAllCoroutines();
        rigid.velocity = Vector3.zero;
        //if (delay == true)
        //{
        //    spin.enabled = false;
        //    yield return new WaitForSeconds(Random.Range(delaytimesmall, delaytimelarge));
        //}
        if (!nonexplosive)
        {
            vial.SetActive(false);
            explosion.SetActive(true);
        }
        if (!nonexplosive)
        {
            Destroy(gameObject);
        }
        print("EXPLODE");
    }
    public IEnumerator explode()
    {
        yield return new WaitForSeconds(explodetimer);
        rigid.velocity = Vector3.zero;
        if (delay == true)
        {
            spin.enabled = false;
            yield return new WaitForSeconds(Random.Range(delaytimesmall, delaytimelarge));
        }
        if (!nonexplosive)
        {
            vial.SetActive(false);
            explosion.SetActive(true);
        }
        yield return new WaitForSeconds(timeexplosionactive);
        if (!nonexplosive)
        {
            Destroy(gameObject);
        }
        print("EXPLODE");
    }
    public void Init(float time)
    {
        print("TIMER: " + time);
        explodetimer = time;
        print("EXPLODE TIMER: " + explodetimer);
        //rigid.velocity = Vector3.zero;
        StartCoroutine(explode());
    }
    // Update is called once per frame
    void Update()
    {

    }
}
