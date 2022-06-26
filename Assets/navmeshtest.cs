using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class navmeshtest : MonoBehaviour
{
    // Start is called before the first frame update
    public NavMeshAgent agent;
    public GameObject destination;
    void Start()
    {
        agent.SetDestination(destination.transform.position);
        agent.isStopped = false;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
