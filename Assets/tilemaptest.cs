using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tilemaptest : MonoBehaviour
{
    public Collider2D collider1;
    List<Collider2D> list = new List<Collider2D>();
    ContactFilter2D filter;
    // Start is called before the first frame update
    void Start()
    {
        filter = new ContactFilter2D();   
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("HIT COLLIDER TILEMAP");
    }

    // Update is called once per frame
    void Update()
    {
        collider1.OverlapCollider(filter, list);
        foreach(Collider2D a in list)
        {
            print("A COLLIDED: " + a.name);
        }
        if(list.Count == 0)
        {
            print("NOTHING COLLIDED");
        }
    }
}
