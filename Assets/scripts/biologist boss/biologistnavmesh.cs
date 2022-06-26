using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class biologistnavmesh : MonoBehaviour
{
    public NavMeshAgent agent;
    //public enemyai ai;
    public float range;
    public bool moving;
    public bool canmove;
    public bool inrange;
    Vector3 playerpos;
    Vector3 castpos;
    RaycastHit2D hit2;
    RaycastHit2D hit;
    bool switching = false;
    public player Player;
    //int doitonce = 1;
    //public enemyscript enemy;
    //bool coroutineactive = false;
    //public enemyshoot shoot;
    public float movemultiplier;
    public float circleradius;
    public bool canrot;
    Vector3 back;
    public Transform rotpoint;
    public Vector3 lastpos;
    public Transform destpoint;
    // Use this for initialization
    void Start()
    {
        Player = FindObjectOfType<player>();
    }
    private void Update()
    {
        //Player position + rotpoint.transform.up
        if (canrot)
        {
            this.transform.eulerAngles = new Vector3(Mathf.Atan2((Player.transform.position.y - this.transform.position.y), (Player.transform.position.x - this.transform.position.x)) * Mathf.Rad2Deg,0,0);
        }
        if (canmove)
        {
            if(inrange == false)
            {
                inrange = Vector3.Distance(Player.transform.position, this.transform.position) <= range;
            }
            else
            {
                inrange = Vector3.Distance(Player.transform.position, this.transform.position) <= (range+2);
                if(inrange == false)
                {
                    print("RANGE: " + Vector3.Distance(Player.transform.position, this.transform.position));
                }
            }
            //print("DIST: " + Vector2.Distance(Player.transform.position, this.transform.position));
            //agent.SetDestination(new Vector3(Player.transform.position.x, Player.transform.position.y, this.transform.position.z) /*rotpoint.right * circledist +*/ /*rotpoint.up * circledist*/);
            //agent.SetDestination(Player.transform.position /*rotpoint.transform.right * 1*/+ rotpoint.transform.up * 12+ rotpoint.transform.right * 12);
            print("DIST TRAVELLED: " + Vector3.Distance(this.transform.position, lastpos));
            //agent.SetDestination(rotpoint.transform.position /*rotpoint.transform.right * 1*/+ rotpoint.transform.right + rotpoint.transform.up * circleradius * Time.deltaTime);
            if (inrange)
            {
                Vector3 normvec = Vector3.Normalize((this.transform.position + rotpoint.transform.right * Time.deltaTime * agent.speed) - Player.transform.position);
                //agent.SetDestination(Player.transform.position + normvec * range);
                print("NORM VEC: " + normvec +" NORM VEC MAG: "+normvec.magnitude+ " DIST TO PLAYER: "+Vector3.Distance(this.transform.position, Player.transform.position));
                Debug.DrawLine(this.transform.position, Player.transform.position + normvec * range, Color.cyan);
                Debug.DrawLine(this.transform.position, this.transform.position + rotpoint.transform.right * Time.deltaTime * agent.speed, Color.green);
                agent.SetDestination(this.transform.position + rotpoint.transform.right * Time.deltaTime * agent.speed);
            }
            else
            {
                agent.SetDestination(Player.transform.position);
            }
            //Debug.DrawLine(rotpoint.position, rotpoint.transform.position+ rotpoint.transform.right * 15, Color.yellow);
            //if(rotpoint.transform.eulerAngles.y % 90 <= .3f)
            //{
            //    print("ANGLE: " + rotpoint.transform.eulerAngles.y + " DIST FROM PLAYER: " + Vector3.Distance(rotpoint.transform.position, Player.transform.position)+" CIRCLE RADIUS");
            //    //AT RAD: 15, DIST IS: 20, 70, 15.5, WITH JUST TRANSFORM.UP, DIST IS 11.3
            //}
            //lastpos = this.transform.position;
            //if (inrange)
            //{
            //    agent.SetDestination(destpoint.transform.position);
            //}
            //else
            //{
            //    agent.SetDestination(Player.transform.position);
            //}
            //if (inrange == false)
            //{
            //    agent.SetDestination(Player.transform.position + rotpoint.transform.right * 10);
            //    //agent.SetDestination(new Vector3(Player.transform.position.x, Player.transform.position.y, this.transform.position.z) + this.transform.forward * circledist);
            //    //agent.SetDestination(new Vector3(Player.transform.position.x, Player.transform.position.y, this.transform.position.z)/*rotpoint.right * circledist +*/ /*rotpoint.up * circledist*/);
            //    Debug.DrawLine(rotpoint.position, new Vector3(Player.transform.position.x, Player.transform.position.y, rotpoint.position.z) + rotpoint.right * circledist, Color.magenta);
            //    Debug.DrawLine(rotpoint.position, new Vector3(Player.transform.position.x, Player.transform.position.y, rotpoint.position.z) - rotpoint.forward * circledist, Color.blue);
            //    Debug.DrawLine(rotpoint.position, new Vector3(Player.transform.position.x, Player.transform.position.y, rotpoint.position.z) + rotpoint.up * circledist + rotpoint.right * circledist, Color.red);
            //    Debug.DrawLine(rotpoint.position, new Vector3(Player.transform.position.x, Player.transform.position.y, rotpoint.position.z) - rotpoint.up * circledist, Color.green);
            //    Debug.DrawLine(rotpoint.position, new Vector3(Player.transform.position.x, Player.transform.position.y, rotpoint.position.z) + rotpoint.up * 10, Color.yellow);
            //}
            //else
            //{
            //    Vector2 vecttoplayer = this.transform.position - Player.transform.position;
            //    vecttoplayer.Normalize();
            //    //GET ANGLE THEN MAKE DESTINATION CURRETN POS + DIRECTION * 1000 because it will update every frame
            //    float angledif= 180 - (90 + (rotpoint.transform.eulerAngles.z % 90));
            //    angledif *= Mathf.Deg2Rad;
            //    print("ANGLE DIF: " + angledif);
            //    print("VECT TO PLAYER: " + vecttoplayer);
            //    agent.SetDestination(Player.transform.position + this.transform.right* 10);
            //}
            //}
        }
        else
        {
            agent.isStopped = true;
        }
    }
}
//back = this.transform.right;

