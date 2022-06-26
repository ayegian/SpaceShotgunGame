using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyscript2 : MonoBehaviour
{
    Rigidbody adam;
    public player Player;
    public bool destroy;
    public bool alreadyalert;
    public bool move;
    public float enemyhealth;
    public enemyshoot2 enemy;
    public float adjust = 0;
    // Use this for initialization
    void Awake()
    {
        if (enemyhealth == 0)
        {
            enemyhealth = 1;
        }
        if (!this.CompareTag("mutant"))
        {
            enemy = this.gameObject.GetComponent<enemyshoot2>();
        }
        move = false;
        Player = GameObject.FindObjectOfType<player>();
        adam = gameObject.GetComponent<Rigidbody>();
    }
    public void Aware()
    {
        move = true;
        alreadyalert = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (enemyhealth <= 0)
        {
            destroy = true;
            Destroy(transform.root.gameObject);
        }
        //if (enemy.canrotate == true)
        //{
        //    adam.transform.eulerAngles = new Vector3(90, adjust + (Mathf.Atan2((Player.transform.position.x - this.transform.position.x), (Player.transform.position.z - this.transform.position.z)) * Mathf.Rad2Deg), 0);
        //}

    }
}
