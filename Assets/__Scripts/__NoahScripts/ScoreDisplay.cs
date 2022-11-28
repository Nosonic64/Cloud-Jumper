using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    private Text text;

    private void Start()
    {
        text = GetComponent<Text>();
    }

    private void Update()
    {
        text.text = ("Distance: " + ScoreHandler.distance.ToString("F0") + " | Lives: " + PlayerInfo.playerLives + " | Your Top Score: " + ScoreHandler.currentPlayerTopDistance.ToString("F0"));
    }
}