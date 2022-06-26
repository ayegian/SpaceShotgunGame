using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mortarscript2 : MonoBehaviour
{

    public float amountofdelay;
    public Collider collider;
    player Player;
    public bool collided = true;
    public int damage;
    public int delaytimesmall;
    public int delaytimelarge;
    public SpriteRenderer marking;
    public SpriteRenderer explosion;
    public Animator markinganim;
    public Animator explosionanim;
    // Use this for initialization
    private void Awake()
    {
        marking.gameObject.SetActive(true);
        explosion.gameObject.SetActive(false);
        print("CREATED");
        this.gameObject.tag = "mortar";
    }
    void Start()
    {
        collided = true;
        amountofdelay = Random.Range(delaytimesmall, delaytimelarge);
        Player = FindObjectOfType<player>();
        //collider = this.GetComponent<Collider>();
        collider.enabled = false;
        StartCoroutine(Mortar());
    }
    IEnumerator Mortar()
    {
        print("MORTAR");
        yield return new WaitForSeconds(amountofdelay);
        collider.enabled = true;
        marking.gameObject.SetActive(false);
        explosion.gameObject.SetActive(true);
        yield return new WaitForSeconds(.5f);
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (!collision.gameObject.CompareTag("player"))
        {
            print("COLLIDED");
            collided = true;
        }
        if (collision.gameObject.CompareTag("player") /*&& Player.invincible == false*/)
        {
            Player.health -= damage;
            //Player.invincible = true;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            Player.health -= damage;
        }
        //if(Player.invincible == false)
        //{
        //}
    }
    //private void OnTriggerEnter(Collider collision)
    //{
    //    if (!collision.gameObject.CompareTag("player"))
    //    {
    //        print("COLLIDED");
    //        collided = true;
    //    }
    //    if (collision.gameObject.CompareTag("player") /*&& Player.invincible == false*/)
    //    {
    //        Player.health -= damage;
    //        //Player.invincible = true;
    //    }
    //}
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("player"))
    //    {
    //        Player.health -= damage;
    //    }
    //    //if(Player.invincible == false)
    //    //{
    //    //}
    //}
    // Update is called once per frame
    void Update()
    {

    }
}
