using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ontriggerdamage : MonoBehaviour
{
    public int damage;
    public bool destroyaftertime;
    public float destroytime;
    public bool destroyoncollide;
    public bool dontdestroyontag;
    public string dontdestroytag;
    public bool onlydestroyonwall;
    // Start is called before the first frame update
    void Start()
    {
        if (destroyaftertime)
        {
            StartCoroutine(destroyobj());
        }
    }
    IEnumerator destroyobj()
    {
        yield return new WaitForSeconds(destroytime);
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            collision.GetComponentInChildren<player>().health -= damage;
        }
        if (onlydestroyonwall)
        {
            if (collision.CompareTag("wall"))
            {
                Destroy(gameObject);
            }
        }
        else if (collision.isTrigger == false&&destroyoncollide == true)
        {
            if(dontdestroyontag == false)
            {
                print("THIS OBJ: "+this.gameObject.name+"DESTROY OBJ: " + collision.gameObject.name);
                //Time.timeScale = 0;
                Destroy(gameObject);
            }
            else
            {
                if (collision.gameObject.CompareTag(dontdestroytag) == false)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("player"))
        {
            other.GetComponentInChildren<player>().health -= damage;
        }
        if (onlydestroyonwall)
        {
            if (other.CompareTag("wall"))
            {
                Destroy(gameObject);
            }
        }
        else if (other.isTrigger == false && destroyoncollide == true)
        {
            print("DESTROYER NAME: " + other.name);
            if (dontdestroyontag == false)
            {
                Destroy(gameObject);
            }
            else
            {
                if (other.gameObject.CompareTag(dontdestroytag) == false)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
