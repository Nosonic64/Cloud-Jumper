using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallKillBox : MonoBehaviour
{
    // This script handles the player falling off the bottom of the screen
    #region private variables
    private AudioSource audioSource;
    private float respawnTimerWait = 3f; // How long it takes to respawn the player
    #endregion

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        // Theres a chance the player hits the kill box and a bell powerup at the same time and it causes shenanigans.
        // We check to make sure the player does not have the bell powerup before executing this code.
        if (other.gameObject.CompareTag("Player") && !GameManager.instance.player.HasBell) 
        {
            if(GameManager.instance.player.PlayerLives > 1) // If the player falls off the bottom of the screen but still has a life, we respawn them 
            {
                audioSource.Play();
                GameManager.instance.levelChunkManager.ResetTimerCounter = respawnTimerWait; // We move the screen up as we want to make sure the player can reach new clouds once they respawn
                GameManager.instance.player.Invoke("NormalRespawn", respawnTimerWait);
            }

            if(GameManager.instance.player.PlayerLives <= 1) // If the player falls off the bottom of the screen and it takes their last life, we setup the game over screen
            {
                audioSource.Play();
                GameManager.instance.levelChunkManager.ResetTimerCounter = GameManager.instance.levelChunkManager.ResetTimer;
                GameManager.instance.player.GameOverSetup();
            }
        }
    }
}
