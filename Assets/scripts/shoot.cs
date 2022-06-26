using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot : MonoBehaviour {
    public int ammo;
    Vector3 nintey;
    Vector3 rotation;
    public player Player;
    Collider2D slugcollider;
    private bool canreload;
    bool reloading;
    public GameObject Laser;
    public Rigidbody2D rigid;
    public Vector2 force;
    Rigidbody2D shotgun;
    Vector2 velocity;
    Vector3 random;
    public int maxammo;
    Rigidbody2D player;
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
    public bool limited = false;
    public AudioSource shotgun_sound;
    public AudioSource shotgun_reload_sound;
    public AudioSource shotgun_done_reload_sound;
    //SET TOTAL AMMO DEFAULT FOR LEVEL HERE
    public int total_ammo = 100;
    public GameObject shell_sprite;
    public Transform shell_sprite_spawnpoint;
    public Animator this_anim;
	// Use this for initialization
	void Start () {
        canshoot = true;
        Player = GetComponent<player>();
        player = gameObject.GetComponent<Rigidbody2D>();
        ammo = maxammo;
        canreload = true;
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
        player.velocity = left -(transform.right * backblastforce);
        shotgun_sound.Play();
        beam = Instantiate(Laser, this.transform.position + new Vector3(0, 0, -50) + transform.right*2f, Quaternion.identity) as GameObject;
        StartCoroutine("Destroy");
        yield return new WaitForSeconds(shootdelay);
        canshoot = true;
        canreload = true;
    }

    
    void Shoot()
    {
        Fire();     
    }
    private IEnumerator Destroy() 
    {
        shotgun = beam.GetComponent<Rigidbody2D>();

        rotation = new Vector3(Mathf.Acos(beam.transform.rotation.z), Mathf.Asin(beam.transform.rotation.z));
        beam.transform.rotation = transform.rotation * Quaternion.Euler(0, 0, 90);
     
        shotgun.velocity = (player.velocity);
        yield return new WaitForSeconds(.10f);
        Destroy(beam);
     
    }
    void Load()
    {

       

        
    }
    IEnumerator Reload()
    {
        reloading = true;
        canreload = false;
        while (ammo != (maxammo))
        {
            if (ammo < maxammo && (limited == false || total_ammo > 0))
            {
                canshoot = false;
                //this_anim.SetTrigger("reload");
                this_anim.Play("Reload", -1, 0);
                yield return new WaitForSeconds(.2f);
                Instantiate(shell_sprite, shell_sprite_spawnpoint.position, shell_sprite_spawnpoint.rotation);
                ammo = ammo + 1;
                total_ammo--;
                if (ammo == maxammo)
                {
                    canreload = true;
                    reloading = false;
                }
                shotgun_reload_sound.Play();
                canshoot = true;
            }
        }
       // yield return new WaitForSeconds(.1f);
        //shotgun_done_reload_sound.Play();
    }
    void buttons()
    {

        if (Input.GetKeyDown(KeyCode.R) || Input.GetButtonDown("Fire2"))
        {

            if (canreload == true && ammo < maxammo)
            {
                print("RELOAD");
                StopCoroutine("Reload");
                canreload = true;
                reloading = false;
                canshoot = true;
                StartCoroutine("Reload");
            }
        }
        //if (Input.GetKeyDown(KeyCode.R) || Input.GetButtonDown("Fire2"))
        //{

        //    if (canreload == true && ammo < maxammo)
        //    {
        //        StopCoroutine(Reload());
        //        StartCoroutine(Reload());
        //    }
        //}

    }
   
    // Update is called once per frame
    void Update () {
        Debug.DrawLine(this.transform.position, this.transform.position + transform.right * 100, Color.magenta);
        //if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Fire1"))
        //{
        //    if (ammo > 0 && canshoot == true)
        //    {
        //        StopCoroutine(Reload());
        //        StartCoroutine(Fire());
        //        ammo = ammo - 1;
        //    }
        //}
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Fire1"))
        {
            print("SPACE PRESSED");
            if (reloading == true)
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
        if (Input.GetKeyUp(KeyCode.Space) || Input.GetButtonUp("Fire1"))
        {
            CancelInvoke();
        }
       
    }
}
