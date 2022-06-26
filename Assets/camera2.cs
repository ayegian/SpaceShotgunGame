using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera2 : MonoBehaviour
{
    public Camera maincam;
    public player Player;
    Vector3 forward;
    //public AudioListener listener;
    // Use this for initialization
    void Start()
    {
        //listener = GetComponent<AudioListener>();
        Player = GameObject.FindObjectOfType<player>();
        maincam = Camera.main;
        forward = new Vector3(0, 100, 0);
    }

    // Update is called once per frame
    void Update()
    {
        maincam.transform.position = Player.transform.position + forward;
    }
}
