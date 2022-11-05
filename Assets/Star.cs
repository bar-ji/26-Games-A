using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Audio;


public class Star : MonoBehaviour
{
    [SerializeField] private AudioClip starSFX;
    [SerializeField] private AudioMixer mixer;

    void Update()
    {
        transform.Rotate(Vector3.forward, Time.deltaTime * 50);

        if(transform.localScale.magnitude <= 0)
            Destroy(gameObject);
    }

    void OnTriggerEnter()
    {
        transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InElastic);
    }

    private void OnDestroy() 
    {
        float volume;
        mixer.GetFloat("Volume", out volume);
        AudioSource.PlayClipAtPoint(starSFX, transform.position);
    }
}
