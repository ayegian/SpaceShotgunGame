using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyscript : MonoBehaviour
{
    Rigidbody2D adam;
    public player Player;
    public bool destroy;
    public bool alreadyalert;
    public bool move;
    public float enemyhealth;
    public enemyshoot enemy;
    // Use this for initialization
    void Awake()
    {
        if (enemyhealth == 0)
        {
            enemyhealth = 1;
        }
        if (!this.CompareTag("mutant"))
        {
            enemy = this.gameObject.GetComponent<enemyshoot>();
        }
        move = false;
        Player = GameObject.FindObjectOfType<player>();
        adam = gameObject.GetComponent<Rigidbody2D>();
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
            Destroy(this.transform.parent.gameObject);
        }
        if (enemy.canrotate == true)
        {
            adam.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2((Player.transform.position.y - this.transform.position.y), (Player.transform.position.x - this.transform.position.x)) * Mathf.Rad2Deg);
        }

    }
}