using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using UnityEngine;

public class boss2ai : MonoBehaviour
{
    public bossstuff bosshealth;
    public int attacktype;
    public int lastattacktype;
    public float timebetweenattacks;
    public float rangenormal;
    public float range;
    public bool inrange = true;
    public LayerMask mask;
    public player player;
    public Transform playerpos;
    public boss2navmesh navmesh;
    public Rigidbody2D bossrigid;
    public GameObject sword;
    Vector3 swordstartpos;
    public float startangle;
    public float slashangle;
    public float slashtime;
    public bool slashing;
    public float chargetime;
    public bool ischarging;
    public float maxspeed;
    public float minspeed;
    public bool runningatplayer = false;
    public float runtime;
    public GameObject regularenemy;
    public GameObject phase_2_regularenemy;
    public float timetospawn;
    public float mgblastbackforce;
    public GameObject mgbullet;
    public float mgbulletspeed;
    public int mgtimesshoot;
    public float mgstartshootangle;
    public GameObject laser;
    public GameObject large_laser_warning;
    public GameObject large_laser;
    public GameObject no_sound_laser;
    public Transform large_laser_shoot_pos;
    public float laser_rotate_speed;
    public float laserspeed;
    public float timetospin;
    public float numspinattacks;    
    public float phase_2_timetospin;
    public float phase_2_numspinattacks;
    public bool spinning = false;
    public bool canmove;
    public bool canrotate;
    public bool using_large_laser;
    public float large_laser_time;
    public GameObject _40mm;
    public GameObject phase_2_40mm;
    public float speed40;
    public int timesshoot40;
    public int phase_2_timesshoot40;
    public int phase_2_num_per_volley;
    public int phase_2_spread_degrees;
    public float timebetween40shots;
    public float phase_2_timebetween40shots;
    public GameObject mortar;
    public GameObject missile;
    public GameObject spawnedobject;
    public Rigidbody2D spawnedobjectrigid;
    RaycastHit2D hit;
    RaycastHit2D hit2;
    public AudioSource transformsound;
    public AudioSource mgburstsound;
    public AudioSource missilesiren;
    public AudioSource beforecharge;
    public AudioSource beforelaser;
    public AudioSource beforegrenade;
    public AudioSource beforespawn;
    public bool alreadytransformed;


