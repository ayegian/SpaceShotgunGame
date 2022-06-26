using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class currentlevel : MonoBehaviour {
    public int level;
    public levelmanager LevelManager;
    // Use this for initialization
    public static currentlevel LEVEL
    {
        get;
        private set;
    }
    void Awake()
    {
        if (LEVEL == null)
        {
            LEVEL = this;
            Object.DontDestroyOnLoad(gameObject);

        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start () {
          }
	
	// Update is called once per frame
	void Update () {
		
	}
}
