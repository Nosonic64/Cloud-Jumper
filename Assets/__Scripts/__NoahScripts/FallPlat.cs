using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallPlat : MonoBehaviour
{
    public float disableTimer;
    public float disableTimerSet;
    public bool playerTouched;
    private BoxCollider collider;
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

        if (playerTouched && !PlayerInfo.playerGrounded && disableTimer <= 5f || disableTimer <= 0f)
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
