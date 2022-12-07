using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] private Text timer;
    private float seconds = 10;
    private float miliseconds = 0;
    private Switcher thingsToSwitch;
    private bool timerUp = false;

    private void Start()
    {
        thingsToSwitch = GetComponent<Switcher>();
    }

    private void OnEnable()
    {
        seconds = 10;
        miliseconds = 0;
        timerUp = false;
    }

    void Update()
    {

        if (miliseconds <= 0)
        {
            if (seconds <= 0)
            {
                GameManager.instance.levelChunkManager.ResetTimerCounter = 0f;
                GameManager.instance.levelChunkManager.PassiveScrollMultiple = 0f;
                GameManager.instance.player.GoBackToInitial();
                thingsToSwitch.SwitchStuff();
                timerUp = true;
            }
            else if (seconds >= 0)
            {
                seconds--;
            }

            miliseconds = 100;
        }

        if(seconds > 0f || miliseconds > 0f) 
        {
            if (GameManager.instance.player.RetryCount > 0)
            {
                miliseconds -= Time.deltaTime * 100;
            }
            else
            {
                seconds = 0f;
                miliseconds = 0f;
            }
        }

        timer.text = string.Format("{0}:{1}", seconds, (int)miliseconds);

        if (GameManager.instance.scoreManager.Distance > 0f)
        {
            GameManager.instance.scoreManager.Distance -= Time.deltaTime * GameManager.instance.scoreManager.Distance / 12f;
        }
        else
        {
            GameManager.instance.scoreManager.Distance = 0f;
        }

        if(Input.GetKeyDown(KeyCode.I) && !timerUp)
        {
            GameManager.instance.levelChunkManager.ResetTimerCounter = 0f;
            GameManager.instance.player.RetryCount--;
            GameManager.instance.player.GameOverRespawn();
            gameObject.SetActive(false);
        }
    }
}

