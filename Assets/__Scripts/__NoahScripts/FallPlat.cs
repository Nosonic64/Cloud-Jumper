using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallPlat : MonoBehaviour
{
    #region private variables
    private BoxCollider platCollider;
    private LandingParticle landingParticle;
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

        if(disableTimer <= 0f || Vector3.Distance(transform.position, GameManager.instance.player.transform.position) > 5f)
        {
            landingParticle.transform.parent = null;
            Destroy(gameObject);
        }
    }
}
