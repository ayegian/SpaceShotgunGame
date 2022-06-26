using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class on_timer_done_load : MonoBehaviour
{
    public timer_script timer;
    int index;
    // Start is called before the first frame update
    void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
        index = scene.buildIndex+1;
        timer.WaitVoid();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer.done)
        {
            SceneManager.LoadScene(index, LoadSceneMode.Single);
        }
    }
}
