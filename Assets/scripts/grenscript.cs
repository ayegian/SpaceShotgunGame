using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grenscript : MonoBehaviour
{
    public GameObject explosion;
    public bool timed;
    public bool randtimed;
    public float randtimelow;
    public float randtimehigh;
    public float explodetime;
    public bool explodeoncol;
    public bool nonexplosive;
    public bool instanondestroy;
    public bool onlyexplodeontag;
    public bool dontexplodeontag;
    public string explodetag;
    // Start is called before the first frame update
    void Start()
    {
        if (timed)
        {
            StartCoroutine(explode());
        }        
    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (explodeoncol)
    //    {
    //        print("40 MIL COLLISION: " + collision.transform.name);
    //        GameObject a = Instantiate(explosion, this.transform.position, this.transform.rotation);
    //        a.transform.parent = null;
    //        Destroy(gameObject);
    //    }
    //}
    private void OnDestroy()
    {
        if (instanondestroy)
        {
            GameObject a = Instantiate(explosion, this.transform.position, this.transform.rotation);
            a.transform.parent = null;
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (onlyexplodeontag)
        {
            print("GREN COLLISION: " + collision.transform.name);
            if (collision.gameObject.CompareTag(explodetag))
            {
                GameObject a = Instantiate(explosion, this.transform.position, this.transform.rotation);
                a.transform.parent = null;
                Destroy(gameObject);
            }
        }
        else if (explodeoncol && collision.isTrigger == false && (!dontexplodeontag||collision.gameObject.CompareTag(explodetag)==false))
        {
            print("GREN COLLISION: " + collision.transform.name);
            if (nonexplosive == false && instanondestroy == false)
            {
                GameObject a = Instantiate(explosion, this.transform.position, this.transform.rotation);
                a.transform.parent = null;
            }
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("GREN TRIGGER ENTER");
        if (onlyexplodeontag)
        {
            print("GREN COLLISION: " + collision.transform.name);
            if (collision.gameObject.CompareTag(explodetag))
            {
                GameObject a = Instantiate(explosion, this.transform.position, this.transform.rotation);
                a.transform.parent = null;
                Destroy(gameObject);
            }
        }
        else if (explodeoncol && collision.isTrigger == false && (!dontexplodeontag || collision.gameObject.CompareTag(explodetag) == false))
        {
            print("GREN COLLISION: " + collision.transform.name);
            if (nonexplosive == false && instanondestroy == false)
            {
                GameObject a = Instantiate(explosion, this.transform.position, this.transform.rotation);
                a.transform.parent = null;
            }
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (onlyexplodeontag)
        {
            print("GREN COLLISION: " + collision.transform.name);
            if (collision.gameObject.CompareTag(explodetag))
            {
                GameObject a = Instantiate(explosion, this.transform.position, this.transform.rotation);
                a.transform.parent = null;
                Destroy(gameObject);
            }
        }
        else if (explodeoncol && (!dontexplodeontag || collision.gameObject.CompareTag(explodetag) == false))
        {
            print("GREN COLLISION: " + collision.transform.name);
            if (nonexplosive == false && instanondestroy == false)
            {
                GameObject a = Instantiate(explosion, this.transform.position, this.transform.rotation);
                a.transform.parent = null;
            }
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (onlyexplodeontag)
        {
            if (collision.gameObject.CompareTag(explodetag))
            {
                print("GREN COLLISION: " + collision.transform.name);
                GameObject a = Instantiate(explosion, this.transform.position, this.transform.rotation);
                a.transform.parent = null;
                Destroy(gameObject);
            }
        }
        else if (explodeoncol && (!dontexplodeontag || collision.gameObject.CompareTag(explodetag) == false))
        {
            print("GREN COLLISION: " + collision.transform.name);
            if(nonexplosive == false && instanondestroy == false)
            {
                GameObject a = Instantiate(explosion, this.transform.position, this.transform.rotation);
                a.transform.parent = null;
            }
            Destroy(gameObject);
        }
    }
    IEnumerator explode()
    {
        if(randtimed == true)
        {
            yield return new WaitForSeconds(Random.Range(randtimelow, randtimehigh));
        }
        else
        {
            yield return new WaitForSeconds(explodetime);
        }
        if (nonexplosive == false && instanondestroy == false)
        {
            GameObject a = Instantiate(explosion, this.transform.position, this.transform.rotation);
            a.transform.parent = null;
        }
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
