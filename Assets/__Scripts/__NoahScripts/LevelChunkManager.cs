using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChunkManager : MonoBehaviour
{
    // This script holds variables that all level chunks need to function.
    // It also contains a list of level chunks that it sorts into different difficultys
    // on start.
    #region private variables
    private float resetTimer = 10f;
    private float resetTimerCounter;
    private float passiveScrollMultiple = 0f;
    private int currentDifficulty = 0;
    private Dictionary<int, List<GameObject>> levelChunkDictionary = new Dictionary<int, List<GameObject>>();
    #endregion

    #region serialized variables
    [Header("Place all level chunks in here")]
    [SerializeField] List<GameObject> levelChunks;
    [Header("Level Chunk Stats")]
    [SerializeField] private int[] difficultyThresholds;
    [Header("Done in percentages, dont go over 100 or under 0")]
    [SerializeField] private int[] powerUpSpawnChancePerDifficulty = new int[0];
    [Header("The amount of times we try and spawn a powerup")]
    [SerializeField] private int[] powerUpAmountToTryAndSpawnPerDifficulty = new int[0];
    [Header("Misc")]
    [SerializeField] private GameObject backgroundArt;
    [Header("Debug")]
    [SerializeField] private bool dontBreakPlats; //Turn this on in the inspector to stop platforms from breaking at all
    #endregion

    #region getters and setters
    public float ResetTimer { get => resetTimer;}
    public float ResetTimerCounter { get => resetTimerCounter; set => resetTimerCounter = value; }
    public bool DontBreakPlats { get => dontBreakPlats;}
    public int CurrentDifficulty { get => currentDifficulty; set => currentDifficulty = value; }
    public int[] DifficultyThresholds { get => difficultyThresholds;}
    public Dictionary<int, List<GameObject>> LevelChunkDictionary { get => levelChunkDictionary;}
    public float PassiveScrollMultiple { get => passiveScrollMultiple; set => passiveScrollMultiple = value; }
    public int[] PowerUpSpawnChancePerDifficulty { get => powerUpSpawnChancePerDifficulty;}
    public int[] PowerUpAmountToTryAndSpawnPerDifficulty { get => powerUpAmountToTryAndSpawnPerDifficulty;}
    public GameObject BackgroundArt { get => backgroundArt;}
    #endregion

    private void Start()
    {
        SortLevelChunks();
    }

    private void Update()
    {
        if(resetTimerCounter > 0) //resetTimerCounter being over 0 means we are scrolling the screen down due to player death or game over.
        {
            resetTimerCounter -= Time.deltaTime;    
        }

        if(passiveScrollMultiple < 0.90f && !GameManager.instance.player.BeforeStart) //This code scrolls the screen down passively, outside of the player hitting the top of the screen. 
        {
            passiveScrollMultiple += Time.deltaTime / 120f;
        }
    }

    private void SortLevelChunks() 
    {
        //Sorts GameObjects from levelChunks into appropriate lists based on difficultys (an int) they have on them.
        //E.g. We go through levelChunks and find 5 GameObjects with difficulty 2, we put them into l2
        List<GameObject> l0 = new List<GameObject>();
        List<GameObject> l1 = new List<GameObject>();
        List<GameObject> l2 = new List<GameObject>();
        List<GameObject> l3 = new List<GameObject>();
        List<GameObject> l4 = new List<GameObject>();
        // TODO: If more difficultys are added, we need to add more difficultys to this list
        foreach (GameObject go in levelChunks)
        {
            var difficulty = go.GetComponent<LevelChunk>().ChunkDifficulty;
            switch (difficulty)
            {
                case 0:
                    l0.Add(go);
                    break;

                case 1:
                    l1.Add(go);
                    break;

                case 2:
                    l2.Add(go);
                    break;

                case 3:
                    l3.Add(go);
                    break;

                case 4:
                    l4.Add(go);
                    break;

            }
        }

        levelChunkDictionary.Add(0,l0);
        levelChunkDictionary.Add(1,l1);
        levelChunkDictionary.Add(2,l2);
        levelChunkDictionary.Add(3,l3);
        levelChunkDictionary.Add(4,l4);
    }
}
