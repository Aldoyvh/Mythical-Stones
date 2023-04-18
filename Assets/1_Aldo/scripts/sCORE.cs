using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class sCORE : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Score")]
    public static int points = 0;
    TextMeshProUGUI score;

    void Start()
    {
        score = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        score.text = "Score: " + points;

    }
}