    //ATTACKS AND CHANGES
    //need change for spinning attack
    //P1: charge (no backblast), turret (slow shoot spread), grenade (no airburst), spinning(less attacks)
    //P2: charge (backblast), turret(fast single), grende (airburst), spinning(more attacks), mortar + missile(may change)
    // Start is called before the first frame update
    void Awake()
    {
        range = rangenormal;
        alreadytransformed = false;
        canmove = true;
        canrotate = true;
        player = GameObject.FindObjectOfType<player>();
        playerpos = player.gameObject.transform;
        bossrigid = this.GetComponent<Rigidbody2D>();
        swordstartpos = sword.transform.localPosition;
        sword.SetActive(false);
        timebetweenattacks *= FindObjectOfType<difficulty_script>().difficulty_wait_time_multipliers[FindObjectOfType<difficulty_script>().difficulty];
        attacktype = 0;
    }
    public IEnumerator betweenattacks()
    {
        attacktype = 999;
        print("BETWEEN");
        yield return new WaitForSeconds(timebetweenattacks/2);
        if (bosshealth.bosshealth > 50)
        {
            attacktype = Random.Range(1, 5);
        }
        else
        {
            attacktype = Random.Range(1, 6);
            print(attacktype);
            print("HEALTHLOW");
        }
        while (attacktype == lastattacktype)
        {
            print("working");
            if (bosshealth.bosshealth > 50)
            {
                attacktype = Random.Range(1, 5);
            }
            else
            {
                attacktype = Random.Range(1, 6);
            }
        }
        print("ATTACK TYPE: " + attacktype);
        lastattacktype = attacktype;
        if (attacktype == 1)
        {
            beforecharge.Play();
            yield return new WaitForSeconds(timebetweenattacks / 2);
            //StartCoroutine(attacktwo());
            StartCoroutine(attackone());
        }
        else if (attacktype == 2)
        {
            beforelaser.Play();
            yield return new WaitForSeconds(timebetweenattacks / 2);
            StartCoroutine(attacktwo());
        }
        else if (attacktype == 3)
        {
            beforegrenade.Play();
            yield return new WaitForSeconds(timebetweenattacks / 2);
            StartCoroutine(attackthree());
        }
        else if (attacktype == 4)
        {
            beforespawn.Play();
            yield return new WaitForSeconds(timebetweenattacks / 2);
            StartCoroutine(attackfour());
        }
        else if (attacktype == 5)
        {
            missilesiren.Play();
            yield return new WaitForSeconds(timebetweenattacks / 2);
            StartCoroutine(attackfive());
        }
    }
    IEnumerator attackone()
    {
        range = 2;
        inrange = false;
        ischarging = true;
        runningatplayer = true;
        yield return new WaitForSeconds(chargetime);
        range = rangenormal;
        ischarging = false;
        runningatplayer = false;
        print("REACHED END OF ATTACK ONE");
        if (bosshealth.bosshealth <= 50 && attacktype != 0)
        {
            StartCoroutine(mgburst());
        }
        else
        {
            if(attacktype != 0)
            {
                attacktype = 0;
            }
        }
    }
    public IEnumerator attacktwo()
    {
        canmove = false;
        canrotate = false;
        spinning = true;
        if(bosshealth.bosshealth > 50)
        {
            for (int i = 0; i < numspinattacks; i++)
            {
                Fire(new Vector3(0, 0, this.transform.rotation.eulerAngles.z), laserspeed, 0, laser);
                yield return new WaitForSeconds(timetospin / numspinattacks);
            }
        }
        else
        {
            for (int i = 0; i < phase_2_numspinattacks; i++)
            {
                if(i%2 == 0)
                {
                    Fire(new Vector3(0, 0, this.transform.rotation.eulerAngles.z), laserspeed, 0, laser);
                }
                else
                {
                    Fire(new Vector3(0, 0, this.transform.rotation.eulerAngles.z), laserspeed, 0, no_sound_laser);
                }
                yield return new WaitForSeconds(phase_2_timetospin / phase_2_numspinattacks);
            }
        }
        spinning = false;
        canmove = true;
        canrotate = true;
        attacktype = 0;
    }
    public IEnumerator attackthree()
    {
        canmove = false;
        canrotate = true;
        if(bosshealth.bosshealth> 50)
        {
            for (int i = 0; i < timesshoot40; i++)
            {
                print("SHOOT GRENADE");
                Fire(new Vector3(0, 0, this.transform.rotation.eulerAngles.z), speed40, 0, _40mm, 1.2f);
                yield return new WaitForSeconds(timebetween40shots);
            }
        }
        else
        {
            for (int i = 0; i < phase_2_timesshoot40; i++)
            {
                print("SHOOT GRENADE");
                for(int j = 0; j<phase_2_num_per_volley; j++)
                {
                    Fire(new Vector3(0, 0, this.transform.eulerAngles.z - (phase_2_spread_degrees * ((phase_2_num_per_volley - 1) / 2)) + phase_2_spread_degrees * j), speed40, 0, phase_2_40mm, 1.2f);
                }
                yield return new WaitForSeconds(phase_2_timebetween40shots);
            }
        }
        canrotate = true;
        canmove = true;
        attacktype = 0;
    }
    IEnumerator attackfour()
    {
        yield return new WaitForSeconds(timetospawn);
        if(bosshealth.bosshealth > 50)
        {
            Instantiate(regularenemy, this.transform.position + this.transform.up, Quaternion.identity);
        }
        else
        {
            Instantiate(phase_2_regularenemy, this.transform.position + this.transform.up, Quaternion.identity);
        }
        attacktype = 0;
    }
    public IEnumerator attackfive()
    {
        print("ATTACK 5");
        for (int i = 0; i < 15; i++)
        {
            print("MORTAR");
            Vector3 pos = new Vector3(Random.Range(-40, 40), Random.Range(-40f, 40f), -10);
            Instantiate(mortar, pos, Quaternion.identity);
            yield return new WaitForSeconds(.1f);
        }
        for (int i = 0; i <= 3; i++)
        {
            Instantiate(missile, this.transform.position + transform.up * 1.5f, Quaternion.Euler(new Vector3(0, 0, this.transform.rotation.eulerAngles.z-90)));
            Instantiate(missile, this.transform.position - transform.up * 1.5f, Quaternion.Euler(new Vector3(0, 0, this.transform.rotation.eulerAngles.z-90)));
            yield return new WaitForSeconds(.5f);
        }
        attacktype = 0;
    }
    public IEnumerator attackfive_redo()
    {
        bool temp_rot = canrotate;
        canrotate = false;
        bool temp_move = canmove;
        canmove = false;
        //SHOOTS MISSLES OUT OF BACKPACK FOR ANIM MORTARS ARE ACTUALLM MISSILES FALLING FROM SKY, GET SECOND SPRITE AND SHOOTS LARGE LASER OUT OF CHEST WHILE TRACKING PLAYER
        //WARM UP ANIM SHIT
        GameObject warning_large_laser_instan = Instantiate(large_laser_warning, large_laser_shoot_pos.position, large_laser_shoot_pos.rotation) as GameObject;
        warning_large_laser_instan.transform.parent = large_laser_shoot_pos.transform;
        using_large_laser = true;

        print("ATTACK 5");

        for (int i = 0; i < 15; i++)
        {
            print("MORTAR");
            Vector3 pos = new Vector3(Random.Range(-40, 40), Random.Range(-40f, 40f), -10);
            Instantiate(mortar, pos, Quaternion.identity);
            yield return new WaitForSeconds(.1f);
        }
        GameObject large_laser_instan = Instantiate(large_laser, large_laser_shoot_pos.position, large_laser_shoot_pos.rotation) as GameObject;
        large_laser_instan.transform.parent = large_laser_shoot_pos.transform;
        Destroy(warning_large_laser_instan);
        print("LARGE LASER START");
        yield return new WaitForSeconds(large_laser_time);
        print("LARGE LASER DONE");
        Destroy(large_laser_instan);
        using_large_laser = false;
        canmove = temp_move;
        canrotate = temp_rot;
        attacktype = 0;
    }
    void Fire(Vector3 shootangle, float speed, float backblastforce,GameObject shootobject = null, float rightmult = 1)
    {
        print("SHOOTANGLE: " + shootangle);
        Renderer rend = shootobject.GetComponentInChildren<SpriteRenderer>();
        Vector3 currentvelo = bossrigid.velocity;
        bossrigid.velocity = currentvelo - (transform.right * backblastforce);
        spawnedobject = Instantiate(shootobject, this.transform.position + (transform.right * rightmult * .7f * (rend.bounds.size.y)), Quaternion.Euler(shootangle)) as GameObject;
        //spawnedobject = Instantiate(mgbullet, this.transform.position + new Vector3(0, 0, 10) + transform.right * 2, Quaternion.identity) as GameObject;
        spawnedobjectrigid = spawnedobject.GetComponent<Rigidbody2D>();
        spawnedobjectrigid.velocity = spawnedobject.transform.right * speed;
    }
    public IEnumerator mgburst()
    {
        mgburstsound.Play();
        float mgshootangle = mgstartshootangle;
        float changeangle = mgstartshootangle / mgtimesshoot;
        for (int i = 0; i < mgtimesshoot; i++)
        {
            print("MG SHOOT ANGLE: " + mgshootangle);
            Fire(new Vector3(0, 0, this.transform.rotation.eulerAngles.z) + new Vector3(0, 0, mgshootangle), mgbulletspeed, mgblastbackforce, mgbullet);
            Fire(new Vector3(0, 0, this.transform.rotation.eulerAngles.z) - new Vector3(0, 0, mgshootangle), mgbulletspeed, mgblastbackforce, mgbullet);
            mgshootangle -= changeangle;
            yield return new WaitForSeconds(.1f);
        }
        yield return new WaitForSeconds(.7f);
        if(attacktype != 0)
        {
            attacktype = 0;
        }
    }
    public void swordslash()
    {
        //sword.transform.Rotate(this.transform.forward, (Time.deltaTime * slashangle) / slashtime);
        sword.transform.RotateAround(this.transform.position, Vector3.forward, (Time.deltaTime * slashangle) / slashtime);
    }
    public IEnumerator startslash()
    {
        print("STOP ATTACK ONE");
        //StopCoroutine(attackone());
        sword.SetActive(true);
        slashing = true;
        ischarging = false;
        runningatplayer = false;
        yield return new WaitForSeconds(slashtime);
        if(bosshealth.bosshealth <= 50)
        {
            StartCoroutine(mgburst());
        }
        else if(attacktype != 0)
        {
            navmesh.startrunaway();
            attacktype = 0;
        }
        range = rangenormal;
        slashing = false;
        sword.SetActive(false);
        sword.transform.localEulerAngles = new Vector3(0, 0, startangle);
        sword.transform.localPosition = swordstartpos;
    }
    void buttontest()
    {
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            StartCoroutine(attackfive_redo());
        }
    }
    // Update is called once per frame
    void Update()
    {
        buttontest();
        if (using_large_laser)
        {
            Vector3 diff = player.transform.position - transform.position;
            diff.Normalize();
            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            rot_z = rot_z - transform.eulerAngles.z;
            float negnum = 1;
            if (rot_z < -360)
            {
                rot_z += 360;
            }
            if (rot_z > -180 && rot_z < 0)
            {
                negnum = -1;
            }
            print("ROTATE Z ABS: " + Mathf.Abs(rot_z)+" LASER ROT: "+(laser_rotate_speed*Time.deltaTime));
            if (Mathf.Abs(rot_z) <= laser_rotate_speed * Time.deltaTime)
            {
                print("ROTZ LESS");
                //print("ROTZ: " + rot_z);
                rot_z = Mathf.Abs(rot_z);
            }
            else
            {
                print("ROTZ MORE");
                rot_z = laser_rotate_speed;
            }
            transform.RotateAround(transform.forward, rot_z * Mathf.Deg2Rad * Time.deltaTime * negnum);
            print("ROTATE Z: " +rot_z);
        }
        if(alreadytransformed == false && bosshealth.bosshealth <= 50)
        {
            transformsound.Play();
        }
        if (inrange == true && ischarging == true)
        {
            print("IS CHARGING");
            StopAllCoroutines();
            StartCoroutine(startslash());
        }
        if (slashing == true)
        {
            swordslash();
        }
        if (runningatplayer == true)
        {
            if (navmesh.agent.speed < maxspeed)
            {
                navmesh.agent.speed += Time.deltaTime * (maxspeed - minspeed);
            }
        }
        if (spinning == true)
        {
            if(bosshealth.bosshealth > 50)
            {
                this.transform.Rotate(Vector3.forward, (Time.deltaTime * 360) / timetospin);
            }
            else
            {
                this.transform.Rotate(Vector3.forward, (Time.deltaTime * 360) / phase_2_timetospin);
            }
        }
        UnityEngine.Debug.DrawRay(this.transform.position + transform.right * 1.2f + transform.up * .5f, transform.right * 10);
        UnityEngine.Debug.DrawRay(this.transform.position + transform.right * 1.2f - transform.up * .5f, transform.right * 10);
        if (canrotate == true)
        {
            this.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2((player.transform.position.y - this.transform.position.y), (player.transform.position.x - this.transform.position.x)) * Mathf.Rad2Deg);
        }
        if (canmove == true)
        {
            hit2 = Physics2D.Raycast(this.transform.position + transform.right * 1f + transform.up * .3f, transform.right, range + 1, mask);
            hit = Physics2D.Raycast(this.transform.position + transform.right * 1f - transform.up * .3f, transform.right, range + 1, mask);
            UnityEngine.Debug.DrawRay(this.transform.position + transform.right * 1f - transform.up * .3f, transform.right * 1000, Color.magenta);
            UnityEngine.Debug.DrawRay(this.transform.position + transform.right * 1f + transform.up * .3f, transform.right * 1000, Color.magenta);
            print("HIT 1: " + hit.collider.gameObject.name);
            print(" HIT 2: " + hit2.collider.gameObject.name);
            if ((Mathf.Abs(hit.point.x - playerpos.position.x) <= 1) && (Mathf.Abs(hit.point.y - playerpos.position.y) <= 1) && (Mathf.Abs(hit2.point.x - playerpos.position.x) <= 1) && (Mathf.Abs(hit2.point.y - playerpos.position.y) <= 1))
            {
                inrange = true;
                print("IN RANGE");
            }
            else
            {
                print(" HIT POINT: " + hit.point.ToString() + " PLAYER POS: " + playerpos.position.ToString());
                inrange = false;
                print("NOT IN RANGE");
            }
        }
        else
        {
            inrange = true;
        }
        if (attacktype == 0)
        {
           //StartCoroutine(betweenattacks());
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
           // Time.timeScale = 1;
        }
    }
}








