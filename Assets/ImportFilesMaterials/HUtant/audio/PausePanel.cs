using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PausePanel : MonoBehaviour
{
    
    public AudioMixerGroup Mixer;
    [SerializeField] private string _nameGroupsToggleMusic ;
    [SerializeField] private string _nameGroupsChangeVolume ;

    private void Start()
    {
        GetComponentInChildren<Toggle>().isOn = PlayerPrefs.GetInt(_nameGroupsToggleMusic, 1) == 1;
        GetComponentInChildren<Slider>().value = PlayerPrefs.GetFloat(_nameGroupsChangeVolume, 1);
    }

    public void ToggleMusic(bool enabled)
    {
        if (enabled)

            Mixer.audioMixer.SetFloat(_nameGroupsToggleMusic, 0);
        else 
            Mixer.audioMixer.SetFloat(_nameGroupsToggleMusic, -80);

        PlayerPrefs.SetInt(_nameGroupsToggleMusic, enabled ? 1 : 0);
    }
    
    public void ChangeVolume(float volume)
    {

        Mixer.audioMixer.SetFloat(_nameGroupsChangeVolume, Mathf.Lerp(-25, 0, volume));
        PlayerPrefs.SetFloat(_nameGroupsChangeVolume, volume);
    }
}