//enemy = GetComponentInChildren<enemyscript>();

//shoot = GetComponentInChildren<enemyshoot>();

//Player = FindObjectOfType<player>();

//agent = this.GetComponent<NavMeshAgent>();
//        if (enemy.gameObject.CompareTag("enemy") || enemy.gameObject.CompareTag("machinegunguy") || enemy.gameObject.CompareTag("charger"))
//        {
//    ai = GetComponentInChildren<simpleenemyai>();
//}
//        else
//        {
//    ai = GetComponentInChildren<specialenemyai>();
//}
//}
//IEnumerator Move()
//{
//    movemultiplier = Random.Range(1f, 2f);
//    coroutineactive = true;
//    ai.moving = true;
//    yield return new WaitForSeconds(shoot.seconds - .5f);
//    ai.moving = false;
//    yield return new WaitForSeconds(.5f);
//    coroutineactive = false;
//}
////IEnumerator ChangeShooting()
////{
////    print("STARTING");
////    switching = true;
////    print(shoot.seconds);
////    yield return new WaitForSeconds(shoot.seconds);
////    shoot.shooting = false;
////    switching = false;
////    print("FININSHED.");
////}
//// Update is called once per frame
//void Update()
//{
//    if (inrange == false)
//    {
//        //if (!shoot.gameObject.CompareTag("charger"))
//        //{
//        //    shoot.StopAllCoroutines();
//        //    if (shoot.timeuntil < (shoot.seconds / 2))
//        //    {
//        //        shoot.timeuntil = (shoot.seconds / 2);
//        //    }
//        //    shoot.shooting = false;
//        //}
//        //if (switching == false && shoot.shooting == true && shoot.gameObject.CompareTag("charger"))
//        //{
//        //    shoot.StopAllCoroutines();
//        //    print("Changing Shooting");
//        //    StartCoroutine(ChangeShooting());
//        //}
//        print("ad");
//        agent.isStopped = false;
//        agent.SetDestination(new Vector3(Player.transform.position.x, Player.transform.position.y, this.transform.position.z));
//    }
//    if (moving == false && inrange == true)
//    {
//        StopCoroutine(Move());
//        print("Stopped");
//        agent.isStopped = true;
//    }
//    if (this.transform.childCount == 0)
//    {
//        Destroy(gameObject);
//    }
//    print("IMPORTANT:" + "Moving:" + moving + "INRANGE:" + inrange);
//    playerpos = Player.transform.position;
//}
