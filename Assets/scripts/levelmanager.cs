using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class levelmanager : MonoBehaviour {
    // Use this for initialization
    public int loadlevel;
    private void Start()
    {
        loadlevel = currentlevel.LEVEL.level; 
    }

    public void LoadCurrentLevel(string level)
    {
        SceneManager.LoadScene("level" + loadlevel);
    }
	public void LoadLevel(string level)
    {
        SceneManager.LoadScene(level);
    }
	// Update is called once per frame
	void Update () {
		
	}
}
