using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sound_on_enable : MonoBehaviour
{
    public AudioSource sound;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void OnEnable()
    {
        sound.Play();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
