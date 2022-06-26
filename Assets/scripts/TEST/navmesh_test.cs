using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI;
public class navmesh_test : MonoBehaviour
{
    public Transform destination;
    public NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent.destination = destination.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
