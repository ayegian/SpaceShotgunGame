using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class navmeshscript : MonoBehaviour {
    public NavMeshAgent agent;
    public enemyai ai;
    Vector3 playerpos;
    Vector3 castpos;
    RaycastHit2D hit2;
    RaycastHit2D hit;
    bool switching = false;
    public player Player;
    int doitonce = 1;
    public enemyscript enemy;
    bool coroutineactive = false;
    public enemyshoot shoot;
    public float movemultiplier;
    Vector3 back;
	// Use this for initialization
	void Start () {
        back = this.transform.right;
        enemy = GetComponentInChildren<enemyscript>();
        shoot = GetComponentInChildren<enemyshoot>();
        Player = FindObjectOfType<player>();
        agent = this.GetComponent<NavMeshAgent>();
        if(enemy.gameObject.CompareTag("enemy")|| enemy.gameObject.CompareTag("machinegunguy")|| enemy.gameObject.CompareTag("charger"))
        {
            ai = GetComponentInChildren<simpleenemyai>();
        }
        else
        {
            ai = GetComponentInChildren<specialenemyai>();
        }
    }
    IEnumerator Move()
    {
        movemultiplier = Random.Range(1f, 2f);
        coroutineactive = true;
        ai.moving = true;
        yield return new WaitForSeconds(shoot.seconds - .5f);
        ai.moving = false;
        yield return new WaitForSeconds(.5f);
        coroutineactive = false;
    }   
    IEnumerator ChangeShooting()
    {
        print("STARTING");
        switching = true;
        print(shoot.seconds);
        yield return new WaitForSeconds(shoot.seconds);
        shoot.shooting = false;
        switching = false;
        print("FININSHED.");
    }
    // Update is called once per frame
    void Update () {        
        if (ai.inrange == false && (shoot.timeuntil>=1)) {
            if (!shoot.gameObject.CompareTag("charger"))
            {
                shoot.StopAllCoroutines();
                if (shoot.timeuntil < (shoot.seconds / 2))
                {
                    shoot.timeuntil = (shoot.seconds / 2);
                }
                shoot.shooting = false;
            }
            if (switching == false && shoot.shooting == true&& shoot.gameObject.CompareTag("charger"))
            {
                shoot.StopAllCoroutines();
                print("Changing Shooting");
                StartCoroutine(ChangeShooting());
            }
            print("ad");
            agent.isStopped = false;
            agent.SetDestination(new Vector3(Player.transform.position.x, Player.transform.position.y, this.transform.position.z));
        }
        if(ai.moving == false && ai.inrange == true)
        {
            StopCoroutine(Move());
            print("Stopped");
            agent.isStopped = true;
        }
        if(enemy.destroy == true)
        {
            Destroy(gameObject);
        }
        print("IMPORTANT:" + "Moving:" + ai.moving + "INRANGE:" + ai.inrange);
        playerpos = Player.transform.position;
    }
}