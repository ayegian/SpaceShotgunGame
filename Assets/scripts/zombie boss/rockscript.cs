using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rockscript : MonoBehaviour
{
    public GameObject smallrock;
    public float smallrockspeed;
    public int degreesbetweenrocks;
    public int damage = 1;
    public bool explode_on_death;
    public bool diff_instan_point;
    public Transform instan_point;
    public bool keep_intact;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void OnDestroy()
    {
        if (explode_on_death)
        {
            explode();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (explode_on_death == false && collision.gameObject.CompareTag("player"))
        {
            collision.gameObject.GetComponent<player>().health -= damage;
            //collision.gameObject.GetComponent<player>().invincible = true;
        } 
        explode();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.isTrigger == false && explode_on_death == false && collision.transform.CompareTag("boss") == false)
        {
            explode();
        }
    }
    public void timetoexplodevoid(float b)
    {
        StartCoroutine(timetoexplode(b));
    }
    public IEnumerator timetoexplode(float a)
    {
        yield return new WaitForSeconds(a);
        explode();
    }

    public void explode()
    {
        print("EXPLODE");
        for (int i = 0; i < (360/degreesbetweenrocks); i++)
        {
            GameObject rockinstan;
            if (diff_instan_point == false)
            {
                rockinstan = Instantiate(smallrock, this.transform.position, Quaternion.Euler(0, 0, degreesbetweenrocks * i));
            }
            else
            {
                rockinstan = Instantiate(smallrock, instan_point.transform.position, Quaternion.Euler(0, 0, degreesbetweenrocks * i));
            }
            //Time.timeScale = 0;
            rockinstan.GetComponent<Rigidbody2D>().velocity = rockinstan.transform.right * smallrockspeed;
        }
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
