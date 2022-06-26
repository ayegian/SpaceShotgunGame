using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oncollisiondestroy : MonoBehaviour
{
    public string[] dontdestroytags;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        bool destroy = true;
        foreach(string s in dontdestroytags)
        {
            if (collision.transform.CompareTag(s))
            {
                destroy = false;
            }
        }
        if (destroy)
        {
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
