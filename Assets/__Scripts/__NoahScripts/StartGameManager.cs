using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameManager : MonoBehaviour
{
    [SerializeField] private GameObject startButton;
    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            startButton.SetActive(false);
            GameStart();
        }
    }
    public void GameStart()     
    {
        GameManager.instance.player.PlayerGameStart();
    }
}
