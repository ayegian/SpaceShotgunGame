using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyshoot2 : MonoBehaviour
{
    public bool canmove;
    public bool chargerback = false;
    public float laseradjust;
    public Transform shootpoint;
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
    public bool multshots = false;
    public float multanglerange;
    public int numbulletsmult;
    public float timebetweenbulletmult;
    public bool dontaddvelo;
    public bool stillwhileshoot;
    public float timestill;
    // Use this for initialization
    public void Awake()
    {
        if(laser != null)
        {
            rend = laser.GetComponentInChildren<Renderer>();
        }
        AI = this.GetComponent<enemyai>();
        Player = GameObject.FindObjectOfType<player>();
        shooting = false;
        if (this.gameObject.CompareTag("laserguy"))
        {
            range = 30;
        }
        else if(range == 0)
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
        if (multshots)
        {
            print("CAN ROT FALSE")
;            canrotate = false;
            this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, (this.transform.eulerAngles.y - (multanglerange / 2)), this.transform.eulerAngles.z);
            for (int i = 0; i < numbulletsmult; i++)
            {
                print("BULLET NUM I: " + (i + 1));
                shoot();
                yield return new WaitForSeconds(timebetweenbulletmult);
                print("TIME WAIT DONE");
                this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, (this.transform.eulerAngles.y + (multanglerange / numbulletsmult)), this.transform.eulerAngles.z);
            }
            print("CAN ROT TRUE");
            canrotate = true;
        }
        else if (this.gameObject.CompareTag("charger"))
        {
            yield return new WaitForSeconds(seconds);
        }
        else
        {
            shoot();
        }
        print("SHOOTING FALSE");
        shooting = false;
        timeuntil = seconds;
    }

    private void shoot()
    {
        print("SHOOTPOINT EULER Y" + shootpoint.eulerAngles.y + " SHOOTPOINT EULER Z: " + shootpoint.eulerAngles.z);
        GameObject proj = Instantiate(laser, shootpoint.position, Quaternion.Euler(0, shootpoint.transform.eulerAngles.y + laseradjust, 0));
        proj.GetComponent<Rigidbody>().velocity = shootpoint.right * bulletspeed;
    }
    // Update is called once per frame
    void Update()
    {
        if (shooting == false && AI.inrange == true)
        {
            shooting = true;
            print("SHOOTING");
            StartCoroutine(WaittoShoot());
        }
    }
}
//print("SHOOTING= " + shooting);
//print("AIINRANGE: " + AI.inrange);
//canrotate = false;
//if (this.gameObject.CompareTag("machinegunguy"))
//{
//    this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, (this.transform.eulerAngles.y - 60), this.transform.eulerAngles.z);
//    for (int i = 0; i < numbullets; i++)
//    {
//        shoot();
//        yield return new WaitForSeconds(.3f);
//        this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, (this.transform.eulerAngles.y + (120 / numbullets)), this.transform.eulerAngles.z);
//    }

//}