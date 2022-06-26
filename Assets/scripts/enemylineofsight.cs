using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemylineofsight : MonoBehaviour {
    public player Player;
    public enemyscript enemy;
    // Use this for initialization
    void Start()
    {
        this.transform.position = enemy.transform.position;
        this.transform.eulerAngles = enemy.transform.eulerAngles;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("player") && Physics2D.Raycast(enemy.transform.position, this.transform.eulerAngles, 150f))
        {

            enemy.alreadyalert = true;
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}

