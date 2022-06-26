using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class takedamagebasic : MonoBehaviour
{
    public int health;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("slug"))
        {
            health -= 1;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
