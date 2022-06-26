using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class timer_text : MonoBehaviour
{
    public TextMeshProUGUI text;
    public timer_script timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timer.wait_timer > 0)
        {
            text.SetText(timer.wait_timer.ToString());
        }
        else
        {
            text.SetText("");
        }
    }
}
