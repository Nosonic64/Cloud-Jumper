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
            if (seconds <= 0) //If the timers up, we transition to either NameInput, or if the player doesnt have a high enough score, we reset the values of all objects
            {
                PlayerDoesNotContinue();
            }
            else if (seconds >= 0)
            {
                seconds--;
            }

            miliseconds = 100;
        }

        if(seconds > 0f || miliseconds > 0f) 
        {
            if (GameManager.instance.player.RetryCount > 0) //If the player has a retry left, we countdown the timer
            {
                miliseconds -= Time.deltaTime * 100;
            }
            else //If the player does not have a retry left, we immediately reset everything
            {
                PlayerDoesNotContinue();
            }
        }

        timer.text = string.Format("{0}:{1}", seconds, (int)miliseconds);

        if (GameManager.instance.scoreManager.Distance > 0f) //We lower the players current score over time until they insert another coin to respawn / continue
        {
            GameManager.instance.scoreManager.Distance -= Time.deltaTime * GameManager.instance.scoreManager.Distance / 12f;
        }
        else
        {
            GameManager.instance.scoreManager.Distance = 0f;
        }

        if(Input.GetKeyDown(KeyCode.I) && !timerUp) //If the player inserts a coin to respawn, we disable this script, reset some values and respawn the player
        {
            GameManager.instance.levelChunkManager.ResetTimerCounter = 0f;
            GameManager.instance.player.RetryCount--;
            GameManager.instance.player.GameOverRespawn();
            gameObject.SetActive(false);
        }
    }

    private void PlayerDoesNotContinue()
    {
        GameManager.instance.levelChunkManager.ResetTimerCounter = 0f;
        GameManager.instance.levelChunkManager.PassiveScrollMultiple = 0f;
        GameManager.instance.player.GoBackToInitial();
        thingsToSwitch.SwitchStuff();
        timerUp = true;
    }
}

