using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NameInput : MonoBehaviour
{
    #region private variables
    private string letters = " ABCDEFGHIJKLMNOPQRSTUVWXYZ ";
    private int selectedLetter;
    private StartGameHandler thingsToSwitch;
    private float inputDelay = 0.2f;
    private float inputDelayCounter;
    #endregion

    #region serialized variables
    [SerializeField] private GameOver gameOverUi;
    [SerializeField] private HighScoreManager highScoreManager;
    [SerializeField] private ScoreUi scoreUi;
    [SerializeField] private ScoreFlag scoreFlag;
    [SerializeField] private Text[] texts = new Text[0];
    #endregion

    void Start()
    {
        thingsToSwitch = GetComponent<StartGameHandler>();
        selectedLetter = 1;
        if (GameManager.instance.scoreManager.CurrentPlayerTopDistance < GameManager.instance.scoreData.scores[9].score)
        {
            ResetStuff();
        }
        UpdateLetterDisplay();
    }

    void Update()
    {
        if(inputDelayCounter > 0f)
        {
            inputDelayCounter -= Time.deltaTime;
        }

        if (Input.GetAxisRaw("Horizontal") < 0 && selectedLetter != 1 && inputDelayCounter <= 0f)
        {
            selectedLetter -= 1;
            inputDelayCounter = inputDelay;
            UpdateLetterDisplay();
        }

        if (Input.GetAxisRaw("Horizontal") > 0 && selectedLetter != letters.Length - 2 && inputDelayCounter <= 0f)
        {
            selectedLetter += 1;
            inputDelayCounter = inputDelay;
            UpdateLetterDisplay();
        }

        if (Input.GetButtonDown("Jump"))
        {
            texts[3].text += letters[selectedLetter];
        }

        if (texts[3].text.ToCharArray().Length == 3)
        {
            //ScoreData.scores[0] = new Score(texts[3].text, Mathf.Floor(GameManager.instance.scoreManager.CurrentPlayerTopDistance));
            GameManager.instance.scoreData.AddScore(texts[3].text, (int)Mathf.Floor(GameManager.instance.scoreManager.CurrentPlayerTopDistance));
            scoreUi.UpdateScores();
            //highScoreManager.SaveScore();
            GameManager.instance.scoreData.SaveScoresToFile();
            ResetStuff();
        }
    }

    private void UpdateLetterDisplay()
    {
        texts[0].text = letters[selectedLetter - 1].ToString();
        texts[1].text = letters[selectedLetter].ToString();
        texts[2].text = letters[selectedLetter + 1].ToString();
    }

    private void ResetStuff()
    {
        GameManager.instance.player.GameOver = false;
        GameManager.instance.scoreManager.Distance = 0;
        GameManager.instance.scoreManager.CurrentPlayerTopDistance = 0;
        texts[3].text = "";
        selectedLetter = 1;
        gameOverUi.seconds = 10;
        gameOverUi.miliseconds = 0;
        gameOverUi.timerUp = false;
        GameManager.instance.player.GoBackToInitial();
        thingsToSwitch.SwitchStuff();
    }
}
