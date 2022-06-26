using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserguyscript : MonoBehaviour {

    public GameObject laser;
    public player Player;
    Rigidbody2D Playerrb;
    Vector3 Opposite;
    public GameObject beam;
    bool shooting;
    float angle;
    public simpleenemyai AI;
    Vector3 firingangle;
    public float damage;
    public enemyscript enemy;
    public float seconds;
    // Use this for initialization
    void Start()
    {
        enemy = this.GetComponent<enemyscript>();
        Player = GameObject.FindObjectOfType<player>();
        Opposite = new Vector3(-1, -1, 0);
        Playerrb = Player.GetComponent<Rigidbody2D>();
        shooting = false;
    }
    IEnumerator Wait()
    {
        shooting = true;
        yield return new WaitForSeconds(seconds);
        Fire();
        shooting = false;
    }
    void Fire()
    {
        print("INSTANTIATING");
        beam = Instantiate(laser, this.transform.position + (1.5f * transform.right), Quaternion.identity) as GameObject;
    }
    // Update is called once per frame
    void Update()
    {
        if (shooting == false && enemy.alreadyalert == true)
        {
            StartCoroutine(Wait());
        }
    }
}