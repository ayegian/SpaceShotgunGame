using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rottoplayer : MonoBehaviour
{
    public player Player;
    public bool canrot;
    public Rigidbody2D thisrigid;
    public bool movetowardsplayer;
    public float speed;
    public float adjust;
    // Start is called before the first frame update
    void Start()
    {
        Player = FindObjectOfType<player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canrot)
        {
            this.transform.eulerAngles = new Vector3(0,0,Mathf.Atan2((Player.transform.position.y - this.transform.position.y), (Player.transform.position.x - this.transform.position.x)) * Mathf.Rad2Deg);
        }
        if(movetowardsplayer == true)
        {
            thisrigid.velocity = thisrigid.transform.right * speed;
        }
    }
}
