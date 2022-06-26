using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class destpointscript : MonoBehaviour
{
    public biologistnavmesh bioai;
    public NavMeshAgent bioagent;
    public player Player;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindObjectOfType<player>();
    }

    // Update is called once per frame
    void Update()
    {
        if(bioai.inrange == true)
        {
            //this.transform.Rotate(Player.transform.up, Time.deltaTime * ((2 * Mathf.PI * bioai.range) / bioagent.speed));
            transform.RotateAround(Player.transform.position, transform.forward, Time.deltaTime * (bioagent.speed/1)* ((2 * Mathf.PI * bioai.range) / bioagent.speed));
            if (Vector3.Distance(this.transform.position, Player.transform.position) < bioai.range)
            {
                Vector3 norm = Vector3.Normalize(this.transform.position - Player.transform.position);
                this.transform.position = Player.transform.position + bioai.range * norm;
            }
        }
        else
        {
            this.transform.position = bioagent.transform.position;
        }
    }
}
