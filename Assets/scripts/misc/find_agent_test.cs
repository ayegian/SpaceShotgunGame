using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class find_agent_test : MonoBehaviour
{
    public NavMeshAgent[] agents;
    public NavMeshSurface[] surfaces;
    // Start is called before the first frame update
    void OnEnable()
    {
        print("FIND AGENTS");
        agents = gameObject.GetComponentsInChildren<NavMeshAgent>();
        surfaces = gameObject.GetComponentsInChildren<NavMeshSurface>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
