using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turret_script : MonoBehaviour
{
    public GameObject laser;
    public float rps;
    public bool ready_to_shoot;
    public float bulletspeed;
    public GameObject player;
    public Transform shootpoint;
    RaycastHit2D hit;
    float timer;
    public int num_projs;
    public int deg_between_projs;
    public float before_shoot_anim_time;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<player>().gameObject;
        timer = 1 / rps;
    }
    void Shoot()
    {
        timer = 1 / rps;
        ready_to_shoot = false;
        for(int j = 0; j<num_projs; j++)
        {
            GameObject beam = Instantiate(laser, shootpoint.position, Quaternion.Euler(shootpoint.eulerAngles.x, shootpoint.eulerAngles.y, shootpoint.eulerAngles.z -(deg_between_projs * ((num_projs - 1) / 2)) + deg_between_projs * j));
            Rigidbody2D laserrb = beam.GetComponent<Rigidbody2D>();
            laserrb.velocity = laserrb.transform.right * bulletspeed;
        }
        anim.SetBool("shooting", false);
    }
    // Update is called once per frame
    void Update()
    {
        this.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2((player.transform.position.y - this.transform.position.y), (player.transform.position.x - this.transform.position.x)) * Mathf.Rad2Deg);
        if (!ready_to_shoot)
        {
            timer -= Time.deltaTime;
            if(timer <= before_shoot_anim_time)
            {
                anim.SetBool("shooting", true);
            }
            if(timer <= 0)
            {
                ready_to_shoot = true;
            }
        }
        else 
        {
            hit = Physics2D.Raycast(shootpoint.transform.position, shootpoint.transform.right, 1000);
            Debug.DrawRay(shootpoint.transform.position, shootpoint.transform.right * 1000);
            if (hit.collider.gameObject.CompareTag("player"))
                Shoot();
            else
            {
                print("TURRET RAYCAST: " + hit.collider.gameObject.tag+" NAME: "+hit.collider.gameObject.name);
            }
        }
    }
}
