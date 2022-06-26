using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.InteropServices;
using UnityEngine;
public class bossai : MonoBehaviour {
    Vector2 right;
    Vector3 pos;
    public player Player;
    public Transform left_arm_shootpoint;
    public Transform right_arm_shootpoint;
    public Transform left_arm_rotpoint;
    public Transform right_arm_rotpoint;
    public float arm_angles_per_second;
    public float arm_time_rot_stage_one;
    public float arm_rot_wait_time;
    public float arm_time_rot_stage_two;
    public float arm_time_rot_stage_three;
    public bool arm_rotating;
    public int rotating_mag;
    public Transform missiles_left_shootpos;
    public Transform missiles_right_shootpos;
    public bool shooting_right;
    public bool shooting_left;
    public float laser_rps;
    public Animator body_anim;
    public Animator left_arm_anim;
    public Animator right_arm_anim;
    public GameObject flameball;
    public float flameball_rps;
    public float flameball_offset;
    public bossstuff health;
    public float betweenattacktime;
    public GameObject fire;
    public GameObject missile;
    public GameObject bullet;
    //public bool rot_to_player= false;
    public GameObject beam;
    public GameObject laser;
    public GameObject mortar;
    public Rigidbody2D beamrb;
    Rigidbody2D rigid;
    public bool betweenattacks;
    public float minigunrandangle;
    public float minigun_angles_per_second_attacking;
    public float minigun_angles_per_second_not_attacking;
    public bool minigun_rot_attack;
    public bool minigun_rot_not_attack;
    public float movetimeright;
    public float movetimeleft;
    public float movespeed;
    public int attacktype;
    public float attacktime;
    public bool moveright = false;
    public bool moveleft = false;
    public bool coroutineattackactive = false;
    public bool coroutinemoveactive = false;
    float movetimetotal;
    int lastattacktype = 0;
    public float bulletspeed;
    public Rigidbody2D bossrigid;
    public AudioSource transformsound;
    public AudioSource missilesiren;
    public AudioSource beforelaser;
    public AudioSource beforebullet;
    public AudioSource beforefire;
    public AudioSource minigunfire;
    public bool alreadytransformed;

    // Use this for initialization
    void Start() {
        health = this.GetComponent<bossstuff>();
        bulletspeed = 12;
        betweenattacktime *= FindObjectOfType<difficulty_script>().difficulty_wait_time_multipliers[FindObjectOfType<difficulty_script>().difficulty];
        attacktype = 0;
        rigid = this.GetComponent<Rigidbody2D>();
        Player = FindObjectOfType<player>();


        //body_anim.SetBool("mortar", true);
        //body_anim.SetBool("mortar done", true);

        //body_anim.SetBool("idle", false);
        //left_arm_anim.SetBool("idle", false);
        //left_arm_anim.SetBool("idle", false);
        //body_anim.SetBool("flamethrower", true);
        //left_arm_anim.SetBool("flamethrower", true);
        //right_arm_anim.SetBool("flamethrower", true);
        //if (body_anim.GetBool("laser"))
        //{
        //    print("LASERING");
        //}

    }

