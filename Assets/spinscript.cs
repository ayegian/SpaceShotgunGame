using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spinscript : MonoBehaviour
{
    public bool pickspeed;
    public float angles_per_second;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (pickspeed)
        {
            this.transform.Rotate(this.transform.forward, Time.deltaTime * angles_per_second);
        }
        else
        {
            this.transform.Rotate(this.transform.forward, Time.deltaTime * 360);
        }
    }
}
