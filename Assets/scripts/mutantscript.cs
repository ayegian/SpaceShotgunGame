using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mutantscript : MonoBehaviour {
    Rigidbody2D adam;
    public player Player;
    Vector3 force;
    public bool move;
    public enemyscript enemy;
    float maxhealth;
    // Use this for initialization
    void Start() {
        move = false;
        adam = gameObject.GetComponent<Rigidbody2D>();
        maxhealth = enemy.enemyhealth;
        Player = GameObject.FindObjectOfType<player>();
        enemy = GameObject.FindObjectOfType<enemyscript>();
    }
	
	// Update is called once per frame
	void Update () {
        if(enemy.move == true)
        {
        }
        float Movex = Player.transform.position.x;
        float Movey = Player.transform.position.y;
       Vector3 Move = this.transform.position;
        Vector3 Move2 = new Vector3(this.transform.position.x,this.transform.position.y, this.transform.position.z);
        if (enemy.move == true)
        {
            adam.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2((Player.transform.position.y - this.transform.position.y), (Player.transform.position.x - this.transform.position.x)) * Mathf.Rad2Deg);
            this.transform.position = Move - (transform.right * -.1f);
        }
        if(maxhealth != enemy.enemyhealth)
        {
            enemy.alreadyalert = true;
            enemy.move = true;
        }
	}
    public void Aware()
    {
        enemy.move = true;

    }
    public IEnumerator Moving()
    {
        enemy.move = false;
        yield return new WaitForSeconds(1f);
        enemy.move = true;
    }
    
        private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("player")/* && Player.invincible == false*/)
        {
            Player.health = Player.health - 1;
            //Player.invincible = true;
            StartCoroutine(Moving());
        }
    }
    
}
