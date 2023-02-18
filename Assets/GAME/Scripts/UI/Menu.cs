using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] GameObject inGameUi;

    int playButtonNum;

    Coroutine CorChangePlayButton;

    [SerializeField] GameObject StartButton;

    [SerializeField] GameObject fakeButtons;

    private void OnEnable()
    {
        EventManager.OnPlayButtonPressed += PlayButton;    
    }

    private void OnDisable()
    {
        EventManager.OnPlayButtonPressed -= PlayButton;
    }


    public void StartGame()
    {
        inGameUi.SetActive(true);
         
        EventManager.OnGameStarted?.Invoke();

        gameObject.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private void Start()
    {
        CorChangePlayButton = StartCoroutine(IEChangePlayButton());
    }

    IEnumerator IEChangePlayButton()
    {
        while (true) 
        {
            playButtonNum = Random.Range(0, 10);

            yield return new WaitForSeconds(2);
        }
    }

    private bool PlayButton(int num)
    {
        if (num == playButtonNum)
        {
            StopCoroutine(CorChangePlayButton);

            StartButton.SetActive(true);

            fakeButtons.SetActive(false);
            
            return true;
        }

        return false;
    }
}
