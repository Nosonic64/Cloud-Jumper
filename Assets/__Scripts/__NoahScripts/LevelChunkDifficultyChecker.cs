using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChunkDifficultyChecker : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if ((int)Mathf.Floor(GameManager.instance.scoreManager.Distance) > GameManager.instance.levelChunkManager.DifficultyThresholds[GameManager.instance.levelChunkManager.CurrentDifficulty])
            {
                GameManager.instance.levelChunkManager.CurrentDifficulty++;
            }
            Destroy(gameObject);
        }
    }
}
