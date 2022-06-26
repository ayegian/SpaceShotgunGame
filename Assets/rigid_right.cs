using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rigid_right : MonoBehaviour
{
    public Rigidbody2D this_rigid;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        this_rigid.velocity = this_rigid.transform.right * speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
