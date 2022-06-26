using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_towards_target : MonoBehaviour
{
    public Rigidbody2D this_rigid;
    public GameObject target;
    public float speed;
    public bool stop_on_disable;
    public bool deadzone;
    public float deadzone_dist;
    // Start is called before the first frame update
    void Start()
    {

    }
    void OnDisable()
    {
        if (stop_on_disable)
        {
            this_rigid.velocity = Vector3.zero;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(!deadzone || (target.transform.position - this.transform.position).magnitude > deadzone_dist){
            this_rigid.velocity = (target.transform.position - this.transform.position).normalized * speed;
        }
        else
        {
            this_rigid.velocity = Vector2.zero;
        }
    }
}
