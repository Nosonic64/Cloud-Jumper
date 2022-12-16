using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameManager : MonoBehaviour
{
    [SerializeField] private GameObject startButton;
    private float resetScreenScrollAmount = 2f;
    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            startButton.SetActive(false);
            GameManager.instance.splashScreen.SwitchSplashOn();
            GameStart();
        }
    }
    public void GameStart()     
    {
        GameManager.instance.levelChunkManager.ResetTimerCounter = resetScreenScrollAmount;
        GameManager.instance.player.Invoke("PlayerGameStart", resetScreenScrollAmount);
        GameManager.instance.splashScreen.Invoke("SwitchSplashOff", resetScreenScrollAmount);
    }
}
