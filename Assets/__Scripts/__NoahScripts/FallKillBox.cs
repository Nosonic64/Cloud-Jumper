using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallKillBox : MonoBehaviour
{
    #region private variables
    private AudioSource audioSource;
    #endregion

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if(GameManager.instance.player.PlayerLives > 0)
            {
                GameManager.instance.player.NormalRespawn();
            }

            if(GameManager.instance.player.PlayerLives <= 0)
            {
                audioSource.Play();
                GameManager.instance.scoreManager.PlayerDeathScoreChange();
            }
        }
    }
}
