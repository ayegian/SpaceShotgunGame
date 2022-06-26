using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Bosshealthcounter : MonoBehaviour {
    public bossstuff bosshealth;
    public TextMeshProUGUI healthcounter;
    // Use this for initialization
    void Start () {
        healthcounter = this.GetComponent<TextMeshProUGUI>();
        healthcounter.text = bosshealth.bossname + ":" + '\n' +
        bosshealth.bosshealth.ToString() + " Health";
    }
	// Update is called once per frame
	void Update () {
        if(bosshealth != null)
        {
            healthcounter.text = bosshealth.bossname + ":" + '\n' +
            bosshealth.bosshealth.ToString() + " Health";
        }
	}
}
