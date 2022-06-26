using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spinscript3 : MonoBehaviour
{
    public float angles_per_second;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(this.transform.up, Time.deltaTime * angles_per_second);
    }
}
