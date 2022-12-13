using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayHighestScore : MonoBehaviour
{
    private Text text;

    private void Awake()
    {
        text = GetComponent<Text>();
    }
    private void OnEnable()
    {
        text.text = "highest score: " + GameManager.instance.scoreData.scores[0].score + "M";
    }

    private void Update()
    {
        if(GameManager.instance.scoreManager.Distance > GameManager.instance.scoreData.scores[0].score)
        {
            text.text = "highest score: " + GameManager.instance.scoreManager.Distance.ToString("F0") + "M";
        }
        else if(GameManager.instance.scoreManager.CurrentPlayerTopDistance > GameManager.instance.scoreManager.Distance)
        {
            text.text = "highest score: " + GameManager.instance.scoreManager.CurrentPlayerTopDistance.ToString("F0") + "M";
        }
    }
}
