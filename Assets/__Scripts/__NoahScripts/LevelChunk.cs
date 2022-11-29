using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChunk : MonoBehaviour
{
    public GameObject topHelper;
    private float scrollMultiple;
    private bool spawnedNewLevelChunk;

    void Start()
    {
        scrollMultiple = 1f;
        var randomNumber = Random.Range(0, 100);
        if (randomNumber <= PowerUpHandler.chanceToSpawnPowerUp)
        {
            retrySpawn: 
            var i = Random.Range(0, PowerUpHandler.powerUps.Length);
            var x = Random.Range(-1, 14);
            var y = Random.Range(-10, 10);
            var powerUpSpawned = Instantiate(PowerUpHandler.powerUps[i], transform.position + new Vector3(x, y, 4), transform.rotation);
            RaycastHit hit;
            if(Physics.SphereCast(powerUpSpawned.transform.position, 6f, powerUpSpawned.transform.position, out hit))
            {
                Destroy(powerUpSpawned);
                goto retrySpawn;
            }
            powerUpSpawned.transform.parent = transform;
            PowerUpHandler.chanceToSpawnPowerUp = PowerUpHandler.initialChanceToSpawnPowerUp;
        }
        else
        {
            PowerUpHandler.chanceToSpawnPowerUp += 15;
        }
    }

    private void Update()
    {
        if(PlayerInfo.touchingCeiling)
        {
            transform.position -= transform.up * (PlayerInfo.playerYVelocity * scrollMultiple) * Time.deltaTime;
        }

        if(PlayerInfo.spriteCarry)
        {
            transform.position -= transform.up * 60f * Time.deltaTime;
            ScoreHandler.distance += Time.deltaTime;
        }

        if (transform.position.y <= 14f & !spawnedNewLevelChunk)
       {
            var i = Random.Range(0,LevelChunkHandler.levelChunks.Length);
            Instantiate(LevelChunkHandler.levelChunks[i], transform.position + new Vector3(0,22,0), transform.rotation);
            spawnedNewLevelChunk = true;
       }

       if(topHelper.transform.position.y < 0f)
        {
            Destroy(gameObject);
        }

      // if (PlayerInfo.gameOver)
      //  {
      //      transform.position += transform.up * 5f * Time.deltaTime;
      //  }
    }
}
