using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class navmesh_agent_towards_player : MonoBehaviour
{
    public NavMeshAgent agent;
    public player Player;
    // Start is called before the first frame update
    void Start()
    {
        Player = FindObjectOfType<player>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = new Vector3(Player.transform.position.x - this.transform.position.x, Player.transform.position.y, 0);
        Vector3 destpos = new Vector3(Player.transform.position.x, Player.transform.position.y, this.transform.position.z) + dir.normalized;
        agent.SetDestination(destpos);
        print("Supposed to be dest: "+destpos+" AGENT DESTINATION: " + agent.destination);
    }
}
