using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager instance;
    public PlayerMovement player;
    public GameObject gameOverUI;
    public SpriteSequence bellSprite;
    public LevelChunkManager levelChunkManager;
    public PowerUpManager powerUpManager;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        } 
        else if(instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if(player.PlayerLives == 0)
        {
            if (ScoreHandler.distance > ScoreHandler.currentPlayerTopDistance)
            {
                ScoreHandler.currentPlayerTopDistance = ScoreHandler.distance;
            }
            gameOverUI.SetActive(true);
            player.GameOver = true;
            player.gameObject.SetActive(false);
            player.PlayerLives = -1;
        }
    }
}
