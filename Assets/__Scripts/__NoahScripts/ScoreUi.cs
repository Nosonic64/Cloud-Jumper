using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class ScoreUi : MonoBehaviour
{
    public RowUi rowUi;
    public ScoreManager scoreManager;
    public List<RowUi> rows;

    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            var score = 10;
            scoreManager.AddScore(new Score("NOA", score * i));
        }


        var scores = scoreManager.GetHighScores().ToArray();
        for (int i = 0; i < scores.Length; i++)
        {    
            var row = Instantiate(rowUi, transform).GetComponent<RowUi>();
            row.name.text = scores[i].name;
            row.score.text = scores[i].score.ToString();
            rows.Add(row);
        }
    }

    public void UpdateScores()
    {
        foreach(RowUi uniqueRow in rows.ToList())
        {
            Destroy(uniqueRow.gameObject);
            rows.Remove(uniqueRow); 
        }
        if (rows.Count == 0)
        {
            var scores = scoreManager.GetHighScores().ToArray();
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
