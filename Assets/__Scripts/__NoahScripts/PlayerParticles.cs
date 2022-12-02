using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticles : MonoBehaviour
{
    #region serialized variables
    [SerializeField] private ParticleSystem[] particleObjects = new ParticleSystem[0];
    #endregion

    #region getters and setters
    public ParticleSystem[] ParticleObjects { get => particleObjects;}
    #endregion

}
