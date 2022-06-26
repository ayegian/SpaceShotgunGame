using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyaftertime : MonoBehaviour
{
    public float time;
    public bool alreadyset;
    // Start is called before the first frame update
    void Start()
    {
        if (alreadyset)
        {
            StartCoroutine(destroyobj());
        }
    }
    public void Init(float loctime)
    {
        time = loctime;
        StartCoroutine(destroyobj());
    }
    IEnumerator destroyobj()
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
