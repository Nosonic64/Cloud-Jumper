using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NameInput : MonoBehaviour
{
    // This script controls the Name Input screen after the Game Over screen.
    #region private variables
    private string letters = " ABCDEFGHIJKLMNOPQRSTUVWXYZ ";
    private int selectedLetter;
    private Switcher thingsToSwitch;
    private AudioSource audioSource;
    private float inputDelay = 0.12f;
    private float inputDelayCounter;
    private bool startedReset;
    #endregion

    #region serialized variables
    [SerializeField] private ScoreUi scoreUi;
    [SerializeField] private Text[] texts = new Text[0];
    #endregion

    void Start()
    {
        thingsToSwitch = GetComponent<Switcher>();
        audioSource = GetComponent<AudioSource>();
        selectedLetter = 1;
        inputDelayCounter = 0;
        // If the players score is lower than the lowest score on the High-Score table, 
        // we go back to attract mode instantly, skipping name input.
        if (GameManager.instance.scoreManager.CurrentPlayerTopDistance < GameManager.instance.scoreData.scores[9].score)
        {
            ResetStuff();
        }
        UpdateLetterDisplay();
    }

    void Update()
    {
        if(inputDelayCounter > 0f) // We add a small delay between each left and right input
        {
            inputDelayCounter -= Time.deltaTime;
        }

        if (Input.GetAxisRaw("Horizontal") < 0 && selectedLetter != 1 && inputDelayCounter <= 0f)
        {
            selectedLetter -= 1;
            inputDelayCounter = inputDelay;
            audioSource.pitch = 1f;
            audioSource.Play();
            UpdateLetterDisplay();
        }

        if (Input.GetAxisRaw("Horizontal") > 0 && selectedLetter != letters.Length - 2 && inputDelayCounter <= 0f)
        {
            selectedLetter += 1;
            inputDelayCounter = inputDelay;
            audioSource.pitch = 1f;
            audioSource.Play();
            UpdateLetterDisplay();
        }

        if (Input.GetButtonDown("Jump") && inputDelayCounter <= 0f) // We use the jump button as the letter select button.
        {
            // When the player selects a letter, we add it to texts[3] (The name display on screen)
            texts[3].text += letters[selectedLetter];
            audioSource.pitch = 2f;
            audioSource.Play();
        }

        //TODO: you could probably put this if statement into where we get the input
        if (texts[3].text.ToCharArray().Length == 3 && !startedReset)
        {
            inputDelayCounter = 5f;
            startedReset = true;
            // Once the name display hits 3 letters, we save the inputted name and player score to scoreData.
            // We do all the things needed to update the score display and save it to highscores.txt.
            Invoke("ResetStuff", 1f);
        }
    }

    private void UpdateLetterDisplay()
    {
        // The letter roll is seperated into 3 different Unity text ui objects.
        // The letter displayed in each is taken from a string that contains the alphabet.
        // selectedLetter determines what letter from the alphabet string we currently have selected.
        // The letter displayed to the left will always be the selected letter - 1
        // The letter displayed to the right will always be the selected letter + 1
        texts[0].text = letters[selectedLetter - 1].ToString();
        texts[1].text = letters[selectedLetter].ToString();
        texts[2].text = letters[selectedLetter + 1].ToString();
    }

    private void ResetStuff() // When we finish name input, we reset a bunch of stuff and go back to Attract Mode.
    {
        GameManager.instance.scoreData.AddScore(texts[3].text, (int)Mathf.Floor(GameManager.instance.scoreManager.CurrentPlayerTopDistance));
        scoreUi.UpdateScores();
        GameManager.instance.scoreData.SaveScoresToFile();
        GameManager.instance.player.GameOver = false;
        GameManager.instance.scoreManager.Distance = 0;
        GameManager.instance.scoreManager.CurrentPlayerTopDistance = 0;
        GameManager.instance.levelChunkManager.PassiveScrollMultiple = 0;
        GameManager.instance.levelChunkManager.CurrentDifficulty = 0;
        texts[3].text = "";
        selectedLetter = 1;
        inputDelayCounter = 0;
        startedReset = false;
        GameManager.instance.player.GoBackToInitial();
        thingsToSwitch.SwitchStuff();
    }
}
