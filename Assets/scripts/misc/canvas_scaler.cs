﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class canvas_scaler : MonoBehaviour
{
    public CanvasScaler canvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        print("SCALE FACTOR: " + canvas.scaleFactor);
    }
}
