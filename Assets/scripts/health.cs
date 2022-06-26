using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class healthcount : MonoBehaviour {
    public TextMeshProUGUI healthcounter;
    public player Player;
	// Use this for initialization
	void Start () {
        Player = GameObject.FindObjectOfType<player>();
	}

    // Update is called once per frame
    void Update() {
        healthcounter.SetText("Health: " + Player.lastknownhealth);
    }
}
