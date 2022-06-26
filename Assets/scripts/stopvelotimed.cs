using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stopvelotimed : MonoBehaviour
{
    public Rigidbody thisrigid;
    public float basicwaittime;
    public bool randwaittime;
    public float randwaittimehigh;
    public float randwaittimelow;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(waitpause());
    }
    IEnumerator waitpause()
    {
        if (randwaittime)
        {
            yield return new WaitForSeconds(Random.Range(randwaittimelow, randwaittimehigh));
        }
        else
        {
            yield return new WaitForSeconds(basicwaittime);
        }
        thisrigid.velocity = Vector3.zero;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
