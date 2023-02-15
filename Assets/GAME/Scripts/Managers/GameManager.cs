using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public bool isGameEnd = false;

    private void OnEnable()
    {
        EventManager.OnGameEnd += FinishTheGame;
    }

    private void OnDisable()
    {
        EventManager.OnGameEnd -= FinishTheGame;
    }


    void FinishTheGame()
    {
        isGameEnd = true;
    }
}
