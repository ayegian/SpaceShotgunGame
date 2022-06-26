using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class body_parts_rigid : MonoBehaviour
{
    public Rigidbody2D this_rigid;
    public float rand_speed_fast;
    public float rand_speed_slow;
    public float rand_angle_up;
    public float rand_angle_down;
    public bool rand_scale;
    public float rand_scale_up;
    public float rand_scale_down;
    // Start is called before the first frame update
    void Start()
    {
        if (rand_scale)
        {
            this.transform.localScale = new Vector3(Random.Range(rand_scale_up, rand_scale_down), Random.Range(rand_scale_up, rand_scale_down), 1);
        }
        GameObject trans = new GameObject();
        trans.transform.rotation = this.transform.rotation;
        float rand_angle = Random.Range(rand_angle_up, rand_angle_down);
        print("TRANS BEFORE: "+trans.transform.eulerAngles);
        trans.transform.RotateAround(trans.transform.forward, rand_angle*Mathf.Deg2Rad);
        print("TRANS AFTER: " + trans.transform.eulerAngles+" RIGHT: "+trans.transform.right);
        Debug.DrawRay(this.transform.position, trans.transform.right * 100, Color.green, 50);
        //Time.timeScale = 0;
        this_rigid.velocity = trans.transform.right * Random.Range(rand_speed_fast, rand_speed_slow);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
