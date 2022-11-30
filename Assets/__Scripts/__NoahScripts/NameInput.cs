using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NameInput : MonoBehaviour
{
    public Text[] texts = new Text[0];
    private string letters = " ABCDEFGHIJKLMNOPQRSTUVWXYZ ";
    private int i;
    public bool highScoreNotAchieved;
    public ScoreManager scoreManager;
    public ScoreUi scoreUi;
    public ScoreFlag scoreFlag;
    public GameOver gameOverUi;
    private StartGameHandler thingsToSwitch;

    void Start()
    {
        thingsToSwitch = GetComponent<StartGameHandler>();
        i = 1;
    }

    void Update()
    {
        if (ScoreHandler.currentPlayerTopDistance < ScoreData.scores[0].score)
        {
            highScoreNotAchieved = true;
        }
        else
        {
            highScoreNotAchieved = false;
        }

        if (Input.GetKeyDown(KeyCode.A) && i != 1)
        {
            i -= 1;
        }

        if (Input.GetKeyDown(KeyCode.D) & i != letters.Length - 2)
        {
            i += 1;
        }
            texts[0].text = letters[i - 1].ToString();
            texts[1].text = letters[i].ToString();
            texts[2].text = letters[i + 1].ToString();

        if (Input.GetKeyDown(KeyCode.W))
        {
            texts[3].text += letters[i];
        }

        if (texts[3].text.ToCharArray().Length == 3 || highScoreNotAchieved)
        {
            if (!highScoreNotAchieved)
            {
                // scoreManager.AddScore(new Score(texts[3].text, Mathf.Floor(ScoreHandler.currentPlayerTopDistance)));
            ScoreData.scores[0] = new Score(texts[3].text, Mathf.Floor(ScoreHandler.currentPlayerTopDistance));
            scoreUi.UpdateScores();
            scoreManager.SaveScore();
            }

            GameManager.instance.player.GameOver = false;
            ScoreHandler.distance = 0;
            ScoreHandler.currentPlayerTopDistance = 0;
            texts[3].text = "";
            i = 1;
            scoreFlag.i = 0;
            scoreFlag.usePlayerScore = false;
            gameOverUi.seconds = 10;
            gameOverUi.miliseconds = 0;
            gameOverUi.timerUp = false;
            GameManager.instance.player.GoBackToInitial();
            thingsToSwitch.SwitchStuff();
        }
    }
}
