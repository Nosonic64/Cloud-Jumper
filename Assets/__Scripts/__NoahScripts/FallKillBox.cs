using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallKillBox : MonoBehaviour
{
    #region private variables
    private AudioSource audioSource;
    private float respawnTimerWait = 3f;
    #endregion

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(GameManager.instance.player.PlayerLives > 0)
            {
                GameManager.instance.levelChunkManager.ResetTimerCounter = respawnTimerWait;
                GameManager.instance.player.Invoke("NormalRespawn", respawnTimerWait);
            }

            if(GameManager.instance.player.PlayerLives <= 0)
            {
                audioSource.Play();
                GameManager.instance.levelChunkManager.ResetTimerCounter = GameManager.instance.levelChunkManager.ResetTimer;
                GameManager.instance.player.GameOverSetup();
            }
        }
    }
}
