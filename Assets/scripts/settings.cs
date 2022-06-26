using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class settings : MonoBehaviour {
    public AudioMixer mixer;
    public Slider MusicSlider;
    public Slider SFXSlider;
    public float slider_mult = 2;
    public bool music_muted;
    public bool sfx_muted;
        // Use this for initialization

    void Start () {
    }
    public void MusicVolume()
    {
        mixer.SetFloat("MusicVolume", (MusicSlider.value * slider_mult));
    }    
    public void SFXVolume()
    {
        mixer.SetFloat("SFXVolume", (SFXSlider.value*slider_mult));
    }
    public void MuteMusicVolume()
    {
        if (music_muted)
        {
            mixer.SetFloat("MusicVolume", (MusicSlider.value * slider_mult));
            music_muted = false;
        }
        else
        {
            print("MUTE SOUND");
            mixer.SetFloat("MusicVolume", -80);
            music_muted = true;
        }
    }   
    public void MuteSFXVolume()
    {
        if (sfx_muted)
        {
            mixer.SetFloat("SFXVolume", (MusicSlider.value * slider_mult));
            sfx_muted = false;
        }
        else
        {
            mixer.SetFloat("SFXVolume", -80);
            sfx_muted = true;
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}
