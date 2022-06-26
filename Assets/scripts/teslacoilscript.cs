using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teslacoilscript : MonoBehaviour
{
    // Start is called before the first frame update
    BoxCollider2D[] boxes;
    void Start()
    {
        
    }
    public void activatetesla(bool active)
    {
        foreach (BoxCollider2D box in boxes)
        {
            box.enabled = active;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
