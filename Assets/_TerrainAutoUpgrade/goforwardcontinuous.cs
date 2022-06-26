using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goforwardcontinuous : MonoBehaviour
{
    public Rigidbody thisrigid;
    public float speed;
    public bool randspeed;
    public float lowspeed;
    public float highspeed;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (randspeed)
        {
            thisrigid.velocity = Random.Range(lowspeed, highspeed) * thisrigid.transform.forward;
        }
        else
        {
            thisrigid.velocity = speed * thisrigid.transform.forward;
        }
    }
}
