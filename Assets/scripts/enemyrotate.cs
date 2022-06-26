using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyrotate : MonoBehaviour {
    
    Rigidbody2D enemyrb;
    public player Player;
    Rigidbody2D Playerrb;
    Vector3 Opposite;  
    bool shooting;
    float angle;
    Vector3 firingangle;
    Vector3 firingvelocity;
    enemyshoot enemy;
    // Use this for initialization
    void Start () {
        enemy = this.gameObject.GetComponent<enemyshoot>();
        Player = GameObject.FindObjectOfType<player>();
        enemyrb = gameObject.GetComponent<Rigidbody2D>();
        if (!this.CompareTag("mutant"))
        {
            enemy = this.gameObject.GetComponent<enemyshoot>();
        }
    }
	
	// Update is called once per frame
	void Update () {
        if(enemy.canrotate == true || this.CompareTag("mutant"))
        {
            angle = (Mathf.Atan2(Player.transform.position.y - this.transform.position.y, Player.transform.position.x - this.transform.position.x) * Mathf.Rad2Deg);
            firingangle = new Vector3(0, 0, angle);
            enemyrb.transform.eulerAngles = firingangle;
        }
    }
}
