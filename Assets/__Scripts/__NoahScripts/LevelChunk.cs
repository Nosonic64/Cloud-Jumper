using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class LevelChunk : MonoBehaviour
{
    // This script is placed on and controls individual level chunk objects.
    #region private variables
    private bool spawnedNewLevelChunk;
    private float bellScrollSpeedMultiple = 60f;
    private float resetScrollMultiple = 90f;
    private float createNewChunkThreshold = -46f; 
    private float deleteThisChunkThreshold = -92f;
    private float placeNewOffset = 92f;
    private List<GameObject> bgArts = new List<GameObject>();
    #endregion

    #region serialized variables
    [Header("0 is Easiest, 4 is Hardest")]
    [Range(0,4)]
    [SerializeField] private int chunkDifficulty;
    #endregion

    #region getters and setters
    public int ChunkDifficulty { get => chunkDifficulty; }
    #endregion

    void Start()
    {
        // When the level chunk is created, we try to spawn powerups into it
        // The amount we try to spawn is governed by an array in LevelChunkManager, and we select what number to use from
        // That array by the difficulty set for this level chunk.
        SpawnRandomPowerUp(GameManager.instance.levelChunkManager.PowerUpAmountToTryAndSpawnPerDifficulty[chunkDifficulty]);
        SpawnRandomBGArt(Random.Range(6, 10));
    }

    private void Update()
    {
        // Situations that cause the level chunk to scroll down the screen
        #region scrollers

        // Scroll the level chunk down if the player is hitting the YClamp (Ceiling Clamp we set for player Y movement)
        if (GameManager.instance.player.TouchingYClamp && GameManager.instance.player.GetRigidbody.velocity.y > 0)
        {
                transform.position -= transform.up * GameManager.instance.player.GetRigidbody.velocity.y * Time.deltaTime;
                GameManager.instance.scoreManager.Distance += Time.deltaTime;
        }
        else if (!GameManager.instance.player.TouchingYClamp) // Scroll the level chunk down by a passive amount, called up if nothing else is happening to scroll the level chunk down
        {
            transform.position -= (transform.up * (GameManager.instance.levelChunkManager.CurrentDifficulty + 1) * GameManager.instance.levelChunkManager.PassiveScrollMultiple) * Time.deltaTime;
            if(!GameManager.instance.player.GameOver)
            {
                GameManager.instance.scoreManager.Distance += GameManager.instance.levelChunkManager.PassiveScrollMultiple * (Time.deltaTime / 2);
            }
        }

        // Scroll the level chunk down if the player has the bell powerup and is being carried by Kit
        if(GameManager.instance.bellSprite.SpriteCarryingPlayer)
        {
            transform.position -= transform.up * bellScrollSpeedMultiple * Time.deltaTime;
            GameManager.instance.scoreManager.Distance += (Time.deltaTime * 2.2f);
        }

        // Scroll the level chunk down if reset time is over 0 (Usually when we are about to respawn the player or resetting the play space)
        if(GameManager.instance.levelChunkManager.ResetTimerCounter > 0f)
        {
            transform.position -= transform.up * resetScrollMultiple * Time.deltaTime;
        }
        #endregion

        if (transform.position.y <= createNewChunkThreshold & !spawnedNewLevelChunk) // If we are far enough down the screen, spawn a new level chunk above this one
        {
            SpawnRandomLevelChunk();
        }

       if(transform.position.y <= deleteThisChunkThreshold)
        {
            Destroy(gameObject);
        }
    }

    private void SpawnRandomPowerUp(int times) //Trys to spawn a powerups within the level chunk a certain amount of times
    {
        for (var i = 0; i < times; i++)
        {
            var randomNumber = Random.Range(0, 100);
            if (randomNumber <= GameManager.instance.levelChunkManager.PowerUpSpawnChancePerDifficulty[chunkDifficulty])
            {
            retrySpawn:
                var powerUpToSpawn = Random.Range(0, GameManager.instance.powerUpManager.PowerUps.Length);
                var powerUpXSpawn = Random.Range(2, 14);
                var powerUpYSpawn = Random.Range(0, 92);
                var powerUpSpawned = Instantiate(GameManager.instance.powerUpManager.PowerUps[powerUpToSpawn], transform.position + new Vector3(powerUpXSpawn, powerUpYSpawn, 0), transform.rotation);
                // This code below is meant to check everything around where the power up has spawned, and if it colliding with something (Hazard, platform) its meant to retry spawning it in. 
                // it doesnt seem to work though. 
                // TODO: make powerup spawning spherecast work.
                RaycastHit hit;
                if (Physics.SphereCast(powerUpSpawned.transform.position + new Vector3(0, 1, 0), 0.5f, Vector3.down, out hit, 2f))
                {
                    Destroy(powerUpSpawned);
                    goto retrySpawn;
                }
                powerUpSpawned.transform.parent = transform;
            }
        }
    }

    // Spawn a random level chunk corresponding to current difficulty
    // E.g. We are on currentDifficulty 2, so we look into the l2 list of level chunks and randomly select one from that list to spawn
    // Level chunk lists are in the level chunk manager.
    private void SpawnRandomLevelChunk() 
    {
        var currentDifficulty = GameManager.instance.levelChunkManager.CurrentDifficulty;
        List<GameObject> currentDifficultyList;
        if (GameManager.instance.levelChunkManager.LevelChunkDictionary.TryGetValue(currentDifficulty, out currentDifficultyList))
        {
            var levelChunkToSpawnFromArray = Random.Range(0, currentDifficultyList.Count);
            Instantiate(currentDifficultyList[levelChunkToSpawnFromArray], transform.position + new Vector3(0, placeNewOffset, 0), transform.rotation);
            spawnedNewLevelChunk = true;
        }
        else
        {
            Debug.Log("No list could be found at this difficulty level, Check Level Chunk Manager");
        }
    }

    private void SpawnRandomBGArt(int amount)
    {
        for(var i = 0; i < amount; i++)
        {
            var artXSpawn = Random.Range(4, 12);
            var artYSpawn = Random.Range(0, 92);
            var bgArt = Instantiate(GameManager.instance.levelChunkManager.BackgroundArt, transform.position + new Vector3(artXSpawn,artYSpawn, 5), GameManager.instance.levelChunkManager.BackgroundArt.transform.rotation);
            if (bgArts != null)
            {
                foreach (GameObject go in bgArts)
                {
                    if (Vector3.Distance(bgArt.transform.position, go.transform.position) < 5f)
                    {
                        Destroy(bgArt);
                        break;
                    }
                }

                if (bgArt != null)
                {
                    bgArts.Add(bgArt);
                    bgArt.transform.parent = transform;
                }
            }
        }
    }
}
