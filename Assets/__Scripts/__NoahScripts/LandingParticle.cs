using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingParticle : MonoBehaviour
{
    private ParticleSystem landParticle;

    private void Start()
    {
        landParticle = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
       if (!landParticle.isEmitting)
       {
           transform.SetParent(null);
        }
    }
}
