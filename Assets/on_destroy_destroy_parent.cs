using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class on_destroy_destroy_parent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void OnDestroy()
    {
        Destroy(transform.parent.gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
