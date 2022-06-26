using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class healthcounter : MonoBehaviour {
    public player Player;
    public TextMeshProUGUI health;
	// Use this for initialization
	void Start () {
        Player = GameObject.FindObjectOfType<player>();
        health.SetText("Health: " + Player.lastknownhealth.ToString());
    }

    // Update is called once per frame
    void Update () {
        if(Player != null)
        {
            health.SetText("Health: " + Player.lastknownhealth.ToString());
        }
    }
}
