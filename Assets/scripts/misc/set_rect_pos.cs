using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class set_rect_pos : MonoBehaviour
{
    public RectTransform can_obj;
    public float TextWidth;
    // Start is called before the first frame update
    void Start()
    {
        // print("CAN RECT: " + can_obj.rect+" TRANSFORM: "+can_obj.position+" ANCHORED POS: "+can_obj.anchoredPosition);
        // GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "This is a box");
        // can_obj.rect.Set(((float)Screen.width, Screen.height, 10, 10));
        
        GUI.Label(new Rect(Screen.width - TextWidth, 10, TextWidth, 22), "Text");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
           // print("CAN RECT: " + can_obj.rect + " TRANSFORM: " + can_obj.position + " ANCHORED POS: " + can_obj.anchoredPosition+" DIFF X: "+((Screen.width/2)-can_obj.anchoredPosition.x+" DIFF Y: "+((Screen.height/2)-can_obj.anchoredPosition.y)));
            print("WIDTH: " + Screen.width + " HEIGHT: " + Screen.height+" RECT POS: "+can_obj.anchoredPosition);

        }
    }
}
