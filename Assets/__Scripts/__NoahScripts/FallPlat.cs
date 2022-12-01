using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallPlat : MonoBehaviour
{
    #region private variables
    private bool playerTouched;
    private BoxCollider platCollider;
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

        if (playerTouched && !GameManager.instance.player.GroundCheck() && disableTimer <= 5f || disableTimer <= 0f || Vector3.Distance(transform.position, GameManager.instance.player.transform.position) > 4f)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            playerTouched = true;
        }
    }
}
