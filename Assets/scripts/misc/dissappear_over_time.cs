using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dissappear_over_time : MonoBehaviour
{
    public SpriteRenderer sprite;
    public float dissappear_time;
    public float minimum_alpha;
    public float alpha_a_second;
    float alpha_sub;
    // Start is called before the first frame update
    //200 in 2 seconds
    void Start()
    {
        alpha_a_second = (1 - minimum_alpha)/dissappear_time;
    }

    // Update is called once per frame
    void Update()
    {
        if(dissappear_time > 0)
        {
            Color cur_color = sprite.color;
            print("Before ALPHA: " + cur_color.a);
            cur_color.a -= alpha_a_second * Time.deltaTime;
            print("After ALPHA: " + cur_color.a);
            sprite.color = cur_color;
            dissappear_time -= Time.deltaTime;
        }
        else
        {
            Color cur_color = sprite.color;
            print("Before ALPHA: " + cur_color.a);
            cur_color.a = 0;
            print("After ALPHA: " + cur_color.a);
            sprite.color = cur_color;
        }
    }
}
