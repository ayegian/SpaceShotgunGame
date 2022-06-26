using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHead : MonoBehaviour
{
    public player Player;
    public Transform headcastpoint;

    public float timebetweenattacks;
    public int attacktype;
    public int lastattacktype;
    public Animator anim;


    public AudioSource source;
    public AudioClip[] sounds;

    public GameObject blooddrop;
    public int numblooddroplets;
    public int numtimesbloodattack;
    public float timebeforeblood;
    public float timebetweenblood;
    public float timeafterblood;
    public float bloodspeed;
    public Transform bloodpos1;
    public Transform bloodpos2;
    public Transform bloodpos3;
    public Transform bloodpos4;
    public Transform bloodpos5;
    public Transform bloodpos6;
    public Transform bloodpos7;
    public Transform bloodpos8;


    public GameObject[] barf;
    public float timebeforebarf;
    public float timebarfexpand;
    public float timeafterbarf;
    public GameObject barfinstan;
    public bool barfexpanding;

    public GameObject barf_single;
    public Transform barf_single_shootpoint;
    public float barf_rand_angle;

    public Transform barf_shootpoint;
    public GameObject[] barfs;
    public float barfs_per_second;
    public float barf_warmup_time;
    public float barf_seconds;
    public float barf_angle_range;
    public float barf_speed_high;
    public float barf_speed_low;
    public float min_scale;
    public float max_scale;

    //ADD FIFTH AND POSSIBLY SIXTH ATTACK 
    //MAYBE MAKE ARENA A CIRCLE OR IN SOME WAY CHANGE ARENA AND BOSS SO BOSS MOVES OR PLAYER CAN MOVE AOUND MORE
    // Start is called before the first frame update
    void Awake()
    {
        Player = GameObject.FindObjectOfType<player>();
        //timebetweenattacks *= FindObjectOfType<difficulty_script>().difficulty_wait_time_multipliers[FindObjectOfType<difficulty_script>().difficulty];
        attacktype = 0;
    }
    //IEnumerator twohandslam_attackone()
    //{
    //    righthand.SetActive(false);
    //    lefthand.SetActive(false);
    //    shadow.SetActive(true);
    //    shadowtrack = true;
    //    //MAKE SHADOW TRACK PLAYER OR JUST USE A DIFFERENT OBJECT LIKE THE MORTAR
    //    yield return new WaitForSeconds(timebeforeslam);
    //    shadowtrack = false;
    //    righthand.transform.position = shadow.transform.position;
    //    lefthand.transform.position = shadow.transform.position;
    //    yield return new WaitForSeconds(timestillslam);
    //    shadow.SetActive(false);
    //    righthand.SetActive(true);
    //    lefthand.SetActive(true);
    //    //move back hands
    //    yield return new WaitForSeconds(timeafterslam);
    //}
 
    ////throw rock(s) at player and once it hits wall it splits into a bunch of smaller rocks which spread out in a burst pattern.
    //IEnumerator rockthrow_attacktwo()
    //{
    //    yield return new WaitForSeconds(timebeforerock);
    //    GameObject rockinstan = Instantiate(rock, righthandthrowpoint.transform.position, Quaternion.Euler(0,0,Mathf.Atan2((Player.transform.position.y - righthandthrowpoint.transform.position.y), (Player.transform.position.x - righthandthrowpoint.transform.position.x)) * Mathf.Rad2Deg));
    //    rockinstan.GetComponent<Rigidbody2D>().velocity = rockinstan.transform.right * rockspeed;
    //    yield return new WaitForSeconds(timebetweenrocks);
    //    GameObject rockinstan2 = Instantiate(rock, lefthandthrowpoint.transform.position, Quaternion.Euler(0, 0, Mathf.Atan2((Player.transform.position.y - lefthandthrowpoint.transform.position.y), (Player.transform.position.x - lefthandthrowpoint.transform.position.x)) * Mathf.Rad2Deg));
    //    rockinstan2.GetComponent<Rigidbody2D>().velocity = rockinstan.transform.right * rockspeed;
    //    yield return new WaitForSeconds(timeafterrock);
    //}
    //BARF ATTACK CONE AOE DOESNT HIT SIDES OF STAGE
    IEnumerator barfattack_attackone()
    {
        yield return new WaitForSeconds(timebeforebarf);
        barfinstan = Instantiate(barf[Random.Range(0,2)], headcastpoint.position, Quaternion.identity, this.transform);
        barfexpanding = true;
        yield return new WaitForSeconds(timebarfexpand);
        barfexpanding = false;
        yield return new WaitForSeconds(timeafterbarf);
        Destroy(barfinstan.gameObject);
        attacktype = 0;
    }
    //SEVERAL prob 4 maybe more BLOOD DROPLETS GO TO A PART OF SCREEN 2 LEFT HAND 2 RIGHT HAND. POSITION IS CHOSEN maybe random maybe fixed THEN THEY GO DOWNWARDS
    //TRY FIXED POINT FIRST
    IEnumerator bloodattack_attacktwo()
    {
        anim.SetTrigger("blood");
        yield return new WaitForSeconds(timebeforeblood);
        for(int i = 0; i < (numtimesbloodattack/2); i++)
        {
            blooddrop.GetComponent<bloodscript>().herebeforedown = bloodpos1.position;
            GameObject bloodinstan = Instantiate(blooddrop, headcastpoint.position, Quaternion.identity);
            blooddrop.GetComponent<bloodscript>().herebeforedown = bloodpos2.position;
            bloodinstan = Instantiate(blooddrop, headcastpoint.position, Quaternion.identity);
            blooddrop.GetComponent<bloodscript>().herebeforedown = bloodpos3.position;
            bloodinstan = Instantiate(blooddrop, headcastpoint.position, Quaternion.identity);
            blooddrop.GetComponent<bloodscript>().herebeforedown = bloodpos4.position;
            bloodinstan = Instantiate(blooddrop, headcastpoint.position, Quaternion.identity);
            blooddrop.GetComponent<bloodscript>().herebeforedown = bloodpos5.position;
            bloodinstan = Instantiate(blooddrop, headcastpoint.position, Quaternion.identity);
            blooddrop.GetComponent<bloodscript>().herebeforedown = bloodpos6.position;
            bloodinstan = Instantiate(blooddrop, headcastpoint.position, Quaternion.identity);
            blooddrop.GetComponent<bloodscript>().herebeforedown = bloodpos7.position;
            bloodinstan = Instantiate(blooddrop, headcastpoint.position, Quaternion.identity);
            blooddrop.GetComponent<bloodscript>().herebeforedown = bloodpos8.position;
            bloodinstan = Instantiate(blooddrop, headcastpoint.position, Quaternion.identity);
            yield return new WaitForSeconds(timebetweenblood);
        }
        yield return new WaitForSeconds(timeafterblood);
        attacktype = 0;
    }
    //IEnumerator fistpunch_attackfour()
    //{
    //    print("FIST PUNCH");
    //    yield return new WaitForSeconds(timebeforepunch);
    //    print("FIST PUNCH SECONDS: "+ (Vector2.Distance(Player.transform.position, righthand.transform.position) + 10) / punchspeed);
    //    righthandrigid.velocity = punchspeed *  Vector3.Normalize(new Vector2(Player.transform.position.x - righthand.transform.position.x, Player.transform.position.y - righthand.transform.position.y));
    //    yield return new WaitForSeconds((Vector2.Distance(Player.transform.position, righthand.transform.position)+10)/punchspeed);
    //    righthandmovingback = true;
    //}
    void buttons()
    {
        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    StartCoroutine(twohandslam_attackone());
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha2))
        //{
        //    StartCoroutine(rockthrow_attacktwo());
        //}
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            print("BARF");
            source.clip = sounds[0];
            source.Play();
            StartCoroutine(bloodattack_attacktwo());
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            print("BARF 2");
            source.clip = sounds[1];
            source.Play();
            StartCoroutine(barf_try_three());
        }
        //if (Input.GetKeyDown(KeyCode.Alpha4))
        //{
        //    StartCoroutine(fistpunch_attackfour());
        //}
    }
    public IEnumerator barf_trytwo()
    {
        yield return new WaitForSeconds(2f);
        for (int i = 0; i <= 1000; i++)
        {
            for(int j = 0; j<1; j++)
            {
                Instantiate(barf_single, barf_single_shootpoint.position, barf_single_shootpoint.rotation * Quaternion.Euler(0, 0, Random.Range(-1 * barf_rand_angle, barf_rand_angle)));
            }
            yield return new WaitForSeconds(.002f);
        }
        attacktype = 0;
        //this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, this.transform.eulerAngles.y, 0);
        print("MINIGUN DONE");
        
    }
    public IEnumerator barf_try_three()
    {
        anim.SetTrigger("barf");
        yield return new WaitForSeconds(barf_warmup_time);
        int barfs_done = 0;
        while (barfs_done < (barfs_per_second * barf_seconds))
        {
            GameObject barf = Instantiate(barfs[Random.Range(0, barfs.Length)], barf_shootpoint.transform.position, this.transform.rotation * Quaternion.Euler(0, 0, barf_shootpoint.transform.eulerAngles.z - Random.Range(-1 * barf_angle_range, barf_angle_range)));
            float scale = Random.Range(min_scale, max_scale);
            barf.transform.localScale = new Vector3(scale, scale, 1);
            barf.GetComponent<Rigidbody2D>().velocity = barf.transform.up * -1 * Random.Range(barf_speed_low, barf_speed_high);

            barfs_done += 1;
            yield return new WaitForSeconds(1 / barfs_per_second);
        }

    }
    public IEnumerator betweenattacks()
    {
        attacktype = 999;
        yield return new WaitForSeconds(timebetweenattacks);
        attacktype = Random.Range(1, 3);
        //DO ONCE/IF I GET THREE OR MORE ATTACKS
        //while(attacktype == lastattacktype)
        //{
        //    attacktype = Random.Range(1, 2);
        //}
        //lastattacktype = attacktype;
        if(attacktype == 1)
        {
            StartCoroutine(barf_try_three());
        }
        if(attacktype == 2)
        {
            StartCoroutine(bloodattack_attacktwo());
        }
        source.clip = sounds[attacktype-1];
        source.Play();
    }
    // Update is called once per frame
    void Update()
    {
        buttons();
        //if(attacktype == 0)
        //{
        //    StartCoroutine(betweenattacks());
        //}
        if (barfexpanding)
        {
            print("AMOUNT SCALE UP: " + (1 / timebarfexpand) * Time.deltaTime);
            barfinstan.transform.localScale = new Vector3(barfinstan.transform.localScale.x + ((1 / timebarfexpand) * Time.deltaTime), barfinstan.transform.localScale.y + ((1 / timebarfexpand) * Time.deltaTime), 1);
        }
    }
}
