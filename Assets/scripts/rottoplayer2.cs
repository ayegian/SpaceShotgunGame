using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rottoplayer2 : MonoBehaviour
{
    public player Player;
    public bool canrot;
    public Rigidbody thisrigid;
    public bool movetowardsplayer;
    public float speed;
    public float adjust;

    // Start is called before the first frame update
    void Start()
    {
        Player = FindObjectOfType<player>();
        canrot = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canrot)
        {
            //this.transform.LookAt(Player.transform.position);
            this.transform.eulerAngles = new Vector3(90,adjust + Mathf.Atan2((Player.transform.position.x - this.transform.position.x), (Player.transform.position.z - this.transform.position.z)) * Mathf.Rad2Deg,0);
        }
        if (movetowardsplayer)
        {
            thisrigid.velocity = speed * this.transform.up;
        }
    }
}
