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
    private Switcher thingsToSwitch;
    private string letters = " ABCDEFGHIJKLMNOPQRSTUVWXYZ ";
    private int selectedLetter;
    private float inputDelay = 0.2f;
    private float inputDelayCounter;
    #endregion

    #region serialized variables
    [SerializeField] private ScoreUi scoreUi;
    [SerializeField] private ScoreFlag scoreFlag;
    [SerializeField] private Text[] texts = new Text[0];
    #endregion

    void Start()
    {
        thingsToSwitch = GetComponent<Switcher>();
        selectedLetter = 1;
        if (GameManager.instance.scoreManager.CurrentPlayerTopDistance < GameManager.instance.highScoreHandler.scores[0].score)
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
            var playersScore = new Score(texts[3].text, Mathf.Floor(GameManager.instance.scoreManager.CurrentPlayerTopDistance));
            GameManager.instance.highScoreHandler.AddScore(playersScore);
            GameManager.instance.highScoreHandler.SaveScoresToFile();
            scoreUi.UpdateScores();
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
        GameManager.instance.scoreManager.Distance = 0;
        GameManager.instance.scoreManager.CurrentPlayerTopDistance = 0;
        texts[3].text = "";
        selectedLetter = 1;
        thingsToSwitch.SwitchStuff();
    }
}
