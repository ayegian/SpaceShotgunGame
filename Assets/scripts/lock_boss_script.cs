using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lock_boss_script : MonoBehaviour
{
    public int boss_num;
    // Start is called before the first frame update
    void Start()
    {
        difficulty diff_script = GameObject.FindWithTag("difficulty").GetComponent<difficulty>();
        if(diff_script.max_boss_num < boss_num)
        {
            this.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
