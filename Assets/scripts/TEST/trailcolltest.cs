using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trailcolltest : MonoBehaviour
{
    public Transform droppoint;
    public GameObject vine;
    public float dist;
    public Vector3 lastframe;
    public int vinenum = 1;
    // Start is called before the first frame update
    void Start()
    {
        lastframe = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(lastframe, this.transform.position) >= dist)
        {
            GameObject vineinstan = Instantiate(vine, droppoint.transform.position, Quaternion.Euler(0, droppoint.transform.eulerAngles.y+90, droppoint.transform.eulerAngles.z));
            if (vinenum % 2 == 0)
            {
                vinenum = 1;
            }
            vineinstan.transform.localScale = new Vector3(Vector3.Distance(lastframe, this.transform.position), vineinstan.transform.localScale.y, vineinstan.transform.localScale.z);
            lastframe = this.transform.position;
        }
    }
}
