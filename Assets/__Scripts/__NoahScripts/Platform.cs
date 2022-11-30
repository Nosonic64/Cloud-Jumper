using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    #region private variables
    private BoxCollider platCollider;
    private bool playerTouched;
    #endregion

    #region serialized variables
    [SerializeField] private float disableTimer;
    [SerializeField] private float disableTimerSet;
    #endregion

    private void Start()
    {
        platCollider = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        if(GameManager.instance.player.transform.position.y - 1f > transform.position.y)
        {
            platCollider.enabled = true;
        }
        else if(GameManager.instance.player.transform.position.y < transform.position.y)
        {
            platCollider.enabled = false;
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
