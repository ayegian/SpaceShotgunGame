using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monkeybossai : MonoBehaviour
{
    public player Player;
    public Rigidbody2D thisrigid;
    public Transform throwpoint;

    public int attacktype;
    public int lastattacktype;
    public int smallestattacktype;
    public int largestattacktype;
    public float betweenattacktime;

    public GameObject grenade;
    public float timebeforegren;
    public float timeaftergren;
    public float grenspeed;
    public float numgrens;
    public float degreesbetweengren;

    public GameObject banana;
    public float timebetweenbananas;
    public float numbananas;
    public float timebeforebanana;
    public float timeafterbanana;
    public float bananaspeedlow;
    public float bananaspeedhigh;
    public float bananaturntimelow;
    public float bananaturntimehigh;
    public float bananabeforetimelow;
    public float bananabeforetimehigh;

    public float beforelungetime;
    public float afterlungetime;
    public float lungespeed;
    public collisiondamage colldamage;

    public float timebeforeapeshit;
    public float timewhileapeshit;
    public float timeafterapeshit;

    public GameObject slamaoe;
    public float beforeaoe;
    public float afteraoe;

    public GameObject bounceproj;
    public int numbounceproj;
    public float timebetweenbounce;
    public float timebeforebounce;
    public float timeafterbounce;

    public AudioSource source;
    public AudioClip[] sounds;
    // Start is called before the first frame update
    //MAYBE CHANGE BANANA TO TURN WHEN IT IS CLOSSE TO WALL INSTEAD OF RANDOM TIME
    //VINES MAYBe MONKEY LEAPS FROM VINE TO VINE INSTEAD OF FREE LEAP
    //Bouncing attakc and a slam close range aoe attack
    void Awake()
    {
        Player = FindObjectOfType<player>();
        betweenattacktime *= FindObjectOfType<difficulty_script>().difficulty_wait_time_multipliers[FindObjectOfType<difficulty_script>().difficulty];
        attacktype = 0;
    }
    public IEnumerator nextattack()
    {
        attacktype = 999;
        yield return new WaitForSeconds(betweenattacktime/2);
        attacktype = Random.Range(smallestattacktype, largestattacktype);
        source.clip = sounds[attacktype - 1];
        source.Play();
        yield return new WaitForSeconds(betweenattacktime / 2);
        while (attacktype == lastattacktype)
        {
            attacktype = Random.Range(smallestattacktype, largestattacktype);
        }
        lastattacktype = attacktype;
        if (attacktype == 1)
        {
            StartCoroutine(attackone_grenthrow());
        }
        if (attacktype == 2)
        {
            StartCoroutine(attacktwo_bananathrow());
        }
        if (attacktype == 3)
        {
            StartCoroutine(attackthree_lunge(true));
        }
        //if(attacktype == 6)
        //{
        //    StartCoroutine(attackfour_apeshit());
        //}
        if(attacktype == 4)
        {
            StartCoroutine(attackfive_slamattack());
        }
        if(attacktype == 5)
        {
            StartCoroutine(attacksix_bounceattack());
        }
    }
    public IEnumerator attackone_grenthrow()
    {
        print("GREN THROW");
        yield return new WaitForSeconds(timebeforegren);
        Vector3 tempangle = this.transform.eulerAngles;
        this.transform.eulerAngles = new Vector3(0, 0, (Mathf.Atan2((Player.transform.position.y - this.transform.position.y), (Player.transform.position.x - this.transform.position.x)) * Mathf.Rad2Deg)-180);
        for (int j = 0; j < numgrens; j++)
        {
            GameObject greninstan = Instantiate(grenade, throwpoint.position, throwpoint.transform.rotation * Quaternion.Euler(0, 0,  -((numgrens-1)/2)*degreesbetweengren+degreesbetweengren * j) * Quaternion.Euler(new Vector3(0, 0, -90)));
            greninstan.GetComponent<Rigidbody2D>().velocity = greninstan.transform.up * grenspeed;
        }
        //this.transform.eulerAngles = tempangle;
        yield return new WaitForSeconds(timeaftergren);
        attacktype = 0;
    }
    public IEnumerator attacktwo_bananathrow()
    {
        print("BANANA");
        yield return new WaitForSeconds(timebeforebanana);
        for (int j = 0; j < numbananas; j++)
        {
            float instanangle = Random.Range(0, 360);
            float initspeed = Random.Range(bananaspeedlow, bananaspeedhigh);
            float initturntime = Random.Range(bananaturntimelow, bananaturntimehigh);
            float initbeforetime = Random.Range(bananabeforetimelow, bananabeforetimehigh);
            print("INSTAN ANGLE: " + instanangle+" INIT TIME: "+initturntime+" INIT SPEED: "+initspeed);
            throwpoint.eulerAngles = new Vector3(0, 0, instanangle);
            GameObject bananainstan = Instantiate(banana, this.transform.position+ 4f * throwpoint.right, throwpoint.rotation*Quaternion.Euler(0,0,90));
            bananainstan.GetComponent<bananascript>().Init(initspeed, initturntime, initbeforetime);
            throwpoint.eulerAngles = Vector3.zero;
            yield return new WaitForSeconds(timebetweenbananas);

        }
        yield return new WaitForSeconds(timeafterbanana);
        attacktype = 0;
    }
    //Throw projectiles (shit) while lunging?
    //MAKE LEAP IMMIDIATLEY AFTER HITTING PLAYER BECAUE OTHERWISE EASY KILL
    public IEnumerator attackthree_lunge(bool towardplayer)
    {
        print("LUNGE");
        int velonum = -1;
        int anglenum = 1;
        float speed = lungespeed / 2;
        if (towardplayer)
        {
            velonum = -1;
            anglenum = 0;
            speed = lungespeed;
        }
        if (towardplayer)
        {
            yield return new WaitForSeconds(beforelungetime);
            colldamage.enabled = true;
        }
        this.transform.eulerAngles = (anglenum * new Vector3(0,0,180))+new Vector3(0, 0, (Mathf.Atan2((Player.transform.position.y - this.transform.position.y), (Player.transform.position.x - this.transform.position.x)) * Mathf.Rad2Deg) + 90);
        thisrigid.velocity = thisrigid.transform.up * speed * velonum;
        yield return new WaitForSeconds(afterlungetime);
        if (towardplayer)
        {
            attacktype = 0;
            colldamage.enabled = false;
        }
    }
    public IEnumerator attackfour_apeshit()
    {
        print("APE SHIT");
        yield return new WaitForSeconds(timebeforeapeshit);
        for(int i = 0; i<timewhileapeshit; i += 3)
        {
            StartCoroutine(attackthree_lunge(true));
            StartCoroutine(attacktwo_bananathrow());
            if(i%2 == 0)
            {
                StartCoroutine(attackone_grenthrow());
            }
            yield return new WaitForSeconds(3);
        }
        yield return new WaitForSeconds(timeafterapeshit);
        attacktype = 0;
    }
    public IEnumerator attackfive_slamattack()
    {
        yield return new WaitForSeconds(beforeaoe);
        Instantiate(slamaoe, this.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(afteraoe);
        attacktype = 0;
    }
    public IEnumerator attacksix_bounceattack()
    {
        yield return new WaitForSeconds(timebeforebounce);
        Instantiate(bounceproj, throwpoint.transform.position, throwpoint.transform.rotation);
        for (int j = 0; j < numbounceproj; j++)
        {
            float instanangle = Random.Range(0, 360);
            print("INSTAN ANGLE: " + instanangle);
            throwpoint.eulerAngles = new Vector3(0, 0, instanangle);
            GameObject bounceinstan = Instantiate(bounceproj, this.transform.position + 4f * throwpoint.right, throwpoint.rotation * Quaternion.Euler(0, 0, 90));
            throwpoint.eulerAngles = Vector3.zero;
            yield return new WaitForSeconds(timebetweenbounce);
        }
        yield return new WaitForSeconds(timeafterbounce);
        attacktype = 0;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("player"))
        {
            print("LUNGE AWAY");
            thisrigid.velocity = Vector3.zero;
            StartCoroutine(attackthree_lunge(false));
        }
        //else if (collision.gameObject.CompareTag("wall"))
        //{
        //    thisrigid.velocity = Vector3.zero;
        //    StartCoroutine(attackthree_lunge(true));
        //}
    }
    void buttons()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            source.clip = sounds[0];
            source.Play();
            StartCoroutine(attackone_grenthrow());
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            source.clip = sounds[1];
            source.Play();
            StartCoroutine(attacktwo_bananathrow());
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            source.clip = sounds[2];
            source.Play();
            StartCoroutine(attackthree_lunge(true));
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            source.clip = sounds[3];
            source.Play();
            StartCoroutine(attacksix_bounceattack());
        }
    }
    // Update is called once per frame
    void Update()
    {
       //buttons();
        if (attacktype == 0)
        {
            StartCoroutine(nextattack());
        }
    }
}
