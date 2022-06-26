using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class slider_text_2 : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Slider slider;
    public int offset = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float val = slider.value + offset;
        text.SetText(val.ToString());
    }
}
