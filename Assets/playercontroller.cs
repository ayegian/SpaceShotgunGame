using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class playercontroller : MonoBehaviour {
    public NavMeshAgent agent;
    public Camera camera;
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(ray, out hit);
            print(Input.mousePosition);
            print(hit);
            Vector3 pos = Input.mousePosition;
            agent.SetDestination(new Vector3(40,40));
            print(hit.point);
            print("happy");
        }
	}
}
