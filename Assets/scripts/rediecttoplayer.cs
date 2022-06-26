using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rediecttoplayer : MonoBehaviour
{
    public player Player;
    public Rigidbody objrigid;
    public float basicwaittime;
    public bool randtime;
    public float randwaittimeup;
    public float randwaittimelow;
    public float randangleredirectrange;
    // Start is called before the first frame update
    void Start()
    {
        Player = FindObjectOfType<player>();
        StartCoroutine(waitredirect());
    }
    public IEnumerator waitredirect()
    {
        if (randtime)
        {
            yield return new WaitForSeconds(Random.Range(randwaittimelow, randwaittimeup));
        }
        else
        {
            yield return new WaitForSeconds(basicwaittime);
        }
        redirect();
    }
    public void redirect()
    {
        float mag = objrigid.velocity.magnitude;
        objrigid.transform.eulerAngles = new Vector3(90, Random.Range(-1*randangleredirectrange, randangleredirectrange)+Mathf.Atan2((Player.transform.position.x - this.transform.position.x), (Player.transform.position.z - this.transform.position.z)) * Mathf.Rad2Deg, 0);
        objrigid.velocity = objrigid.transform.right * mag;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
