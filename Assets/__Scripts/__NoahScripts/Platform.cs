using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private BoxCollider collider;
    public float disableTimer;
    public float disableTimerSet;
    public bool playerTouched;

    private void Start()
    {
        collider = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        if(PlayerInfo.playerY - 1f > transform.position.y)
        {
            collider.enabled = true;
        }
        else if(PlayerInfo.playerY < transform.position.y)
        {
            collider.enabled = false;
        }

        if(transform.position.y < -1f)
        {
            Destroy(gameObject);
        }
        else if(disableTimer <= 0f && playerTouched)
        {
            gameObject.SetActive(false);
        }

        if(disableTimer > 0f)
        {
            disableTimer -= Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player" & disableTimer <= 0)
        {
            disableTimer = disableTimerSet;
            playerTouched = true;
        }
    }

}
