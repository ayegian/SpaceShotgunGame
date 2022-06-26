using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyrotate2 : MonoBehaviour
{
    public Rigidbody enemyrb;
    public player Player;
    Rigidbody Playerrb;
    Vector3 Opposite;
    bool shooting;
    float angle;
    Vector3 firingangle;
    Vector3 firingvelocity;
    enemyshoot2 enemy;
    public float adjust = 0;
    // Use this for initialization
    void Start()
    {
        enemy = this.gameObject.GetComponent<enemyshoot2>();
        Player = GameObject.FindObjectOfType<player>();
        enemyrb = gameObject.GetComponent<Rigidbody>();
        if (!this.CompareTag("mutant"))
        {
            enemy = this.gameObject.GetComponent<enemyshoot2>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy.canrotate == true || this.CompareTag("mutant"))
        {
            angle = (Mathf.Atan2((Player.transform.position.x - this.transform.position.x), (Player.transform.position.z - this.transform.position.z)) * Mathf.Rad2Deg);
            //angle = (Mathf.Atan2(Player.transform.position.y - this.transform.position.y, Player.transform.position.x - this.transform.position.x) * Mathf.Rad2Deg);
            firingangle = new Vector3(90, angle + adjust, 0);
            this.transform.eulerAngles = firingangle;
            print("NAME: " + gameObject.transform.root.name + " FIRING ANGLE: " + firingangle + " THIS ANGLE: " + this.transform.eulerAngles);
        }
    }
}
