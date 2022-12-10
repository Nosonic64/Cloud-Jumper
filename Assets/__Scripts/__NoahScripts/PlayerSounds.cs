using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    // This script holds an array of sounds that we play from the player.
    // These sounds get played from PlayerMovement under different circumstances.
    #region serialized variables
    [SerializeField] private AudioClip[] sounds = new AudioClip[0];
    #endregion

    #region setters and getters
    public AudioClip[] Sounds { get => sounds;}
    #endregion
}
