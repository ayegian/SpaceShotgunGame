using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class aspect_text : MonoBehaviour
{
    public Camera cam;
    public TextMeshPro text;
    // Start is called before the first frame update
    void Start()
    {
        cam.aspect = (1920 / 1080);   
    }

    // Update is called once per frame
    void Update()
    {
        text.SetText(cam.aspect.ToString());
    }
}
