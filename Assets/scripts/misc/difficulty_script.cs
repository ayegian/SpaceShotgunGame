using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class difficulty_script : MonoBehaviour
{
    public int difficulty;
    public bool limited_ammo;
    public int[] health_difficulty;
    public float[] difficulty_wait_time_multipliers;
    public int cur_boss;
    //public int[] ammo_per_level;
    //resolve ammo_per_level in shoot
    // Start is called before the first frame update
    void Awake()
    {
        difficulty_script[] scripts = FindObjectsOfType<difficulty_script>();
        if(scripts.Length >= 2)
        {
            print("DESTROY THIS");
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        
    }
    public void set_difficulty(int diff)
    {
        difficulty = diff;
    }   
    public void set_limited_ammo(bool limited)
    {
        limited_ammo = limited;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
