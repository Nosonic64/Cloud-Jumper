using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    #region private variables
    private BoxCollider platCollider;
    private bool playerTouched;
    private AudioSource audioSource;
    #endregion

    #region serialized variables
    [SerializeField] private float disableTimer;
    [SerializeField] private float disableTimerSet;
    #endregion

    private void Start()
    {
        platCollider = GetComponent<BoxCollider>();
        audioSource = GetComponent<AudioSource>();
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

        if(transform.position.y < -2f)
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
        if(collision.gameObject.tag == "Player")
        {
            audioSource.Play();
            if (disableTimer <= 0)
            {
                disableTimer = disableTimerSet;
                playerTouched = true;
            }
        }
    }

}
