using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AnimatedButton : MonoBehaviour
{
    [SerializeField] private float scaleMultiplier;
    Vector3 scaleOnStart;

    void Start() => scaleOnStart = transform.localScale;

    public void Hover()
    {
        transform.DOScale(scaleOnStart * scaleMultiplier, 0.2f).SetEase(Ease.OutSine);
        GetComponent<AudioSource>().Play();
    }

    public void DeHover()
    {
        transform.DOScale(scaleOnStart, 0.2f).SetEase(Ease.OutSine);
    }
}
