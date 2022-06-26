using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class border_script : MonoBehaviour
{
    public float border_right;
    public float border_left;
    public float border_up;
    public float border_down;
    public float wall_width;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    void border()
    {
        if (transform.position.x > border_right)
        {
            transform.position = new Vector3(border_right, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < border_left)
        {
            transform.position = new Vector3(border_left, transform.position.y, transform.position.z);
        }
        if (transform.position.y > border_up)
        {
            transform.position = new Vector3(transform.position.x, border_up, transform.position.z);
        }
        else if (transform.position.y < border_down)
        {
            transform.position = new Vector3(transform.position.x, border_down, transform.position.z);
        }
    }
    // Update is called once per frame
    void Update()
    {
        border();
    }
}
