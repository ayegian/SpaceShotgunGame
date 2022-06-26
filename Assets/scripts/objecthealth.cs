using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objecthealth : MonoBehaviour
{
    public float health;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        print("TRIGGER");
        if (other.gameObject.CompareTag("slug"))
        {
            health--;
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("slug"))
        {
            health--;
            if(health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
