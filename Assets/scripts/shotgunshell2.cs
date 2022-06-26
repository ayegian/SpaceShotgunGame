using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shotgunshell2 : MonoBehaviour
{
    public Rigidbody thisrigid;
    public Rigidbody[] rigids;
    public float degreesbetweenpellets;
    public float adjust;
    // Start is called before the first frame update
    void Start()
    {

    }
    private void onmove()
    {
        float startangle = degreesbetweenpellets * -1*(rigids.Length-1)/2;
        float timer = 0;
        foreach (Rigidbody a in rigids)
        {
            a.transform.localEulerAngles = Vector3.zero + new Vector3(0, startangle + timer * degreesbetweenpellets, 0);
            //a.transform.localEulerAngles = new Vector3(a.transform.localEulerAngles.x, /*startangle + degreesbetweenpellets * timer*/0, a.transform.localEulerAngles.z);
            a.transform.parent = null;
            a.velocity = a.transform.forward *thisrigid.velocity.magnitude;
            timer++;
        }
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        if(thisrigid.velocity.magnitude > 0)
        {
            onmove();
        }
    }
}
