using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour {
    public Camera maincam;
    public player Player;
   // public AudioListener listener;
    Vector3 forward;
	// Use this for initialization
	void Start () {
     //   listener = GetComponent<AudioListener>();
        Player = GameObject.FindObjectOfType<player>();
        maincam = Camera.main;
        forward = new Vector3(0, 0, -100);
	}
	
	// Update is called once per frame
	void Update () {
        if(Player != null)
        {
            maincam.transform.position = Player.transform.position + forward;
        }
    }
}
