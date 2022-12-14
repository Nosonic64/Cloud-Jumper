using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttractMode : MonoBehaviour
{
    // This script controls the cycling image display at the start of the game
    #region serialized variables
    [SerializeField] private float timeBetweenImages;
    [SerializeField] private Sprite[] sprites = new Sprite[0];
    [SerializeField] private AudioClip[] clips = new AudioClip[0];
    [SerializeField] private GameObject scoreTable;
    [SerializeField] private GameObject insertCoin;
    [SerializeField] private GameObject highestScoreDisplay;
    [SerializeField] private GameObject scoreDisplay;
    #endregion

    #region private variables
    private Image image;
    private IEnumerator attractModeCoroutine;
    private bool coroutineRunning;
    private float resetScreenScrollAmount = 2f;
    #endregion

    void Awake()
    {
        image = GetComponent<Image>();
    }

    private void OnEnable()
    {
        // When this script is enabled, we start a coroutine.
        // We must hold this coroutine in a variable in
        // order to stop it later.
        attractModeCoroutine = AttractModeCycle();
        StartCoroutine(attractModeCoroutine);
        coroutineRunning = true;
        var music = GameManager.instance.musicManager.GetComponent<AudioSource>();
        music.clip = clips[0];
        music.Play();
        SetInsertCoin(5, 1);
        highestScoreDisplay.SetActive(false);
        scoreDisplay.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) // When a player puts in a coin (Subsituted by hitting I in this case) we stop the coroutine.
        {
            StopCoroutine(attractModeCoroutine);
            coroutineRunning = false;
            image.enabled = true;
            SetScreenTo(4);
            SetInsertCoin(6, 2);
        }

        if (Input.GetButtonDown("Jump") && !coroutineRunning) // If the player has put in a coin and then hits jump, we start the game.
        {
            var music = GameManager.instance.musicManager.GetComponent<AudioSource>();
            music.clip = clips[1];
            music.Play();
            GameStart();
            highestScoreDisplay.SetActive(true);
            scoreDisplay.SetActive(true);
            gameObject.SetActive(false);
        }

    }

    private IEnumerator AttractModeCycle() // This coroutine controls changing the images.
    {
        while (true)
        {
            image.enabled = false;
            yield return new WaitForSeconds(timeBetweenImages);
            image.sprite = sprites[0];
            image.enabled = true;
            yield return new WaitForSeconds(timeBetweenImages);
            image.sprite = sprites[1];
            yield return new WaitForSeconds(timeBetweenImages);
            image.sprite = sprites[2];
            yield return new WaitForSeconds(timeBetweenImages);
            image.sprite = sprites[3];
            yield return new WaitForSeconds(timeBetweenImages);
        }
    }

    private void SetScreenTo(int i) // This function sets the image to a specific one from the array.
    {
        image.sprite = sprites[i];
    }

    private void SetInsertCoin(int sprite, int animSpeed)
    {
        var iImage = insertCoin.GetComponent<Image>();
        iImage.sprite = sprites[sprite];
        var iAnim = insertCoin.GetComponent<Animator>();
        iAnim.speed = animSpeed;

    }

    public void GameStart() // This function controls the things we need to do when starting the game.
    {
        scoreTable.SetActive(false);
        GameManager.instance.levelChunkManager.ResetTimerCounter = resetScreenScrollAmount;
        GameManager.instance.player.Invoke("PlayerGameStart", resetScreenScrollAmount);
    }
}
