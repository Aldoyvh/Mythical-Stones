using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [Header("Gameplay")]
    public Slider slHP;
    public TextMeshProUGUI txtTimer;
    public static TextMeshProUGUI txtHighScore;

    private void Awake()
    {
        instance = this;
       /* if (SaveManager.HasHighScore(SceneManager.GetActiveScene().buildIndex))
        {
            txtHighScore.gameObject.SetActive(true);
            txtHighScore.text = "HS: "+ SaveManager.LoadHighScore(SceneManager.GetActiveScene().buildIndex).ToString("0.##");
        }
        else
        {
            txtHighScore.gameObject.SetActive(false);
        }*/
    }
    private void Update()
    {
        txtTimer.text = TimeSpan.FromSeconds(Time.timeSinceLevelLoad).ToString(@"mm\:ss");
    }
}
