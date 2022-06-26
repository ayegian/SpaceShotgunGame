using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class commonenemyscript : MonoBehaviour {
    public bool alreadyalert;
    public int enemyhealth;
	// Use this for initialization
	void Start () {
        alreadyalert = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (enemyhealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
