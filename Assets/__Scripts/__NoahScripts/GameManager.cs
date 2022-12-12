using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // This script holds references to alot of other scripts.
    // They are usually scripts containing variables we want to reference in multiple places.
    // This script is categorized as a "Singleton"
    // https://gamedevbeginner.com/singletons-in-unity-the-right-way/#:~:text=Generally%20speaking%2C%20a%20singleton%20in,or%20to%20other%20game%20systems.

    static public GameManager instance;
    public PlayerMovement player;
    public GameObject gameOverUI;
    public SpriteSequence bellSprite;
    public LevelChunkManager levelChunkManager;
    public PowerUpManager powerUpManager;
    public ScoreManager scoreManager;
    public ScoreData scoreData;
    public SplashScreen splashScreen;

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
