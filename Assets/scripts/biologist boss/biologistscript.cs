using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class biologistscript : MonoBehaviour
{
    public float betweenattacktime;
    public int attacktype = 0;
    public int lastattacktype;
    public Rigidbody biorigid;
    public Transform throwpoint;
    public player Player;
    public Rigidbody playerrigid;
    public bossstuff bosshealth;

    public int attacktypelowphase1;
    public int attacktypehighphase1;
    public int attacktypelowphase2;
    public int attacktypehighphase2;
    //PHASE NUMBER LISTED ABOVE


    //1 maybe make better in 2 maybe not
    public GameObject quickaoeobject;
    public float beforequick;
    public float afterquick;
    public float quickforce;

    //1 no change i dont think, maybe just more
    public GameObject buttonhookobj;
    public float beforebuttonhook;
    public float afterbuttonhook;
    public float betweenbuttonhook;
    public float buttonhookspeed;
    public float buttonhooknum;

    //JUST ADD ANOTHER FIXED IT FOR P1 AND P2
    //1 change to make more whips in 2
    public GameObject whip;
    public GameObject whipwarning;
    public float beforewhip;
    public float afterwhip;
    public float whipspeed;
    public Transform[] whipstartposes;
    public int usedwhipind;

    //1 maybe change in 2
    public GameObject piranha;
    public float beforepiranha;
    public float afterpiranha;
    public Transform[] piranhainstanpoints;

    //2
    //VINE WILL HAVE WARNING INLAID IN GAMEOBJECT
    public GameObject vine;
    public GameObject vinewarning;
    public float vinewarningtime;
    public float beforevine;
    public float betweenvine;
    public float aftervine;
    public float numvinesatonce;
    public float numvines;
    public float vineanglerange;

    //2 
    public GameObject bubble;
    public float beforebubble;
    public float betweenbubble;
    public float afterbubble;
    public Transform[] bubbleinstanpoints;

    //FIXED JUST USE LIKE NORMAL IN P2
    //1 change in 2. 1: 2 muts, 1 reg. 2: 2 reg, 1 shot
    public GameObject[] seed;
    public float beforeseedcannon;
    public float afterseedcannon;
    public float seedspeed;
    public float degreesbetweenseeds;

    //1 i dont think any change in 2, TOO HARD CURRENTLY
    public GameObject apple;
    public float beforeapple;
    public float afterapple;
    public float betweenapple;
    public float numapplespreadattacks;
    public float numapplesinspread;
    public float degreesbetweenapples;
    public float applespeed;

    public AudioSource source;
    public AudioClip[] sounds;

    public float closedist;

    public float timebeforemovehigh;
    public float timebeforemovelow;
    public bool movewaitdone;
    //NEED 9 ATTACKS, AT LEAST 8 OR 7 AND UPGRADE BUDDY
    //STILL HAVE TO MAKE BETTER BUDDY

    //BOSS MOVE AWAY LIKE JELLYFISH? (Y)

    //GROUND VINE ATTACK OR SOMETHING. ATTACK WOULD BE LIKE THE LASER ATTACK FROM THE LASER ENEMY, BUT ORIGNATING FROM RANDOM CORNERS OF THE MAP, AND DESIGNED TO TRY TO BOX IN PLAYER, COULD COMBINE WITH A VERY BASIC AND WEAK PROJ ATTACK. (M/Y)
    //PIRANNAH ATTACK, SIMILAR TO MISSILES BUT BETTER, WETHER IT BE BETTER AI OR SOMETHING ELSE, UPGRADE ON DAMAGE (M/Y)
    //HOW ABOUT AN ATTACK THAT IS LIKE A SCREEN LONG WHIP EMINATING FROM BOSS (OR NOT), WHERE THE PLAYER HAS TO BREAK WHIP BY HITTING SAME SPOT MULTIPLE TIMES THEN GO THROUGH BROKEN PART OF WHIP (M/Y)
    //POISON GAS BUBBLES?, LIKE SLOW MOVING HOMING TIMED EXPLOSIVES THAT LEAVE BEHIND A POISON GAS (Y) COULD USE BUBBLES FOR UPGRADED COMPANION
    //GROW PLANT TURRETS? (M/N)
    //AOE/FAST MOVE BACK ATTACK (Y)
    //BASIC ATTACKS PROBABLY, WITH SOME CHANGES, LIKE A BUTTONHOOK ATTACK, NOT LIKE BOOMERANGE BECAUSE BOOMERANGE LOOPS AND COMES BACK, BUTTONHOOK GOES OUT STOPS AND COMES BACK, NO LOOP (Y)
    //EXPLOSIVE APPLES?(M/Y)
    //TREES THAT MAKE IT HARDER TO GET TO BOSS?(N)
    //SEED CANNON? (M/Y) MAYBE TOO MANY ENEMIES IF INCLUDE COMPANION
    //

    // Start is called before the first frame update
    void Awake()
    {
        Player = GameObject.FindObjectOfType<player>();
        playerrigid = Player.GetComponentInChildren<Rigidbody>();
        betweenattacktime *= FindObjectOfType<difficulty_script>().difficulty_wait_time_multipliers[FindObjectOfType<difficulty_script>().difficulty];
        attacktype = 0;
        StartCoroutine(randommove());
    }
    public IEnumerator attackone_quickmove()
    {
        yield return new WaitForSeconds(beforequick);
        Instantiate(quickaoeobject, throwpoint.position, throwpoint.rotation);
        biorigid.AddForce(quickforce * biorigid.transform.right);
        yield return new WaitForSeconds(afterquick);
        attacktype = 0;
    }
    public IEnumerator randommove()
    {
        yield return new WaitForSeconds(Random.Range(timebeforemovelow, timebeforemovehigh));
        movewaitdone = true;
    }
    //FIGURE OUT PATTERN FOR BUTTONHOOK
    public IEnumerator attacktwo_buttonhook()
    {
        yield return new WaitForSeconds(beforebuttonhook);
        for(int i = 0; i<buttonhooknum; i++)
        {
            Instantiate(buttonhookobj, throwpoint.position, throwpoint.rotation);
            yield return new WaitForSeconds(betweenbuttonhook);
        }
        yield return new WaitForSeconds(afterbuttonhook);
        attacktype = 0;
    }
    public IEnumerator attackthree_vinewhip()
    {
        int whipind = 0;
        if(usedwhipind != -1)
        {
            whipind = Random.Range(0, whipstartposes.Length);
            while(whipind == usedwhipind)
            {
                whipind = Random.Range(0, whipstartposes.Length);
            }
            usedwhipind = -1;
        }
        else
        {
            whipind = Random.Range(0, whipstartposes.Length);
            usedwhipind = whipind;
        }
        GameObject warning = Instantiate(whipwarning, whipstartposes[whipind].position, whipstartposes[whipind].rotation);
        yield return new WaitForSeconds(beforewhip);
        Destroy(warning);
        Instantiate(whip, whipstartposes[whipind].position, Quaternion.Euler(0, whipstartposes[whipind].eulerAngles.y, 0));
        print("WHIP POS: " + whip.transform.position + " START POS: " + whipstartposes[whipind].position+" WIND INDEX: "+ whipind);
        yield return new WaitForSeconds(afterwhip);
        attacktype = 0;
    }
    public IEnumerator attackfour_piranha()
    {
        yield return new WaitForSeconds(beforepiranha);
        foreach(Transform t in piranhainstanpoints)
        {
            Instantiate(piranha, t.position, t.rotation);
        }
        yield return new WaitForSeconds(afterpiranha);
        attacktype = 0;
    }
    public IEnumerator attackeight_vineattack()
    {
        yield return new WaitForSeconds(beforevine);
        for(int i = 0; i<(numvines/numvinesatonce); i++)
        {
            //List<float> castangles = new List<float>();
            for(int j = 0; j<numvinesatonce; j++)
            {
                //CHANGE TO ARENA SIZE
                Vector3 castpos = new Vector3(Random.Range(-39, -39f), 0, Random.Range(-39,39));
                float castangle = Mathf.Atan2((Player.transform.position.x - castpos.x), (Player.transform.position.z - castpos.z)) * Mathf.Rad2Deg + Random.Range(-1 * vineanglerange, vineanglerange);
                Instantiate(vine, castpos, Quaternion.Euler(0,castangle,0));
                //castangles.Add(castangle);
                Instantiate(vinewarning, castpos, Quaternion.Euler(0, castangle, 0));
            }
            //yield return new WaitForSeconds(vinewarningtime);
            //for (int j = 0; j < numvinesatonce; j++)
            //{
            //    //CHANGE TO ARENA SIZE
            //    Vector3 castpos = new Vector3(Random.Range(-39, -39f), 0, Random.Range(-39, 39));
            //    float castangle = Mathf.Atan2((Player.transform.position.x - castpos.x), (Player.transform.position.z - castpos.z)) * Mathf.Rad2Deg + Random.Range(-1 * vineanglerange, vineanglerange);
            //    Instantiate(vine, castpos, Quaternion.Euler(0, castangles[j], 0));
            //}
            yield return new WaitForSeconds(betweenvine);
        }
        yield return new WaitForSeconds(aftervine);
        attacktype = 0;
    }
    public IEnumerator attackseven_poisonbubble()
    {
        yield return new WaitForSeconds(beforebubble);
        foreach(Transform t in bubbleinstanpoints)
        {
            Instantiate(bubble, t.position, t.rotation);
            yield return new WaitForSeconds(betweenbubble);
        }
        yield return new WaitForSeconds(afterbubble);
        attacktype = 0;
    }
    //Basic mutant, basic shoot, upgraded gets another shotgun shoot
    public IEnumerator attacksix_seedcannon()
    {
        int addnum = 0;
        if(bosshealth.bosshealth <= 50)
        {
            addnum++;
        }
        yield return new WaitForSeconds(beforeseedcannon);
        for(int i = 0; i < seed.Length; i++)
        {
            GameObject seedinstan = Instantiate(seed[(i%2)+addnum], throwpoint.position, this.transform.rotation * Quaternion.Euler(0, 0, throwpoint.transform.eulerAngles.z - (degreesbetweenseeds * ((seed.Length - 1) / 2)) + degreesbetweenseeds * i) * Quaternion.Euler(new Vector3(0, 0, -90)));
            seedinstan.GetComponent<Rigidbody>().velocity = seedinstan.transform.up * seedspeed * -1;
        }
        yield return new WaitForSeconds(afterseedcannon);
        attacktype = 0;
    }
    public IEnumerator appleattack_attackfive()
    {
        yield return new WaitForSeconds(beforeapple);
        for (int i = 0; i < numapplespreadattacks; i++)
        {
            yield return new WaitForSeconds(betweenapple);
            for (int j = 0; j < numapplesinspread; j++)
            {
                GameObject appleinstan = Instantiate(apple, throwpoint.position, this.transform.rotation * Quaternion.Euler(0, 0, throwpoint.transform.eulerAngles.z - (degreesbetweenapples * ((numapplesinspread - 1) / 2)) + degreesbetweenapples * j) * Quaternion.Euler(new Vector3(0, 0, -90)));
                appleinstan.GetComponent<Rigidbody>().velocity = appleinstan.transform.up * -1 * applespeed;
                //spreadinstan.GetComponent<Rigidbody2D>().velocity = spreadinstan.transform.up * spreadprojspeed;
            }
        }
        yield return new WaitForSeconds(afterapple);
        attacktype = 0;
    }
    void buttoncontrol()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            source.clip = sounds[0];
            source.Play();
            StartCoroutine(attackone_quickmove());
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            source.clip = sounds[1];
            source.Play();
            StartCoroutine(attacktwo_buttonhook());
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            source.clip = sounds[2];
            source.Play();
            StartCoroutine(attackthree_vinewhip());
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            source.clip = sounds[3];
            source.Play();
            StartCoroutine(attackfour_piranha());
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            source.clip = sounds[4];
            source.Play();
            StartCoroutine(attackeight_vineattack());
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            source.clip = sounds[5];
            source.Play();
            StartCoroutine(attackseven_poisonbubble());
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            source.clip = sounds[6];
            source.Play();
            StartCoroutine(attacksix_seedcannon());
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            source.clip = sounds[7];
            source.Play();
            StartCoroutine(appleattack_attackfive());
        }
    }
    public void pickclose()
    {
        attacktype = 1;
    }
    public void picknotclose()
    {
        if (bosshealth.bosshealth > 50)
        {
            attacktype = Random.Range(attacktypelowphase1, attacktypehighphase1+1);
        }
        else
        {
            attacktype = Random.Range(attacktypelowphase2, attacktypehighphase2+1);
        }
        while(lastattacktype == attacktype)
        {
            if (bosshealth.bosshealth > 50)
            {
                attacktype = Random.Range(attacktypelowphase1, attacktypehighphase1+1);
            }
            else
            {
                attacktype = Random.Range(attacktypelowphase2, attacktypehighphase2+1);
            }
        }
    }
    public IEnumerator nextattack()
    {
        attacktype = 999;
        yield return new WaitForSeconds(betweenattacktime);
        if (Vector3.Distance(this.transform.position, Player.transform.position) <= closedist)
        {
            pickclose();
        }
        else
        {
            picknotclose();
        }
        lastattacktype = attacktype;
        if (attacktype == 1)
        {
            StartCoroutine(attackone_quickmove());
        }
        if (attacktype == 2)
        {
            StartCoroutine(attacktwo_buttonhook());
        }
        if (attacktype == 3)
        {
            StartCoroutine(attackthree_vinewhip());
        }
        if (attacktype == 4)
        {
            StartCoroutine(attackfour_piranha());
        }
        if (attacktype == 5)
        {
            StartCoroutine(appleattack_attackfive());
        }
        if (attacktype == 6)
        {
            StartCoroutine(attacksix_seedcannon());
        }
        if (attacktype == 7)
        {
            StartCoroutine(attackseven_poisonbubble());
        }
        if (attacktype == 8)
        {
            StartCoroutine(attackeight_vineattack());
        }
        source.clip = sounds[attacktype-1];
        source.Play();
    }
    // Update is called once per frame
    void Update()
    {
        if (movewaitdone == true && attacktype == 0)
        {
            StartCoroutine(attackone_quickmove());
            movewaitdone = false;
            StartCoroutine(randommove());
        }
        if (attacktype == 0)
        {
            StartCoroutine(nextattack());
        }
        //buttoncontrol();
    }
}
