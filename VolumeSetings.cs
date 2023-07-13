using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSetings : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Slider slider;

    private void Start()
    {
        if (PlayerPrefs.HasKey("volumeSound"))
        {
            loadVolume();
            Debug.Log("has key");
        }
        else
        {
            setVolume();
            Debug.Log("has not key");
        }
    }

    public void setVolume()
    {
        float volume = slider.value;
        audioSource.volume = volume;
        PlayerPrefs.SetFloat("volumeSound", volume);
        Debug.Log("Volume1: " + volume);
    }

    public void loadVolume()
    {
        Debug.Log("Volume2: " + PlayerPrefs.GetFloat("volumeSound"));
        slider.value = PlayerPrefs.GetFloat("volumeSound");
        audioSource.volume = PlayerPrefs.GetFloat("volumeSound");
        setVolume();

    }
}
