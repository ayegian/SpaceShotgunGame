using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chemistai : MonoBehaviour
{
    public player player;
    public Transform throwpoint;
    public Bounds bound;
    public bossstuff bosshealth;

    // PHASE NUM ABOVE ATTACK COULD CHANGE EVERYONE THAT STARTS IN PHASE ONE WITH SIMPLE NUM PROJ EDITS IF WANT

    public int attacktype;
    public int lastattacktype;
    public int smallestnotcloseattacktypephase1;
    public int largestnotcloseattacktypephase1;
    public int smallestnotcloseattacktypephase2;
    public int largestnotcloseattacktypephase2;
    public float betweenattacktime;


    //1 automatic in phase 2
    //may make timebefore tesla just one frame so i can still use coroutine for ease of use.
    public teslagridscript teslagrid;
    public float timebeforetesla;

    //1
    public float timebeforefastprojectile;
    public int numtimesfastattack;
    public GameObject fastprojectile;
    public float fastprojectilespeed;
    public float timebetweenfastprojectiles;

    //2
    public float timebeforeexplodevials;
    public GameObject delayexplodevial;
    public float explodevialthrowspeed;
    public int numexplodevials;
    public float timebetweenexplodevials;
    public GameObject explodevialmarker;

    //1
    public float timebeforeacidvials;
    public GameObject acidvial;
    public float acidvialthrowspeed;
    public int numacidvials;
    public float timebetweenacidvials;
    public GameObject acidvialmarker;

    //1
    //default for degrees is 30 default for projinspread is 5
    public float timebeforespreadprojectile;
    public GameObject spreadprojectile;
    public float spreadprojspeed;
    public float timebetweenspreadprojectiles;
    public float numprojectilesinspread;
    public float numtimesspreadattack;
    public float degreesbetweenspreadprojectiles;

    //1
    public float beforeaoe;
    public float afteraoe;
    public GameObject aoeobject;
    public float closedistance;

    //1
    public GameObject smokemachine;
    public float beforedropsmokemachine;

    //2, combine with other attacks?
    //GEYSER COMES WITH BUILT IN WARNING MARKER THAT TURNS INTO DAMAGE BLOCK, LOOK AT MORTAR TO SEE WHAT I MEAN
    //MAKE PHOSPHORUS ATTACK THAT LEAVES BEHIND FIRE
    public GameObject geyser;
    public float beforegeyser;

    //1
    public GameObject phosgren;
    public GameObject phosmarker;
    public float phosthrowspeed;
    public float numphos;
    public float beforephos;
    public float betweenphos;
    public float afterphos;
    public float phosthrowdist;

    //2
    public GameObject mine;
    public float minethrowspeed;
    public float nummines;
    public float beforemine;
    public float betweenmines;
    public float aftermine;
    public float minethrowdist;

    //1 change in 2 to make walls spiked
    public GameObject wall;
    public GameObject P2wall;
    public float beforewall;
    public float betweenwall;
    public float afterwall;
    public float numwalls;

    public bool canrotate = true;

    public AudioSource source;
    public AudioClip[] sounds;

    public int closeattacktypemin;
    public int closeattacktypemax;

    public int currenthealth;

    //FIX ALL THROW ATTACKS, INCREASE NUMBER OF DELAYED PROJECTILES TO NUMBER OF REGULAR ACID STUFF, MAKE GEYSER BETTER OR ADDED IN AS A BACKGROUND ATTACK LIKE TESLA COIL IN 2nd PHASE
    //I THINK MORE VIALS IN GENERAL

    //ADD IN MARKERS FOR VIAL AND OTHER EXPLODE ATTACKS 
    //ADD TRACKING IN ATTACKS TO HIT PLAYER MORE OFTEN
    //PHOSPHORUS ATTACK TEMPORARY WALL AND MINE ATTACK
    //ADD SPIKES TO WALL ATTACK WHEN DAMAGED
    //MAKE ARENA A DECENT AMOUNT LARGER
    // Start is called before the first frame update
    //-24, 39
    //54, 39
    //-24, -39
    //54, -39
    void Awake()
    {
        currenthealth = bosshealth.bosshealth;
        attacktype = 0;
        lastattacktype = 0;
        player = GameObject.FindObjectOfType<player>();
        canrotate = true;
    }
    //FAST SHOOTING PROJECTILE ATTACK, FIRE CAROCEUL ATTACK, ATTACK LIKE ARTILLERY EXCEPT IT SPAWNS ON PLAYER AND IS WATER GEYZER, ALSO MAY SWITCH DELAYED EXPLODE FOR PHOSPHORUSE  ATTACK OR MAY KEEP BOTH, ALSO CLOSE RANGE AOEe ATTACK WHEN PLAYER GETS TOO CLOSE THAT TELEPORTS HIM ALSO SMOKE GRENADE ATTACK
       //MAKE THE TWO BELOWW THIS IEnUMERATORS
    public IEnumerator delayexplode_attackeight()
    {
        yield return new WaitForSeconds(timebeforeexplodevials);
        for (int i = 0; i < numexplodevials; i++)
        {
            yield return new WaitForSeconds(timebetweenexplodevials);
            //CHANGE TO DELAY EXPLODE VIAL
            //CHANGE RANGE TO BOSS ROOM RANGE
            Vector3 pos = new Vector3(Random.Range(-24f, 54), Random.Range(-39, 39f), 10);
            print("POS: " + pos);
            GameObject markerinstan = Instantiate(explodevialmarker, pos, Quaternion.identity);
            GameObject vialinstan = Instantiate(delayexplodevial, throwpoint.position, Quaternion.LookRotation(transform.forward, pos - throwpoint.position));
            float inittimer = (Vector2.Distance(vialinstan.transform.position, pos) / acidvialthrowspeed);
            print("INIT INFO: " + inittimer + " SPEED: " + acidvialthrowspeed + " DISTANCE: " + Vector2.Distance(vialinstan.transform.position, pos));
            print("THIS POS: " + vialinstan.transform.position + " POS: " + pos);
            vialinstan.GetComponent<acidvialscript>().Init(inittimer);
            markerinstan.GetComponent<destroyaftertime>().Init(inittimer);
            Rigidbody2D vialrigid = vialinstan.GetComponent<Rigidbody2D>();
            vialrigid.velocity = acidvialthrowspeed * vialinstan.transform.up;
            //yield return new WaitForSeconds(.1f);
        }
        attacktype = 0;
    }
    //CHECK DAMAGE, IF DAMAGED STOP CURRENT COROuTINE AND START CLOSE ATTACK IF NOT IN PROGRESS ALREADY
    //IF COROUTINE ALREADY ACTIVE ATTACK TYPE IS 11
    public void checkdamage()
    {
        if(currenthealth != bosshealth.bosshealth)
        {
            currenthealth = bosshealth.bosshealth;
            if(attacktype != 11)
            {
                attacktype = 11;
                StopAllCoroutines();
                StartCoroutine(closeattack_attackeleven());
            }
        }
    }
    public IEnumerator vialthrow_attacktwo()
    {
        yield return new WaitForSeconds(timebeforeacidvials);
        for (int i = 0; i < numacidvials; i++)
        {
            yield return new WaitForSeconds(timebetweenacidvials);
            //CHANGE RANGE TO BOSS ROOM RANGE
            Vector3 pos = new Vector3(Random.Range(-24f, 54), Random.Range(-39, 39f), 10);
            print("POS: " + pos);
            GameObject markerinstan = Instantiate(acidvialmarker, pos, Quaternion.identity);
            GameObject vialinstan = Instantiate(acidvial, throwpoint.position, Quaternion.LookRotation(transform.forward, pos - throwpoint.position));
            float inittimer = (Vector2.Distance(vialinstan.transform.position, pos) / acidvialthrowspeed);
            print("INIT INFO: " + inittimer + " SPEED: " + acidvialthrowspeed + " DISTANCE: " + Vector2.Distance(vialinstan.transform.position, pos));
            print("THIS POS: " + vialinstan.transform.position + " POS: " + pos);
            vialinstan.GetComponent<acidvialscript>().Init(inittimer);
            markerinstan.GetComponent<destroyaftertime>().Init(inittimer);
            Rigidbody2D vialrigid = vialinstan.GetComponent<Rigidbody2D>();
            vialrigid.velocity = acidvialthrowspeed*vialinstan.transform.up;
            //yield return new WaitForSeconds(.1f);
        }
        attacktype = 0;
    }
    public IEnumerator spreadattack_attackseven()
    {
        yield return new WaitForSeconds(timebeforespreadprojectile);
        for(int i = 0; i < numtimesspreadattack; i++)
        {
            yield return new WaitForSeconds(timebetweenspreadprojectiles);
            for (int j = 0; j < numprojectilesinspread; j++)
            {
                //GameObject projinstan = Instantiate(fastprojectile, throwpoint.transform.position, this.transform.rotation * Quaternion.Euler(new Vector3(0, 0, -90)));
                GameObject spreadinstan = Instantiate(spreadprojectile, throwpoint.position, Quaternion.Euler(0, 0, throwpoint.transform.eulerAngles.z - (degreesbetweenspreadprojectiles*((numprojectilesinspread-1)/2)) + degreesbetweenspreadprojectiles * j) * Quaternion.Euler(new Vector3(0, 0, -90)));
                spreadinstan.GetComponent<Rigidbody2D>().velocity = spreadinstan.transform.up * spreadprojspeed;
            }
        }
        attacktype = 0;
    }
    public IEnumerator closeattack_attackeleven()
    {
        yield return new WaitForSeconds(beforeaoe);
        Instantiate(aoeobject, this.transform.position+new Vector3(0,0,10), Quaternion.identity);
        yield return new WaitForSeconds(afteraoe);
        //Change this to arena size
        this.transform.position = new Vector3(Random.Range(-24f, 54), Random.Range(-39, 39f), 10);
        attacktype = 0;
    }
    public IEnumerator teslaattack_attackone()
    {
        yield return new WaitForSeconds(timebeforetesla);
        teslagrid.StartCoroutine(teslagrid.activategrid());
        attacktype = 0;
        //activate and deactivate tesla grid from here or just activate and have it deactivate automatically
    }
    public IEnumerator smokemachine_attackfive()
    {
        yield return new WaitForSeconds(beforedropsmokemachine);
        Transform placeholder = throwpoint;
        //REPLACE PLACEHOLER WITH CENTER OF ARENA
        Instantiate(smokemachine, new Vector3(15, 0, this.transform.position.z-50), Quaternion.identity);
        attacktype = 0;
    }
    //ADD TRAIL TO GEYSER ATTACK THAT ALSO EXPLODES WITH GEYSER ALSO HELPS PLAYER TRACK GEYSER bEFORE IT EXPLODES
    public IEnumerator geyserattack_attacknine()
    {
        yield return new WaitForSeconds(beforegeyser);
        //CHANGE THIS POSITION BACK TO PLAYER POSITION JUST USING FOR TESTING
        Instantiate(geyser, throwpoint.transform.position, Quaternion.identity);
        attacktype = 0;
    }
    public IEnumerator fastattack_attacksix()
    {
        yield return new WaitForSeconds(timebeforefastprojectile);
        for(int i = 0; i<numtimesfastattack; i++)
        {
            yield return new WaitForSeconds(timebetweenfastprojectiles);
            GameObject projinstan = Instantiate(fastprojectile, throwpoint.transform.position, this.transform.rotation * Quaternion.Euler(new Vector3(0,0, -90)));
            projinstan.GetComponent<Rigidbody2D>().velocity = projinstan.transform.up * fastprojectilespeed;
        }
        attacktype = 0;
    }
    //IGNORE NOW POSSIBLY SKIP ENTIRLEY
    //public IEnumerator flamecarouselattack_attacknine()
    //{
    //    yield return new WaitForSeconds(1);
    //}
    public void buttoncontrol()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            source.clip = sounds[0];
            source.Play();
            StartCoroutine(teslaattack_attackone());
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            source.clip = sounds[1];
            source.Play();
            StartCoroutine(vialthrow_attacktwo());
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            source.clip = sounds[2];
            source.Play();
            StartCoroutine(spreadattack_attackseven());
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            source.clip = sounds[3];
            source.Play();
            StartCoroutine(delayexplode_attackeight());
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            source.clip = sounds[4];
            source.Play();
            StartCoroutine(smokemachine_attackfive());
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            source.clip = sounds[5];
            source.Play();
            StartCoroutine(geyserattack_attacknine());
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            source.clip = sounds[6];
            source.Play();
            StartCoroutine(fastattack_attacksix());
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            source.clip = sounds[7];
            source.Play();
            StartCoroutine(phosattack_attackfour());
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            source.clip = sounds[8];
            source.Play();
            StartCoroutine(mineattack_attackten());        
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            source.clip = sounds[9];
            source.Play();
            StartCoroutine(wallblock_attackthree());
        }
        if (Input.GetKeyDown(KeyCode.Minus))
        {
            source.clip = sounds[10];
            source.Play();
            StartCoroutine(closeattack_attackeleven());
        }
    }
    public void pickclose()
    {
        attacktype = Random.Range(closeattacktypemin, closeattacktypemax+1);
        if(attacktype == closeattacktypemax)
        {
            attacktype = 11;
        }
    }
    public void picknotclose()
    {
        if(currenthealth > 50)
        {
            attacktype = Random.Range(smallestnotcloseattacktypephase1, largestnotcloseattacktypephase1+1);
        }
        else
        {
            attacktype = Random.Range(smallestnotcloseattacktypephase2, largestnotcloseattacktypephase2+1);
        }
        while (attacktype == lastattacktype)
        {
            if (currenthealth > 50)
            {
                attacktype = Random.Range(smallestnotcloseattacktypephase1, largestnotcloseattacktypephase1+1);
            }
            else
            {
                attacktype = Random.Range(smallestnotcloseattacktypephase2, largestnotcloseattacktypephase2+1);
            }
        }
    }
    public IEnumerator phosattack_attackfour()
    {
        yield return new WaitForSeconds(beforephos);
        for(int i = 0; i<numphos; i++)
        {
            Vector3 pos = new Vector3(Random.Range(this.transform.position.x+phosthrowdist, this.transform.position.x-phosthrowdist), Random.Range(this.transform.position.y+phosthrowdist, this.transform.position.y - phosthrowdist), 10);
            print("POS: " + pos);
            GameObject markerinstan = Instantiate(phosmarker, pos, Quaternion.identity);
            GameObject phosinstan = Instantiate(phosgren, throwpoint.position, Quaternion.LookRotation(transform.forward, pos - throwpoint.position));
            float inittimer = (Vector2.Distance(phosinstan.transform.position, pos) / phosthrowspeed);
            print("INIT INFO: " + inittimer + " SPEED: " + acidvialthrowspeed + " DISTANCE: " + Vector2.Distance(phosinstan.transform.position, pos));
            print("THIS POS: " + phosinstan.transform.position + " POS: " + pos);
            phosinstan.GetComponent<acidvialscript>().Init(inittimer);
            markerinstan.GetComponent<destroyaftertime>().Init(inittimer);
            Rigidbody2D vialrigid = phosinstan.GetComponent<Rigidbody2D>();
            vialrigid.velocity = phosthrowspeed * phosinstan.transform.up;
            yield return new WaitForSeconds(betweenphos);
        }
        yield return new WaitForSeconds(afterphos);
        attacktype = 0;
    }
    //FIX THIS
    IEnumerator mineattack_attackten()
    {
        yield return new WaitForSeconds(beforemine);
        for(int i = 0; i<nummines; i++)
        {
            Vector3 pos = new Vector3(Random.Range(this.transform.position.x + minethrowdist, this.transform.position.x - minethrowdist), Random.Range(this.transform.position.y + minethrowdist, this.transform.position.y - minethrowdist), 10);
            print("POS: " + pos);
            GameObject mineinstan = Instantiate(mine, throwpoint.position, Quaternion.LookRotation(transform.forward, pos - throwpoint.position));
            float inittimer = (Vector2.Distance(mineinstan.transform.position, pos) / minethrowspeed);
            print("INIT INFO: " + inittimer + " SPEED: " + acidvialthrowspeed + " DISTANCE: " + Vector2.Distance(mineinstan.transform.position, pos));
            print("THIS POS: " + mineinstan.transform.position + " POS: " + pos);
            mineinstan.GetComponent<acidvialscript>().Init(inittimer);
            Rigidbody2D vialrigid = mineinstan.GetComponent<Rigidbody2D>();
            vialrigid.velocity = minethrowspeed * mineinstan.transform.up;
            yield return new WaitForSeconds(betweenmines);
        }
        yield return new WaitForSeconds(aftermine);
        attacktype = 0;
    }
    IEnumerator wallblock_attackthree()
    {
        yield return new WaitForSeconds(beforewall);
        for(int i = 0; i< numwalls; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-24f, 54), Random.Range(-39, 39f), 10);
            GameObject wallinstan = Instantiate(wall, pos, Quaternion.identity * Quaternion.Euler(0, 0, Random.Range(0, 360)));
            yield return new WaitForSeconds(betweenwall);
        }
        yield return new WaitForSeconds(afterwall);
        attacktype = 0;
    }
    //public void closetrigger()
    //{
    //    if(Vector2.Distance(player.transform.position, this.transform.position)< closedistance)
    //    {
    //        if(attacktype == 0)
    //        {
    //            attacktype = 999;
    //            StartCoroutine(closeattack_attackeleven());
    //        }
    //    }
    //}
    public IEnumerator nextattack()
    {
        attacktype = 999;
        yield return new WaitForSeconds(betweenattacktime);
        if(Vector3.Distance(this.transform.position, player.transform.position)<= closedistance)
        {
            pickclose();
        }
        else
        {
            picknotclose();
        }
        lastattacktype = attacktype;
        if(attacktype == 1)
        {
            StartCoroutine(teslaattack_attackone());
        }
        if (attacktype == 2)
        {
            StartCoroutine(vialthrow_attacktwo());
        }
        if (attacktype == 3)
        {
            StartCoroutine(wallblock_attackthree());
        }
        if (attacktype == 4)
        {
            StartCoroutine(phosattack_attackfour());
        }
        if (attacktype == 5)
        {
            StartCoroutine(smokemachine_attackfive());
        }
        if (attacktype == 6)
        {
            StartCoroutine(fastattack_attacksix());
        }
        if (attacktype == 7)
        {
            StartCoroutine(spreadattack_attackseven());
        }
        if (attacktype == 8)
        {
            StartCoroutine(delayexplode_attackeight());
        }
        if (attacktype == 9)
        {
            StartCoroutine(geyserattack_attacknine());
        }
        if (attacktype == 10)
        {
            StartCoroutine(mineattack_attackten());
        }
        if (attacktype == 11)
        {
            StartCoroutine(closeattack_attackeleven());
        }
        source.clip = sounds[attacktype-1];
        source.Play();
    }
    // Update is called once per frame
    void Update()
    {
        if (bosshealth.bosshealth <= 50 && teslagrid.auto == false)
        {
            teslagrid.auto = true;
        }
        //buttoncontrol();
        //closetrigger();
        if(attacktype == 0)
        {
            StartCoroutine(nextattack());
        }
        if (canrotate)
        {
            //this.transform.LookAt(player.transform);
            this.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2((player.transform.position.y - this.transform.position.y), (player.transform.position.x - this.transform.position.x)) * Mathf.Rad2Deg);
        }
    }
}
