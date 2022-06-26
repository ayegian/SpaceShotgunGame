using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lose_canvas_enable : MonoBehaviour
{
    public reset_boss reset;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void OnEnable()
    {
        Time.timeScale = 0;
        //reset.destroy_boss_obj();
    }
    void OnDisable()
    {
        Time.timeScale = 1;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
