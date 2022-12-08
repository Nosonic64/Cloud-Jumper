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
    }

    void Update()
    {
        //Causes lightning to strike on a timer
        if(lightningTimerCounter < lightningTimer)
        {
            lightningTimerCounter += Time.deltaTime;
        }
        else
        {
            boxCollider.enabled = true;  
            particle.Play();
            audioSource.Play();
            Invoke("StopLightning", lightningAttackTime); //This invoke with a delay of lightningAttackTime determines how long the lightning hitbox / particle stays on
            lightningTimerCounter = 0;
        }  
    }

    private void StopLightning()
    {
        particle.Stop();
        particle.Clear();
        boxCollider.enabled = false;
    }
}
