using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class slider_text : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI text;
    public float offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = slider.minValue * -1;
    }

    // Update is called once per frame
    void Update()
    {
        float slide_val = slider.value + offset;
        text.SetText(slide_val.ToString());
    }
}
