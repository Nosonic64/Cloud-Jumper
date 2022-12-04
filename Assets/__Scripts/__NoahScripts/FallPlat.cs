using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallPlat : MonoBehaviour
{
    #region private variables
    private BoxCollider platCollider;
    private LandingParticle landingParticle;
    private bool playerTouched;
    #endregion

    #region serialized variables
    [SerializeField] private float disableTimer;
    [SerializeField] private float disableTimerSet;
    #endregion

    void Start()
    {
        disableTimer = disableTimerSet;
        landingParticle = FindObjectOfType<LandingParticle>();
    }


    void Update()
    {
        if (disableTimer > 0f)
        {
            disableTimer -= Time.deltaTime;
        }

        if(disableTimer <= 0f || GameManager.instance.player.transform.position.y > transform.position.y + 3f && playerTouched || GameManager.instance.player.Grounded && !playerTouched && disableTimer <= 5f)
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
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !playerTouched)
        {
            playerTouched = true;
        }
    }
}
