using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{

    public AudioMixer musicMixer, effectMixer;

    public AudioSource backGroundmusicAS;

    [Range(-80, 20)]
    public float effectVol, masterVol;

    public Slider masterSldr,effectSldr;

    public static AudioManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }


    public void MasterVolume()
    {

        DataManager.instance.SetMusicData(masterSldr.value);
        musicMixer.SetFloat("masterVolume",PlayerPrefs.GetFloat("MusicVolume"));
    }

    public void EffectVolume()
    {
        DataManager.instance.FXData(effectSldr.value);
        effectMixer.SetFloat("EffectVolume", PlayerPrefs.GetFloat("FXVolume"));

    }

    void Start()
    {
        PlayAudio(backGroundmusicAS);
        //masterSldr.value = masterVol;
        //effectSldr.value = effectVol;

        masterSldr.minValue = -80;
        masterSldr.maxValue = 20;

        effectSldr.minValue = -80;
        effectSldr.maxValue = 20;

        masterSldr.value = PlayerPrefs.GetFloat("MusicVolume", 0f);
        effectSldr.value = PlayerPrefs.GetFloat("FXVolume", 0f);

    }

    // Update is called once per frame
    void Update()
    {
        //MasterVolume();
        //EffectVolume();
    }

    public void PlayAudio(AudioSource audio)
    {
        audio.Play();
    }

}
