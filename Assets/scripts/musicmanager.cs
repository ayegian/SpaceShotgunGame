using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class musicmanager : MonoBehaviour {
    public currentlevel Currentlevel;
    public AudioSource music;
    // Use this for initialization
    void Start()
    {
     music = AudioSource.FindObjectOfType<AudioSource>();
     Currentlevel = FindObjectOfType<currentlevel>();
     music.loop = true;
    }	
	// Update is called once per frame
	void Update () {
		
	}
}
