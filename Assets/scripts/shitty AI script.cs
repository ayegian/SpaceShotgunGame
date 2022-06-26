using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shittyAIscript : MonoBehaviour {
    int AI;
	// Use this for initialization
	void Start () {
        AI = Random.Range(1, 5);
	}
	IEnumerator Number1()
    {
        yield return new WaitForSeconds(5f);
    }
	// Update is called once per frame
	void Update () {
		
	}
}