//void runatplayer()
//{
//    runningatplayer = true;
//}
//void Fire(Vector3 shootangle, float speedF, float backblastforce, GameObject shootobject = null)
//{
//    print("SHOOTANGLE: " + shootangle);
//    Renderer rend = shootobject.GetComponent<SpriteRenderer>();
//    Vector3 currentvelo = bossrigid.velocity;
//    bossrigid.velocity = currentvelo - (transform.right * backblastforce);
//    spawnedobject = Instantiate(shootobject, this.transform.position + (transform.right * .5f * (rend.bounds.size.y)), Quaternion.Euler(shootangle)) as GameObject;
//    //spawnedobject = Instantiate(mgbullet, this.transform.position + new Vector3(0, 0, 10) + transform.right * 2, Quaternion.identity) as GameObject;
//    spawnedobjectrigid = spawnedobject.GetComponent<Rigidbody2D>();
//    spawnedobjectrigid.velocity = spawnedobject.transform.up * speed;
//}
////2
//if (Input.GetKeyDown(KeyCode.I))
//{
//    StartCoroutine(attackfive());
//}
//if (Input.GetKeyDown(KeyCode.O))
//{
//    inrange = false;
//}
////1
//if (Input.GetKeyDown(KeyCode.P))
//{
//    StartCoroutine(attackfour());
//}
////3
//if (Input.GetKeyDown(KeyCode.LeftBracket))
//{
//    StartCoroutine(attacktwo());
//}
////4
//if (Input.GetKeyDown(KeyCode.RightBracket))
//{
//    StartCoroutine(attackone());
//}
////5
//if (Input.GetKeyDown(KeyCode.Backslash))
//{
//    StartCoroutine(attackthree());
//}
//this.transform.LookAt(player.transform);