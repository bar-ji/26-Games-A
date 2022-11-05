using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private PlaneMovement movement;
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private TMPro.TMP_Text endScore;
    [SerializeField] private TMPro.TMP_Text score;

    float timeGameStarted;

    void Start()
    {
        timeGameStarted = Time.time;
    }

    private void OnEnable()
    {
        movement.OnDeath += EnableUI;
    }

    private void OnDisable()
    {
        movement.OnDeath -= EnableUI;
    }

    void Update()
    {
        score.text = "Score: " + (Time.time - timeGameStarted + movement.score).ToString("F0");
    }

    void EnableUI()
    {
        deathScreen.SetActive(true);
        endScore.text = "Score: " + (Time.time - timeGameStarted + movement.score).ToString("F0");
    }


}
