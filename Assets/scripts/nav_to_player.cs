using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class nav_to_player : MonoBehaviour
{
    public Transform player_pos;
    public NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        player_pos = FindObjectOfType<player>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(new Vector3(player_pos.position.x, player_pos.position.y, transform.position.z));
    }
}
