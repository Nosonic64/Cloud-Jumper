using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public Text timer;
    public float seconds = 10;
    public float miliseconds = 0;
    private StartGameHandler thingsToSwitch;
    public bool timerUp = false;
    public GameObject player;

    private void Start()
    {
        thingsToSwitch = GetComponent<StartGameHandler>();
    }

    void Update()
    {

        if (miliseconds <= 0)
        {
            if (seconds <= 0)
            {
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

        if(ScoreHandler.distance > 0f)
        {
            ScoreHandler.distance -= Time.deltaTime * ScoreHandler.distance / 12f;
        }
        else
        {
            ScoreHandler.distance = 0f;
        }

        timer.text = string.Format("{0}:{1}", seconds, (int)miliseconds);

        if(Input.GetKeyDown(KeyCode.I) && !timerUp)
        {
            seconds = 10;
            miliseconds = 0;
            GameManager.instance.player.RetryCount--;
            player.SetActive(true);
            GameManager.instance.player.GameOverRespawn();
            gameObject.SetActive(false);
        }
    }
}

