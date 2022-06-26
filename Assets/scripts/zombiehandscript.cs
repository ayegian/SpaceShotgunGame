using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombiehandscript : MonoBehaviour
{
    public player Player;
    public GameObject handsprite;
    public Animator anim;
    public GameObject anim2_obj;
    public Animator anim2;
    public BoxCollider2D handcollider;
    public BoxCollider2D handcollider2;
    public BoxCollider2D handcollider3;
    public float timebetweenattackslarge;
    public float timebetweenattackssmall;
    public int attacktype;
    public int lastattacktype;


    public AudioSource source;
    public AudioClip[] sounds;

    public Transform handstartpos;
    public GameObject hand;
    public Rigidbody2D handrigid;
    public GameObject handthrowpoint;
    public bool handmovingback;
    public float handsmovebackspeed;
    // Start is called before the first frame update
    public GameObject rock;
    public float timebeforerock;
    public float timeafterrock;
    public float rockspeed;

    public GameObject shadow;
    public GameObject shadowinstan;
    public bool shadowtrack;
    public float timebeforeslam;
    public float timestillslam;
    public float timeafterslam;

    public float timebeforepunch;
    public float timeafterpunch;
    public float punchspeed;

    public bool staggerstart;
    public float staggertime;

    public bool can_rot;
    //FUCK UPS SIGHTED
    //ROCK WARM UP HAPPENING JUST BEFORE SLAM START UP
    private void Awake()
    {
        can_rot = true;
        attacktype = 999;
        timebetweenattackslarge *= FindObjectOfType<difficulty_script>().difficulty_wait_time_multipliers[FindObjectOfType<difficulty_script>().difficulty];
        timebetweenattackssmall *= FindObjectOfType<difficulty_script>().difficulty_wait_time_multipliers[FindObjectOfType<difficulty_script>().difficulty];
        if (staggerstart)
        {
            StartCoroutine(stagger());
        }
        else
        {
            attacktype = 0;
        }
    }
        void Start()
    {
        Player = GameObject.FindObjectOfType<player>();
    }
    IEnumerator stagger()
    {
        attacktype = 999;
        yield return new WaitForSeconds(staggertime);
        attacktype = 0;
    }
    IEnumerator handslam_attackone()
    {
        print("HANDS SLAM");
        handsprite.SetActive(false);
        //handcollider.enabled = false;
        handcollider2.enabled = false;
        handcollider3.enabled = false;
        can_rot = false;
        shadowinstan = Instantiate(shadow, this.transform.position, Quaternion.identity);
        shadowinstan.SetActive(true);
        move_towards_target shadow_move_script = shadowinstan.GetComponent<move_towards_target>();
        shadow_move_script.target = Player.gameObject;
        //MAKE SHADOW TRACK PLAYER OR JUST USE A DIFFERENT OBJECT LIKE THE MORTAR
        yield return new WaitForSeconds(timebeforeslam);
        shadow_move_script.enabled = false;
        hand.transform.position = shadowinstan.transform.position;
        yield return new WaitForSeconds(timestillslam);
        Collider2D[] overlaps = new Collider2D[100];
        ContactFilter2D filter = new ContactFilter2D();
        filter.NoFilter();
        Physics2D.OverlapCircle(shadowinstan.transform.position, 3.5f, filter, overlaps);
        Debug.DrawRay(shadowinstan.transform.position, 3.5f * shadowinstan.transform.right, Color.blue, 10);
        foreach(Collider2D coll in overlaps)
        {
            if(coll == null)
            {
                print("COLL NULL");
                break;
            }
            else
            {
                print("HIT OBJ: " + coll.gameObject.name);
            }
            if (coll.gameObject.CompareTag("player"))
            {
                Vector2 close_point = shadowinstan.transform.position + (Player.transform.position - shadowinstan.transform.position)* 10;
                print("CLOSE POINT BEFORE: " + close_point);
                //Player.health -= 1;
                close_point = Physics2D.ClosestPoint(close_point, handcollider);
                print("HIT PLAYER CLOSE POINT: "+close_point);
                Player.transform.position = new Vector3(close_point.x, close_point.y, Player.transform.position.z);
                break;
            }
        }
        Destroy(shadowinstan);

        handsprite.SetActive(true);
        //handcollider.enabled = true;
        handcollider2.enabled = true;
        handcollider3.enabled = true;
        //move back hands
        yield return new WaitForSeconds(timeafterslam);
        handmovingback = true;
    }
    IEnumerator rockthrow_attacktwo()
    {
        anim2_obj.SetActive(true);
        can_rot = false;
        print("ROCK ATTACK");
        anim2.SetTrigger("shoot");
        yield return new WaitForSeconds(timebeforerock);
        anim2.SetTrigger("idle");
        anim2_obj.SetActive(false);
        GameObject rockinstan = Instantiate(rock, handthrowpoint.transform.position, handthrowpoint.transform.rotation);
        rockinstan.GetComponent<Rigidbody2D>().velocity = rockinstan.transform.right * rockspeed;
        print("ROCKINSTAN VELO: " + rockinstan.transform.right * rockspeed);
        yield return new WaitForSeconds(timeafterrock);
        print("ROCK ZERO");
        attacktype = 0;
        can_rot = true;
    }
    IEnumerator fistpunch_attackthree()
    {
        print("FIST PUNCH");
        can_rot = false;
        yield return new WaitForSeconds(timebeforepunch);
        print("FIST PUNCH SECONDS: " + (Vector2.Distance(Player.transform.position, hand.transform.position) + 10) / punchspeed);
        handrigid.velocity = punchspeed * Vector3.Normalize(new Vector2(Player.transform.position.x - hand.transform.position.x, Player.transform.position.y - hand.transform.position.y));
        yield return new WaitForSeconds((Vector2.Distance(Player.transform.position, hand.transform.position) + 10) / punchspeed);
        handmovingback = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("boss"))
        {
            handmovingback = true;
        }
    }
    public IEnumerator betweenattacks()
    {
        attacktype = 555;
        print("BETWEEN ATTACK START");
        yield return new WaitForSeconds(Random.Range(timebetweenattackssmall, timebetweenattackslarge));
        print("BETWEEN ATTACKS END");
        attacktype = Random.Range(1, 4);
        while(attacktype == lastattacktype)
        {
            attacktype = Random.Range(1, 4);
        }
        lastattacktype = attacktype;
        if(attacktype == 1)
        {
            StartCoroutine(handslam_attackone());
        }
        else if(attacktype == 2)
        {
            StartCoroutine(rockthrow_attacktwo());
        }
        else if(attacktype == 3)
        {
            StartCoroutine(fistpunch_attackthree());
        }
        source.clip = sounds[attacktype - 1];
        source.Play();
    }
    void buttons()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            source.clip = sounds[0];
            source.Play();
            StartCoroutine(handslam_attackone());
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            source.clip = sounds[1];
            source.Play();
            StartCoroutine(rockthrow_attacktwo());
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            source.clip = sounds[2];
            source.Play();
            StartCoroutine(fistpunch_attackthree());
        }
    }
    // Update is called once per frame
    void Update()
    {
        buttons();
        if (handmovingback)
        {
            if (Vector2.Distance(hand.transform.position, handstartpos.transform.position)< 1)
            {
                print("BACK TO START");
                hand.transform.position = handstartpos.position;
                hand.transform.rotation = handstartpos.rotation;
                handrigid.velocity = Vector2.zero;
                handmovingback = false;
                can_rot = true;
                attacktype = 0;
            }
            else
            {
                handrigid.velocity = Vector3.Normalize(new Vector2(hand.transform.position.x - handstartpos.position.x, hand.transform.position.y - handstartpos.position.y)) * -handsmovebackspeed;
            }
            //if (hand.transform.position == handstartpos.transform.position)
            //{
            //    print("BACK TO START");
            //    hand.transform.rotation = handstartpos.rotation;
            //    handrigid.velocity = Vector2.zero;
            //    handmovingback = false;
            //    attacktype = 0;
            //}
            //else
            //{
            //    handrigid.velocity = Vector3.Normalize(new Vector2(hand.transform.position.x - handstartpos.position.x, hand.transform.position.y - handstartpos.position.y)) * -handsmovebackspeed;
            //}
        }
        if(attacktype == 0&& handmovingback==false)
        {
            //StartCoroutine(betweenattacks());
        }
        if (can_rot)
        {
            this.transform.eulerAngles = new Vector3(0, 0, -90 + Mathf.Atan2((Player.transform.position.y - this.transform.position.y), (Player.transform.position.x - this.transform.position.x)) * Mathf.Rad2Deg);
        }
    }
}
