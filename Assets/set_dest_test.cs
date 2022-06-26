using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class set_dest_test : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform dest;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            print("DESTINATION SET");
            agent.SetDestination(new Vector3(dest.position.x, dest.position.y, transform.position.z));
        }
    }
}
