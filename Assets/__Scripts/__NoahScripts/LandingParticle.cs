using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingParticle : MonoBehaviour
{
    // This script handles the particles that emit when players land on a cloud.
    #region private variables
    private ParticleSystem landParticle;
    #endregion 

    private void Start()
    {
        // In order to notify Unitys inbuilt "OnParticleSystemStopped" function
        // that the particle system has actually stopped, we must setup the callback.
        landParticle = GetComponent<ParticleSystem>();
        var main = landParticle.main;
        main.stopAction = ParticleSystemStopAction.Callback;
    }

    void OnParticleSystemStopped() // Once we are finished playing, we destroy the object the object with this script on it.
    {
        Destroy(gameObject);
    }
}
