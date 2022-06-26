using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class button_color_script : MonoBehaviour
{
    public Image[] objs;
    public int selected;
    public Color selected_color;
    public Color basic_color;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void change_color(int num)
    {
        selected = num;
        for(int i = 0; i<objs.Length; i++)
        {
            if(i == selected)
            {
                objs[i].color = selected_color;
            }
            else
            {
                objs[i].color = basic_color;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
