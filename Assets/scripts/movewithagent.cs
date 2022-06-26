using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movewithagent : MonoBehaviour {
    public GameObject agent;
    Vector3 position;
	// Use this for initialization
	void Start() { 
	}
	
	// Update is called once per frame
	void Update () {
        position = new Vector3(agent.transform.position.x, agent.transform.position.y, this.transform.position.z);
        print(position);
        this.transform.position = position;
	}
}
