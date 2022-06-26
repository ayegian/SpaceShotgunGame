using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movetest : MonoBehaviour
{
    public bool move;
    public float speed;
    public Rigidbody thisrigid;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        thisrigid.velocity = thisrigid.transform.up * speed;
    }
}
