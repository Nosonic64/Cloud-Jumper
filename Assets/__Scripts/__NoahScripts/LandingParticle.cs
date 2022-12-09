using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingParticle : MonoBehaviour
{
    private ParticleSystem landParticle;

    private void Start()
    {
        landParticle = GetComponent<ParticleSystem>();
        var main = landParticle.main;
        main.stopAction = ParticleSystemStopAction.Callback;
    }

    void OnParticleSystemStopped()
    {
        Destroy(gameObject);
    }
}
