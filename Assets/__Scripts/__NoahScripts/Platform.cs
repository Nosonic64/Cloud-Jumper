using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    #region private variables
    private BoxCollider platCollider;
    private AudioSource audioSource;
    private LandingParticle landingParticle;
    private bool playerTouched;
    private float disableTimer;
    private float deleteThreshold = -4f;
    #endregion

    #region serialized variables
    [SerializeField] private float disableTimerSet;
    #endregion

    private void Start()
    {
        platCollider = GetComponent<BoxCollider>();
        audioSource = GetComponent<AudioSource>();
        landingParticle = FindObjectOfType<LandingParticle>();
    }

    private void Update()
    {
        if(GameManager.instance.player.transform.position.y - 1.8f > transform.position.y)
        {
            platCollider.enabled = true;
        }
        else if(GameManager.instance.player.transform.position.y < transform.position.y)
        {
            platCollider.enabled = false;
        }

        if(transform.position.y < deleteThreshold)
        {
            if (landingParticle != null)
            {
                if (landingParticle.transform.parent != null)
                {
                    landingParticle.transform.parent = null;
                }
            }
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
        if(collision.gameObject.CompareTag("Player"))
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
