using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChunk : MonoBehaviour
{
    #region private variables
    private bool spawnedNewLevelChunk;
    private float bellScrollSpeedMultiple = 60f;
    private float createNewChunkThreshold = 0f; 
    private float deleteThisChunkThreshold = -22f;
    #endregion

    void Start()
    {
        SpawnRandomPowerUp();
    }

    private void Update()
    {
        if(GameManager.instance.player.TouchingYClamp && GameManager.instance.player.PlayerLives > 0)
        {
            transform.position -= transform.up * GameManager.instance.player.GetRigidbody.velocity.y * Time.deltaTime;
            ScoreHandler.distance += Time.deltaTime;
        }

        if(GameManager.instance.bellSprite.SpriteCarryingPlayer)
        {
            transform.position -= transform.up * bellScrollSpeedMultiple * Time.deltaTime;
            ScoreHandler.distance += Time.deltaTime;
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

    private void SpawnRandomPowerUp()
    {
        var randomNumber = Random.Range(0, 100);
        if (randomNumber <= GameManager.instance.powerUpManager.ChanceToSpawnPowerUp)
        {
        retrySpawn:
            var powerUpToSpawn = Random.Range(0, GameManager.instance.powerUpManager.PowerUps.Length);
            var powerUpXSpawn = Random.Range(2, 14);
            var powerUpYSpawn = Random.Range(-6, 6);
            var powerUpSpawned = Instantiate(GameManager.instance.powerUpManager.PowerUps[powerUpToSpawn], transform.position + new Vector3(powerUpXSpawn, powerUpYSpawn, 0), transform.rotation);
            //This code below is meant to check everything around where the power up has spawned, and if it colliding with something (Hazard, platform) its meant to retry spawning it in. 
            //it doesnt seem to work though. 
            RaycastHit hit;
            if (Physics.SphereCast(powerUpSpawned.transform.position, 6f, powerUpSpawned.transform.position, out hit))
            {
                Destroy(powerUpSpawned);
                goto retrySpawn;
            }
            powerUpSpawned.transform.parent = transform;
            GameManager.instance.powerUpManager.ChanceToSpawnPowerUp = GameManager.instance.powerUpManager.ChanceToSpawnPowerUpSet;
        }
        else
        {
            GameManager.instance.powerUpManager.ChanceToSpawnPowerUp += GameManager.instance.powerUpManager.AddToChance;
        }

    }

    private void SpawnRandomLevelChunk()
    {
        var levelChunkToSpawnFromArray = Random.Range(0, GameManager.instance.levelChunkManager.LevelChunks.Length);
        Instantiate(GameManager.instance.levelChunkManager.LevelChunks[levelChunkToSpawnFromArray], transform.position + new Vector3(0, 22, 0), transform.rotation);
        spawnedNewLevelChunk = true;
    }
}
