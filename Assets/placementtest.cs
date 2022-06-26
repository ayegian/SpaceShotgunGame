using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class placementtest : MonoBehaviour
{
    public GameObject instanthis;
    public BoxCollider2D[] colls;
    // Start is called before the first frame update
    void Start()
    {
        colls = this.GetComponentsInChildren<BoxCollider2D>();
        foreach(BoxCollider2D a in colls)
        {
            Instantiate(instanthis, a.transform.position, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
