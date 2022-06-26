using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class navmeshscript2 : MonoBehaviour
{
    public NavMeshAgent agent;
    public enemyai ai;
    Vector3 playerpos;
    Vector3 castpos;
    RaycastHit hit2;
    RaycastHit hit;
    bool switching = false;
    public player Player;
    int doitonce = 1;
    public enemyscript2 enemy;
    bool coroutineactive = false;
    public enemyshoot2 shoot;
    public float movemultiplier;
    Vector3 back;
    public bool nosightneeded = false;
    // Use this for initialization
    void Start()
    {
        if(this.transform.position.y != 0)
        {
            this.transform.position = new Vector3(this.transform.position.x, 0, this.transform.position.z);
        }
        back = this.transform.right;
        enemy = GetComponentInChildren<enemyscript2>();
        shoot = GetComponentInChildren<enemyshoot2>();
        Player = FindObjectOfType<player>();
        agent = this.GetComponent<NavMeshAgent>();
        if(ai == null)
        {
            if (enemy.gameObject.CompareTag("enemy") || enemy.gameObject.CompareTag("machinegunguy") || enemy.gameObject.CompareTag("charger") | enemy.gameObject.CompareTag("mutant"))
            {
                ai = GetComponentInChildren<simpleenemyai2>();
            }
            else
            {
                ai = GetComponentInChildren<specialenemyai>();
            }
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
    public void sightneededupdate()
    {
        if (shoot.canrotate&&ai.inrange == false && (shoot.timeuntil >= 0))
        {
            if (!shoot.gameObject.CompareTag("charger"))
            {
                shoot.StopAllCoroutines();
                if (shoot.timeuntil < (shoot.seconds / 2))
                {
                    shoot.timeuntil = (shoot.seconds / 2);
                }
                shoot.shooting = false;
            }
            if (switching == false && shoot.shooting == true && shoot.gameObject.CompareTag("charger"))
            {
                shoot.StopAllCoroutines();
                print("Changing Shooting");
                StartCoroutine(ChangeShooting());
            }
            print("ad");
            agent.isStopped = false;
            print("DESTINATION SET");
            if (shoot.range < 0)
            {
                agent.SetDestination(new Vector3(Player.transform.position.x, 0, Player.transform.position.z) + shoot.gameObject.transform.right);
            }
            else
            {
                agent.SetDestination(new Vector3(Player.transform.position.x, 0, Player.transform.position.z));
            }
        }
        if (ai.moving == false && ai.inrange == true)
        {
            StopCoroutine(Move());
            print("Stopped");
            agent.isStopped = true;
        }
        if (enemy.destroy == true)
        {
            Destroy(gameObject);
        }
        print("IMPORTANT:" + "Moving:" + ai.moving + "INRANGE:" + ai.inrange);
        playerpos = Player.transform.position;
        if (this.transform.position.y != 0)
        {
            this.transform.position = new Vector3(this.transform.position.x, 0, this.transform.position.z);
        }
    }
    public void nosightneededupdate()
    {
        playerpos = Player.transform.position;
        ai.inrange = Vector3.Distance(playerpos, this.transform.position) <= shoot.range;
        if (ai.inrange == false && (shoot.timeuntil >= 0)) {

        }
    }
    // Update is called once per frame
    void Update()
    {
        if(nosightneeded == false)
        {
            sightneededupdate();
        }
        else
        {
            nosightneededupdate();
        }
    }
}
