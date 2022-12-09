using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardLightningCloud : MonoBehaviour
{
    #region private variables
    private float lightningTimerCounter;
    private BoxCollider boxCollider;
    private ParticleSystem particle;
    private AudioSource audioSource;
    private float particleStartPlayOffset = 0.1f;
    #endregion

    #region serialized variables
    [Header("Time between lightning strikes")]
    [SerializeField] private float lightningTimer;
    [Header("How long a lightning strike lasts")]
    [SerializeField] private float lightningAttackTime;
    #endregion

    void Start()
    { 
       boxCollider = GetComponent<BoxCollider>();
       particle = GetComponent<ParticleSystem>();
       audioSource = GetComponent<AudioSource>();
       lightningTimer = lightningTimer - lightningAttackTime + particleStartPlayOffset;
    }

    void Update()
    {
        //Causes lightning to strike on a timer
        if(lightningTimerCounter < (lightningTimer + lightningAttackTime + particleStartPlayOffset))
        {
            lightningTimerCounter += Time.deltaTime;
        }
        else
        {
            particle.Play();
            audioSource.Play();
            Invoke("TurnOnCollisionBox", particleStartPlayOffset);
            Invoke("StopLightning", lightningAttackTime); //This invoke with a delay of lightningAttackTime determines how long the lightning hitbox / particle stays on
            lightningTimerCounter = 0;
        }  
    }

    private void TurnOnCollisionBox()
    {
        boxCollider.enabled = true;
    }

    private void StopLightning()
    {
        particle.Stop();
        boxCollider.enabled = false;
    }

}
