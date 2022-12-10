using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallPlat : MonoBehaviour
{
    // This script controls the platform that appears when players respawn
    #region private variables
    private BoxCollider platCollider;
    private bool playerTouched;
    #endregion

    #region serialized variables
    [SerializeField] private float disableTimer;
    [SerializeField] private float disableTimerSet;
    #endregion

    void Start()
    {
        disableTimer = disableTimerSet;
    }


    void Update()
    {
        if (disableTimer > 0f)
        {
            disableTimer -= Time.deltaTime;
        }

        // We destroy the platform if the timers up, the player has touched 
        // the platform and then jumped from it, or if the player has fallen onto
        // another platform.
        if(disableTimer <= 0f || GameManager.instance.player.transform.position.y > transform.position.y + 3f && playerTouched || GameManager.instance.player.Grounded && !playerTouched && disableTimer <= 5f)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !playerTouched)
        {
            playerTouched = true;
        }
    }
}
