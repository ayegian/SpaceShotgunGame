using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyshoot3 : MonoBehaviour
{
    public bool chargerback = false;
    public GameObject laser;
    Rigidbody laserrb;
    public player Player;
    Vector3 mortarposition;
    public Renderer rend;
    public GameObject beam;
    public bool shooting;
    public enemyai AI;
    public float damage;
    public float seconds;
    public float bulletspeed;
    public float timealive;
    public float range;
    public float timeuntil;
    public bool canrotate = true;
    public int numbullets;
    public bool dontaddvelo;
    // Use this for initialization
    void Awake()
    {
        rend = laser.GetComponentInChildren<Renderer>();
        AI = this.GetComponent<enemyai>();
        Player = GameObject.FindObjectOfType<player>();
        shooting = false;
        if (this.gameObject.CompareTag("laserguy"))
        {
            range = 30;
        }
        else
        {
            range = (bulletspeed * timealive) - 4;
            print("RANGE: " + range);
        }
        timeuntil = seconds;
    }
    public IEnumerator WaittoShoot()
    {
        print("WAITING");
        shooting = true;
        if (!this.gameObject.CompareTag("charger"))
        {
            for (float i = timeuntil; i > 0; i--)
            {
                yield return new WaitForSeconds(1);
                timeuntil -= 1;
                print("Timeuntil = " + timeuntil);
            }
        }
        canrotate = false;
        if (this.gameObject.CompareTag("machinegunguy"))
        {
            this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, (this.transform.eulerAngles.y - 60), this.transform.eulerAngles.z);
            for (int i = 0; i < numbullets; i++)
            {
                shoot();
                yield return new WaitForSeconds(.3f);
                this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, (this.transform.eulerAngles.y + (120 / numbullets)), this.transform.eulerAngles.z);
            }

        }
        else
        {
            shoot();
        }
        canrotate = true;
        if (this.gameObject.CompareTag("charger"))
        {
            yield return new WaitForSeconds(seconds);
        }
        print("SHOOTING FALSE");
        shooting = false;
        timeuntil = seconds;
    }

    private void shoot()
    {
        print("FIRE");
        if (this.gameObject.CompareTag("mortarguy"))
        {
            mortarposition = new Vector3(Player.transform.position.x - Random.Range(-18, 18), Player.transform.position.y - Random.Range(-18, 18f), 10);
            beam = Instantiate(laser, mortarposition, Quaternion.identity);
        }
        else if (this.gameObject.CompareTag("charger"))
        {
            beam = Instantiate(laser, this.transform.position + (transform.right * .5f * (rend.bounds.size.y)), this.transform.rotation) /** Quaternion.Euler(0, 0, 90))*/ as GameObject;
            chargerback = true;
        }
        else if (this.gameObject.CompareTag("machinegunguy"))
        {
            beam = Instantiate(laser, this.transform.position + (1f * transform.right * (rend.bounds.size.x / 2)), this.transform.rotation) /** Quaternion.Euler(0, 0, -90))*/ as GameObject;
            laserrb = beam.GetComponent<Rigidbody>();
            laserrb.velocity = transform.right * bulletspeed;
        }
        else
        {
            beam = Instantiate(laser, this.transform.position + (1f * transform.right * (rend.bounds.size.x / 2)), this.transform.rotation) as GameObject;
        }
        if (this.gameObject.CompareTag("enemy") && dontaddvelo == false)
        {
            laserrb = beam.GetComponent<Rigidbody>();
            laserrb.velocity = transform.right * bulletspeed;
        }
    }
    // Update is called once per frame
    void Update()
    {
        print("SHOOTING= " + shooting);
        print("AIINRANGE: " + AI.inrange);
        if (shooting == false && AI.inrange == true)
        {
            shooting = true;
            print("SHOOTING");
            StartCoroutine(WaittoShoot());
        }
    }
}
