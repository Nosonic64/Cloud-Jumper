using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttractMode : MonoBehaviour
{
    // This script controls the cycling image display at the start of the game
    #region serialized variables
    [SerializeField] private Sprite[] sprites = new Sprite[0];
    [SerializeField] private float timeBetweenImages;
    [SerializeField] private GameObject scoreTable;
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
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) // When a player puts in a coin (Subsituted by hitting I in this case) we stop the coroutine.
        {
            StopCoroutine(attractModeCoroutine);
            coroutineRunning = false;
            image.enabled = true;
            SetScreenTo(3);
        }

        if (Input.GetButtonDown("Jump") && !coroutineRunning) // If the player has put in a coin and then hits jump, we start the game.
        {
            GameStart();
            gameObject.SetActive(false);
        }

    }

    private IEnumerator AttractModeCycle() // This coroutine controls changing the images.
    {
        while (true)
        {
            image.enabled = true;
            image.sprite = sprites[0];
            yield return new WaitForSeconds(timeBetweenImages);
            image.sprite = sprites[1];
            yield return new WaitForSeconds(timeBetweenImages);
            image.sprite = sprites[2];
            yield return new WaitForSeconds(timeBetweenImages);
            image.enabled = false;
            yield return new WaitForSeconds(timeBetweenImages);
        }
    }

    private void SetScreenTo(int i) // This function sets the image to a specific one from the array.
    {
        image.sprite = sprites[i];
    }

    public void GameStart() // This function controls the thing we need to do when starting the game.
    {
        scoreTable.SetActive(false);
        GameManager.instance.levelChunkManager.ResetTimerCounter = resetScreenScrollAmount;
        GameManager.instance.player.Invoke("PlayerGameStart", resetScreenScrollAmount);
    }
}
