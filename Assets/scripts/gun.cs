using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun : MonoBehaviour {
    LineRenderer gunline;
    Vector3 position;
    public int range;
    Vector3 direction;
    Ray shootray;
    public player Player;
	// Use this for initialization
	void Start () {
        gunline = GetComponent<LineRenderer>();
        gunline.SetVertexCount(2);
	}
	IEnumerator Wait()
    {
        yield return new WaitForSeconds(.5f);
        gunline.enabled = false;

    }
	// Update is called once per frame
	void Update () {
        position = new Vector3(0, 0, 0);
        direction = transform.up;
        this.transform.position = Player.transform.position; 
        if (Input.GetKeyDown(KeyCode.Space))
        {

            gunline.enabled = false;
            StopAllCoroutines();
            gunline.SetPosition(0, transform.position);
            shootray.origin = transform.position;
            shootray.direction = transform.up;
            gunline.enabled = true;
            gunline.SetPosition(1, shootray.origin + shootray.direction);
            StartCoroutine("Wait");
        }
		
	}
}
