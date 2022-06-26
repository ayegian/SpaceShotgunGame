using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class set_difficulty : MonoBehaviour
{
    public difficulty_script difficulty;
    // Start is called before the first frame update
    void Start()
    {
        difficulty = FindObjectOfType<difficulty_script>();
    }
    public void diff_void(int diff)
    {
        difficulty.difficulty = diff;
    }
    public void limited_void(bool limited)
    {
        difficulty.limited_ammo = limited;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
