using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomizeEntryBG : MonoBehaviour
{
    // This script randomizes the Koi image used in the background of the High-Score display scores.
    #region serialized fields
    [SerializeField] private Sprite[] sprites = new Sprite[0];
    #endregion

    #region
    private Image image;
    private Animator anim;
    private float timeElapsed;
    private float lerpDuration;
    #endregion

    private void Awake()
    {
        image = GetComponent<Image>();
        anim = GetComponent<Animator>();
        image.sprite = sprites[Random.Range(0,sprites.Length)]; // Chooses a random image to display from the array.
        anim.Play("Ui_KoiBG_Flail", -1, Random.Range(0.0f, 1.0f));
    }
}
