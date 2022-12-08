using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChunk : MonoBehaviour
{
    #region private variables
    private bool spawnedNewLevelChunk;
    private float bellScrollSpeedMultiple = 60f;
    private float resetScrollMultiple = 15f;
    private float createNewChunkThreshold = -46f; 
    private float deleteThisChunkThreshold = -92f;
    private float placeNewOffset = 92f;
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
        SpawnRandomPowerUp(3);
    }

    private void Update()
    {

        if (GameManager.instance.player.TouchingYClamp && GameManager.instance.player.PlayerLives > 0)
        {
           if (GameManager.instance.player.GetRigidbody.velocity.y > 0)
           {
                transform.position -= transform.up * GameManager.instance.player.GetRigidbody.velocity.y * Time.deltaTime;
                GameManager.instance.scoreManager.Distance += Time.deltaTime;
           }
        }
        else if (!GameManager.instance.player.TouchingYClamp)
        {
            transform.position -= (transform.up * GameManager.instance.levelChunkManager.CurrentDifficulty * GameManager.instance.levelChunkManager.PassiveScrollMultiple) * Time.deltaTime;
        }

        if(GameManager.instance.bellSprite.SpriteCarryingPlayer)
        {
            transform.position -= transform.up * bellScrollSpeedMultiple * Time.deltaTime;
            GameManager.instance.scoreManager.Distance += Time.deltaTime;
        }

        if(GameManager.instance.levelChunkManager.ResetTimerCounter > 0f)
        {
            transform.position -= transform.up * resetScrollMultiple * Time.deltaTime;
        }

        if (transform.position.y <= createNewChunkThreshold & !spawnedNewLevelChunk)
        {
            SpawnRandomLevelChunk();
        }

       if(transform.position.y <= deleteThisChunkThreshold)
        {
            Destroy(gameObject);
        }
    }

    private void SpawnRandomPowerUp(int times)
    {
        for (var i = 0; i < times; i++)
        {
            var randomNumber = Random.Range(0, 100);
            if (randomNumber <= GameManager.instance.powerUpManager.ChanceToSpawnPowerUp)
            {
            retrySpawn:
                var powerUpToSpawn = Random.Range(0, GameManager.instance.powerUpManager.PowerUps.Length);
                var powerUpXSpawn = Random.Range(2, 14);
                var powerUpYSpawn = Random.Range(46, 92);
                var powerUpSpawned = Instantiate(GameManager.instance.powerUpManager.PowerUps[powerUpToSpawn], transform.position + new Vector3(powerUpXSpawn, powerUpYSpawn, 0), transform.rotation);
                //This code below is meant to check everything around where the power up has spawned, and if it colliding with something (Hazard, platform) its meant to retry spawning it in. 
                //it doesnt seem to work though. 
                RaycastHit hit;
                if (Physics.SphereCast(powerUpSpawned.transform.position + new Vector3(0,1,0), 0.5f, Vector3.down, out hit, 2f))
                {
                    Destroy(powerUpSpawned);
                    goto retrySpawn;
                }
                powerUpSpawned.transform.parent = transform;
                GameManager.instance.powerUpManager.ChanceToSpawnPowerUp = GameManager.instance.powerUpManager.ChanceToSpawnPowerUpSet;
            }
            else
            {
                //For each time we dont spawn a powerup, we up the chance one can spawn next time.
                //Once we do spawn a powerup, we reset the chance to its base value
                GameManager.instance.powerUpManager.ChanceToSpawnPowerUp += GameManager.instance.powerUpManager.AddToChance;
            }
        }

    }

    // Spawn a random level chunk corresponding to current difficulty
    // E.g. We are on currentDifficulty 2, so we look into the l2 list of level chunks and randomly select one from that list to spawn
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
}
