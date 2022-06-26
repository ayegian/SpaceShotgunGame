using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemytakedamage : MonoBehaviour {
    public bool shot;
    public enemyscript enemy;
    public enemyshoot shoot;
    public player Player;
    public Rigidbody2D rigid;
    public Vector2 right;
    public float touchknockback;
    public float slugknockback;
    // Use this for initialization
    void Start () {
        shoot = this.GetComponentInParent<enemyshoot>();
        Player = GameObject.FindObjectOfType<player>();
        touchknockback = 10;
        right = transform.right;
        rigid = Player.gameObject.GetComponent<Rigidbody2D>();
        enemy = this.GetComponentInParent<enemyscript>();
        }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("slug"))
        {
            enemy.enemyhealth -= 1;
            shoot.StopAllCoroutines();
            shoot.shooting = false;
        }
        if (collision.gameObject.CompareTag("player") && transform.parent.CompareTag("mutant"))
        {
            Player.health -= 5;
            StartCoroutine(movewait());
        }
    }
    public void TouchPlayer()
    {
        rigid.velocity = rigid.velocity - (right * -1 * touchknockback); 
    }
    public IEnumerator movewait()
    {
        enemy.move = false;
        yield return new WaitForSeconds(1f);
        enemy.move = true;
    }
    public void Getshot()
    {
        shot = false;
        enemy.enemyhealth--;
        rigid.velocity = rigid.velocity + (right * -1 * slugknockback);
        enemy.move = true;
    }
    // Update is called once per frame
    void Update () {
    }
}
