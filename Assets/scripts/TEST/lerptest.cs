using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lerptest : MonoBehaviour
{
    // Start is called before the first frame update
    public float upperangle;
    public float lowerangle;
    public float timelerp;
    public float timer;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.localEulerAngles = new Vector3(this.transform.localEulerAngles.x, this.transform.localEulerAngles.y, Mathf.Lerp(upperangle, lowerangle, timer));
        print("LERP VAL: " + Mathf.Lerp(upperangle, lowerangle, timer));
        timer += Time.deltaTime / timelerp;
        if(timer >= 1)
        {
            float temp = upperangle;
            upperangle = lowerangle;
            lowerangle = temp;
            timer = 0;
        }
    }
}
