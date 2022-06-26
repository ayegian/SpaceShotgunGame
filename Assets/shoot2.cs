using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot2 : MonoBehaviour
{
    public SpriteRenderer playersprite;
    public int ammo;
    Vector3 nintey;
    Vector3 rotation;
    public player Player;
    Collider2D slugcollider;
    public AudioSource shotgun_fire;
    public AudioSource shotgun_reload_sound;
    private bool canreload;
    public GameObject Laser;
    public Rigidbody rigid;
    public Vector2 force;
    Rigidbody shotgun;
    Vector2 velocity;
    Vector3 random;
    public int maxammo;
    Rigidbody player;
    public int speed;
    public GameObject beam;
    private Vector3 left;
    private Vector3 right;
    public SpriteMask mask;
    Vector3 scale;
    Vector3 position;
    public float backblastforce;
    public float shootdelay = .1f;
    public bool canshoot = true;
    public bool reloading = false;
    public int reversenum = 1;
    public bool limited = false;
    //SET TOTAL AMMO DEFAULT HERE
    public int total_ammo = 100;
    // Use this for initialization
    void Start()
    {
        reversenum = 1;
        canshoot = true;
        Player = GetComponent<player>();
        player = gameObject.GetComponent<Rigidbody>();
        ammo = maxammo;
        canreload = false;
        right = new Vector3(1, 0);
        limited = FindObjectOfType<difficulty_script>().limited_ammo;
    }
    IEnumerator Fire()
    {
        canreload = false;
        canshoot = false;
        print("BEFORE FIRE");
        yield return new WaitForEndOfFrame();
        print("AFTER FIRE");
        left = player.velocity;
        player.velocity = left - (transform.right * backblastforce*reversenum);
        shotgun_fire.Play();
        beam = Instantiate(Laser, this.transform.position+ transform.right * 2, Quaternion.identity) as GameObject;
        StartCoroutine("Destroy");
        yield return new WaitForSeconds(shootdelay);
        canshoot = true;
        canreload = true;
    }
    public void startreverse(float f)
    {
        StartCoroutine(reverse(f));
    }
    public IEnumerator reverse(float revtime)
    {
        for (int i = 0; i < 10; i++)
        {
            if (playersprite.color == Color.white)
            {
                playersprite.color = Color.magenta;
            }
            else
            {
                playersprite.color = Color.white;
            }
            yield return new WaitForSeconds(.1f);
        }
        playersprite.color = Color.magenta;
        reversenum = -1;
        print("REVERSE START");
        //This line reverses current player velo
        //player.velocity = player.velocity * reversenum;
        yield return new WaitForSeconds(revtime);
        playersprite.color = Color.white;
        print("REVERSE END");
        reversenum = 1;
    }

    void Shoot()
    {
        Fire();
    }
    private IEnumerator Destroy()
    {
        shotgun = beam.GetComponent<Rigidbody>();

        rotation = new Vector3(Mathf.Acos(beam.transform.rotation.z), Mathf.Asin(beam.transform.rotation.z));
        beam.transform.rotation = Quaternion.Euler(0, this.transform.rotation.y-90,0) * transform.rotation;

        shotgun.velocity = (player.velocity);
        yield return new WaitForSeconds(.1f);
        Destroy(beam);

    }
    void Load()
    {




    }
    IEnumerator Reload()
    {
        reloading = true;
        canreload = false;
        while (ammo != maxammo)
        {
            if (ammo < maxammo&&(limited == false || total_ammo > 0))
            {
                canshoot = false;
                yield return new WaitForSeconds(.2f);
                shotgun_reload_sound.Play();
                ammo = ammo + 1;
                total_ammo--;
                if (ammo == maxammo)
                {
                    //AudioSource.PlayClipAtPoint(reload, transform.position);
                    canreload = true;
                    reloading = false;
                }
                canshoot = true;
            }
        }

    }
    void buttons()
    {

        if (Input.GetKeyDown(KeyCode.R) || Input.GetButtonDown("Fire2"))
        {

            if (canreload == true && ammo<maxammo)
            {
                print("RELOAD");
                StopCoroutine("Reload");
                canreload = true;
                reloading = false;
                canshoot = true;
                StartCoroutine("Reload");
            }
        }
        //if (Input.GetKeyDown(KeyCode.F))
        //{
        //    print("STOP COROUTINE RELOAD");
        //    StopCoroutine("Reload");
        //    canreload = true;
        //    reloading = false;
        //    canshoot = true;
        //}
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(this.transform.position, this.transform.position + transform.right * 100, Color.magenta);
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Fire1"))
        {
            print("SPACE PRESSED");
            if(reloading == true)
            {
                print("STOP RELOAD");
                StopCoroutine("Reload");
                canreload = true;
                canshoot = true;
                reloading = false;
                StartCoroutine(Fire());
                ammo = ammo - 1;
            }
            else if (ammo > 0 && canshoot == true)
            {
                print("CAN SHOOT");
                StopCoroutine("Reload");
                canreload = true;
                reloading = false;
                canshoot = true;
                StartCoroutine(Fire());
                ammo = ammo - 1;
            }
        }
        buttons();
        //if (Input.GetKeyUp(KeyCode.Space) || Input.GetButtonUp("Fire1"))
        //{
        //    CancelInvoke();
        //}

    }
}
