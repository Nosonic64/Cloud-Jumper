using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttractMode : MonoBehaviour
{
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
        attractModeCoroutine = AttractModeCycle();
        StartCoroutine(attractModeCoroutine);
        coroutineRunning = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            StopCoroutine(attractModeCoroutine);
            coroutineRunning = false;
            SetScreenTo(2);
        }

        if (Input.GetButtonDown("Jump") && !coroutineRunning)
        {
            GameStart();
            gameObject.SetActive(false);
        }

    }

    private IEnumerator AttractModeCycle()
    {
        while (true)
        {
            image.sprite = sprites[0];
            yield return new WaitForSeconds(timeBetweenImages);
            image.sprite = sprites[1];
            yield return new WaitForSeconds(timeBetweenImages);
            image.sprite = sprites[2];
            yield return new WaitForSeconds(timeBetweenImages);
            image.sprite = sprites[3];
            yield return new WaitForSeconds(timeBetweenImages);
        }
    }

    private void SetScreenTo(int i)
    {
        image.sprite = sprites[i];
    }

    public void GameStart()
    {
        scoreTable.SetActive(false);
        GameManager.instance.levelChunkManager.ResetTimerCounter = resetScreenScrollAmount;
        GameManager.instance.player.Invoke("PlayerGameStart", resetScreenScrollAmount);
    }
}
