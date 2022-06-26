using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class chemistai2 : MonoBehaviour
{
    public player player;
    public Transform throwpoint;
    public Bounds bound;
    public bossstuff bosshealth;
    public Transform player_tele_point;
    public GameObject turrets_object;
    public GameObject old_turrets_object;
    // PHASE NUM ABOVE ATTACK COULD CHANGE EVERYONE THAT STARTS IN PHASE ONE WITH SIMPLE NUM PROJ EDITS IF WANT

    public int attacktype;
    public int lastattacktype;
    public float closedistance;
    public int smallestnotcloseattacktypephase1;
    public int largestnotcloseattacktypephase1;
    public int smallestnotcloseattacktypephase2;
    public int largestnotcloseattacktypephase2;    
    //public int closeattacktypemin;
    //public int closeattacktypemax;
    public float betweenattacktime;

    public Vector3 arenaboundstopright;
    public Vector3 arenaboundsbottomleft;


    //1 automatic in phase 2
    //fixed
    //may make timebefore tesla just one frame so i can still use coroutine for ease of use.
    public teslagridscript teslagrid;
    public float timebeforetesla;


    public teslagridscript lasergrid;
    public float timebeforelaser;

    //1
    //remove
    public float timebeforefastprojectile;
    public int numtimesfastattack;
    public GameObject fastprojectile;
    public float fastprojectilespeed;
    public float timebetweenfastprojectiles;

    //2
    //maybe fixed
    public float timebeforeexplodevials;
    public GameObject delayexplodevial;
    public float explodevialthrowspeed;
    public int numexplodevials;
    public float timebetweenexplodevials;
    public GameObject explodevialmarker;

    //1
    //fixed
    public float timebeforeacidvials;
    public GameObject acidvial;
    public float acidvialthrowspeed;
    public int numacidvials;
    public float timebetweenacidvials;
    public GameObject acidvialmarker;
    public float acidvialrandrange;
    public float timebeforeacidfalls;
    //1
    // fixed
    //use when close
    //default for degrees is 30 default for projinspread is 5
    public float timebeforespreadprojectile;
    public GameObject spreadprojectile;
    public float spreadprojspeed;
    public float timebetweenspreadprojectiles;
    public float numprojectilesinspread;
    public float numtimesspreadattack;
    public float degreesbetweenspreadprojectiles;    
    
    public float timebeforespawnprojectile;
    public GameObject spawnprojectile;
    public float spawnprojspeed;
    public float timebetweenspawnprojectiles;
    public float numprojectilesinspawn;
    public float numtimesspawnattack;
    public float degreesbetweenspawnprojectiles;

    //1
    //remove, instead teleport player to beginning
    public float beforeaoe;
    public float afteraoe;
    public GameObject aoeobject;

    //1
    //remove
    public GameObject smokemachine;
    public float beforedropsmokemachine;

    //2, combine with other attacks?
    //fixed
    //GEYSER COMES WITH BUILT IN WARNING MARKER THAT TURNS INTO DAMAGE BLOCK, LOOK AT MORTAR TO SEE WHAT I MEAN
    //MAKE PHOSPHORUS ATTACK THAT LEAVES BEHIND FIRE
    public GameObject geyser;
    public int chanceofgeyser;
    public float beforegeyser;

    //1
    //fixed
    //use when close
    public GameObject phosgren;
    public GameObject phosmarker;
    public float phosthrowspeed;
    public float numphos;
    public float beforephos;
    public float betweenphos;
    public float afterphos;
    public float phosthrowdist;

    //2
    //fixed
    public GameObject mine;
    public float minethrowspeed;
    public float nummines;
    public float beforemine;
    public float betweenmines;
    public float aftermine;
    public float minethrowdist;

    // 1
    //fixed
    public float before_laser_time;
    public float after_laser_time;
    public GameObject laser;
    public int laser_neg_num;
    public float laser_rot_speed;
    public float laser_rot_time;
    public float laser_start_rot;
    public bool lasering;

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
        betweenattacktime *= FindObjectOfType<difficulty_script>().difficulty_wait_time_multipliers[FindObjectOfType<difficulty_script>().difficulty];
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
            Vector3 throwpos = player.transform.position + new Vector3(UnityEngine.Random.Range(-1 * acidvialrandrange, acidvialrandrange), UnityEngine.Random.Range(-1 * acidvialrandrange, acidvialrandrange), 1);
            print("THROWPOS START: " + throwpos);
            if (throwpos.x > arenaboundstopright.x - 1.5f)
            {
                throwpos = new Vector3(arenaboundstopright.x - 1.5f, throwpos.y, throwpos.z);
            }
            else if (throwpos.x < arenaboundsbottomleft.x + 1.5f)
            {
                throwpos = new Vector3(arenaboundsbottomleft.x + 1.5f, throwpos.y, throwpos.z);
            }
            if (throwpos.y > arenaboundstopright.y - 1.5f)
            {
                throwpos = new Vector3(throwpos.x, arenaboundstopright.y - 1.5f, throwpos.z);
            }
            else if (throwpos.y < arenaboundsbottomleft.y + 1.5f)
            {
                throwpos = new Vector3(throwpos.x, arenaboundsbottomleft.y + 1.5f, throwpos.z);
            }
            print("THROWPOS EXPLOSIVE END: " + throwpos);
            //Vector3 random = new Vector3(UnityEngine.Random.Range(-20, 20), UnityEngine.Random.Range(-20, 20f), 0);
            //float random_mag = random.magnitude;
            //int mask = LayerMask.NameToLayer("Default");
            //RaycastHit2D hit = Physics2D.Raycast(throwpos, random.normalized * random_mag/*, QueryTriggerInteraction.Ignore*/);
            ////RaycastHit2D hit = Physics2D.Raycast(throwpos, random.normalized, random_mag, mask/*, QueryTriggerInteraction.Ignore*/);
            //print("RAYCAST HIT POINT: " + hit.point + " START POS: " + throwpos + " RANDOM: " + random + " DIRECTION: " + random.normalized + " RANDOM MAG: " + random_mag);
            ////UnityEngine.Debug.DrawRay(throwpos, random.normalized * random_mag, Color.cyan, 5, false);
            ////UnityEngine.Debug.DrawLine(throwpos, hit.point, Color.cyan, 5, false);
            //Vector3 pos = hit.point;
            //GameObject markerinstan = Instantiate(acidvialmarker, pos, Quaternion.identity);
            GameObject vialinstan = Instantiate(delayexplodevial, throwpos, Quaternion.identity);
            //float inittimer = (Vector2.Distance(vialinstan.transform.position, pos) / acidvialthrowspeed);
            //print("INIT INFO: " + inittimer + " SPEED: " + acidvialthrowspeed + " DISTANCE: " + Vector2.Distance(vialinstan.transform.position, pos));
            //print("THIS POS: " + vialinstan.transform.position + " POS: " + pos);
            //StartCoroutine(vial_move(vialinstan, markerinstan, inittimer));
            //yield return new WaitForSeconds(.1f);
        }
        attacktype = 0;
    }    
    //public IEnumerator delayexplode_attackeight()
    //{
    //    yield return new WaitForSeconds(timebeforeexplodevials);
    //    for (int i = 0; i < numexplodevials; i++)
    //    {
    //        yield return new WaitForSeconds(timebetweenexplodevials);
    //        //CHANGE TO DELAY EXPLODE VIAL
    //        //CHANGE RANGE TO BOSS ROOM RANGE
    //        Vector3 throwpos = player.transform.position+new Vector3(UnityEngine.Random.Range(24,27), UnityEngine.Random.Range(24,27),1);
    //        if (throwpos.x > arenaboundstopright.x - 1.5f)
    //        {
    //            throwpos = new Vector3(arenaboundstopright.x - 1.5f, throwpos.y, throwpos.z);
    //        }            
    //        else if (throwpos.x < arenaboundsbottomleft.x + 1.5f)
    //        {
    //            throwpos = new Vector3(arenaboundsbottomleft.x + 1.5f, throwpos.y, throwpos.z);
    //        }            
    //        if (throwpos.y > arenaboundstopright.y - 1.5f)
    //        {
    //            throwpos = new Vector3(throwpos.x, arenaboundstopright.y - 1.5f, throwpos.z);
    //        }            
    //        else if (throwpos.y < arenaboundsbottomleft.y + 1.5f)
    //        {
    //            throwpos = new Vector3(throwpos.x, arenaboundsbottomleft.y + 1.5f, throwpos.z);
    //        }
    //        Vector3 pos = throwpos + new Vector3(UnityEngine.Random.Range(-20, 20), UnityEngine.Random.Range(-20, 20f), 10);
    //        print("POS: " + pos);
    //        GameObject markerinstan = Instantiate(explodevialmarker, pos, Quaternion.identity);
    //        GameObject vialinstan = Instantiate(delayexplodevial, throwpos, Quaternion.LookRotation(transform.forward, pos - throwpos));
    //        float inittimer = (Vector2.Distance(vialinstan.transform.position, pos) / acidvialthrowspeed);
    //        print("INIT INFO: " + inittimer + " SPEED: " + acidvialthrowspeed + " DISTANCE: " + Vector2.Distance(vialinstan.transform.position, pos));
    //        print("THIS POS: " + vialinstan.transform.position + " POS: " + pos);
    //        vialinstan.GetComponent<acidvialscript>().Init(inittimer);
    //        markerinstan.GetComponent<destroyaftertime>().Init(inittimer);
    //        Rigidbody2D vialrigid = vialinstan.GetComponent<Rigidbody2D>();
    //        vialrigid.velocity = acidvialthrowspeed * vialinstan.transform.up;
    //        //yield return new WaitForSeconds(.1f);
    //    }
    //    attacktype = 0;
    //}
    public IEnumerator laser_attack() {
        yield return new WaitForSeconds(before_laser_time);
        canrotate = false;
        lasering = true;
        int rand = UnityEngine.Random.Range(0, 2);
        float addnum = 0;
        if (rand == 1)
        {
            laser_neg_num = -1;
            addnum = 60;
        }
        else
        {
            laser_neg_num = 1;
        }
        this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, this.transform.eulerAngles.y, laser_start_rot+addnum);
        GameObject laserinstan = Instantiate(laser, throwpoint);
        print("CHEMIST LASER ATTACK INSTAN");
        yield return new WaitForSeconds(laser_rot_time);
        Destroy(laserinstan);
        print("CHEMIST LASER ATTACK DESTROY");
        canrotate = true;
        lasering = false;
        attacktype = 0;
        yield return new WaitForSeconds(after_laser_time);
    }
    //CHECK DAMAGE, IF DAMAGED STOP CURRENT COROuTINE AND START CLOSE ATTACK IF NOT IN PROGRESS ALREADY
    //IF COROUTINE ALREADY ACTIVE ATTACK TYPE IS 11
    public void checkdamage()
    {
        if (currenthealth != bosshealth.bosshealth)
        {
            currenthealth = bosshealth.bosshealth;
            player.transform.root.position = player_tele_point.position;
            old_turrets_object.SetActive(false);
            turrets_object.SetActive(true);
        }
    }
    public IEnumerator vialthrow_attacktwo()
    {
        yield return new WaitForSeconds(timebeforeacidvials);
        for (int i = 0; i < numacidvials; i++)
        {
            yield return new WaitForSeconds(timebetweenacidvials);
            //CHANGE RANGE TO BOSS ROOM RANGE
            Vector3 throwpos = player.transform.position + new Vector3(UnityEngine.Random.Range(-1 * acidvialrandrange, acidvialrandrange), UnityEngine.Random.Range(-1 * acidvialrandrange, acidvialrandrange), 1);
            print("THROWPOS START: " + throwpos);
            if (throwpos.x > arenaboundstopright.x - 1.5f)
            {
                throwpos = new Vector3(arenaboundstopright.x - 1.5f, throwpos.y, throwpos.z);
            }
            else if (throwpos.x < arenaboundsbottomleft.x + 1.5f)
            {
                throwpos = new Vector3(arenaboundsbottomleft.x + 1.5f, throwpos.y, throwpos.z);
            }
            if (throwpos.y > arenaboundstopright.y - 1.5f)
            {
                throwpos = new Vector3(throwpos.x, arenaboundstopright.y - 1.5f, throwpos.z);
            }
            else if (throwpos.y < arenaboundsbottomleft.y + 1.5f)
            {
                throwpos = new Vector3(throwpos.x, arenaboundsbottomleft.y + 1.5f, throwpos.z);
            }
            print("THROWPOS END: " + throwpos);
            Vector3 random = new Vector3(UnityEngine.Random.Range(-20, 20), UnityEngine.Random.Range(-20, 20f), 0);
            float random_mag = random.magnitude;
            int mask = LayerMask.NameToLayer("Default");
            RaycastHit2D hit = Physics2D.Raycast(throwpos, random.normalized * random_mag/*, QueryTriggerInteraction.Ignore*/);
            //RaycastHit2D hit = Physics2D.Raycast(throwpos, random.normalized, random_mag, mask/*, QueryTriggerInteraction.Ignore*/);
            print("RAYCAST HIT POINT: " + hit.point+" START POS: "+throwpos+" RANDOM: "+random+" DIRECTION: " + random.normalized+" RANDOM MAG: "+random_mag);
            //UnityEngine.Debug.DrawRay(throwpos, random.normalized * random_mag, Color.cyan, 5, false);
            //UnityEngine.Debug.DrawLine(throwpos, hit.point, Color.cyan, 5, false);
            Vector3 pos = hit.point;
            GameObject markerinstan = Instantiate(acidvialmarker, pos, Quaternion.identity);
            GameObject vialinstan = Instantiate(acidvial, throwpos, Quaternion.LookRotation(transform.forward, pos - throwpos));
            float inittimer = (Vector2.Distance(vialinstan.transform.position, pos) / acidvialthrowspeed);
            print("INIT INFO: " + inittimer + " SPEED: " + acidvialthrowspeed + " DISTANCE: " + Vector2.Distance(vialinstan.transform.position, pos));
            print("THIS POS: " + vialinstan.transform.position + " POS: " + pos);
            StartCoroutine(vial_move(vialinstan, markerinstan, inittimer));
            //yield return new WaitForSeconds(.1f);
        }
        attacktype = 0;
    }
    public IEnumerator vial_move(GameObject vialinstan, GameObject markerinstan, float inittime)
    {
        yield return new WaitForSeconds(timebeforeacidfalls);
        vialinstan.GetComponentInChildren<acidvialscript>().Init(inittime);
        markerinstan.GetComponent<destroyaftertime>().Init(inittime);
        Rigidbody2D vialrigid = vialinstan.GetComponentInChildren<Rigidbody2D>();
        vialrigid.velocity = acidvialthrowspeed * vialinstan.transform.up;
    }
    public IEnumerator spreadattack_attackseven()
    {
        yield return new WaitForSeconds(timebeforespreadprojectile);
        for (int i = 0; i < numtimesspreadattack; i++)
        {
            yield return new WaitForSeconds(timebetweenspreadprojectiles);
            for (int j = 0; j < numprojectilesinspread; j++)
            {
                //GameObject projinstan = Instantiate(fastprojectile, throwpoint.transform.position, this.transform.rotation * Quaternion.Euler(new Vector3(0, 0, -90)));
                GameObject spreadinstan = Instantiate(spreadprojectile, throwpoint.position, Quaternion.Euler(0, 0, throwpoint.transform.eulerAngles.z - (degreesbetweenspreadprojectiles * ((numprojectilesinspread - 1) / 2)) + degreesbetweenspreadprojectiles * j) * Quaternion.Euler(new Vector3(0, 0, -90)));
                spreadinstan.GetComponent<Rigidbody2D>().velocity = spreadinstan.transform.up * spreadprojspeed;
            }
        }
        attacktype = 0;
    }
    //public IEnumerator closeattack_attackeleven()
    //{
    //    yield return new WaitForSeconds(beforeaoe);
    //    Instantiate(aoeobject, this.transform.position + new Vector3(0, 0, 10), Quaternion.identity);
    //    yield return new WaitForSeconds(afteraoe);
    //    //Change this to arena size
    //    this.transform.position = new Vector3(UnityEngine.Random.Range(-24f, 54), UnityEngine.Random.Range(-39, 39f), 10);
    //    attacktype = 0;
    //}
    public IEnumerator teslaattack_attackone()
    {
        yield return new WaitForSeconds(timebeforetesla);
        print("ACTIVATE TESLA GRID");
        teslagrid.StartCoroutine(teslagrid.activategrid());
        attacktype = 0;
        //activate and deactivate tesla grid from here or just activate and have it deactivate automatically
    }  
    public IEnumerator lasergrid_attack()
    {
        yield return new WaitForSeconds(timebeforelaser);
        lasergrid.StartCoroutine(lasergrid.activategrid());
        attacktype = 0;
        //activate and deactivate laser grid from here or just activate and have it deactivate automatically
    }
    //public IEnumerator smokemachine_attackfive()
    //{
    //    yield return new WaitForSeconds(beforedropsmokemachine);
    //    Transform placeholder = throwpoint;
    //    //REPLACE PLACEHOLER WITH CENTER OF ARENA
    //    Instantiate(smokemachine, new Vector3(15, 0, this.transform.position.z - 50), Quaternion.identity);
    //    attacktype = 0;
    //}
    //ADD TRAIL TO GEYSER ATTACK THAT ALSO EXPLODES WITH GEYSER ALSO HELPS PLAYER TRACK GEYSER bEFORE IT EXPLODES
    public IEnumerator geyserattack_attacknine()
    {
        yield return new WaitForSeconds(beforegeyser);
        //CHANGE THIS POSITION BACK TO PLAYER POSITION JUST USING FOR TESTING
        Instantiate(geyser, throwpoint.transform.position, Quaternion.identity);
        attacktype = 0;
    }
    //public IEnumerator fastattack_attacksix()
    //{
    //    yield return new WaitForSeconds(timebeforefastprojectile);
    //    for (int i = 0; i < numtimesfastattack; i++)
    //    {
    //        yield return new WaitForSeconds(timebetweenfastprojectiles);
    //        GameObject projinstan = Instantiate(fastprojectile, throwpoint.transform.position, this.transform.rotation * Quaternion.Euler(new Vector3(0, 0, -90)));
    //        projinstan.GetComponent<Rigidbody2D>().velocity = projinstan.transform.up * fastprojectilespeed;
    //    }
    //    attacktype = 0;
    //}
    //IGNORE NOW POSSIBLY SKIP ENTIRLEY
    //public IEnumerator flamecarouselattack_attacknine()
    //{
    //    yield return new WaitForSeconds(1);
    //}
    public IEnumerator spawning_proj_attack()
    {
        yield return new WaitForSeconds(timebeforespawnprojectile);
        for (int i = 0; i < numtimesspawnattack; i++)
        {
            yield return new WaitForSeconds(timebetweenspawnprojectiles);
            for (int j = 0; j < numprojectilesinspawn; j++)
            {
                //GameObject projinstan = Instantiate(fastprojectile, throwpoint.transform.position, this.transform.rotation * Quaternion.Euler(new Vector3(0, 0, -90)));
                GameObject spawninstan = Instantiate(spawnprojectile, throwpoint.position, Quaternion.Euler(0, 0, throwpoint.transform.eulerAngles.z - (degreesbetweenspawnprojectiles * ((numprojectilesinspawn - 1) / 2)) + degreesbetweenspawnprojectiles * j) * Quaternion.Euler(new Vector3(0, 0, -90)));
                spawninstan.GetComponent<Rigidbody2D>().velocity = spawninstan.transform.up * spawnprojspeed;
            }
        }
        attacktype = 0;
    }
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
            source.clip = sounds[7];
            source.Play();
            StartCoroutine(delayexplode_attackeight());
        }
        //if (Input.GetKeyDown(KeyCode.Alpha5))
        //{
        //    source.clip = sounds[4];
        //    source.Play();
        //    StartCoroutine(smokemachine_attackfive());
        //}
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            source.clip = sounds[5];
            source.Play();
            StartCoroutine(geyserattack_attacknine());
        }
        //if (Input.GetKeyDown(KeyCode.Alpha7))
        //{
        //    source.clip = sounds[6];
        //    source.Play();
        //    StartCoroutine(fastattack_attacksix());
        //}
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            source.clip = sounds[3];
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
            source.clip = sounds[9];
            source.Play();
            StartCoroutine(laser_attack());
        }        
        if (Input.GetKeyDown(KeyCode.Equals))
        {
            source.clip = sounds[1];
            source.Play();
            StartCoroutine(spawning_proj_attack());
        }        
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            source.clip = sounds[2];
            source.Play();
            StartCoroutine(lasergrid_attack());
        }
        //if (Input.GetKeyDown(KeyCode.Minus))
        //{
        //    source.clip = sounds[10];
        //    source.Play();
        //    //StartCoroutine(closeattack_attackeleven());
        //}
    }
    public void pickclose()
    {
        attacktype = UnityEngine.Random.Range(closeattacktypemin, closeattacktypemax + 1);
    }
    public void picknotclose()
    {
        if (currenthealth > 10)
        {
            attacktype = UnityEngine.Random.Range(smallestnotcloseattacktypephase1, largestnotcloseattacktypephase1 + 1);
        }
        else
        {
            attacktype = UnityEngine.Random.Range(smallestnotcloseattacktypephase2, largestnotcloseattacktypephase2 + 1);
        }
        //while (attacktype == lastattacktype)
        //{
        //    if (currenthealth > 50)
        //    {
        //        attacktype = UnityEngine.Random.Range(smallestnotcloseattacktypephase1, largestnotcloseattacktypephase1 + 1);
        //    }
        //    else
        //    {
        //        attacktype = UnityEngine.Random.Range(smallestnotcloseattacktypephase2, largestnotcloseattacktypephase2 + 1);
        //    }
        //}
    }
    public IEnumerator phosattack_attackfour()
    {
        yield return new WaitForSeconds(beforephos);
        for (int i = 0; i < numphos; i++)
        {
            Vector3 pos = new Vector3(UnityEngine.Random.Range(this.transform.position.x - (phosthrowdist/2), this.transform.position.x + (phosthrowdist / 2)), UnityEngine.Random.Range(this.transform.position.y - phosthrowdist, this.transform.position.y - (phosthrowdist / 2)), 10);
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
    //IEnumerator mineattack_attackten()
    //{
    //    yield return new WaitForSeconds(beforemine);
    //    for (int i = 0; i < nummines; i++)
    //    {
    //        Vector3 pos = new Vector3(UnityEngine.Random.Range(arenaboundsbottomleft.x + 1.5f, arenaboundstopright.x - 1.5f), UnityEngine.Random.Range(arenaboundsbottomleft.y + 1.5f, arenaboundstopright.y - 1.5f), 0);
    //        print("POS: " + pos);
    //        GameObject mineinstan = Instantiate(mine, throwpoint.position, Quaternion.LookRotation(transform.forward, pos - throwpoint.position));
    //        float inittimer = (Vector2.Distance(mineinstan.transform.position, pos) / minethrowspeed);
    //        print("INIT INFO: " + inittimer + " SPEED: " + acidvialthrowspeed + " DISTANCE: " + Vector2.Distance(mineinstan.transform.position, pos));
    //        print("THIS POS: " + mineinstan.transform.position + " POS: " + pos);
    //        mineinstan.GetComponent<acidvialscript>().Init(inittimer);
    //        Rigidbody2D vialrigid = mineinstan.GetComponent<Rigidbody2D>();
    //        vialrigid.velocity = minethrowspeed * mineinstan.transform.up;
    //        yield return new WaitForSeconds(betweenmines);
    //    }
    //    yield return new WaitForSeconds(aftermine);
    //    attacktype = 0;
    //}    
    IEnumerator mineattack_attackten()
    {
        yield return new WaitForSeconds(beforemine);
        for (int i = 0; i < nummines; i++)
        {
            GameObject mineinstan = Instantiate(mine, throwpoint.position, Quaternion.identity);
            yield return new WaitForSeconds(betweenmines);
        }
        yield return new WaitForSeconds(aftermine);
        attacktype = 0;
    }
    IEnumerator wallblock_attackthree()
    {
        yield return new WaitForSeconds(beforewall);
        for (int i = 0; i < numwalls; i++)
        {
            Vector3 pos = new Vector3(UnityEngine.Random.Range(-20f, 20f), UnityEngine.Random.Range(-15, 15f), 10);
            pos += new Vector3(player.transform.position.x, player.transform.position.y, 0);
            GameObject wallinstan = Instantiate(wall, pos, Quaternion.identity * Quaternion.Euler(0, 0, UnityEngine.Random.Range(0, 360)));
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
        if (Vector2.Distance(this.transform.position, player.transform.position) <= closedistance)
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
            StartCoroutine(spreadattack_attackseven());
        }
        if (attacktype == 2)
        {
            print("ACID VIAL");
            StartCoroutine(phosattack_attackfour());
        }
        if (attacktype == 3)
        {
            StartCoroutine(spawning_proj_attack());
        }
        if (attacktype == 4)
        {
            StartCoroutine(teslaattack_attackone());
        }
        if (attacktype == 5)
        {
            StartCoroutine(mineattack_attackten());
        }
        if (attacktype == 6)
        {
            StartCoroutine(laser_attack());
        }
        if (attacktype == 7)
        {
            StartCoroutine(delayexplode_attackeight());
        }
        if (attacktype == 8)
        {
            StartCoroutine(vialthrow_attacktwo());
        }
        if (attacktype == 9)
        {
            StartCoroutine(lasergrid_attack());
        }
        //if (attacktype == 10)
        //{
        //    StartCoroutine(geyserattack_attacknine());
        //}
        //if (attacktype == 11)
        //{
        //    StartCoroutine(closeattack_attackeleven());
        //}
        source.clip = sounds[attacktype - 1];
        source.Play();
    }
    //public IEnumerator nextattack()
    //{
    //    attacktype = 999;
    //    yield return new WaitForSeconds(betweenattacktime);
    //    if (Vector2.Distance(this.transform.position, player.transform.position) <= closedistance)
    //    {
    //        pickclose();
    //    }
    //    else
    //    {
    //        picknotclose();
    //    }
    //    lastattacktype = attacktype;
    //    if (attacktype == 1)
    //    {
    //        StartCoroutine(teslaattack_attackone());
    //    }
    //    if (attacktype == 2)
    //    {
    //        print("ACID VIAL");
    //        StartCoroutine(vialthrow_attacktwo());
    //    }
    //    if (attacktype == 3)
    //    {
    //        StartCoroutine(wallblock_attackthree());
    //    }
    //    if (attacktype == 4)
    //    {
    //        StartCoroutine(phosattack_attackfour());
    //    }
    //    //if (attacktype == 5)
    //    //{
    //    //    StartCoroutine(smokemachine_attackfive());
    //    //}
    //    //if (attacktype == 6)
    //    //{
    //    //    StartCoroutine(fastattack_attacksix());
    //    //}
    //    if (attacktype == 7)
    //    {
    //        StartCoroutine(spreadattack_attackseven());
    //    }
    //    if (attacktype == 8)
    //    {
    //        StartCoroutine(delayexplode_attackeight());
    //    }
    //    if (attacktype == 9)
    //    {
    //        StartCoroutine(geyserattack_attacknine());
    //    }
    //    if (attacktype == 10)
    //    {
    //        StartCoroutine(mineattack_attackten());
    //    }
    //    //if (attacktype == 11)
    //    //{
    //    //    StartCoroutine(closeattack_attackeleven());
    //    //}
    //    source.clip = sounds[attacktype - 1];
    //    source.Play();
    //}
    // Update is called once per frame
    void Update()
    {
        if (bosshealth.bosshealth <= 5 && teslagrid.auto == false)
        {
            teslagrid.auto = true;
        }
        buttoncontrol();
        checkdamage();
        //closetrigger();
        if (attacktype == 0)
        {
            if (UnityEngine.Random.Range(0, chanceofgeyser) == 0)
            {
                //Instantiate(geyser, throwpoint.transform.position + Vector3.forward * 100, Quaternion.identity);
            }
               // StartCoroutine(nextattack());
        }
        if (canrotate)
        {
            //this.transform.LookAt(player.transform);
            this.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2((player.transform.position.y - this.transform.position.y), (player.transform.position.x - this.transform.position.x)) * Mathf.Rad2Deg);
        }
        if (lasering)
        {
            this.transform.eulerAngles += Vector3.forward * Time.deltaTime * laser_rot_speed*laser_neg_num;
        }
    }
}
