using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class difficulty : MonoBehaviour
{
    //THIS IS A SHIT SCRIPT BECAUSE I DIDNT MAKE A BASE PARENT CLASS, REMEMBER FOR FUTURE
    // Start is called before the first frame update
    public int difficulty_num = 1; //0 easy 1 normal 2 hard EASY IS 2.5f MORE TIME BETWEEN ATTACKS, HARD IS 2.5f LESS TIME
    public int[] difficult_healths = {10, 8, 6};
    public bool limited_ammo = false;
    public float[] ammo_per_level = { 100, 100, 100, 100, 100, 100, 100, 100 };
    public int max_boss_num = 0;
    public int cur_boss_num = 0;
    void Start()
    {
        if (GameObject.FindWithTag("difficulty") != this.gameObject)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Object.DontDestroyOnLoad(this.gameObject);
        }
        //UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
     }
    public void set_difficulty(int diff_num)
    {
        difficulty_num = diff_num;
    }
    public void set_limited(bool limited)
    {
        limited_ammo = limited;
    }
    void OnSceneLoaded()
    {
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
