using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisiondamage : MonoBehaviour
{
    public float damage;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            collision.gameObject.GetComponent<player>().health -= damage;
            //collision.gameObject.GetComponent<player>().invincible = true; 
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            collision.gameObject.GetComponent<player>().health -= damage;
            //collision.gameObject.GetComponent<player>().invincible = true; 
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
