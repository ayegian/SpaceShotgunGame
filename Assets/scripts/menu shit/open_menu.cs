using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class open_menu : MonoBehaviour
{
    public GameObject menu;
    // Start is called before the first frame update
    void Start()
    {
        print("MENU SET START");
        //if (menu.activeSelf)
        //{
        //    Time.timeScale = 0;
        //}
        //else
        //{
        //    Time.timeScale = 1;
        //}
    }
    public void openmenu()
    {
        menu.SetActive(!menu.activeSelf);
        if (menu.activeSelf)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menu.SetActive(!menu.activeSelf);
            print("MENU SET");
            if (menu.activeSelf)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        }
    }
}
