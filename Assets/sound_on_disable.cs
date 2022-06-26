using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sound_on_disable : MonoBehaviour
{
    public AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void OnDisable()
    {
        audio.Play();
        audio.transform.parent = null;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
