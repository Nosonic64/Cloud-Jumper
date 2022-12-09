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
        if (other.gameObject.CompareTag("Player") && !GameManager.instance.player.HasBell)
        {
            if(GameManager.instance.player.PlayerLives > 1) //If the player falls off the bottom of the screen but still has a life, we respawn them 
            {
                audioSource.Play();
                GameManager.instance.levelChunkManager.ResetTimerCounter = respawnTimerWait;
                GameManager.instance.player.Invoke("NormalRespawn", respawnTimerWait);
            }

            if(GameManager.instance.player.PlayerLives <= 1) //If the player falls off the bottom of the screen and it takes their last life, we setup the game over screen
            {
                audioSource.Play();
                GameManager.instance.levelChunkManager.ResetTimerCounter = GameManager.instance.levelChunkManager.ResetTimer;
                GameManager.instance.player.GameOverSetup();
            }
        }
    }
}
