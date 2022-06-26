using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randompiecesofscript : MonoBehaviour {
    Vector2 velocity;
	// Use this for initialization
	void Start () {
        velocity = new Vector2(((((Input.mousePosition.x / Screen.width) * 25) - 12.5f) - this.transform.position.x), ((((Input.mousePosition.y / Screen.height) * 11) - 5.5f) - this.transform.position.y));
        print(velocity);
        Mathf.Cos((((Input.mousePosition.y / Screen.height) * 11) - 5.5f) - this.transform.position.y);
        Mathf.Sin((((Input.mousePosition.x / Screen.width) * 25) - 12.5f) - this.transform.position.x);
        this.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2((((Screen.height) * 100000) - 50000), (((Screen.width) * 100000) - 50000)) * Mathf.Rad2Deg);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
