using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class alertbox : MonoBehaviour {
    public enemyscript enemy;
    private BoxCollider2D box;
    private int first;
	// Use this for initialization
	void Start () {
        first = 0;
        enemy = this.GetComponentInParent<enemyscript>();
        box = this.GetComponent<BoxCollider2D>();
        box.enabled = false;
    }
    IEnumerator enablebox()
    {
        box.enabled = true;
        yield return new WaitForSeconds(1);
        box.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
		if(enemy.alreadyalert == true && first == 0)
        {
            first = 1;
            StartCoroutine(enablebox());
        }
	}
}
