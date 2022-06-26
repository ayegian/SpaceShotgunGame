using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour {
    Vector3 apple;
    float horizontal;
    float vertical;
    bool shrimp;
    public player Player;
    public Rigidbody2D rigid;
    
	// Use this for initialization
	void Start () {
        Player = GetComponent<player>();
        rigid = GetComponent<Rigidbody2D>();
	}
    private void OnCollisionEnter2D(Collision2D collision)
    {
        shrimp = true;
    }
    // Update is called once per frame
    void Update () {
        
        
        float moveright = transform.position.x + .1f;
        float moveleft = transform.position.x - .1f;
        float moveup = transform.position.y + .1f;
        float movedown = transform.position.y - .1f;
        Vector3 left = new Vector3(moveleft, this.transform.position.y, this.transform.position.z);
        Vector3 right = new Vector3(moveright, this.transform.position.y, this.transform.position.z);
        Vector3 up = new Vector3(this.transform.position.x, moveup, this.transform.position.z);
        Vector3 down = new Vector3(this.transform.position.x, movedown, this.transform.position.z);
        if (Input.GetKey(KeyCode.D))
        {
            this.transform.position = (right);
        }
        if (Input.GetKey(KeyCode.A))
        {
            this.transform.position = (left);
        }
        if ((Input.GetKey(KeyCode.W)))
        {
            this.transform.position = (up);
        }
        if ((Input.GetKey(KeyCode.S)))
        {
            this.transform.position = (down);
        }


    }
}
