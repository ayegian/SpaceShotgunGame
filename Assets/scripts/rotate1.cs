using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate1 : MonoBehaviour {
    private Rigidbody2D adam;
    private Vector3 diff;
    private player Player;
	// Use this for initialization
	void Awake () {
        Player = GetComponent<player>();
        adam = GetComponent<Rigidbody2D>();
	}

    // Update is called once per frame
    void Update()
    {
        diff = Camera.main.ScreenToWorldPoint(Input.mousePosition)- transform.position;
  
        diff.Normalize();
        
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z);

    }
    }
