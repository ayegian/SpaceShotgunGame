using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sound_on_component_enable : MonoBehaviour
{
    public SpriteRenderer sprite;
    bool played = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!played && sprite.enabled)
        {
            GetComponent<AudioSource>().Play();
        }
    }
}
