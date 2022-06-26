using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class on_click_stuff : MonoBehaviour
{
    public bool enable_something;
    public bool disable_something;
    public GameObject enable_obj;
    public GameObject disable_obj;
    // Start is called before the first frame update
    void Start()
    {

    }
    public void on_click_enable_or_disable()
    {
        if (enable_something)
        {
            enable_obj.SetActive(true);
        }
        if (disable_something)
        {
            disable_obj.SetActive(false);
        }
    }
    public void on_click_exit()
    {
        Application.Quit();
    }
    public void on_click_load_level(string level)
    {
        SceneManager.LoadScene(level);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
