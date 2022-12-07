using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUi : MonoBehaviour
{
    public RowUi rowUi;
    public HighScoreManager highScoreManager;
    public List<RowUi> rows;

    void Start()
    {
        GameManager.instance.scoreData.LoadScoresFromFile();
        UpdateScores();
    }

    public void UpdateScores() //Updates the score on screen by creating gameobjects with text that contain Name and Score values from scoreData
    {
        foreach(RowUi uniqueRow in rows.ToList()) //We have to destroy all the objects occupying the scoreboard before making new ones
        {
            Destroy(uniqueRow.gameObject);
            rows.Remove(uniqueRow); 
        }
        if (rows.Count == 0)
        {
            var scores = GameManager.instance.scoreData.scores;
            for (int i = 0; i < scores.Length; i++)
            {
                var row = Instantiate(rowUi, transform).GetComponent<RowUi>();
                row.name.text = scores[i].name;
                row.score.text = scores[i].score.ToString();
                rows.Add(row);
            }
        }
    }
}
