using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scale_down_over_time : MonoBehaviour
{
    public float scale_down_time;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.localScale.x > 0)
        {
            this.transform.localScale -= new Vector3(1,1,1)* (1 / scale_down_time) * Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
