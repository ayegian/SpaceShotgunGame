using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enable_after_time : MonoBehaviour
{
    public bool enable_something;
    public bool disable_something;
    public GameObject enable_this;
    public GameObject disable_this;
    public float wait_time;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(wait());
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(wait_time);
        if (enable_something)
        {
            enable_this.SetActive(true);
        }
        if (disable_something)
        {
            disable_this.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
