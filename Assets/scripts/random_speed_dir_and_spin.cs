using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class random_speed_dir_and_spin : MonoBehaviour
{
    public float min_speed;
    public float max_speed;
    public float min_velo_angle;
    public float max_velo_angle;
    public float min_spin_speed;
    public float max_spin_speed;
    public Rigidbody2D this_rigid;
    // Start is called before the first frame update
    void Start()
    {
        float rand_angle = Random.Range(min_velo_angle, max_velo_angle) + this.transform.eulerAngles.z;
        print("RAND SPIN ANGLE: " + rand_angle);
        rand_angle *= Mathf.Deg2Rad;
        print("RAND SPIN ANGLE RAD: " + rand_angle);
        Vector2 dir = new Vector2(Mathf.Cos(rand_angle), Mathf.Sin(rand_angle));
        print("RAND SPIN DIR: " + dir);
        this_rigid.velocity = dir * Random.Range(min_speed, max_speed);
        this_rigid.angularVelocity = Random.Range(-1, 2) * Random.Range(min_spin_speed, max_spin_speed);
        print("RAND SPIN ANGULAR VELO: " + this_rigid.angularVelocity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
