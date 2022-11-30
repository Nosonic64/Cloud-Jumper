using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreFlag : MonoBehaviour
{
    private Slider slider;
    private Text text;
    public int i;
    public bool usePlayerScore;

    void Start()
    {
        i = 0;
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
            slider.maxValue = ScoreHandler.currentPlayerTopDistance;
        }
        else if (i != ScoreData.scores.Count)
        {
            slider.maxValue = ScoreData.scores[i].score;
        }
        else
        {
            slider.minValue = ScoreHandler.distance - 0.1f;
            slider.maxValue = ScoreHandler.distance + 0.1f;
        }

        text.text = slider.maxValue.ToString("F0") + "m";
        slider.value = ScoreHandler.distance;

            if (slider.value >= slider.maxValue)
            {
                if (usePlayerScore)
                {
                    usePlayerScore = false;
                    if (i != 0)
                    {
                        slider.minValue = ScoreData.scores[i - 1].score;
                    }
                    return;
                }
                slider.minValue = ScoreData.scores[i].score;
                i++;
                    if(i == ScoreData.scores.Count)
                    {

                    }
            }

        // When a player game-overs, we use the players top score in that play session to display at the top of the flag
        if (GameManager.instance.player.GameOver)
        {
            usePlayerScore = true;
            slider.minValue = 0f;
        }
    }
}
