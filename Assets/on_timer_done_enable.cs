using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class on_timer_done_enable : MonoBehaviour
{
    public timer_script timer;
    public GameObject enable_obj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timer.done)
        {
            enable_obj.SetActive(true);
        }
    }
}
