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
    public ScoreManager scoreManager;

    public ScoreData scoreData;

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
}
