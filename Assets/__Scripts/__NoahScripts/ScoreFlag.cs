using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreFlag : MonoBehaviour
{
    private Slider slider;
    private Text text;
    private int highScoreToBeat;
    private bool usePlayerScore;

    void Start()
    {
        highScoreToBeat = 0;
        slider = GetComponent<Slider>();
        text = GetComponentInChildren<Text>();
    }

    void LateUpdate()
    {
        //If we are using the players score (ie they have game overed once and retried) the max value of the slider will equal the players top score in that play session
        //Else, if we havent passed the top high score, display the high score currently above the players score
        //Else, if we have passed the highest score, we just display the players score
        if (usePlayerScore)
        {
            slider.maxValue = GameManager.instance.scoreManager.CurrentPlayerTopDistance;
        }
        else if (highScoreToBeat != ScoreData.scores.Count)
        {
            slider.maxValue = ScoreData.scores[highScoreToBeat].score;
        }
        else
        {
            slider.minValue = GameManager.instance.scoreManager.Distance - 0.1f;
            slider.maxValue = GameManager.instance.scoreManager.Distance + 0.1f;
        }

        text.text = slider.maxValue.ToString("F0") + "m";
        slider.value = GameManager.instance.scoreManager.Distance;

            if (slider.value >= slider.maxValue)
            {
                if (usePlayerScore)
                {
                    usePlayerScore = false;
                    if (highScoreToBeat != 0)
                    {
                        slider.minValue = ScoreData.scores[highScoreToBeat - 1].score;
                    }
                    return;
                }
                slider.minValue = ScoreData.scores[highScoreToBeat].score;
                highScoreToBeat++;
            }

        // When a player game-overs, we use the players top score in that play session to display at the top of the flag
        if (GameManager.instance.player.GameOver)
        {
            usePlayerScore = true;
            slider.minValue = 0f;
        }
    }

    private void OnDisable()
    {
        highScoreToBeat = 0;
        usePlayerScore = false;
    }
}
