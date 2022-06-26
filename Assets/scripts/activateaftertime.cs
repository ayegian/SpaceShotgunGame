using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activateaftertime : MonoBehaviour
{
    public GameObject activateobj;
    public float time;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(activate());
    }
    public IEnumerator activate()
    {
        yield return new WaitForSeconds(time);
        activateobj.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
