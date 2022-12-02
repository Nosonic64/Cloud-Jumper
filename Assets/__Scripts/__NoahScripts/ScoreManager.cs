using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    #region private variables
    private float distance;
    private float currentPlayerTopDistance;
    #endregion

    #region getters and setters
    public float Distance { get => distance; set => distance = value; }
    public float CurrentPlayerTopDistance { get => currentPlayerTopDistance; set => currentPlayerTopDistance = value; }
    #endregion

    public void PlayerDeathScoreChange()
    {
        if (GameManager.instance.scoreManager.Distance > GameManager.instance.scoreManager.CurrentPlayerTopDistance)
        {
            GameManager.instance.scoreManager.CurrentPlayerTopDistance = GameManager.instance.scoreManager.Distance;
        }
        GameManager.instance.player.transform.position = new Vector3(0, 0, 0);
        GameManager.instance.gameOverUI.SetActive(true);
        GameManager.instance.player.GameOver = true;
        GameManager.instance.player.gameObject.SetActive(false);
        GameManager.instance.player.PlayerLives = -1;
    }
}
