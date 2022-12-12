using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChunkDifficultyChecker : MonoBehaviour
{
    // This script checks the players score compared to the threshold to change the difficulty.
    // If the players score is over or equal to the difficulty threshold, we go to the next difficulty.
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if ((int)Mathf.Floor(GameManager.instance.scoreManager.Distance) >= GameManager.instance.levelChunkManager.DifficultyThresholds[GameManager.instance.levelChunkManager.CurrentDifficulty])
            {
                GameManager.instance.levelChunkManager.CurrentDifficulty++;
            }
            Destroy(gameObject);
        }
    }
}
