using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class SetAudio : MonoBehaviour
{
    [SerializeField] private AudioMixer master;

    void Start()
    {
        float vol = PlayerPrefs.GetFloat(master.name + "VOLUME");
        GetComponent<Slider>().value = vol <= 0.0001f ? 0.5f : vol;
    }

    public void SetVolume(float vol)
    {
        master.SetFloat("Volume", Mathf.Log10(vol) * 20);
        PlayerPrefs.SetFloat(master.name + "VOLUME", vol);
    }
}
