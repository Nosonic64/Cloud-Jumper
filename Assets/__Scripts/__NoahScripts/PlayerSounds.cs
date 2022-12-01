using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [SerializeField] private AudioClip[] sounds = new AudioClip[0];

    public AudioClip[] Sounds { get => sounds;}
}
