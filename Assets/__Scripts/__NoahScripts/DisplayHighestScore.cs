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
}
