using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class marine1ai : MonoBehaviour
{
    //PUT GENERAL AI IDEA HERE.
    //Could just do random, with mg burst when too close. maybe minigun only when a ways from wall, but it doesn't really matter.
    public player Player;
    public Rigidbody bossrigid;
    public Transform throwpoint;
    public int attacktype = 0;
    public int lastattacktype;
    public float betweenattacktime;
    public float longdist;
    public int notlongattackslow;
    public int notlongattackshigh;
    public int longattackslow;
    public int longattackshigh;

    public bool forrot;
    public bool backrot;
    public GameObject incendiarygren;
    public GameObject fraggren;
    public GameObject smokegren;
    public float grenanglerange;
    public List<GameObject> grens = new List<GameObject>();
    public int numgrens;
    public float beforegren;
    public float aftergren;
    public float betweengren;

    public GameObject spreadgrenfrag;
    public GameObject spreadgrensmoke;
    public GameObject spreadgrenphos;
    public List<GameObject> spreadgrens = new List<GameObject>();
    public float timebeforespread;
    public float timeafterspread;
    public float spreadprojspeed;
    public float numinspread;
    public float degreesbetweenspreadproj;

    public GameObject missile;
    public int nummissiles;
    public float beforemissiles;
    public float aftermissiles;
    public float betweenmissiles;
    public GameObject mortar;
    public int nummortars;
    public float beforemortar;
    public float aftermortar;
    public float betweenmortar;

    public GameObject mgbullet;
    public float beforeminigun;
    public float afterminigun;
    public float betweenminigun;
    public int numshotsminigun;
    public float backblastforceminigun;
    public float minigunspeed;
    public float anglerangeminigun;
    public float timebeforeburst;
    public float timeafterburst;
    public float timebetweenburst;
    public int numshotsburst;
    public float burstspeed;
    public float anglerangeburst;
    public float backblastforceburst;

    public GameObject flames;
    public GameObject flamestemp;
    public float beforeflamethrower;
    public float afterflamethrower;
    public float maxtimeforwardflamethrower;
    public float maxtimeshootingflamethrower;
    public float maxtimebackflamethrower;
    public float timebetweenshotsflamethrower;
    public float backblastforceflamethrower;
    public float anglerangeflamethrower;
    public float flamethrowerbulletspeed;
    public float flamethrowerstartrange;
    public float flameexpandtime;
    public Transform backthrowpoint;
    public float flamethrower_slowdown = .5f;

    //THIS BELOW IS MAKING MARINE MOVE ONCE EVERY X SECONDS SO HE DOESN'T GET STUCK
    public float timebeforemovelow;
    public float timebeforemovehigh;
    public bool movewaitdone;
    public int numshotsmove;
    public float backblastforcemove;
    public float timebetweenshotsmove;

    public AudioSource source;
    public AudioClip[] sounds;
    // Start is called before the first frame update
    //MAKE ALL GRENADES BOUNCY OR JUST FRAG?
    //ALSO DO I DO SEPERATE ATTACKS FOR GRENADES, OR JUST MAKE IT RANDOM?
    //DO GRENADES GO IN RANDOM DIRECTION OR AT PLAYER OR SOME INBETWEEN
    //GREN ORDER 0:FRAG 1:SMOKE 2:INCEN
    //DECIDE WHAT TO DO WITH ROCKET
    //MORTAR? OR A SMARTPAD HE CALLS IN SUPPORT WITH?
    //MG BURST COULD TURN INTO 2 ATTACKS, A MINIGUN AND A MUCH SHORTER FASTER BURST
    //DECIDE HOW TO USE FLAMETHROWER
    //MAYBE SHOOT AWAY FROM PLAYER, THEN FLAMETHROWER 
    //CONSTANT SNIPER TARGETING PLAYER, FORCING THEM TO MOVE?
    void Awake()
    {
        backrot = false;
        forrot = true;
        Player = FindObjectOfType<player>();
        grens.Add(fraggren);
        grens.Add(smokegren);
        grens.Add(incendiarygren);
        spreadgrens.Add(spreadgrenfrag);
        spreadgrens.Add(spreadgrensmoke);
        spreadgrens.Add(spreadgrenphos);
        attacktype = 0;
    }
    void Start()
    {
        betweenattacktime *= FindObjectOfType<difficulty_script>().difficulty_wait_time_multipliers[FindObjectOfType<difficulty_script>().difficulty];
        StartCoroutine(randommove());
    }
    //Shoot grenades at player angle plus or minus random angle within range
    public IEnumerator attackone_grenadelauncher()
    {
        yield return new WaitForSeconds(beforegren);
        for(int i = 0; i<numgrens; i++)
        {
            float instanangle = Random.Range(grenanglerange, -grenanglerange);
            print("INSTAN ANGLE: " + instanangle);
            throwpoint.eulerAngles = new Vector3(0, instanangle, 0);
            GameObject greninstan = Instantiate(grens[Random.Range(0,3)], throwpoint.position, throwpoint.rotation * Quaternion.Euler(90, 180, 0));
            throwpoint.eulerAngles = Vector3.zero;
            yield return new WaitForSeconds(betweengren);
        }
        yield return new WaitForSeconds(aftergren);
        attacktype = 0;
    }
    public IEnumerator attacktwo_grenadespread()
    {
        print("GREN THROW");
        yield return new WaitForSeconds(timebeforespread);
        Vector3 tempangle = this.transform.eulerAngles;
        //this.transform.eulerAngles = new Vector3(90, (Mathf.Atan2((Player.transform.position.y - this.transform.position.y), (Player.transform.position.x - this.transform.position.x)) * Mathf.Rad2Deg) - 180, 0);
        for (int j = 0; j < numinspread; j++)
        {
            GameObject greninstan = Instantiate(spreadgrens[Random.Range(0, 3)], throwpoint.position, throwpoint.rotation *Quaternion.Euler(0, 0, -((numinspread - 1) / 2) * degreesbetweenspreadproj + degreesbetweenspreadproj * j) /*Quaternion.Euler(new Vector3(0, -90,0))*/);
            //GameObject greninstan = Instantiate(spreadgrens[Random.Range(0, 3)], throwpoint.position, throwpoint.transform.rotation * Quaternion.Euler(0, -((numinspread - 1) / 2) * degreesbetweenspreadproj + degreesbetweenspreadproj * j,0) /*Quaternion.Euler(new Vector3(0, -90,0))*/);
            greninstan.GetComponent<Rigidbody>().velocity = greninstan.transform.right * spreadprojspeed;
        }
        //this.transform.eulerAngles = tempangle;
        yield return new WaitForSeconds(timeafterspread);
        attacktype = 0;
    }
    void Fire(float shootangle, float speed, float backblastforce, GameObject shootobject = null, float rightmult = 1)
    {
        throwpoint.eulerAngles = new Vector3(90, throwpoint.localEulerAngles.y+shootangle+180, 0);
        print("SHOOTANGLE: " + shootangle);
        Renderer rend = shootobject.GetComponentInChildren<SpriteRenderer>();
        Vector3 currentvelo = bossrigid.velocity;
        bossrigid.velocity = currentvelo - (transform.right * backblastforce);
        print("THOWPOINT EULER: " + throwpoint.eulerAngles);
        GameObject spawnedobject = Instantiate(shootobject, throwpoint.position, throwpoint.rotation /** Quaternion.Euler(0,180,0)* *//*Quaternion.Euler(0,shootangle,0)*/) as GameObject;

        //spawnedobject = Instantiate(mgbullet, this.transform.position + new Vector3(0, 0, 10) + transform.right * 2, Quaternion.identity) as GameObject;
        Rigidbody spawnedobjectrigid = spawnedobject.GetComponent<Rigidbody>();
        spawnedobjectrigid.velocity = throwpoint.transform.right *-1* speed*rightmult;
        throwpoint.localEulerAngles = Vector3.zero;
    }
    public IEnumerator basic_mgburst(int nummgbullets, float mganglerange, float mgbulletspeed, float mgbackblastforce, float timebetweenshots, bool backwards)
    {
        if(backwards){
            forrot = false;
            backrot = true;
            yield return new WaitForEndOfFrame();
        }
        //mgburstsound.Play();
        for (int i = 0; i < nummgbullets; i++)
        {
            float mgshootangle = Random.Range(-1 * mganglerange, mganglerange) + throwpoint.transform.eulerAngles.y;
            print("MG SHOOT ANGLE: " + mgshootangle);
            Fire(mgshootangle, mgbulletspeed, mgbackblastforce, mgbullet);
            //Fire(new Vector3(0, 0, this.transform.rotation.eulerAngles.z) - new Vector3(0, 0, mgshootangle), mgbulletspeed, mgbackblastforce, mgbullet);
            yield return new WaitForSeconds(timebetweenshots);
        }
        if(backwards){
            forrot = true;
            backrot = false;
            yield return new WaitForEndOfFrame();
        }
        //yield return new WaitForSeconds(.7f);
        //if (attacktype != 0)
        //{
        //    attacktype = 0;
        //}
    }
    //MAYBE COMBINE WITH MISSILES
    public IEnumerator attackthree_mortarandmissiles()
    {
        yield return new WaitForSeconds(beforemortar);
        for(int i = 0; i< nummortars; i++)
        {
            //CHANGE TO ARENA SIZE
            Vector3 pos = new Vector3(Random.Range(-38, 38f), 0, Random.Range(-38f, 38f));
            Instantiate(mortar, pos, Quaternion.Euler(this.transform.eulerAngles.x, 0, this.transform.eulerAngles.z));
            //yield return new WaitForSeconds(betweenmortar);
        }
        for (int i = 0; i < nummissiles; i++)
        {
            Instantiate(missile, throwpoint.position, throwpoint.transform.rotation);
            yield return new WaitForSeconds(betweenmissiles);
        }
        yield return new WaitForSeconds(aftermortar);
        attacktype = 0;
    }
    //public IEnumerator attackfour_missiles()
    //{
    //    yield return new WaitForSeconds(beforemissiles);

    //    yield return new WaitForSeconds(aftermissiles);
    //}
    public IEnumerator attackfour_minigun()
    {
        yield return new WaitForSeconds(beforeminigun);
        StartCoroutine(basic_mgburst(numshotsminigun, anglerangeminigun, minigunspeed, backblastforceminigun, betweenminigun, false));
        yield return new WaitForSeconds(numshotsminigun * betweenminigun);
        yield return new WaitForSeconds(afterminigun);
        print("MINIGUN DONE");
        attacktype = 0;
    }
    public IEnumerator attackfive_burst()
    {
        yield return new WaitForSeconds(timebeforeburst);
        StartCoroutine(basic_mgburst(numshotsburst, anglerangeburst, burstspeed, backblastforceburst, timebetweenburst, false));
        yield return new WaitForSeconds(numshotsburst * timebetweenburst);
        yield return new WaitForSeconds(timeafterburst);
        attacktype = 0;
    }
    public IEnumerator special_mgburst(int nummgbullets, float mganglerange, float mgbulletspeed, float mgbackblastforce, float timebetweenshots)
    {
        backrot = true;
        forrot = false;
        //mgburstsound.Play();
        print("SPECIAL MG");
        bool flamed = false;
        for (int i = 0; i < nummgbullets; i++)
        {
            if (Vector3.Distance(backthrowpoint.position, Player.transform.position) > flamethrowerstartrange)
            {
                float mgshootangle = Random.Range(-1 * mganglerange, mganglerange) + throwpoint.transform.eulerAngles.y /*- 180*/;
                print("MG SHOOT ANGLE: " + mgshootangle);
                Fire(mgshootangle, mgbulletspeed, mgbackblastforce, mgbullet, 1);
                //Fire(new Vector3(0, 0, this.transform.rotation.eulerAngles.z) - new Vector3(0, 0, mgshootangle), mgbulletspeed, mgbackblastforce, mgbullet);
                yield return new WaitForSeconds(timebetweenshots);
            }
            else
            {
                flamed = true;
                backrot = false;
                forrot = true;
                this.transform.eulerAngles = new Vector3(90, 270 + Mathf.Atan2((Player.transform.position.x - this.transform.position.x), (Player.transform.position.z - this.transform.position.z)) * Mathf.Rad2Deg, 0);
                StartCoroutine(flamethrower());
                yield break;
            }
        }
        if (flamed == false)
        {
            StartCoroutine(flamethrower());
            flamed = true;
        }
        backrot = false;
        forrot = true;
        //yield return new WaitForSeconds(.7f);
        //if (attacktype != 0)
        //{
        //    attacktype = 0;
        //}
    }
    public IEnumerator flamethrower()
    {
        bossrigid.velocity = bossrigid.velocity * flamethrower_slowdown;
        flamestemp = Instantiate(flames, throwpoint);
        flamestemp.transform.position = this.transform.position;
        //flamestemp.transform.localPosition = Vector3.zero;
        //flamestemp.transform.localEulerAngles = Vector3.zero;
        print("FLAMES TEMP CHANGE SCALE");
        //flamestemp.transform.localScale = new Vector3(1, 1, 1);
        //flamestemp = Instantiate(flames, throwpoint.transform.position, throwpoint.transform.rotation);
        flamestemp.SetActive(true);
        StartCoroutine(flamesexpand());
        yield return new WaitForSeconds(maxtimeshootingflamethrower);
        flamestemp.SetActive(false);
        Destroy(flamestemp);
        attacktype = 0;
    }
    public IEnumerator attacksix_flamethrower()
    {
        yield return new WaitForSeconds(beforeflamethrower);
        int numshots = (int)(maxtimeforwardflamethrower / timebetweenshotsflamethrower);
        this.transform.eulerAngles = new Vector3(90, 90 + Mathf.Atan2((Player.transform.position.x - this.transform.position.x), (Player.transform.position.z - this.transform.position.z)) * Mathf.Rad2Deg, 0);
        StartCoroutine(special_mgburst(numshots, anglerangeflamethrower, flamethrowerbulletspeed, backblastforceflamethrower, timebetweenshotsflamethrower));
        yield return new WaitForSeconds(afterflamethrower);
        //PUT ATTACK = 0 IN FLAMETHROWER ABOVE JUST AFTER FLAMES GET DESTROYED
    }
    public IEnumerator randommove(){
        yield return new WaitForSeconds(Random.Range(timebeforemovelow, timebeforemovehigh)+(numshotsmove*timebetweenshotsmove));
        movewaitdone = true;
    }
    public IEnumerator flamesexpand()
    {
        float flametimer = 0;
        while(flametimer < flameexpandtime)
        {
            flametimer += Time.deltaTime;
            flamestemp.transform.localScale = new Vector3(flamestemp.transform.localScale.x + ((1 / flameexpandtime) * Time.deltaTime), flamestemp.transform.localScale.y + ((1 / flameexpandtime) * Time.deltaTime), flamestemp.transform.localScale.z + ((1 / flameexpandtime) * Time.deltaTime));
            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }
    public void buttons()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            source.clip = sounds[0];
            source.Play();
            StartCoroutine(attackone_grenadelauncher());
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            source.clip = sounds[1];
            source.Play();
            StartCoroutine(attacktwo_grenadespread());
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            source.clip = sounds[2];
            source.Play();
            StartCoroutine(attackthree_mortarandmissiles());
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            source.clip = sounds[3];
            source.Play();
            StartCoroutine(attackfour_minigun());
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            source.clip = sounds[4];
            source.Play();
            StartCoroutine(attackfive_burst());
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            source.clip = sounds[5];
            source.Play();
            StartCoroutine(attacksix_flamethrower());
        }
    }
    public void picklong()
    {
        attacktype = Random.Range(longattackslow, longattackshigh + 1);
        while (attacktype == lastattacktype)
        {
            attacktype = Random.Range(notlongattackslow, notlongattackshigh + 1);
        }
    }
    public void picknotlong()
    {
        attacktype = Random.Range(notlongattackslow, notlongattackshigh + 1);
        while(attacktype == lastattacktype)
        {
            attacktype = Random.Range(notlongattackslow, notlongattackshigh + 1);
        }
    }
    public IEnumerator nextattack()
    {
        attacktype = 999;
        yield return new WaitForSeconds(betweenattacktime);
        if (Vector3.Distance(this.transform.position, Player.transform.position) >= longdist)
        {
            picklong();
        }
        else
        {
            picknotlong();
        }
        lastattacktype = attacktype;
        if (attacktype == 1)
        {
            StartCoroutine(attackone_grenadelauncher());
        }
        if (attacktype == 2)
        {
            StartCoroutine(attacktwo_grenadespread());
        }
        if (attacktype == 3)
        {
            StartCoroutine(attackthree_mortarandmissiles());
        }
        if (attacktype == 5)
        {
            StartCoroutine(attackfour_minigun());
        }
        if (attacktype == 4)
        {
            StartCoroutine(attackfive_burst());
        }
        if (attacktype == 6)
        {
            StartCoroutine(attacksix_flamethrower());
        }
        source.clip = sounds[attacktype-1];
        source.Play();
    }
    // Update is called once per frame
    void Update()
    {
        if(movewaitdone == true && attacktype == 0){
            StartCoroutine(basic_mgburst(numshotsmove, anglerangeburst, burstspeed, backblastforcemove, timebetweenshotsmove, /*true*/false));
            movewaitdone = false;
            StartCoroutine(randommove());
        }
        //buttons();
        if(attacktype == 0)
        {
            StartCoroutine(nextattack());
        }
        if(forrot == true)
        {
            this.transform.eulerAngles = new Vector3(90, 270+Mathf.Atan2((Player.transform.position.x - this.transform.position.x), (Player.transform.position.z - this.transform.position.z)) * Mathf.Rad2Deg, 0);
        }
        else if(backrot == true)
        {
            this.transform.eulerAngles = new Vector3(90, 90+Mathf.Atan2((Player.transform.position.x - this.transform.position.x), (Player.transform.position.z - this.transform.position.z)) * Mathf.Rad2Deg, 0);
        }
    }
}
