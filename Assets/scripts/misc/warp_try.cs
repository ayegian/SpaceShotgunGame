using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class warp_try : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform spawnpoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            agent.Warp(spawnpoint.position);
        }
    }
}
