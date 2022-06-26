using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followobject : MonoBehaviour
{
    public GameObject followthis;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = followthis.transform.position;
    }
}
