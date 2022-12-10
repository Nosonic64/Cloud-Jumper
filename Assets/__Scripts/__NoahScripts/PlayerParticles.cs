using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticles : MonoBehaviour
{
    // This script holds an array of particles.
    // We play these particles from PlayerMovement under different circumstances.
    #region serialized variables
    [SerializeField] private ParticleSystem[] particleObjects = new ParticleSystem[0];
    #endregion

    #region getters and setters
    public ParticleSystem[] ParticleObjects { get => particleObjects;}
    #endregion

}
