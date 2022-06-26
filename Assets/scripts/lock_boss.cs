using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lock_boss : MonoBehaviour
{
    public int boss_num;
    // Start is called before the first frame update
    void Start()
    {
        if(FindObjectOfType<difficulty_script>().cur_boss < boss_num)
        {
            this.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
