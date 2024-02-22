using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider bgmSlider;

    void Awake()
    {
        float sfxDB;
        float bgmDB;
        audioMixer.GetFloat("sfxVol", out sfxDB);
        audioMixer.GetFloat("bgmVol", out bgmDB);
        sfxSlider.value = GetFloatVolume(sfxDB);
        bgmSlider.value = GetFloatVolume(bgmDB);
    }

    private float GetFloatVolume(float value)
    {
        return Mathf.Pow(10, value / 20);
    }

    private float GetDb(float value)
    {
        return Mathf.Log10(value) * 20;
    }

    public void SetBGM(float value)
    {
        audioMixer.SetFloat("bgmVol", GetDb(value));
    }

    public void SetSFX(float value)
    {
        audioMixer.SetFloat("sfxVol", GetDb(value));
    }
}
