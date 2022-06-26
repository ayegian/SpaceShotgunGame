using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class menuscript : MonoBehaviour
{
    public bool ispaused = false;
    public GameObject Menu;
    public GameObject DeadMenu;
    public GameObject Setting;
    // Use this for initialization
    public static menuscript Menuscript
    {
        get;
        private set;
    }
    void Awake()
    {
        if (Menuscript == null)
        {
            Menuscript = this;
            Object.DontDestroyOnLoad(gameObject);

        }
        else
        {
            Destroy(gameObject);
        }
        if(Menu == null)
        {
            Menu = GameObject.Find("pause menu");
            Setting = GameObject.Find("settings");
        }
        Menu.SetActive(false);
        print("OFF");
        Setting.SetActive(false);
    }
    public void MainMenu()
    {
        Time.timeScale = 1;
        Menu.SetActive(false);
        SceneManager.LoadScene("start");
    }
    public void Quit()
    {
        Menu.SetActive(false);
        Application.Quit();
    }
    public void Settingmenu()
    {
        print("ON");
        Setting.SetActive(true);
        Menu.SetActive(false);
    }
    public void Back()
    {
        Setting.SetActive(false);
        Menu.SetActive(true);
    }
    public void Resume()
    {
        print("SDPOFNSE");
        Time.timeScale = 1;
        AudioListener.pause = false;
        Menu.SetActive(false);
        DeadMenu.SetActive(false);
        ispaused = false;
        Setting.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            print("Happen");
            if (ispaused == false)
            {
                ispaused = true;
                Menu.SetActive(true);
                Time.timeScale = 0;
                AudioListener.pause = true;
            }
            else if (ispaused == true)
            {

                Resume();
            }
        }
    }
}