    IEnumerator betweenphases()
    {
        attacktype = 999;
        print("BETWEEN");
        yield return new WaitForSeconds(betweenattacktime/2);
        attacktype = Random.Range(1, 5);
        while (attacktype == lastattacktype)
        {
            attacktype = Random.Range(1, 5);
        }
        print("ATTACK TYPE: "+attacktype);
        lastattacktype = attacktype;
        if(attacktype == 1)
        {
            body_anim.SetBool("idle", false);
            left_arm_anim.SetBool("idle", false);
            right_arm_anim.SetBool("idle", false);
            body_anim.SetBool("laser", true);
            left_arm_anim.SetBool("laser", true);
            right_arm_anim.SetBool("laser", true);
            //if (body_anim.GetBool("laser"))
            //{
            //    print("LASERING");
            //}
            beforelaser.Play();
            yield return new WaitForSeconds(betweenattacktime / 2);
            beforelaser.Stop();
            StartCoroutine(move());
           // StartCoroutine(attackone());
        }
        if (attacktype == 2)
        {
            body_anim.SetBool("idle", false);
            body_anim.SetBool("mortar", true);
            missilesiren.Play();
            yield return new WaitForSeconds(betweenattacktime / 2);
            StartCoroutine(attacktwo());
        }
        if (attacktype == 3)
        {
            print("MINIGUN");
            body_anim.SetBool("idle", false);
            body_anim.SetBool("minigun", true);
            beforebullet.Play();
            minigun_rot_attack = true;
            yield return new WaitForSeconds(betweenattacktime / 2);
            StartCoroutine(attackthree());
        }
        if (attacktype == 4)
        {
            beforefire.Play();
            body_anim.SetBool("idle", false);
            left_arm_anim.SetBool("idle", false);
            right_arm_anim.SetBool("idle", false);
            body_anim.SetBool("flamethrower", true);
            left_arm_anim.SetBool("flamethrower", true);
            right_arm_anim.SetBool("flamethrower", true);        
            yield return new WaitForSeconds(betweenattacktime / 2);
            StartCoroutine(flamethrower_try());
        }
    }
    IEnumerator attackone()
    {
        movetimeright = 1.3f;
        movetimeleft = 2.6f;
        movetimetotal = ((2 * movetimeright) + movetimeleft);
        //movespeed = .3f;
        bulletspeed = 12;
        attacktime = (((5 * (movetimetotal)) + 6)/1.5f);
        print("ATTACK TIME: " + attacktime);
        coroutineattackactive = true;
        yield return new WaitForSeconds(.3f);
        for (float i = attacktime; i >= 0; i--)
        {
            shootlaserdown();
            yield return new WaitForSeconds(.3f);
        }
        attacktype = 0;
        coroutineattackactive = false;
    }
    IEnumerator shoot_lasers_left()
    {
        while (shooting_left)
        {
            beam = Instantiate(laser, left_arm_shootpoint.transform.position, left_arm_shootpoint.transform.rotation) as GameObject;
            beamrb = beam.GetComponent<Rigidbody2D>();
            beamrb.velocity = transform.up * -1 * bulletspeed;
            yield return new WaitForSeconds(1 / laser_rps);
        }
    }    
    IEnumerator shoot_lasers_right()
    {
        while (shooting_right)
        {
            beam = Instantiate(laser, right_arm_shootpoint.transform.position, right_arm_shootpoint.transform.rotation) as GameObject;
            beamrb = beam.GetComponent<Rigidbody2D>();
            beamrb.velocity = transform.up * -1 * bulletspeed;
            yield return new WaitForSeconds(1 / laser_rps);
        }
    }
    void shootlaserdown()
    {
        beam = Instantiate(laser, this.transform.position, this.transform.rotation) as GameObject;
        beamrb = beam.GetComponent<Rigidbody2D>();
        beamrb.velocity = transform.up * -1 * bulletspeed;
    }
    void shootbulletdown()
    {
        beam = Instantiate(bullet, this.transform.position, this.transform.rotation*Quaternion.Euler(0, 0, Random.Range(-1*minigunrandangle, minigunrandangle))) as GameObject;
        beamrb = beam.GetComponent<Rigidbody2D>();
        beamrb.velocity = beamrb.transform.up * -1 * bulletspeed;
    }
    void missilesright()
    {
        Instantiate(missile, missiles_right_shootpos.position, missiles_right_shootpos.rotation);
    }
    void missilesleft()
    {
        Instantiate(missile, missiles_left_shootpos.position, missiles_left_shootpos.rotation);
    }
    //MINIGUN ATTACK
    IEnumerator attackthree()
    {
        bulletspeed = 25;
        coroutineattackactive = true;
        yield return new WaitForSeconds(2f);
        beforebullet.Stop();
        minigunfire.loop = true;
        minigunfire.Play();
        for (int i = 0; i<= 100; i++)
        {
            shootbulletdown();
            yield return new WaitForSeconds(.02f);
        }
        minigunfire.Stop();
        coroutineattackactive = false;
        attacktype = 0;
        //this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, this.transform.eulerAngles.y, 0);
        print("MINIGUN DONE");
        minigun_rot_attack = false;
        minigun_rot_not_attack = true;
        body_anim.SetBool("minigun", false);
        body_anim.SetBool("idle", true);
    }
    //MISSILE AND MORTAR ATTACK
    IEnumerator attacktwo()
    {
        body_anim.SetBool("mortar done", true);
        coroutineattackactive = true;
        for (int i = 0; i < 10; i++)
        {
            pos = new Vector3(Random.Range(-83, -37f), Random.Range(-27f, 10f), -10);
            Instantiate(mortar, pos, this.transform.rotation);
            yield return new WaitForSeconds(.1f);
        }
        for (int i = 0; i < 2; i++)
        {
            missilesleft();
            missilesright();
            yield return new WaitForSeconds(.4f);
        }
        attacktype = 0;
        coroutineattackactive = false;
        missilesiren.Stop();
        body_anim.SetBool("mortar", false);
        body_anim.SetBool("mortardone", false);
        body_anim.SetBool("idle", true);
    }
    IEnumerator flamethrower_try()
    {
        left_arm_anim.SetBool("flamethrower happening", true);
        right_arm_anim.SetBool("flamethrower happening", true);
        left_arm_anim.SetBool("flamethrower", false);
        right_arm_anim.SetBool("flamethrower", false);
        arm_rotating = true;
        rotating_mag = 1;
        yield return new WaitForSeconds(arm_time_rot_stage_one);
        arm_rotating = false;
        yield return new WaitForSeconds(arm_rot_wait_time);
        arm_rotating = true;
        beforefire.Stop();
        StartCoroutine(flame_ball_shoot());
        rotating_mag = -1;
        yield return new WaitForSeconds(arm_time_rot_stage_two);
        rotating_mag = 1;
        yield return new WaitForSeconds(arm_time_rot_stage_three);
        arm_rotating = false;
        yield return new WaitForEndOfFrame();
        left_arm_rotpoint.localEulerAngles = new Vector3(left_arm_rotpoint.localEulerAngles.x, left_arm_rotpoint.localEulerAngles.y, 0);
        right_arm_rotpoint.localEulerAngles = new Vector3(right_arm_rotpoint.localEulerAngles.x, right_arm_rotpoint.localEulerAngles.y, 0);
        left_arm_anim.SetBool("flamethrower happening", false);
        right_arm_anim.SetBool("flamethrower happening", false);
        body_anim.SetBool("flamethrower", false);
        body_anim.SetBool("idle", true);
        left_arm_anim.SetBool("idle", true);
        right_arm_anim.SetBool("idle", true);
        attacktype = 0;
    }
    IEnumerator flame_ball_shoot()
    {
        while (arm_rotating)
        {
            Instantiate(flameball, left_arm_shootpoint.transform.position, left_arm_shootpoint.transform.rotation * Quaternion.Euler(0,0,flameball_offset));
            Instantiate(flameball, right_arm_shootpoint.transform.position, right_arm_shootpoint.transform.rotation * Quaternion.Euler(0, 0, flameball_offset));
            yield return new WaitForSeconds(1 / flameball_rps);
        }
    }
    //FIRE AOE
    IEnumerator attackfour()
    {
        print("FIRE");
        coroutineattackactive = true;
        yield return new WaitForSeconds(1);
        Instantiate(fire, this.transform.position+new Vector3(0,0,-15), this.transform.rotation);
        yield return new WaitForSeconds(1.5f);
        coroutineattackactive = false;
        attacktype = 0;
    }
    //IEnumerator attackfour()
    //{
    //    //print("FIRE");
    //    //coroutineattackactive = true;
    //    //yield return new WaitForSeconds(1);
    //    //Instantiate(fire, this.transform.position/*- (transform.up * 3)*/, this.transform.rotation);
    //    //yield return new WaitForSeconds(1.5f);
    //    //coroutineattackactive = false;
    //    //attacktype = 0;
    //    coroutineattackactive = true;
    //    for (int i = 0; i < 10; i++)
    //    {
    //        pos = new Vector3(Random.Range(-83, -37f), Random.Range(-27f, 10f), -10);
    //        Instantiate(fire, pos+new Vector3(0,0,-15), this.transform.rotation);
    //        yield return new WaitForSeconds(.1f);
    //    }
    //    for (int i = 0; i < 2; i++)
    //    {
    //        missilesleft();
    //        missilesright();
    //        yield return new WaitForSeconds(.4f);
    //    }
    //    attacktype = 0;
    //    coroutineattackactive = false;
    //}
    IEnumerator move()
    {
        movetimetotal = ((2 * movetimeright) + movetimeleft);
        print("MOVETIME TOTAL: " + movetimetotal);
        coroutinemoveactive = true;
        shooting_left = true;
        shooting_right = true;
        StartCoroutine(shoot_lasers_left());
        StartCoroutine(shoot_lasers_right());
        yield return new WaitForSeconds(.5f);
        shooting_left = false;
        moveright = true;
        yield return new WaitForSeconds(movetimeright);
        moveright = false;
        shooting_left = true;
        StartCoroutine(shoot_lasers_left());
        yield return new WaitForSeconds(.5f);
        shooting_right = false;
        moveleft = true;
        yield return new WaitForSeconds(movetimeleft);
        moveleft = false;
        print("MOVELEFT FALSE");
        shooting_right = true;
        StartCoroutine(shoot_lasers_right());
        yield return new WaitForSeconds(.5f);
        shooting_left = false;
        moveright = true;
        yield return new WaitForSeconds(movetimeright);
        shooting_left = true;
        StartCoroutine(shoot_lasers_left());
        moveright = false;
        yield return new WaitForSeconds(.5f);
        shooting_left = false;
        shooting_right = false;
        coroutinemoveactive = false;
        body_anim.SetBool("laser", false);
        left_arm_anim.SetBool("laser", false);
        right_arm_anim.SetBool("laser", false);
        body_anim.SetBool("idle", true);
        left_arm_anim.SetBool("idle", true);
        right_arm_anim.SetBool("idle", true);
        attacktype = 0;
    }
    void testbuttons()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            StartCoroutine(move());
        }        
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            StartCoroutine(flamethrower_try());
        }        
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            missilesleft();
            missilesright();
        }
    }
	// Update is called once per frame
	void Update() {
       // testbuttons();
        Debug.DrawLine(new Vector3(-83, 0, 0), new Vector3(-34, 0, 0));
        Debug.DrawLine(new Vector3(0, -28.5f, 0), new Vector3(0, 12, 0));
        print("MOVELEFT: " + moveleft + " MOVERIGHT: " + moveright);
        Vector3 Move = this.transform.position;
        if(attacktype == 0)
        {
           StartCoroutine(betweenphases());
        }

        if (minigun_rot_attack)
        {
            float normalize = Mathf.Atan2((Player.transform.position.y - this.transform.position.y), (Player.transform.position.x - this.transform.position.x));
            ////);
            normalize *= Mathf.Rad2Deg;
            normalize += 90;
            normalize -= transform.eulerAngles.z;
            if (normalize <= -180)
            {
                normalize += 360;
            }
            else if(normalize > 180)
            {
                normalize -= 360;
            }
           // print("MINIGUN ANGLE: " + normalize + " MINI MINUS CURR: " + printnum);
            if (normalize >= 0)
            {
                normalize = 1;
            }
            else
            {
                normalize = -1;
            }
            print("MINIGUN ANGLE PART 2: " + normalize);
            normalize *= minigun_angles_per_second_attacking;
            normalize *= Mathf.Deg2Rad;
            transform.RotateAround(transform.forward, normalize * Time.deltaTime);
        }
        else if (minigun_rot_not_attack)
        {
            //int rot_mag = 1;
            //if (transform.eulerAngles.z > 0)
            //{
            //    rot_mag = 1;
            //}
            //else
            //{
            //    rot_mag = 0;
            //}
            float rot_comp = minigun_angles_per_second_not_attacking * Time.deltaTime;
            float rot_comp2 = transform.eulerAngles.z;
            if(rot_comp >= Mathf.Abs(rot_comp2))
            {
                print("FINISHED");
                transform.eulerAngles = Vector3.zero;
                minigun_rot_not_attack = false;
            }
            else
            {
                int rot_mag = 1;
                print("MINIGUN ANGLE " + transform.eulerAngles.z);
                if (transform.eulerAngles.z > 0 && transform.eulerAngles.z <= 180)
                {
                    print("ROTMAG -1");
                    rot_mag = -1;
                }
                else
                {
                    print("ROTMAG 1");
                    rot_mag = 1;
                }
                transform.RotateAround(transform.forward, rot_comp * rot_mag*Mathf.Deg2Rad);
            }
        }
        //if (Input.GetKeyDown(KeyCode.U))
        //{
        //    attacktype = 1;
        //    StartCoroutine(attackone());
        //    StartCoroutine(move());
        //}
        //if (alreadytransformed == false && health.bosshealth <= 50)
        //{
        //    transformsound.Play();
        //}
        //if (attacktype == 1 && coroutineattackactive == false)
        //{
        //    //movetimeright = 1.3f;
        //    //movetimeleft = 2.6f;
        //    //movespeed = .3f;
        //    print("MOVE");
        //    StartCoroutine(move());
        //    StartCoroutine(attackone());
        //}
        //if(attacktype == 2&& coroutineattackactive == false)
        //{
        //    StartCoroutine(attacktwo());
        //}
        //&& menuscript.Menuscript.ispaused == false
        if (moveright == true)
        {
            print("MOVE RIGHT");
            bossrigid.velocity = -1 * new Vector2(movespeed, 0);
            //this.transform.position = Move - transform.right * movespeed;
        }
        // && menuscript.Menuscript.ispaused == false
        if (moveleft == true)
        {
            print("MOVE LEFT");
            bossrigid.velocity = new Vector2(movespeed, 0);
            //this.transform.position = Move + transform.right * movespeed;
        }
        if(moveleft == false && moveright == false)
        {
            bossrigid.velocity = Vector2.zero;
        }
        //if(attacktype == 3 && coroutineattackactive == false)
        //{
        //    StartCoroutine(attackthree());
        //}
        //if(attacktype == 3)
        //{
        //    this.transform.eulerAngles = new Vector3(0, 0, (Mathf.Atan2((Player.transform.position.y - this.transform.position.y), (Player.transform.position.x - this.transform.position.x)) * Mathf.Rad2Deg)+90);
        //}
        if (arm_rotating)
        {
            left_arm_rotpoint.RotateAround(left_arm_rotpoint.forward, Time.deltaTime * arm_angles_per_second*rotating_mag*Mathf.Deg2Rad);
            right_arm_rotpoint.RotateAround(right_arm_rotpoint.forward, Time.deltaTime * arm_angles_per_second*rotating_mag*Mathf.Deg2Rad*-1);
        }
        //if(attacktype == 4 && coroutineattackactive == false)
        //{
        //    StartCoroutine(attackfour());
        //}
        //if(attacktype == 5 && coroutineattackactive == false)
        //{
        //    StartCoroutine(attackfive());
        //}
      }
}
