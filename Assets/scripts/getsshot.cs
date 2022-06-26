using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getsshot : MonoBehaviour
{
    enemyscript enemy;
    public BoxCollider2D hitbox;
    // Use this for initialization
    void Start()
    {
        hitbox = this.GetComponent<BoxCollider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
    // Update is called once per frame
    void Update()
    {

    }
}
