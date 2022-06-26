using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class marine2ai : MonoBehaviour
{
    //KNIVES DISSSAPEARING
    //KEEP IN MIND 1st PHASE IS MUCH SHORTER THAN STANDARD BOSS, STAND: 50 hp THIS: 25-35 hp. or this boss 50hp, 15 1st phase.
    //GENERAL AI STRATEGY HERE.
    //MAKE GO TO CENTER BEFORE DOING WALL AND ROCK ATTACKS
    //PHASE NUMBER ABOVE ATTACKS
    public SpriteRenderer thissprite;
    public bosstakedamage bosstakedam;
    public Transform center;
    public float betweenattacktime;
    public player Player;
    public shoot2 Playershoot;
    public Rigidbody thisrigid;
    public Transform infront;
    public Rigidbody playerrigid;
    public float infrontmag;
    public Transform toplayer;
    public Transform opptoplayer;
    public int currenthealth;
    public int attacktype = 99;
    public bossstuff bossstuff;
    public Transform throwpoint;
    public Transform[] teleportpos;
    public int lastattacktype;
    public rottoplayer2 thisrottoplayer;
    public bool inrock;
    public bool inwall;
    public int rockattacklast;
    public int rockattack;
    public int shortdist;
    public int closeattackslowp1;
    public int closeattackshighp1;
    public int closeattackslowp2;
    public int closeattackshighp2;
    public int longattackslowp1;
    public int longattackshighp1;
    public int longattackslowp2;
    public int longattackshighp2;
    // Start is called before the first frame update

    //1 change by making redirect in 2
    //done
    //TRACKING ATTACKS
    //INLAY REDIRECT IN SEPERATE KNIFE THROWN IN PHASE 2
    public GameObject knife;
    public GameObject p2knife;
    public int numknives;
    public float degreesbetweenknives;
    public float timebeforeknives;
    public float timeafterknives;
    public float knifespeed;

    //prob not using
    public GameObject clone;

    //1 change by making rounds stay still for a while like buttonhook rounds in 2
    //change done
    public GameObject pistolround;
    public GameObject phase2pistolround;
    public float pistolroundspeed;
    public float pistolbackblast;

    //1, change to gren in 2
    public GameObject smokeburst;
    public GameObject telegrenade;

    //SERIES LOCKED IN ONCE ROCKS DROP HAVE TO SHOOT ALL
    //ROCKFALL GETS ROCKS FOR LATER USE, HAVE TO DECIDE HOW THEY FALL, IM THINKING THEY FALL TRACKING PLAYER MOVEMENT TO TRY AND HIT HIM, BECAUSE CAN USE MAX OF ROCKHOLDER ROCKS, OF COURSE COULD ALSO USE A SPREAD OF OTHER PROJECTILES LIKE BOMBS TO MAKE A SPREAD
    //COULD ALTER ROCK AND RUBBLE TO INCLUDE RUBBLE AND ROCK SHADOW
    //all rock attacks go in 2
    public float aftercallrocktime;
    public float numrocksdrop;
    public float timefall;
    public GameObject rock;
    public GameObject rockshadow;
    public float numrubbleinrockdrop;
    public GameObject rubble;
    public GameObject rubbleshadow;
    public float timeafterrockandwallfall;

    //DEFAULT FOR SPREAD AND SHOOT AND SMASH IS 3 I HAVE TO MANUALLY EDIT THIS IN FUTURE BECAUSE NOW THE ROCKSIN VARIABLES ARE NOT IN USE
    public List<GameObject> rocks = new List<GameObject>();
    public rockholder[] rockholders;
    public rockholder currentholder;
    public GameObject rockholderparent;
    public List<rockholder> rockholdersoccupied = new List<rockholder>();

    public float rocksinspread;
    public float rockspreadspeed;
    public float rockspreaddegrees;
    public float rockspreadtimetodestroy;

    public float rocksinsmash;
    public float rocksmashdegrees;
    public float rocksmashspeed;
    public float rocksmashtimetodestroy;

    public float rocksinshoot;
    public float rocktimebetweenshoot;
    public float rockshootspeed;

    //dont think im using
    //maybe thought could be better than caroucel
    public LineRenderer laser;
    public Color laserwarningcolor;
    public Color laserrealcolor;
    public float timetolaser;
    public float lasertimeactive;
    public bool warminguplaser;

    //SERIES LOCKED IN ONCE DONE
    //2, use in parallel with either laser or carocel
    public List<GameObject> walls = new List<GameObject>();
    public GameObject wall;
    public float numrubbleinwalldrop;
    public float numwallsinwalldrop;
    public Transform[] wallstartpositions;
    //TRADING MOVE POSITIONS FOR A MOVE DIRECTION;
    public Transform[] wallmovepositions;
   
    //2
    public GameObject carouselproj;
    public float carouselnumproj;
    public float carouselprojtime;
    public float carouselrotangle;
    public float carouselprojspeed;

    //1 could change in 2 to make either 2 sword slashes in row, or slash then teleport
    //done
    public GameObject swordwarning;
    public GameObject sword;
    public float swordslashdistance;
    public float swordslashoverwatchdist;
    public bool swordoverwatch;
    public toggle_collider swordtogcoll;
    public destroyaftertime sworddestroyafter;
    public float swordwarmuptime;
    public float swordbeforeoverwatch;
    public float swordafterwarning;
    public flash_sprite swordspriteflash;
    public GameObject swordoverwatchcircle;
    public float swordteleporttime;

    //1 no change in 2 but could chain with spike wall attack
    public float timebeforereverse;
    public float timereverseactive;

    //1 maybe change in 2
    public float lightningwarmup;
    public GameObject lightning;
    public testlightning lightningscript;

    public float timebetweenpistolshoot;
    public float numtimespistolshoot;

    public AudioSource source;
    public AudioClip[] sounds;
    public AudioClip[] rocksounds;
    //CHANGE ALL FIRST PHASE ATTACKS TO FIT SECOND PHASE.

    //REVERSAL ATTACK IN PHASE 1(done), LIGHTNING ATTACK PHASE 1(did lightning need to improve maybe, get opinion of others), NEED A COUPLE P 2 ATTACKS

    //MOMENTUM REVERSAL ATTACK OR MAKE IT SO FOR A CERTAIN AMOUNT OF TIME WHEN YOU SHOOT FORWARD, YOU MOVE FORWARD INSTEAD OF BACK

    //ATTACK WHERE HE THROWS PEICES OF ARENA AT PLAYER OR AROUND ARENA, THEN DOES AN INSTA HIT LASER ATTACK WITH A WARMUP, FORCING PLAYER TO USE PEICES OF ARENA TO BLOCK LASER

    //POSSIBLY ONLY 50 HEALTH

    //START SECOND PHASE SOON BECAUSE THAT GIVES PSYCHIC ATTACKS WHICH GIVES WAY MORE OPTIONS, HAVE SOME CHEEKY LINE OF DIALOGUE ABOUT THAT? (M/Y)

    //RIGHT NOW FIRST PHASE ATTACKS: KNIFE ATTACK, PISTOL MOVE, POSSIBLY CLONE ATTACK, LIGHTINING ATTACK, SWORD ATTACK MAYBE NEED SOME MORE AT LEAST 2 HOPEFULLY 4, EVEN IF CHANGE PHASE SOON, STILL IMPORTANT. SECOND PHASE ATTACKS: ALTER KNIFE ATTACK, POSSIBLY ALTER PISTOL MOVE (RELOCATE DURING ROCKFALL), POSSIBLY ALTER CLONE ATTACK POSSIBLY NOT USE, THREE ROCK ATTACKS, NEED A COUPLE MORE AS IN AT LEAST 4 HOPEFULLY 6 MORE

    //SELF DESTRUCT ADDED TO ROCKS SO THEY ALWAYS BURST SEMI CLOSE TO PLAYER?

    //HOW ABOUT HE USES PSYCHIC POWERS TO MAKE ROCKS HOVER AROUND HIM THEN FIRE THEM AT PLAYER IN VARYING PATTERNS, COULD DO STUFF WITH ROCKS AFTER FIRING THEM

    //ATTACK WHERE GUY STAYS SHIELDED IN MIDDLE OF ARENA AND SHIT GOES ON AND IF YOU DODGE ENOUGH YOU HAVE TIME TO GET A HIT IN, LIKE APESHIT FOR MONKEY

    //MAYBE AN CAROCEL ATTACK LIKE THE FIRE ONE I WAS GOING TO MAKE (M)

    //SLASH ATTACK LIKE THAT ASSIST MODEL FROM SMASH BROS, POSSIBLY WHILE THROWING STUFF (M/N READ MORE AT END) COULD BE TOO EASY TO DODGE BECAUSE BASICALLY SAME THING AS GEYSER, COULD CHANGE IT TO BE AN INSTANT ATTACK WHERE YOU HAVE TO GET BEHIND COVER TO AVOID DAMAGE BUT MARINE THROWS SHIT AT YOU WHILE YOU TRY TO GET TO COVER

    //CLONES GO IN DIFFERENT PATTERNS?

    //SOME ATTACKS WITH GRENADES POSSIBLY (KINDA DIFFICULT TO DO BECAUSE ALREADY A LOT OF EXPLOSIVE/GRENADE ATTACK)

    //COULD MAKE ARENA FILLED WITH TRAPS THAT MARINE ACTIVATES, LIKE TESLA COIL BUT MORE AND BETTER (M/Y) 

    //SHATTER ATTACK? LIKE ROCK WITH ZOMBIE BOSS BUT COULD/WOULD CHANGE IT UP A BIT/LOT (DONE I THINK)

    //MAYBE SOMETHING SIMILAR TO CHEMIST SMOKE ATTACK, WHERE YOU HAVE TO TAKE IT OUT PRETTY QUICKLY (M) (ENEMY SPAWNER? PROB NOT BECAUSE KINDA WANT A 1 ON 1 FIGHT)

    //MAYBE GIVE MARINE PSYCHIC ATTACKS? GIVE HIM A SENETENCE WHERE HE SAYS HE WAS/IS A HUMAN TEST SUBJECT AND CALL HIM EXPERIMENT XXX OR WHATEVER, WOULD OPEN UP A NEW AVENUE. (Y)

    //GOT 4 ATTACKS NEED TEN THEN A BONUS THING LIKE THE SHEILDED APESHIT NOTED ABOVE

    //HOW ABOUT USING PSYCHIC POWERS TO REDIRECT ATTACKS ONCE OR TWICE WHEN IN AIR, EG. THROW KNIFE, ABOUT TO MISS, CHANGE DIRECTION WITH POWERS, WOULD NEED SOME WAY TO SHOW KNIFE ABOUT TO CHANGE
    private void Awake()
    {
        betweenattacktime *= FindObjectOfType<difficulty_script>().difficulty_wait_time_multipliers[FindObjectOfType<difficulty_script>().difficulty];
        attacktype = 0;
        thisrottoplayer.canrot = true;
        //currenthealth = bossstuff.bosshealth;
        infrontmag = Vector3.Distance(infront.position, this.transform.position);
    }
    void Start()
    {
        Player = FindObjectOfType<player>();
        playerrigid = Player.GetComponentInChildren<Rigidbody>();
        Playershoot = Player.GetComponentInChildren<shoot2>();
    }
    public IEnumerator attackfour_lightningattack()
    {
        yield return new WaitForSeconds(lightningwarmup);
        GameObject lightninginstan = Instantiate(lightning, throwpoint.transform.position, Quaternion.Euler(0, 180+throwpoint.transform.eulerAngles.y, 0));
        lightninginstan.GetComponent<testlightning>().makelightning(0, 0, 0);
        attacktype = 0;
    }
    public IEnumerator Invincible(float time)
    {
        bosstakedam.invincible = true;
        thissprite.color = Color.yellow;
        yield return new WaitForSeconds(time);
        bosstakedam.invincible = false;
        thissprite.color = Color.white;
    }
    //SHOULD THIS REVERSE CURRENT VELO OR NOT? CURRENTLY IT DOES;
    public IEnumerator attackfive_reversalattack()
    {
        yield return new WaitForSeconds(timebeforereverse);
        print("REVERSAL");
        Playershoot.startreverse(timereverseactive);
        attacktype = 0;
    }
    public void trackingadjust(float objectspeed, GameObject objecttoadjust)
    {
        print("OBJECTTOADJUST: " + objecttoadjust.name);
        float timetoplayer = Vector3.Distance(Player.transform.position, objecttoadjust.transform.position) / objectspeed;
        //print("DISTANCE: " + Vector3.Distance(Player.transform.position, objecttoadjust.transform.position) + " SPEED: " + objectspeed + "TIME TO PLAYER: " + timetoplayer);
        Vector3 adjust = (timetoplayer * playerrigid.velocity)/2 + playerrigid.transform.position;
        //print("ADJUST: " + adjust);
        Debug.DrawLine(adjust - transform.up, adjust + transform.up, Color.magenta, 10000);
        objecttoadjust.transform.eulerAngles = new Vector3(objecttoadjust.transform.eulerAngles.x, Mathf.Atan2((adjust.x - objecttoadjust.transform.position.x), (adjust.z - objecttoadjust.transform.position.z))* Mathf.Rad2Deg, objecttoadjust.transform.eulerAngles.z);
        //print("ADJUST ANGLE: " + Mathf.Atan2((adjust.x - objecttoadjust.transform.position.x), (adjust.z - objecttoadjust.transform.position.z)) * Mathf.Rad2Deg);
    }
    //TRACKS BASED ON CURRENT VELO COULD CHANGE TO ANTICIPATE FUTURE SPEED AFTER PLAYER SHOOTS ONCE OR TWICE;
    public IEnumerator attackthree_knifethrow()
    {
        yield return new WaitForSeconds(timebeforeknives);
        float timetoplayer = Vector3.Distance(Player.transform.position, this.transform.position)/knifespeed;
        Vector3 adjust = timetoplayer * playerrigid.velocity;
        Vector3 tempangle = this.transform.eulerAngles;
        this.transform.eulerAngles = new Vector3(90, -90+Mathf.Atan2((Player.transform.position.x - this.transform.position.x), (Player.transform.position.z - this.transform.position.z)) * Mathf.Rad2Deg, 0);
        if(bossstuff.phase2 == false)
        {
            for (int j = 0; j < numknives; j++)
            {
                GameObject knifeinstan = Instantiate(knife, throwpoint.position, throwpoint.transform.rotation);
                trackingadjust(knifespeed, knifeinstan);
                knifeinstan.transform.eulerAngles += new Vector3(0, 90 + -((numknives - 1) / 2) * degreesbetweenknives + degreesbetweenknives * j, 0);
                knifeinstan.GetComponent<Rigidbody>().velocity = knifeinstan.transform.right * -knifespeed;
            }
        }
        else
        {
            for (int j = 0; j < numknives; j++)
            {
                GameObject knifeinstan = Instantiate(p2knife, throwpoint.position, throwpoint.transform.rotation);
                trackingadjust(knifespeed, knifeinstan);
                knifeinstan.transform.eulerAngles += new Vector3(0, 90 + -((numknives - 1) / 2) * degreesbetweenknives + degreesbetweenknives * j, 0);
                knifeinstan.GetComponent<Rigidbody>().velocity = knifeinstan.transform.right * -knifespeed;
            }
        }
        //this.transform.eulerAngles = tempangle;
        yield return new WaitForSeconds(timeafterknives);
        attacktype = 0;
    }
    //MAKE PISTOL ROUNDS SELF PROPEL MAYBE IF NOT FIX THIS BELOW CODE
    public void pistolmove()
    {
        GameObject round;
        if (bossstuff.phase2)
        {
            round = Instantiate(phase2pistolround, toplayer.position + (toplayer.up * -1 * infrontmag), toplayer.transform.rotation * Quaternion.Euler(0, 0, 90));
        }
        else
        {
            round = Instantiate(pistolround, toplayer.position + (toplayer.up * -1 * infrontmag), toplayer.transform.rotation * Quaternion.Euler(0, 0, 90));
        }
        round.GetComponent<Rigidbody>().velocity = pistolroundspeed * -1 * toplayer.transform.up;
        round = Instantiate(pistolround, opptoplayer.position + (opptoplayer.up * -1 * infrontmag), opptoplayer.transform.rotation*Quaternion.Euler(0,0, 90));
        round.GetComponent<Rigidbody>().velocity = pistolroundspeed *-1 * opptoplayer.transform.up;
        print("ADD FORCE BACKWARDS");
        thisrigid.AddForce(toplayer.transform.up * pistolbackblast);
        thisrigid.AddForce(opptoplayer.transform.up * pistolbackblast);
    }
    public void cloneattack()
    {
        foreach(Transform a in teleportpos)
        {
            Instantiate(clone, a.position, Quaternion.identity);
        }
    }
    public IEnumerator swordteleport()
    {
        yield return new WaitForSeconds(swordteleporttime);
        Instantiate(telegrenade, this.transform.position, Quaternion.identity*Quaternion.Euler(90,0,0));
        int rand = Random.Range(0, teleportpos.Length);
        this.transform.position = teleportpos[rand].position;
    }
    public void teletocenter()
    {
        Instantiate(telegrenade, this.transform.position, Quaternion.identity * Quaternion.Euler(90, 0, 0));
        this.transform.position = center.position;
        thisrigid.velocity = Vector3.zero;
    }
    public void ondamage()
    {
        print("ONDAMAGE");
        //THIS SPAWNS A SMOKE BUT REPLACING WITH TELEGREN
        if (bossstuff.phase2 == false)
        {
            //Instantiate(smokeburst, this.transform.position, Quaternion.identity);
            Instantiate(telegrenade, this.transform.position, Quaternion.identity * Quaternion.Euler(90, 0, 0));
        }
        else
        {
            Instantiate(telegrenade, this.transform.position, Quaternion.identity * Quaternion.Euler(90, 0, 0));
        }
        int rand = Random.Range(0, teleportpos.Length);
        this.transform.position = teleportpos[rand].position;
    }
    //VARY DROPTIMES IN RUBBLE AND ROCK SCRIPTS, RUBBLE DESTROYED ON INSTANTIATION, ROCKS STAY TO BE USED, ALTHOUGH COULD BE COOL TO USE BOTH ROCKS AND RUBBLE AS PROJECTILES ALTHOUGH THEY WOULD DO THE SAME THINGS
    public IEnumerator attacksix_rockfall()
    {
        yield return new WaitForSeconds(0);
        teletocenter();
        for (int i = 0; i < numrocksdrop; i++)
        {
            //CHANGE TO ARENA SIZE
            Vector3 pos = new Vector3(Random.Range(9, 98f), 0, Random.Range(-26, 60f));
            Instantiate(rock, pos, this.transform.rotation);
            //yield return new WaitForSeconds(betweenmortar);
        }
        for (int i = 0; i < numrubbleinrockdrop; i++)
        {
            //CHANGE TO ARENA SIZE
            Vector3 pos = new Vector3(Random.Range(9, 98f), 0, Random.Range(-26, 60f));
            Instantiate(rubble, pos, this.transform.rotation);
            //yield return new WaitForSeconds(betweenmortar);
        }
        StartCoroutine(Invincible(timeafterrockandwallfall));
        yield return new WaitForSeconds(timeafterrockandwallfall);
        StartCoroutine(callrocks());
    }
    //FIX TO MAKE WALLS FALL IN FIXED POSITIONS AND MAKE WALLS MOVE TO FIXED POSITIONS
    public IEnumerator attackseven_wallfall()
    {
        yield return new WaitForSeconds(0);
        teletocenter();
        for (int i = 0; i < numwallsinwalldrop; i++)
        {
            GameObject wallinstan = Instantiate(wall, wallstartpositions[i].position, wallstartpositions[i].rotation);
            //walls.Add(wallinstan);
            //int f = i + 1;
            //if (f == numwallsinwalldrop)
            //{
            //    f = 0;
            //}
            wallinstan.GetComponent<wallmover>().startmove(i, wallmovepositions);
        }
        for (int i = 0; i < numrubbleinwalldrop; i++)
        {
            //CHANGE TO ARENA SIZE
            Vector3 pos = new Vector3(Random.Range(9, 98f), 0, Random.Range(-26, 60f));
            Instantiate(rubble, pos, this.transform.rotation);
            //yield return new WaitForSeconds(betweenmortar);
        }
        StartCoroutine(Invincible(timeafterrockandwallfall+ carouselnumproj * carouselprojtime));
        yield return new WaitForSeconds(timeafterrockandwallfall);
        print("CAROUCEL");
        StartCoroutine(carousel());
    }
    public IEnumerator callrocks()
    {
        source.clip = sounds[7];
        source.Play();
        yield return new WaitForSeconds(0);
        for (int i = 0; i < rocks.Count; i++)
        {
            rocks[i].GetComponent<rockcallback>().callback(rockholders[i].gameObject);
        }
        yield return new WaitForSeconds(aftercallrocktime);
        foreach (rockholder a in rockholders)
        {
            if (a.occupied)
            {
                rockholdersoccupied.Add(a);
            }
        }
        attacktype = 0;
        rockattack = 0;
    }
    //ADD CHANGE HOLDER TO THESE AND MAKE SURE THE ROCKS YOU GET ARE OCCUPIED
    //BASIC ROCK SHOTGUN
    public IEnumerator rockspread()
    {
        yield return new WaitForSeconds(0);
        //int timer = 0;
        //for (int i = 0; i < rockholders.Length; i++)
        //{
        //    if (currentholder == rockholders[i])
        //    {
        //        break;
        //    }
        //    timer++;
        //}
        //int timerup = timer + 1;
        //if (timerup == rockholders.Length)
        //{
        //    timerup = 0;
        //}
        //int timerdown = timer - 1;
        //if (timerdown < 0)
        //{
        //    timerdown = rockholders.Length - 1;
        //}
        //GameObject[] throwingrocks = { rocks[timer], rocks[timerup], rocks[timerdown] };
        GameObject[] throwingrocks = getrocklist(rocksinspread);
        print("THROWINGROCKS LENGTH: " + throwingrocks.Length);
        float rocksinspreadtemp = rocksinspread;
        if (throwingrocks.Length < rocksinspread)
        {
            rocksinspreadtemp = throwingrocks.Length;
        }
        for (int i = 0; i < rocksinspreadtemp; i++)
        {
            print("THROWINGROCKS TYPE: " + throwingrocks[i].GetType()+" I: "+i);
            throwingrocks[i].transform.rotation = throwingrocks[i].transform.rotation /* * Quaternion.Euler(new Vector3(0, 0, -90)*/;
            trackingadjust(rockspreadspeed, throwingrocks[i]);
            throwingrocks[i].transform.eulerAngles += new Vector3(0, 0, throwpoint.transform.eulerAngles.z - (rockspreaddegrees * ((rocksinspreadtemp - 1) / 2)) + rockspreaddegrees * i);
            throwingrocks[i].transform.parent = null;
            throwingrocks[i].GetComponent<Rigidbody>().velocity = throwingrocks[i].transform.up * rockspreadspeed;
            throwingrocks[i].GetComponent<rockscript2>().timetoexplodevoid(rockspreadtimetodestroy);
            //spreadinstan.GetComponent<Rigidbody2D>().velocity = spreadinstan.transform.up * spreadprojspeed;
        }
        attacktype = 0;
        rockattack = 0;
    }
    //BASIC ROCKS SHOOT
    public IEnumerator rockshoot()
    {
        //yield return new WaitForSeconds(0);
        //int timer = 0;
        //for (int i = 0; i < rockholders.Length; i++)
        //{
        //    if (currentholder == rockholders[i])
        //    {
        //        break;
        //    }
        //    timer++;
        //}
        //int timerup = timer + 1;
        //if (timerup == rockholders.Length)
        //{
        //    timerup = 0;
        //}
        //int timerdown = timer - 1;
        //if (timerdown < 0)
        //{
        //    timerdown = rockholders.Length - 1;
        //}
        //GameObject[] throwingrocks = { rocks[timer], rocks[timerup], rocks[timerdown] };
        GameObject[] throwingrocks = getrocklist(rocksinshoot);
        float rocksinshoottemp = rocksinshoot;
        if (throwingrocks.Length < rocksinshoot)
        {
            rocksinshoottemp = throwingrocks.Length;
        }
        for(int i = 0; i < rocksinshoottemp; i++)
        {
            //trackingadjust(rockshootspeed, rock);
            //rock.GetComponent<Rigidbody>().velocity = throwpoint.transform.forward * rockshootspeed;
            throwingrocks[i].transform.rotation = throwingrocks[i].transform.rotation /* * Quaternion.Euler(new Vector3(0, 0, -90)*/;
            trackingadjust(rockspreadspeed, throwingrocks[i]);
            throwingrocks[i].transform.eulerAngles += new Vector3(0, 0, throwpoint.transform.eulerAngles.z);
            throwingrocks[i].transform.parent = null;
            //throwingrocks[i].transform.eulerAngles = new Vector3(throwingrocks[i].transform.eulerAngles.x, Mathf.Atan2((Player.transform.position.x - throwingrocks[i].transform.position.x), (Player.transform.position.z - throwingrocks[i].transform.position.z)) * Mathf.Rad2Deg, throwingrocks[i].transform.eulerAngles.z);
            throwingrocks[i].GetComponent<Rigidbody>().velocity = throwingrocks[i].transform.up * rockshootspeed;
            yield return new WaitForSeconds(rocktimebetweenshoot);
        }
        attacktype = 0;
        rockattack = 0;
    }
    //CLOSEISH AOE PUTS ROCKS OUT A LITTLE FAR FROM SELF THEN EXPLODES THEM ALL;
    public IEnumerator rocksmash()
    {
        yield return new WaitForSeconds(0);
        //int timer = 0;
        //for (int i = 0; i < rockholders.Length; i++)
        //{
        //    if (currentholder == rockholders[i])
        //    {
        //        break;
        //    }
        //    timer++;
        //}
        //int timerup = timer + 1;
        //if (timerup == rockholders.Length)
        //{
        //    timerup = 0;
        //}
        //int timerdown = timer - 1;
        //if (timerdown < 0)
        //{
        //    timerdown = rockholders.Length - 1;
        //}
        //GameObject[] throwingrocks = { rocks[timer], rocks[timerup], rocks[timerdown] };
        GameObject[] throwingrocks = getrocklist(rocksinsmash);
        float rocksinsmashtemp = rocksinsmash;
        if (throwingrocks.Length < rocksinsmash)
        {
            rocksinsmashtemp = throwingrocks.Length;
        }
        for (int i = 0; i < rocksinsmashtemp; i++)
        {
            throwingrocks[i].transform.rotation = throwingrocks[i].transform.rotation * Quaternion.Euler(0, 0, throwpoint.transform.eulerAngles.z - (rocksmashdegrees * ((rocksinsmash - 1) / 2)) + rocksmashdegrees * i) * Quaternion.Euler(new Vector3(0, 0, -90));
            throwingrocks[i].transform.parent = null;
            throwingrocks[i].GetComponent<Rigidbody>().velocity = throwingrocks[i].transform.up * rocksmashspeed;
            throwingrocks[i].GetComponent<rockscript2>().timetoexplodevoid(rocksmashtimetodestroy);
            //spreadinstan.GetComponent<Rigidbody2D>().velocity = spreadinstan.transform.up * spreadprojspeed;
        }
        rockholdersoccupied = new List<rockholder>();
        foreach (rockholder a in rockholders)
        {
            if (a.occupied)
            {
                rockholdersoccupied.Add(a);
            }
        }
        attacktype = 0;
        rockattack = 0;
    }
    //MAYBE AN ATTACK WHERE ROCKS INCREASE IN SPEED WHILE EXTENDING OUTWARD THEN GO BACK TO REGULAR
    public GameObject[] getrocklistnotused2(int numrocks)
    {
        int timer = 0;
        for (int i = 0; i < rockholders.Length; i++)
        {
            if (currentholder == rockholders[i])
            {
                break;
            }
            timer++;
        }
        List<int> indexlist = new List<int>();
        //UP, DOWN, UP, DOWN ECT.
        GameObject[] throwingrocks = null;
        return throwingrocks;
    }
    public GameObject[] getrocklist(float numrocks)
    {
        float mindistancefrom180 = 0;
        List<GameObject> throwingrocks = new List<GameObject>();
        rockholdersoccupied = new List<rockholder>();
        foreach(rockholder a in rockholders)
        {
            if (a.occupied)
            {
                rockholdersoccupied.Add(a);
            }
        }
        foreach (rockholder a in rockholdersoccupied)
        {
            if (a.occupied == false)
            {
                rockholdersoccupied.Remove(a);
            }
            if(a.occupied == true)
            {
                print("OCCUPPIED: MIN DIST: "+ (Mathf.Abs((Mathf.Atan2((rockholderparent.transform.position.x - a.transform.position.x), (rockholderparent.transform.position.z - a.transform.position.z)) * Mathf.Rad2Deg))));
                if ((Mathf.Abs(Mathf.Atan2((rockholderparent.transform.position.x - a.transform.position.x), (rockholderparent.transform.position.z - a.transform.position.z)) * Mathf.Rad2Deg)) > mindistancefrom180)
                {
                    mindistancefrom180 = (Mathf.Abs(Mathf.Atan2((rockholderparent.transform.position.x - a.transform.position.x), (rockholderparent.transform.position.z - a.transform.position.z)) * Mathf.Rad2Deg));
                    currentholder = a;
                    print("CURRENT HOLDER: " + a.gameObject.name);
                }
            }
        }
        print("CURRENT HOLDER: " + currentholder.gameObject.name);
        int currentindex = rockholdersoccupied.IndexOf(currentholder);
        print("CURRENT INDEX FOUND");
        if(currentindex != -1)
        {
            throwingrocks.Add(rockholdersoccupied[currentindex].rock);
            rockholdersoccupied.Remove(rockholdersoccupied[currentindex]);
        }
        for (int i = 0; i<numrocks-1; i++)
        {
            if (rockholdersoccupied.Count == 0)
            {
                print("BREAK NUM: "+i);
                break;
            }
            print("GET INDEX NUM: " + i);
            int rockind = getrockindex(i % 2 == 1, currentindex-i);
            print("FOUND INDEX NUM: " + i);
            throwingrocks.Add(rockholdersoccupied[rockind].rock);
            rockholders[rockind].occupied = false;
            rockholdersoccupied.Remove(rockholdersoccupied[rockind]);
        }
        GameObject[] throwingrocksset = throwingrocks.ToArray();
        return throwingrocksset;
    }
    public int getrockindex(bool up, int startindex)
    {
        int rockindex = startindex;
        if (up)
        {
            rockindex++;
            if(rockindex >= rockholdersoccupied.Count)
            {
                rockindex = 0;
            }
        }
        else
        {
            rockindex--;
            if(rockindex < 0)
            {
                rockindex = rockholdersoccupied.Count - 1;
            }
        }
        return rockindex;
    }
    public IEnumerator laserattack()
    {
        laser.startColor = laserwarningcolor;
        laser.endColor = laserwarningcolor;
        laser.gameObject.SetActive(true);
        warminguplaser = true;
        yield return new WaitForSeconds(timetolaser);
        warminguplaser = false;
        laser.startColor = laserrealcolor;
        laser.endColor = laserrealcolor;
        yield return new WaitForSeconds(lasertimeactive);
        laser.gameObject.SetActive(false);
    }
    public void redirectprojtrack(Rigidbody objrigid)
    {

    }
    public void redirectprojnotrack(Rigidbody objrigid)
    {
        float mag = objrigid.velocity.magnitude;
        objrigid.transform.eulerAngles = new Vector3(90, Mathf.Atan2((Player.transform.position.x - this.transform.position.x), (Player.transform.position.z - this.transform.position.z)) * Mathf.Rad2Deg, 0);
        objrigid.velocity = objrigid.transform.right * mag;
    }
    public IEnumerator carousel()
    {
        thisrottoplayer.canrot = false;
        Transform a = throwpoint;
        for(int i = 0; i < carouselnumproj; i++)
        {
            GameObject proj = Instantiate(carouselproj, throwpoint.transform.position, throwpoint.transform.rotation);
            proj.GetComponent<Rigidbody>().velocity = carouselprojspeed * proj.transform.up;
            throwpoint.transform.Rotate(new Vector3(0, 0, carouselrotangle));
            yield return new WaitForSeconds(carouselprojtime);
        }
        thisrottoplayer.canrot = true;
        yield return new WaitForSeconds(0);
        attacktype = 0;
    }
    public IEnumerator attacktwo_swordattack()
    {
        GameObject swordinstan = Instantiate(swordwarning, throwpoint.transform);
        swordinstan.transform.localPosition = Vector3.zero;
        swordinstan.transform.eulerAngles = new Vector3(90, swordinstan.transform.eulerAngles.y, swordinstan.transform.eulerAngles.z);
        swordtogcoll = swordinstan.GetComponent<toggle_collider>();
        swordspriteflash = swordinstan.GetComponent<flash_sprite>();
        sworddestroyafter = swordinstan.GetComponent<destroyaftertime>();
        yield return new WaitForSeconds(swordbeforeoverwatch);
        swordoverwatchcircle.SetActive(true);
        swordoverwatch = true;
        yield return new WaitForSeconds(swordwarmuptime-swordbeforeoverwatch);
        swordspriteflash.startflash();
        yield return new WaitForSeconds(swordafterwarning);
        swordslash();
        attacktype = 0;
    }
    public void swordslash()
    {
        swordspriteflash.endflash();
        swordspriteflash.changecolor(67, 70, 75);
        swordoverwatch = false;
        swordspriteflash.gameObject.transform.parent = null;
        this.transform.position += swordslashdistance * this.transform.right;
        swordtogcoll.togglecollider(true);
        sworddestroyafter.Init(.2f);
        swordoverwatchcircle.SetActive(false);
        if(bossstuff.phase2)
        {
            StartCoroutine(swordteleport());
        }
    }
    public IEnumerator attackone_pistolburst()
    {
        thisrottoplayer.canrot = false;
        for (int i = 0; i<numtimespistolshoot; i++)
        {
            print("PISTOL MOVE");
            pistolmove();
            yield return new WaitForSeconds(timebetweenpistolshoot);
        }
        thisrottoplayer.canrot = true;
        attacktype = 0;
    }
    public IEnumerator nextattackrock()
    {
        rockattack = 999;
        yield return new WaitForSeconds(betweenattacktime);
        rockattack = Random.Range(1, 4);
        while (rockattack == rockattacklast)
        {
            rockattack = Random.Range(1, 4);
        }
        print("ROCK ATTACK NUM: " + rockattack);
        rockattacklast = rockattack;
        if(rockattack == 1)
        {
            StartCoroutine(rockshoot());
        }
        else if(rockattack == 2)
        {
            StartCoroutine(rockspread());
        }
        else if(rockattack == 3)
        {
            StartCoroutine(rocksmash());
        }
        source.clip = rocksounds[rockattack - 1];
        source.Play();
    }
    public void picklong()
    {
        if (bossstuff.phase2)
        {
            attacktype = Random.Range(longattackslowp2, longattackshighp2 + 1);
            while (attacktype == lastattacktype)
            {
                attacktype = Random.Range(longattackslowp2, longattackshighp2 + 1);
            }
        }
        else
        {
            attacktype = Random.Range(longattackslowp1, longattackshighp1 + 1);
            while (attacktype == lastattacktype)
            {
                attacktype = Random.Range(longattackslowp1, longattackshighp1 + 1);
            }
        }
    }
    public IEnumerator nextattackstandard()
    {
        attacktype = 999;
        yield return new WaitForSeconds(betweenattacktime);
        if (Vector3.Distance(this.transform.position, Player.transform.position) <= shortdist)
        {
            attacktype = 1;
            StartCoroutine(attackone_pistolburst());
        }
        else
        {
            picklong();
        }
        lastattacktype = attacktype;
        print("ATTACKTYPE P1"+attacktype);
        if (attacktype == 1)
        {
            StartCoroutine(attackone_pistolburst());
        }
        if (attacktype == 2)
        {
            StartCoroutine(attacktwo_swordattack());
        }
        if (attacktype == 3)
        {
            StartCoroutine(attackthree_knifethrow());
        }
        if (attacktype == 4)
        {
            StartCoroutine(attackfour_lightningattack());
        }
        if (attacktype == 5)
        {
            StartCoroutine(attackfive_reversalattack());
        }
        if (attacktype == 6)
        {
            StartCoroutine(attacksix_rockfall());
        }
        if (attacktype == 7)
        {
            StartCoroutine(attackseven_wallfall());
        }
        print("ATTACKTYPE: " + attacktype);
        source.clip = sounds[attacktype-1];
        source.Play();
    }
    public void buttoncont() {
        //if (warminguplaser)
        //{
        //    laser.SetPosition(0, this.transform.position);
        //    laser.SetPosition(1, Player.transform.position);
        //}
        //pistolmove();
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            source.clip = sounds[0];
            source.Play();
            StartCoroutine(attackthree_knifethrow());
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            source.clip = sounds[1];
            source.Play();
            StartCoroutine(attacksix_rockfall());
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            source.clip = sounds[2];
            source.Play();
            StartCoroutine(callrocks());
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            source.clip = sounds[3];
            source.Play();
            print("SPREAD");
            StartCoroutine(rockspread());
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            source.clip = sounds[4];
            source.Play();
            print("SHOOT");
            StartCoroutine(rockshoot());
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            source.clip = sounds[5];
            source.Play();
            print("SMASH");
            StartCoroutine(rocksmash());
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            source.clip = sounds[6];
            source.Play();
            print("SMASH");
            StartCoroutine(attackseven_wallfall());
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            source.clip = sounds[7];
            source.Play();
            StartCoroutine(carousel());
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            source.clip = sounds[8];
            source.Play();
            StartCoroutine(attacktwo_swordattack());
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            source.clip = sounds[9];
            source.Play();
            StartCoroutine(attackfour_lightningattack());
        }
        if (Input.GetKeyDown(KeyCode.Minus))
        {
            source.clip = sounds[10];
            source.Play();
            StartCoroutine(attackfive_reversalattack());
        }
        if (Input.GetKeyDown(KeyCode.Equals))
        {
            source.clip = sounds[11];
            source.Play();
            print("PISTOL BURST");
            StartCoroutine(attackone_pistolburst());
        }

        //if(currenthealth != bossstuff.bosshealth)
        //{
        //    currenthealth = bossstuff.bosshealth;
        //    ondamage();
        //}
        //MAY WANT TO REMOVE THIS LATER BUT MAYBE NOT IT IS IN UPDATE BUT ONLY AT MOST 12 IN LIST SO PROB NOT TO BAD
    }
        // Update is called once per frame
    void Update()
    {
        if(rockholdersoccupied.Count > 0)
        {
            print("ROCKHOLDER > 0");
            if(rockattack == 0)
            {
                print("ROCK ATTACK IS 0");
            }
        }
        if(rockholdersoccupied.Count > 0&& rockattack == 0)
        {
            print("ROCK ATTACK");
            StartCoroutine(nextattackrock());
        }
        else if(attacktype == 0 && rockattack == 0)
        {
            print("NEXT ATTACK");
            StartCoroutine(nextattackstandard());
        }
        if (swordoverwatch && Vector3.Distance(this.transform.position, Player.transform.position) <= swordslashoverwatchdist)
        {
            print("OVERWATCH");
            //REMOVE STOP ALL, PROBLEM BECAUSE STOP COROUTINE SWORD ATTACK IS NOT WORKING
            StopCoroutine(attacktwo_swordattack());
            StopAllCoroutines();
            swordslash();
            attacktype = 0;
        }
        if(currenthealth != bossstuff.bosshealth)
        {
            currenthealth = bossstuff.bosshealth;
            ondamage();
        }
    }
}
