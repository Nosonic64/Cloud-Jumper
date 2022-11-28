using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreManager : MonoBehaviour
{
    private ScoreData sd;

    private void Awake()
    {
        var json = PlayerPrefs.GetString("scores", "{}");
        sd = new ScoreData();  
    }

    public IEnumerable<Score> GetHighScores()
    {
        return ScoreData.scores.OrderByDescending(x => x.score);
    }

    public void AddScore(Score score)
    {
        ScoreData.scores.Add(score);
    }

    private void OnDestroy()
    {
        SaveScore();
    }

    public void SaveScore()
    {
        var json = JsonUtility.ToJson(sd);
        PlayerPrefs.SetString("scores",json);
    }
}
