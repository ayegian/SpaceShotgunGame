using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class boss2navmesh : MonoBehaviour
{
    public NavMeshAgent agent;
    public boss2ai bossai;
    public bool runningaway;
    public float runmagnitude;
    public float runtime;
    // Start is called before the first frame update
    void Start()
    {
        //agent.SetDestination(bossai.playerpos.position);
    }
    public void startrunaway()
    {
        StartCoroutine(runaway());
    }
    public IEnumerator runaway()
    {
        agent.isStopped = false;
        runningaway = true;
        agent.SetDestination(this.transform.position - this.transform.right * runmagnitude);
        yield return new WaitForSeconds(runtime);
        runningaway = false;
    }
    // Update is called once per frame
    void Update()
    {
        if(runningaway == false)
        {
            if (bossai.inrange == false)
            {
                print("AGENT DESTINATION: " + bossai.playerpos.position);
                agent.SetDestination(new Vector3(bossai.playerpos.position.x, bossai.playerpos.position.y, this.transform.position.z));
                agent.isStopped = false;
            }
            else
            {
                agent.isStopped = true;
            }
        }
    }
}
