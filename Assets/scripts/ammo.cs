using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ammo : MonoBehaviour {
    public TextMeshProUGUI Ammocounter;
    public shoot Player;
    public bool limited;
    // Use this for initialization
    void Start () {
        Player = GameObject.FindObjectOfType<shoot>();
        limited = Player.limited;
        if (limited == false)
        {
            Ammocounter.SetText("Ammo: " + Player.ammo.ToString());
        }
        else
        {
            Ammocounter.SetText("Ammo: " + Player.ammo.ToString() + "/" + Player.total_ammo.ToString());
        }
    }

    // Update is called once per frame
    void Update () {
        if(Player != null)
        {

            if (limited == false)
            {
                Ammocounter.SetText("Ammo: " + Player.ammo.ToString());
            }            
            else
            {
                Ammocounter.SetText("Ammo: " + Player.ammo.ToString()+"/"+Player.total_ammo.ToString());
            }
        }
    }
}
