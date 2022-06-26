using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toggle_collider : MonoBehaviour
{
    public Collider colliderinstan;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void togglecollider(bool on)
    {
        colliderinstan.enabled = on;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
