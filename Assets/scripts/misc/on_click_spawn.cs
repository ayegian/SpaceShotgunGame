using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class on_click_spawn : MonoBehaviour
{
    public Transform spawnpoint;
    public GameObject spawn_obj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Instantiate(spawn_obj, spawnpoint.position, spawnpoint.rotation);
        }
    }
}
