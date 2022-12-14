using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardLightningCloud : MonoBehaviour
{
    // This script handles the Lightning Cloud hazard
    // We strike the lightning on a timer
    #region private variables
    private float lightningTimerCounter;
    private BoxCollider boxCollider;
    private ParticleSystem particle;
    private AudioSource audioSource;
    private float colliderEnableOffset = 0.1f;
    #endregion

    #region serialized variables
    [Header("Time between lightning strikes")]
    [SerializeField] private float lightningTimer;
    [Header("How long a lightning strike lasts")]
    [SerializeField] private float lightningAttackTime;
    [Header("Point of the timer the indicator fog begins")]
    [SerializeField] private float lightningIndicatorTime;
    [Header("Misc")]
    [SerializeField] private ParticleSystem beforeLightningParticle;
    #endregion

    void Start()
    { 
       boxCollider = GetComponent<BoxCollider>();
       particle = GetComponent<ParticleSystem>();
       audioSource = GetComponent<AudioSource>();
       // We have to subtract offsets for how long the lightning stays on screen 
       // and the time before we turn on the hitbox for the hazard to cycle correctly.
       lightningTimer = lightningTimer - lightningAttackTime + colliderEnableOffset;
       lightningTimerCounter = Random.Range(0, lightningTimer);
       var main = beforeLightningParticle.main;
       main.duration = lightningTimer - lightningIndicatorTime;
    }

    void Update()
    {
        if(lightningTimerCounter >= lightningIndicatorTime && !beforeLightningParticle.isPlaying)
        {
            beforeLightningParticle.Play();
            audioSource.Play();
        }

        //Causes lightning to strike on a timer
        if(lightningTimerCounter < (lightningTimer + lightningAttackTime + colliderEnableOffset))
        {
            lightningTimerCounter += Time.deltaTime;
        }
        else
        {
            particle.Play();
            Invoke("TurnOnCollisionBox", colliderEnableOffset); // The lightning strike visual is slower to appear than the hitbox turning on, so we delay turning on the hitbox
            Invoke("TurnOffCollisionBox", lightningAttackTime); // The delay of this invoke determines how long the lightning hitbox / particle stays on
            lightningTimerCounter = 0;
        }  
    }

    private void TurnOnCollisionBox()
    {
        boxCollider.enabled = true;
    }

    private void TurnOffCollisionBox()
    {
        particle.Stop();
        boxCollider.enabled = false;
    }

}
