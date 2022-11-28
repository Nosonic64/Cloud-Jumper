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


        if (PlayerInfo.gameOver)
        {
            usePlayerScore = true;
            slider.minValue = 0f;
        }
    }
}
