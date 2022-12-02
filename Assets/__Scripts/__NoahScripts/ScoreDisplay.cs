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

    private void LateUpdate()
    {
        text.text = ("Distance: " + GameManager.instance.scoreManager.Distance.ToString("F0") + " | Lives: " + GameManager.instance.player.PlayerLives + " | Your Top Score: " + GameManager.instance.scoreManager.CurrentPlayerTopDistance.ToString("F0"));
    }
}