using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class geyserscript : MonoBehaviour
{

    public float amountofdelay;
    Collider2D collider;
    player Player;
    public bool collided = true;
    public int damage;
    public float beforestopdelaytimesmall;
    public float beforestopdelaytimelarge;
    public float afterstopdelaytime;
    public SpriteRenderer marking;
    public SpriteRenderer explosion;
    public Animator markinganim;
    public Animator explosionanim;
    public bool followplayer;
    // Use this for initialization
    //MAKE SOUND EFFECT JUST BEFORE COLLIDER IS ENABLED TO LET PLAYER KNOW
    private void Awake()
    {
        followplayer = true;
        marking.gameObject.SetActive(true);
        explosion.gameObject.SetActive(false);
        print("CREATED");
        Player = FindObjectOfType<player>();
    }
    void Start()
    {
        collided = true;
        amountofdelay = Random.Range(beforestopdelaytimesmall, beforestopdelaytimelarge);
        collider = this.GetComponent<Collider2D>();
        collider.enabled = false;
        StartCoroutine(Mortar());
    }
    IEnumerator Mortar()
    {
        print("MORTAR");
        yield return new WaitForSeconds(amountofdelay);
        followplayer = false;
        yield return new WaitForSeconds(afterstopdelaytime);
        collider.enabled = true;
        marking.gameObject.SetActive(false);
        explosion.gameObject.SetActive(true);
        yield return new WaitForSeconds(.5f);
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("player"))
        {
            print("COLLIDED");
            collided = true;
        }
        if (collision.gameObject.CompareTag("player")/* && Player.invincible == false*/)
        {
            Player.health -= damage;
            //Player.invincible = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (Player.invincible == false)
        //{
        Player.health -= damage;
        //}
    }
    // Update is called once per frame
    void Update()
    {
        if (followplayer)
        {
            this.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, this.transform.position.z);
        }
    }
}
