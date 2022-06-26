using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pushoffsurface : MonoBehaviour {
    bool onwall;
    Vector2 zero;
    Rigidbody2D rigid;
    Vector2 right;
	// Use this for initialization
	void Start () {
        rigid = GetComponentInParent<Rigidbody2D>();
        zero = new Vector2(0, 0);
	}
    private void OnTriggerExit2D(Collider2D trigger)
    {
        if (trigger.gameObject.CompareTag("wall"))
        {
            onwall = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.CompareTag("wall"))
        {
            onwall = true;
        }
    }

    // Update is called once per frame
    void Update () {
        //right = transform.right;
        //if (Input.GetKey(KeyCode.E) && onwall == true)
        //{
        //    rigid.velocity = zero;
        //}
        //if(Input.GetKeyDown(KeyCode.F) && onwall == true)
        //{
        //    rigid.velocity += (right * 8);
        //}
    }
}
